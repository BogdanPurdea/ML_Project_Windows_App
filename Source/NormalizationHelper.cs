using System.Globalization;

namespace Source
{
    public static class NormalizationHelper
    {

        #region Logics --------------------------------------------------------------

        /// <summary>
        /// Computes Mean and Standard Deviation for Z-Score Normalization.
        /// Use this on your RAW training data before training starts.
        /// </summary>
        /// <returns>Tuple containing Mean array and StdDev array</returns>
        public static (double[] Mean, double[] StdDev) ComputeZScoreStats(List<double[]> data)
        {
            if (data == null || data.Count == 0)
                return (new double[0], new double[0]);

            int dim = data[0].Length;
            double[] mean = new double[dim];
            double[] stdDev = new double[dim];

            // 1. Calculate Mean
            foreach (var row in data)
            {
                for (int i = 0; i < dim; i++)
                {
                    mean[i] += row[i];
                }
            }

            for (int i = 0; i < dim; i++)
            {
                mean[i] /= data.Count;
            }

            // 2. Calculate Standard Deviation
            foreach (var row in data)
            {
                for (int i = 0; i < dim; i++)
                {
                    double diff = row[i] - mean[i];
                    stdDev[i] += diff * diff;
                }
            }

            for (int i = 0; i < dim; i++)
            {
                // Standard Deviation = Sqrt(Sum(diff^2) / N)
                // We use Population Standard Deviation here.
                stdDev[i] = Math.Sqrt(stdDev[i] / data.Count);

                // SAFETY CHECK: Prevent division by zero if a feature is constant (e.g., all values are 0).
                // If stdDev is 0, we set it to 1 so division doesn't crash, and value remains 0 after normalization.
                if (stdDev[i] == 0) stdDev[i] = 1.0;
            }

            return (mean, stdDev);
        }

        /// <summary>
        /// Applies (x - mean) / stdDev to a single row.
        /// This is used by both the Training Page (batch) and FoodClassifier (single prediction).
        /// </summary>
        public static double[] NormalizeRow(double[] input, double[] mean, double[] stdDev)
        {
            if (input.Length != mean.Length)
                throw new ArgumentException($"Input dimension {input.Length} does not match stats dimension {mean.Length}");

            double[] normalized = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                normalized[i] = (input[i] - mean[i]) / stdDev[i];
            }
            return normalized;

            // Debug
            // First record normalized excel file:
            // -1.0069;4.9858;-1.2527;3.1672;-0.7867;1.0905;-0.7852;0  <-- CORRECT
        }

        #endregion


        #region Serialization Helpers for Database Storage --------------------------

        /// <summary>
        /// Converts a double array to a semicolon-separated string.
        /// Uses InvariantCulture to ensure dots (.) are used for decimals, not commas.
        /// Example: [1.5, 2.5] -> "1.5;2.5"
        /// </summary>
        public static string SerializeArray(double[] array)
        {
            if (array == null || array.Length == 0) return string.Empty;

            return string.Join(";", array.Select(x => x.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Converts a semicolon-separated string back to a double array.
        /// </summary>
        public static double[] DeserializeArray(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return new double[0];

            try
            {
                return data.Split(';')
                           .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                           .ToArray();
            }
            catch
            {
                // Fallback for corrupt data
                return new double[0];
            }
        }

        #endregion

    }
}