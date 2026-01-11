using System.Globalization;

namespace Source
{

    ///<summary>
    /// Provides utility methods for Z-Score Normalization (Standardization).
    /// Why is this critical for RBF Networks?
    /// RBF neurons use Euclidean Distance to measure similarity. If features have different scales 
    /// (e.g., Calories in 100s vs. Fat in 0.1s), the large features will dominate the distance calculation
    /// completely masking the smaller features.
    /// Neuron Saturation Risk:
    /// Without normalization, distances can be huge. The Gaussian function is exp(-distance). 
    /// If distance is huge, exp(-huge) = 0. The neuron dies (saturates) and learns nothing.
    /// Z-Score keeps all values roughly between -3 and +3, ensuring gradients flow.
    ///</summary>
    public static class NormalizationHelper
    {

        #region Logics --------------------------------------------------------------

        ///<summary>
        /// Computes Mean and Standard Deviation for Z-Score Normalization.
        /// Use this on your RAW training data before training starts.
        ///</summary>
        ///<param name="data">The raw training dataset.</param>
        ///<returns>A tuple containing (Mean array, StdDev array).</returns>
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
                // If stdDev is 0, we set it to 1.0 (no scaling) so division doesn't crash.
                if (stdDev[i] == 0) stdDev[i] = 1.0;
            }

            return (mean, stdDev);
        }

        ///<summary>
        /// Applies Z-Score Normalization to a single row.
        /// Formula: z = (x - mean) / stdDev
        ///</summary>
        ///<param name="input">The raw input vector.</param>
        ///<param name="mean">The pre-computed means.</param>
        ///<param name="stdDev">The pre-computed standard deviations.</param>
        ///<returns>A new array containing the normalized values.</returns>
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

            // Debug Example:
            // Raw: [2000 kcal, ...] -> Normalized: [1.5, ...]
            // This brings it into the same range as other features.
        }

        #endregion


        #region Serialization Helpers for Database Storage --------------------------

        ///<summary>
        /// Converts a double array to a semicolon-separated string for database storage.
        ///</summary>
        public static string SerializeArray(double[] array)
        {
            if (array == null || array.Length == 0) return string.Empty;

            // Use InvariantCulture to ensure we use '.' for decimals, even on European machines.
            return string.Join(";", array.Select(x => x.ToString(CultureInfo.InvariantCulture)));
        }

        ///<summary>
        /// Converts a semicolon-separated string back to a double array.
        ///</summary>
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