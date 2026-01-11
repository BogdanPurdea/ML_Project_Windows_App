namespace Source
{

    ///<summary>
    /// Data Transfer Object (DTO) holding the results of a binary classification evaluation.
    /// Confusion Matrix Terms:
    /// <list type="bullet">
    /// - TP (True Positive):</b> Correctly predicted as Healthy;
    /// - TN (True Negative):</b> Correctly predicted as Unhealthy;
    /// - FP (False Positive):</b> Unhealthy food falsely predicted as Healthy (Type I Error);
    /// - FN (False Negative):</b> Healthy food falsely predicted as Unhealthy (Type II Error);
    ///</summary>
    public class EvaluationMetrics
    {

        ///<summary>
        /// Overall correctness (TP+TN)/Total.
        ///</summary>
        public double Accuracy { get; set; }

        ///<summary>
        /// Out of all Predicted Positives, how many are actually Positive? TP/(TP+FP).
        ///</summary>
        public double Precision { get; set; }

        ///<summary>
        /// Out of all Actual Positives, how many did we find? TP/(TP+FN).
        /// (Also known as Sensitivity).
        ///</summary>
        public double Recall { get; set; } // Same as Sensitivity

        ///<summary>
        /// Synonym for Recall.
        ///</summary>
        public double Sensitivity { get; set; }

        ///<summary>
        /// Harmonic mean of Precision and Recall.
        /// Balances both metrics.
        ///</summary>
        public double FMeasure { get; set; }

        ///<summary>
        /// Area Under the Receiver Operating Characteristic Curve.
        /// Defines ability to distinguish classes.
        ///</summary>
        public double AucRoc { get; set; }

        ///<summary>
        /// Area Under the Precision-Recall Curve.
        /// Better for imbalanced datasets.
        ///</summary>
        public double Auprc { get; set; }

        // Confusion Matrix
        public int TP { get; set; }
        public int TN { get; set; }
        public int FP { get; set; }
        public int FN { get; set; }

        public override string ToString()
        {
            return $"Accuracy: {Accuracy:P2}\n" +
                   $"Precision: {Precision:P2}\n" +
                   $"Recall (Sensitivity): {Recall:P2}\n" +
                   $"F-Measure: {FMeasure:P2}\n" +
                   $"AUC-ROC: {AucRoc:F4}\n" +
                   $"AUPRC: {Auprc:F4}\n" +
                   $"[TP: {TP}, TN: {TN}, FP: {FP}, FN: {FN}]";
        }
    }

    ///<summary>
    /// Static helper class to compute classification metrics.
    ///</summary>
    public static class MetricsCalculator
    {

        ///<summary>
        /// Calculates metrics based on raw probability scores and ground truth labels.
        ///</summary>
        ///<param name="rawScores">List of probability outputs from the model (0.0 to 1.0).</param>
        ///<param name="actualLabels">List of ground truth labels (0.0 or 1.0).</param>
        ///<param name="threshold">Cutoff threshold for binary decision (default 0.5).</param>
        ///<returns>Populated <see cref="EvaluationMetrics"/> object.</returns>
        public static EvaluationMetrics Calculate(List<double> rawScores, List<double> actualLabels, double threshold = 0.5)
        {
            var metrics = new EvaluationMetrics();
            int n = rawScores.Count;

            // 1. Calculate Confusion Matrix using dynamic threshold
            for (int i = 0; i < n; i++)
            {
                // FIX: Use the passed threshold
                int predictedClass = rawScores[i] >= threshold ? 1 : 0;
                int actualClass = (int)actualLabels[i];

                if (predictedClass == 1 && actualClass == 1) metrics.TP++;
                else if (predictedClass == 0 && actualClass == 0) metrics.TN++;
                else if (predictedClass == 1 && actualClass == 0) metrics.FP++;
                else if (predictedClass == 0 && actualClass == 1) metrics.FN++;
            }

            // 2. Basic Metrics
            metrics.Accuracy = (double)(metrics.TP + metrics.TN) / n;

            // Handle division by zero edge cases
            metrics.Precision = (metrics.TP + metrics.FP) > 0 ? (double)metrics.TP / (metrics.TP + metrics.FP) : 0;
            metrics.Recall = (metrics.TP + metrics.FN) > 0 ? (double)metrics.TP / (metrics.TP + metrics.FN) : 0;
            metrics.Sensitivity = metrics.Recall;   // Synonym

            if (metrics.Precision + metrics.Recall > 0)
            {
                metrics.FMeasure = 2 * (metrics.Precision * metrics.Recall) / (metrics.Precision + metrics.Recall);
            }

            // 3. AUC - ROC Calculation (Threshold independent)
            var predictions = rawScores.Zip(actualLabels, (s, l) => new { Score = s, Label = l })
                                       .OrderByDescending(x => x.Score)
                                       .ToList();

            metrics.AucRoc = CalculateAucRoc(predictions);
            metrics.Auprc = CalculateAuprc(predictions);

            return metrics;
        }

        ///<summary>
        /// Calculates AUC-ROC using the "Area Under Curve" method.
        /// Conceptual Explanation:
        /// This calculates the probability that the model ranks a random positive example higher than a random negative example.
        /// We iterate through the sorted predictions (high confidence to low).
        /// - Every time we see a Negative (0), we add the number of Positives (1s) seen so far to the area.
        /// - This is equivalent to the Mann-Whitney U Test statistic integration.
        ///</summary>
        private static double CalculateAucRoc(dynamic sortedPredictions)
        {
            // Mann-Whitney U or Trapezoidal integration
            double auc = 0.0;
            double height = 0.0;
            int posCount = 0;
            int negCount = 0;

            foreach (var p in sortedPredictions)
            {
                if (p.Label == 1)
                {
                    height += 1.0;
                    posCount++;
                }
                else
                {
                    auc += height;
                    negCount++;
                }
            }

            return (posCount == 0 || negCount == 0) ? 0 : auc / (posCount * negCount);
        }

        private static double CalculateAuprc(dynamic sortedPredictions)
        {
            // Area Under Precision-Recall Curve using Average Precision logic
            double sumPrecision = 0.0;
            int runningTp = 0;
            int totalPositives = 0;

            // Count total positives first
            foreach (var p in sortedPredictions)
            {
                if (p.Label == 1) totalPositives++;
            }

            if (totalPositives == 0) return 0;

            for (int i = 0; i < sortedPredictions.Count; i++)
            {
                if (sortedPredictions[i].Label == 1)
                {
                    runningTp++;
                    // Precision at this cutoff k
                    double precisionAtK = (double)runningTp / (i + 1);
                    sumPrecision += precisionAtK;
                }
            }

            return sumPrecision / totalPositives;
        }
    }
}