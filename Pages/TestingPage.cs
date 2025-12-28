using Source;
using System.Data;
using System.Globalization;

namespace WinForm_RFBN_APP
{
    public partial class TestingPage : UserControl
    {
        public TestingPage()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            // 1. Load Model
            using var db = new Source.Data.AppDbContext();
            var entity = db.TrainedModels
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefault(m => m.ModelName == "FoodClassifier_V1");

            if (entity == null)
            {
                RichTextBoxOutput.AppendText("No model found. Please train first.\r\n");
                return;
            }

            // Reconstruct Network
            var model = new Source.RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            model.Bias = entity.Bias;
            // Note: Ensure CultureInfo.InvariantCulture is used if these were saved with dots
            model.Weights = entity.WeightsData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Sigmas = entity.SigmasData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Centroids = DeserializeCentroids(entity.CentroidsData);

            // 2. Load Test Data 
            // (Crucial: LoadCsv must use CultureInfo.InvariantCulture as discussed previously)
            var testData = LoadCsv("test_20k_normalized_data.csv");
            RichTextBoxOutput.AppendText("Test Data Loaded. Finding Optimal Threshold...\r\n");

            List<double> rawScores = new List<double>();
            List<double> actuals = testData.Targets;

            // 3. Background Evaluation
            Task.Run(() =>
            {
                // A. Generate Scores
                foreach (var input in testData.Inputs)
                {
                    rawScores.Add(model.Forward(input));
                }

                // B. Find Optimal Threshold
                double bestThreshold = 0.5;
                double bestAccuracy = 0.0;
                // FIX: Added 'new' keyword here
                EvaluationMetrics bestMetrics = new EvaluationMetrics();

                // Scan from 0.05 to 0.95 to find the sweet spot
                for (double t = 0.05; t < 0.95; t += 0.05)
                {
                    var m = MetricsCalculator.Calculate(rawScores, actuals, t);

                    // You can optimize for Accuracy, F-Measure, or Precision depending on your goal
                    if (m.Accuracy > bestAccuracy)
                    {
                        bestAccuracy = m.Accuracy;
                        bestThreshold = t;
                        bestMetrics = m;
                    }
                }

                // C. Update UI
                this.Invoke((MethodInvoker)delegate
                {
                    RichTextBoxOutput.AppendText($"\n--- OPTIMIZED RESULTS (Threshold: {bestThreshold:F2}) ---\r\n");
                    RichTextBoxOutput.AppendText(bestMetrics.ToString() + "\r\n");

                    // Comparison with default
                    var defaultMetrics = Source.MetricsCalculator.Calculate(rawScores, actuals, 0.5);
                    RichTextBoxOutput.AppendText($"\n(Default 0.5 Accuracy was: {defaultMetrics.Accuracy:P2})\r\n");
                });
            });
        }

        #region Data Access ---------------------------------------------------------

        /// <summary>
        /// LoadCsv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Skip(1);
            var inputs = new List<double[]>();
            var targets = new List<double>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                double[] rowInput = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    // FIX: Use CultureInfo.InvariantCulture to handle "." decimals correctly
                    rowInput[i] = double.Parse(parts[i], CultureInfo.InvariantCulture);
                }

                inputs.Add(rowInput);
                targets.Add(double.Parse(parts[8], CultureInfo.InvariantCulture));
            }

            return (inputs, targets);
        }

        /// <summary>
        /// DeserializeCentroids
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[][] DeserializeCentroids(string data)
        {
            // Split the main string by '|' to get each centroid (row)
            var rows = data.Split('|');
            var result = new double[rows.Length][];

            for (int i = 0; i < rows.Length; i++)
            {
                // Split each row by ',' and parse the doubles safely
                // FIX: Added CultureInfo.InvariantCulture to handle "." decimals correctly
                result[i] = rows[i]
                    .Split(',')
                    .Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture))
                    .ToArray();
            }

            return result;
        }

        #endregion

    }
}
