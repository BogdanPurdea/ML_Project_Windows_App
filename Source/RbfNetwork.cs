using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class RbfNetwork
    {
        public int InputCount { get; private set; }
        public int HiddenCount { get; private set; }
        public int OutputCount { get; private set; }

        // Hidden Layer Parameters
        public double[][] Centroids { get; set; } // [HiddenNode][InputIndex]
        public double[] Sigmas { get; set; }      // [HiddenNode]

        // Output Layer Parameters
        public double[] Weights { get; set; }     // [HiddenNode] (assuming 1 output for binary classification)
        public double Bias { get; set; }

        public RbfNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            InputCount = inputCount;
            HiddenCount = hiddenCount;
            OutputCount = outputCount;
        }

        public double Forward(double[] inputs)
        {
            if (inputs.Length != InputCount)
                throw new ArgumentException($"Expected {InputCount} inputs, got {inputs.Length}");

            double[] hiddenOutputs = new double[HiddenCount];

            // 1. RBF Layer
            for (int j = 0; j < HiddenCount; j++)
            {
                double distSq = 0.0;
                for (int i = 0; i < InputCount; i++)
                {
                    double diff = inputs[i] - Centroids[j][i];
                    distSq += diff * diff;
                }
                hiddenOutputs[j] = Math.Exp(-distSq / (2 * Math.Pow(Sigmas[j], 2)));
            }

            // 2. Linear Layer
            double outputSum = Bias;
            for (int j = 0; j < HiddenCount; j++)
            {
                outputSum += hiddenOutputs[j] * Weights[j];
            }

            // 3. FIX: Sigmoid Activation (Forces output 0.0 to 1.0)
            return 1.0 / (1.0 + Math.Exp(-outputSum));
        }

        public int Classify(double[] inputs)
        {
            double output = Forward(inputs);
            // Threshold at 0.5 for binary classification
            return output >= 0.5 ? 1 : 0;
        }
    }
}
