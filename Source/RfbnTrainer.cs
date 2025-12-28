using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class RbfTrainer
    {
        private Random _rnd = new Random();

        public RbfNetwork Train(List<double[]> inputs, List<double> targets, int hiddenNeurons, int epochs, double learningRate)
        {
            int inputDim = inputs[0].Length;
            var network = new RbfNetwork(inputDim, hiddenNeurons, 1);

            // PHASE 1: Unsupervised Learning (K-Means) to find Centroids
            network.Centroids = ComputeCentroidsKMeans(inputs, hiddenNeurons);

            // PHASE 2: Calculate Sigmas (Widths)
            // Heuristic: Average distance to nearest neighbor centroid or global variance
            network.Sigmas = ComputeSigmas(network.Centroids);

            // PHASE 3: Supervised Learning (Gradient Descent) for Weights
            // Initialize weights randomly
            network.Weights = new double[hiddenNeurons];
            for (int i = 0; i < hiddenNeurons; i++) network.Weights[i] = _rnd.NextDouble() * 0.1;
            network.Bias = 0.0;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double totalError = 0;

                for (int i = 0; i < inputs.Count; i++)
                {
                    // Forward pass to get hidden layer activations
                    double[] hiddenActivations = new double[hiddenNeurons];
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        double distSq = 0;
                        for (int d = 0; d < inputDim; d++)
                        {
                            double diff = inputs[i][d] - network.Centroids[h][d];
                            distSq += diff * diff;
                        }
                        hiddenActivations[h] = Math.Exp(-distSq / (2 * Math.Pow(network.Sigmas[h], 2)));
                    }

                    // Compute current output
                    double currentOutput = network.Bias;
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        currentOutput += hiddenActivations[h] * network.Weights[h];
                    }

                    // Compute Error (Target - Output)
                    double error = targets[i] - currentOutput;
                    totalError += error * error;

                    // Update Weights (LMS / Delta Rule)
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        network.Weights[h] += learningRate * error * hiddenActivations[h];
                    }
                    network.Bias += learningRate * error;
                }
            }

            return network;
        }

        private double[][] ComputeCentroidsKMeans(List<double[]> data, int k)
        {
            int dim = data[0].Length;
            // Initialize centroids randomly from data points
            double[][] centroids = new double[k][];
            for (int i = 0; i < k; i++)
                centroids[i] = (double[])data[_rnd.Next(data.Count)].Clone();

            int[] assignments = new int[data.Count];
            bool changed = true;
            int maxIter = 100;
            int iter = 0;

            while (changed && iter < maxIter)
            {
                changed = false;
                iter++;

                // Assign data to nearest centroid
                for (int i = 0; i < data.Count; i++)
                {
                    int bestCluster = -1;
                    double bestDist = double.MaxValue;

                    for (int c = 0; c < k; c++)
                    {
                        double dist = 0;
                        for (int d = 0; d < dim; d++)
                        {
                            double diff = data[i][d] - centroids[c][d];
                            dist += diff * diff;
                        }
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            bestCluster = c;
                        }
                    }

                    if (assignments[i] != bestCluster)
                    {
                        assignments[i] = bestCluster;
                        changed = true;
                    }
                }

                // Update centroids
                int[] counts = new int[k];
                double[][] sums = new double[k][];
                for (int c = 0; c < k; c++) sums[c] = new double[dim];

                for (int i = 0; i < data.Count; i++)
                {
                    int cluster = assignments[i];
                    counts[cluster]++;
                    for (int d = 0; d < dim; d++)
                        sums[cluster][d] += data[i][d];
                }

                for (int c = 0; c < k; c++)
                {
                    if (counts[c] > 0)
                        for (int d = 0; d < dim; d++)
                            centroids[c][d] = sums[c][d] / counts[c];
                }
            }
            return centroids;
        }

        private double[] ComputeSigmas(double[][] centroids)
        {
            int k = centroids.Length;
            int dim = centroids[0].Length;
            double[] sigmas = new double[k];

            // Simple heuristic: distance to nearest other centroid
            // A more robust one is: max_distance / sqrt(2*k)

            if (k == 1) return new double[] { 1.0 }; // Fallback

            for (int i = 0; i < k; i++)
            {
                double minDst = double.MaxValue;
                for (int j = 0; j < k; j++)
                {
                    if (i == j) continue;
                    double dist = 0;
                    for (int d = 0; d < dim; d++)
                    {
                        double diff = centroids[i][d] - centroids[j][d];
                        dist += diff * diff;
                    }
                    dist = Math.Sqrt(dist);
                    if (dist < minDst) minDst = dist;
                }
                // Sigma is often set to d_nearest * constant (e.g., 2.0 to ensure overlap)
                sigmas[i] = minDst * 1.5;
            }
            return sigmas;
        }
    }
}
