using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;

namespace WinForm_RFBN_APP.Benchmarking
{
    public class AccordBenchmark
    {
        private ActivationNetwork _network;

        public void TrainAndEvaluate(List<double[]> trainInputs, List<double> trainTargets,
                                     List<double[]> testInputs, List<double> testTargets,
                                     int hiddenNeurons)
        {
            // 1. Convert Data to Arrays (Accord expects arrays of arrays)
            double[][] inputs = trainInputs.ToArray();
            double[][] outputs = trainTargets.Select(t => new double[] { t }).ToArray();

            double[][] tInputs = testInputs.ToArray();
            double[][] tOutputs = testTargets.Select(t => new double[] { t }).ToArray();

            // 2. Setup Network Topology
            // Input Count -> Hidden Neurons (Gaussian) -> Output (Sigmoid)
            // Note: We use Sigmoid for output to match your binary classification (0 to 1)
            _network = new ActivationNetwork(new SigmoidFunction(),
                                             inputs[0].Length,
                                             hiddenNeurons,
                                             1);

            // MANUALLY set the Hidden Layer to use Gaussian (RBF) activation
            var hiddenLayer = _network.Layers[0];

            for (int i = 0; i < hiddenLayer.Neurons.Length; i++)
            {
                // FIX: Cast the generic 'Neuron' to 'ActivationNeuron' to access the property
                if (hiddenLayer.Neurons[i] is ActivationNeuron neuron)
                {
                    // GaussianFunction(alpha). Alpha is roughly 1 / (2 * sigma^2)
                    neuron.ActivationFunction = new GaussianFunction(1.0);
                }
            }

            // Randomize weights/centers slightly to break symmetry
            new NguyenWidrow(_network).Randomize();

            Console.WriteLine("Training Accord.NET RBFN...");

            // 3. Train using Resilient Backpropagation (RPROP)
            // This is generally faster and more robust than standard Gradient Descent
            var teacher = new ResilientBackpropagationLearning(_network);

            // Train for fixed epochs or until error is low
            int epochs = 100;
            for (int i = 0; i < epochs; i++)
            {
                double error = teacher.RunEpoch(inputs, outputs);
                if (i % 10 == 0) Console.WriteLine($"Epoch {i}: Error = {error:F4}");
            }

            // 4. Evaluation
            Console.WriteLine("\n--- Accord.NET Results ---");
            int tp = 0, tn = 0, fp = 0, fn = 0;

            for (int i = 0; i < tInputs.Length; i++)
            {
                double prediction = _network.Compute(tInputs[i])[0];
                int predictedClass = prediction >= 0.5 ? 1 : 0;
                int actualClass = (int)tOutputs[i][0];

                if (predictedClass == 1 && actualClass == 1) tp++;
                else if (predictedClass == 0 && actualClass == 0) tn++;
                else if (predictedClass == 1 && actualClass == 0) fp++;
                else if (predictedClass == 0 && actualClass == 1) fn++;
            }

            double accuracy = (double)(tp + tn) / (tp + tn + fp + fn);
            Console.WriteLine($"Accuracy: {accuracy:P2}");
            Console.WriteLine($"[TP: {tp}, TN: {tn}, FP: {fp}, FN: {fn}]");
        }
    }
}