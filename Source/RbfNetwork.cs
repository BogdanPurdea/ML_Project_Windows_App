namespace Source
{
    ///<summary>
    /// Represents a Radial Basis Function Neural Network (RBFN) devised for binary classification.
    /// Architecture:
    /// - Input Layer:</b> Receives feature vectors (e.g., Z-Score normalized food nutrients);
    /// - Hidden Layer:</b> Composed of RBF neurons (prototypes) that measure similarity to input via Gaussian functions;
    /// - Output Layer:</b> Single neuron using a Sigmoid activation to produce a probability (0.0 to 1.0).
    /// Role in Pipeline:
    ///   Acts as the inference engine. Once trained (centroids, sigmas, weights set), it classifies new data points.
    ///</summary>
    public class RbfNetwork
    {
        ///<summary>
        /// Dimensionality of the input vector (e.g., 6 features: Calories, Fat, Protein, etc.).
        ///</summary>
        public int InputCount { get; private set; }

        ///<summary>
        /// Number of RBF neurons in the hidden layer. Each neuron represents a local region in the feature space.
        ///</summary>
        public int HiddenCount { get; private set; }

        ///<summary>
        /// Dimension of the output layer. For binary classification, this is typically 1.
        ///</summary>
        public int OutputCount { get; private set; }

        // --- Hidden Layer Parameters ---

        ///<summary>
        /// The center vectors (prototypes) for each RBF neuron.
        /// Dimensions: [HiddenCount][InputCount].
        /// Unsupervised Learning: These are determined via K-Means clustering.
        ///</summary>
        public double[][] Centroids { get; set; }

        ///<summary>
        /// The spread or width (Standard Deviation) for each RBF neuron's Gaussian curve.
        /// Influence: 
        /// - Large Sigma: Neuron responds to a wide area (global features).
        /// - Small Sigma: Neuron responds only to very close matches (local features).
        ///</summary>
        public double[] Sigmas { get; set; }

        // --- Output Layer Parameters ---

        ///<summary>
        /// Synaptic weights connecting the Hidden Layer (RBF activations) to the Output Layer.
        /// Supervised Learning: Learned via Gradient Descent.
        ///</summary>
        public double[] Weights { get; set; }

        ///<summary>
        /// Bias term for the output neuron, allowing the decision boundary to shift.
        ///</summary>
        public double Bias { get; set; }

        ///<summary>
        /// Initializes a new instance of the <see cref="RbfNetwork"/> class.
        ///</summary>
        ///<param name="inputCount">Number of input features.</param>
        ///<param name="hiddenCount">Number of hidden RBF neurons.</param>
        ///<param name="outputCount">Number of output neurons (usually 1).</param>
        public RbfNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            InputCount = inputCount;
            HiddenCount = hiddenCount;
            OutputCount = outputCount;
        }

        ///<summary>
        /// Performs a forward pass (inference) through the network.
        ///</summary>
        ///<param name="inputs">The input feature vector. MUST be normalized (Z-Score) to match training data.</param>
        ///<returns>A value between 0.0 (Unhealthy) and 1.0 (Healthy).</returns>
        ///<exception cref="ArgumentException">Thrown if input dimension does not match network configuration.</exception>
        public double Forward(double[] inputs)
        {
            if (inputs.Length != InputCount)
                throw new ArgumentException($"Network expected {InputCount} inputs, but got {inputs.Length}.");

            // 1. RBF Layer (Gaussian Kernel)
            // Each hidden neuron calculates how "close" the input is to its centroid.
            double[] hiddenActivations = new double[HiddenCount];
            for (int j = 0; j < HiddenCount; j++)
            {
                double distSq = 0.0;
                for (int i = 0; i < InputCount; i++)
                {
                    double diff = inputs[i] - Centroids[j][i];
                    distSq += diff * diff; // Euclidean Distance Squared
                }

                // Gaussian Activation Function:
                // Formula: φ(x) = e^( - ||x - c||² / (2 * σ²) )
                // output = exp( -distanceSquared / (2 * sigmaSquared) )
                //
                // Why?
                // - If distance is 0 (perfect match), exp(0) = 1.0 (Max Activation).
                // - As distance increases, the term inside exp becomes negative large, so exp -> 0.
                // - Sigma (σ) controls how fast it drops off.
                hiddenActivations[j] = Math.Exp(-distSq / (2 * Math.Pow(Sigmas[j], 2)));
            }

            // 2. Linear Combination
            // Weighted sum of RBF activations + Bias
            double outputSum = Bias;
            for (int j = 0; j < HiddenCount; j++)
            {
                outputSum += hiddenActivations[j] * Weights[j];
            }

            // 3. Sigmoid Activation
            // Saps the linear sum into probability range [0, 1].
            // Formula: σ(z) = 1 / (1 + e^(-z))
            return 1.0 / (1.0 + Math.Exp(-outputSum));
        }
    }
}