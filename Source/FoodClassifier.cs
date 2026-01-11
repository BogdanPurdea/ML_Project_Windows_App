namespace Source
{
    ///<summary>
    /// Production Wrapper for the RBF Neural Network.
    /// Why this exists:
    /// A raw neural network expects normalized inputs (e.g., -1.2 to 2.5). 
    /// Real-world data comes in raw units (e.g., 200 Calories, 15g Fat).
    /// This class encapsulates both the trained network AND the normalization statistics (Mean/StdDev)
    /// to ensure that new data is pre-processed exactly the same way the training data was.
    ///</summary>
    public class FoodClassifier
    {

        #region Fields --------------------------------------------------------------

        private readonly RbfNetwork _network;
        private readonly double[] _means;
        private readonly double[] _stdDevs;

        ///<summary>
        /// The CSV header schema used during training, ensuring we map inputs correctly.
        ///</summary>
        public string InputSchema { get; private set; }

        ///<summary>
        /// True if the model and statistics are successfully loaded.
        ///</summary>
        public bool IsLoaded => _network != null;

        #endregion


        #region Public Methods ------------------------------------------------------

        ///<summary>
        /// Initializes a new instance of the <see cref="FoodClassifier"/>.
        ///</summary>
        ///<param name="network">The trained RBF network.</param>
        ///<param name="means">Normalization means (from training set).</param>
        ///<param name="stdDevs">Normalization standard deviations (from training set).</param>
        ///<param name="schema">Input schema string.</param>
        public FoodClassifier(RbfNetwork network, double[] means, double[] stdDevs, string schema)
        {
            _network = network;
            _means = means;
            _stdDevs = stdDevs;
            InputSchema = schema;
        }
        
        ///<summary>
        /// Predicts the class of a raw input vector.
        ///</summary>
        ///<param name="rawInput">Raw feature values (e.g., [250, 12.5, ...]).</param>
        ///<returns>Probability of being 'Healthy' (0.0 to 1.0).</returns>
        ///<exception cref="InvalidOperationException">Thrown if model is not loaded.</exception>
        public double Predict(double[] rawInput)
        {
            if (_network == null) throw new InvalidOperationException("Model not loaded.");
            
            // 1. Z-Score Normalization
            double[] normalizedInput = Normalize(rawInput);
            
            // 2. Inference
            return _network.Forward(normalizedInput);
        }

        #endregion


        #region Private Methods ------------------------------------------------------

        ///<summary>
        /// Internal helper to apply the stored Z-Score statistics.
        ///</summary>
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
        
        #endregion

    }
}