namespace Source
{
    /// <summary>
    /// Wrapper class that holds both the Neural Network and the Normalization Statistics.
    /// This ensures raw data is always processed exactly how the model expects it.
    /// </summary>
    public class FoodClassifier
    {
        private readonly RbfNetwork _network;
        private readonly double[] _means;
        private readonly double[] _stdDevs;

        public bool IsLoaded => _network != null;

        public FoodClassifier(RbfNetwork network, double[] means, double[] stdDevs)
        {
            _network = network;
            _means = means;
            _stdDevs = stdDevs;
        }

        /// <summary>
        /// Takes RAW (human-readable) input, normalizes it internally, and returns the probability.
        /// </summary>
        public double Predict(double[] rawInput)
        {
            if (_network == null) throw new InvalidOperationException("Model not loaded.");

            // 1. Normalize the data using stored training stats
            double[] normalizedInput = Normalize(rawInput);

            // 2. Pass to network
            return _network.Forward(normalizedInput);
        }

        private double[] Normalize(double[] raw)
        {
            if (raw.Length != _means.Length)
                throw new ArgumentException($"Input dimension mismatch. Expected {_means.Length}, got {raw.Length}");

            double[] normalized = new double[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                // Z-Score Formula: (x - mean) / stdDev
                normalized[i] = (raw[i] - _means[i]) / _stdDevs[i];
            }
            return normalized;
        }
    }
}