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

        /// <summary>
        /// Expose the schema so the UI knows how to parse CSVs for this model
        /// </summary>
        public string InputSchema { get; private set; }

        public bool IsLoaded => _network != null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="network"></param>
        /// <param name="means"></param>
        /// <param name="stdDevs"></param>
        /// <param name="schema"></param>
        public FoodClassifier(RbfNetwork network, double[] means, double[] stdDevs, string schema)
        {
            _network = network;
            _means = means;
            _stdDevs = stdDevs;
            InputSchema = schema;
        }
        
        /// <summary>
        /// Takes RAW (human-readable) input, normalizes it internally, and returns the probability.
        /// </summary>
        public double Predict(double[] rawInput)
        {
            if (_network == null) throw new InvalidOperationException("Model not loaded.");
            double[] normalizedInput = Normalize(rawInput);
            return _network.Forward(normalizedInput);
        }

        private double[] Normalize(double[] raw)
        {
            if (raw.Length != _means.Length)
                throw new ArgumentException($"Input dimension mismatch. Model expects {_means.Length} inputs (based on schema '{InputSchema}'), but got {raw.Length}.");

            double[] normalized = new double[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                normalized[i] = (raw[i] - _means[i]) / _stdDevs[i];
            }
            return normalized;
        }
    }
}