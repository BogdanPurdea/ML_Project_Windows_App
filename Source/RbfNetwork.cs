using System;
using System.Linq;

namespace Source
{
    public class RbfNetwork
    {
        public int InputCount { get; private set; }
        public int HiddenCount { get; private set; }
        public int OutputCount { get; private set; }

        // Hidden Layer
        public double[][] Centroids { get; set; }
        public double[] Sigmas { get; set; }

        // Output Layer
        public double[] Weights { get; set; }
        public double Bias { get; set; }

        public RbfNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            InputCount = inputCount;
            HiddenCount = hiddenCount;
            OutputCount = outputCount;
        }

        /// <summary>
        /// Computes the forward pass. 
        /// NOTE: Input MUST be normalized before calling this if the network was trained on normalized data.
        /// </summary>
        public double Forward(double[] inputs)
        {
            if (inputs.Length != InputCount)
                throw new ArgumentException($"Network expected {InputCount} inputs, but got {inputs.Length}.");

            // 1. RBF Layer (Gaussian Kernel)
            double[] hiddenActivations = new double[HiddenCount];
            for (int j = 0; j < HiddenCount; j++)
            {
                double distSq = 0.0;
                for (int i = 0; i < InputCount; i++)
                {
                    double diff = inputs[i] - Centroids[j][i];
                    distSq += diff * diff;
                }

                // Gaussian: exp( -dist^2 / (2 * sigma^2) )
                hiddenActivations[j] = Math.Exp(-distSq / (2 * Math.Pow(Sigmas[j], 2)));
            }

            // 2. Linear Combination
            double outputSum = Bias;
            for (int j = 0; j < HiddenCount; j++)
            {
                outputSum += hiddenActivations[j] * Weights[j];
            }

            // 3. Sigmoid Activation (0.0 to 1.0)
            return 1.0 / (1.0 + Math.Exp(-outputSum));
        }
    }
}