namespace Source
{
    public class RbfTrainer
    {

        #region Fields --------------------------------------------------------------

        private Random _rnd = new Random();

        #endregion


        #region Public Methods ------------------------------------------------------

        public RbfNetwork Train(List<double[]> inputs, List<double> targets, int hiddenNeurons, int epochs, double learningRate)
        {
            int inputDim = inputs[0].Length;
            var network = new RbfNetwork(inputDim, hiddenNeurons, 1);

            // 1. Unsupervised: Centroids (K-Means)
            network.Centroids = ComputeCentroidsKMeans(inputs, hiddenNeurons);

            // 2. Heuristic: Sigmas
            // Note: Since data is Z-Score (approx -3 to 3), avgDist is usually ideal.
            network.Sigmas = ComputeSigmas(network.Centroids);

            // 3. Supervised: Weights (Gradient Descent with Sigmoid)
            network.Weights = new double[hiddenNeurons];
            for (int i = 0; i < hiddenNeurons; i++) network.Weights[i] = (_rnd.NextDouble() * 2 - 1) * 0.1; // Init -0.1 to 0.1
            network.Bias = 0.0;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double totalError = 0;

                for (int i = 0; i < inputs.Count; i++)
                {
                    // --- Forward Pass ---
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

                    // Calculate Output (Sigmoid)
                    double linearSum = network.Bias;
                    for (int h = 0; h < hiddenNeurons; h++) linearSum += hiddenActivations[h] * network.Weights[h];
                    double output = 1.0 / (1.0 + Math.Exp(-linearSum));

                    // --- Backward Pass ---
                    double error = targets[i] - output;
                    totalError += error * error;

                    // Gradient for Sigmoid: Error * Output * (1 - Output)
                    double gradient = error * output * (1.0 - output);

                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        network.Weights[h] += learningRate * gradient * hiddenActivations[h];
                    }
                    network.Bias += learningRate * gradient;
                }
            }

            return network;
        }

        #endregion


        #region Private Methods ------------------------------------------------------

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
            double[] sigmas = new double[k];

            // Fallback if there is only one hidden neuron
            if (k <= 1)
            {
                return new double[] { 1.0 };
            }

            // 1. Calculate the average distance between all pairs of centroids
            double totalDist = 0;
            int pairCount = 0;
            int dim = centroids[0].Length;

            for (int i = 0; i < k; i++)
            {
                for (int j = i + 1; j < k; j++)
                {
                    double distSq = 0;
                    for (int d = 0; d < dim; d++)
                    {
                        double diff = centroids[i][d] - centroids[j][d];
                        distSq += diff * diff;
                    }
                    totalDist += Math.Sqrt(distSq);
                    pairCount++;
                }
            }

            // Avoid division by zero if something went wrong
            if (pairCount == 0) return Enumerable.Repeat(1.0, k).ToArray();

            double avgDist = totalDist / pairCount;

            // 2. Set all sigmas to a multiple of this average.
            // FIX: Reduced multiplier from 2.0 to 1.0 to prevent oversaturation.
            // If you still see "All 1s", try 0.8.
            double sigmaValue = avgDist * 1.0;

            for (int i = 0; i < k; i++)
            {
                sigmas[i] = sigmaValue;
            }

            return sigmas;
        }

        #endregion

    }
}
