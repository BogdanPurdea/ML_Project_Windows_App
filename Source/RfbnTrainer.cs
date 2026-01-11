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
            // Note: Since data is Z-Score (approx -3 to 3)
            network.Sigmas = ComputeSigmas(network.Centroids, inputs);

            // 3. Supervised: Weights (Gradient Descent with Sigmoid)
            network.Weights = new double[hiddenNeurons];
            for (int i = 0; i < hiddenNeurons; i++) network.Weights[i] = (_rnd.NextDouble() * 2 - 1) * 0.1; // Init -0.1 to 0.1
            network.Bias = 0.0;

            double currentLearningRate = learningRate;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                // This stops the "bouncing" and lets the model settle on higher accuracy
                if (epoch > 0 && epoch % 10 == 0)
                {
                    currentLearningRate *= 0.95;
                }

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
                        network.Weights[h] += currentLearningRate * gradient * hiddenActivations[h];
                    }
                    network.Bias += currentLearningRate * gradient;
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

        private double[] ComputeSigmas(double[][] centroids, List<double[]> inputs)
        {
            int k = centroids.Length;
            int dim = centroids[0].Length;
            double[] sigmas = new double[k];

            // 1. Assign every input to its nearest centroid to form clusters
            List<double>[] clusterDistances = new List<double>[k];
            for (int i = 0; i < k; i++) clusterDistances[i] = new List<double>();

            foreach (var row in inputs)
            {
                int bestCentroid = -1;
                double minDistSq = double.MaxValue;

                for (int c = 0; c < k; c++)
                {
                    double distSq = 0;
                    for (int d = 0; d < dim; d++)
                    {
                        double diff = row[d] - centroids[c][d];
                        distSq += diff * diff;
                    }
                    if (distSq < minDistSq)
                    {
                        minDistSq = distSq;
                        bestCentroid = c;
                    }
                }
                // Save the squared distance for variance calculation
                clusterDistances[bestCentroid].Add(minDistSq);
            }

            // 2. Calculate Sigma as the root mean square distance of the cluster
            for (int i = 0; i < k; i++)
            {
                if (clusterDistances[i].Count > 1)
                {
                    double sumSq = clusterDistances[i].Sum();
                    // Sigma = Sqrt(Avg Squared Distance)
                    double calculatedSigma = Math.Sqrt(sumSq / clusterDistances[i].Count);

                    // SAFETY CLAMP: 
                    // In Z-Score space, a Sigma > 3.0 means the neuron is "flat" over the whole data range.
                    // We clamp it to 3.0 to force the neuron to be useful.
                    if (calculatedSigma > 3.0) calculatedSigma = 3.0;

                    // Prevent overfitting (too sharp)
                    if (calculatedSigma < 0.1) calculatedSigma = 0.1;

                    sigmas[i] = calculatedSigma;
                }
                else
                {
                    // Fallback for dead neurons (0 or 1 data point)
                    sigmas[i] = 1.0;
                }
            }

            return sigmas;
        }

        #endregion

    }
}
