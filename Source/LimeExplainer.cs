using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;

namespace Source
{
    public class LimeExplainer
    {
        private double[] _means;
        private double[] _stdDevs;
        private int _featureCount;
        private Random _rand;

        public LimeExplainer(List<double[]> trainingData)
        {
            _rand = new Random(42);
            if (trainingData == null || trainingData.Count == 0)
                throw new ArgumentException("Training data cannot be null or empty.");

            _featureCount = trainingData[0].Length;
            CalculateStats(trainingData);
        }

        private void CalculateStats(List<double[]> data)
        {
            _means = new double[_featureCount];
            _stdDevs = new double[_featureCount];
            int n = data.Count;

            for (int j = 0; j < _featureCount; j++)
            {
                var col = data.Select(r => r[j]).ToList();
                _means[j] = col.Average();
                double sumSq = col.Sum(v => Math.Pow(v - _means[j], 2));
                _stdDevs[j] = Math.Sqrt(sumSq / n);
                if (_stdDevs[j] < 1e-9)
                    _stdDevs[j] = 1.0; // Avoid zero std dev
            }
        }

        /// <summary>
        /// Generates a local explanation for a specific instance.
        /// </summary>
        public List<(int FeatureIndex, double Weight)> Explain(
            double[] instance,
            Func<double[][], double[]> predictFn,
            int numSamples = 2000,
            double kernelWidth = 0.75
        )
        {
            // 1. Generate Perturbed Samples (Normal distribution around instance, scaled by stdDev)
            double[][] perturbations = new double[numSamples][];

            for (int i = 0; i < numSamples; i++)
            {
                double[] row = new double[_featureCount];
                for (int j = 0; j < _featureCount; j++)
                {
                    // Gaussian noise (Box-Muller)
                    double u1 = 1.0 - _rand.NextDouble();
                    double u2 = 1.0 - _rand.NextDouble();
                    double randStdNormal =
                        Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

                    row[j] = instance[j] + randStdNormal * _stdDevs[j];
                }
                perturbations[i] = row;
            }

            // First sample is the instance itself (anchor)
            perturbations[0] = instance;

            // 2. Get Model Predictions
            var predictions = predictFn(perturbations);

            // 3. Calculate Weights (Kernel based on distance in normalized space)
            double[] weights = new double[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                double distanceSq = 0;
                for (int j = 0; j < _featureCount; j++)
                {
                    double d = (perturbations[i][j] - instance[j]) / _stdDevs[j];
                    distanceSq += d * d;
                }
                double distance = Math.Sqrt(distanceSq);
                double width = kernelWidth * Math.Sqrt(_featureCount);
                weights[i] = Math.Exp(-(distance * distance) / (width * width));
            }

            // 4. Weighted Ridge Regression
            // Solve: theta = (X' W X + lambda I)^-1 X' W y
            // Implementation: Transform X and y by sqrt(W), then simple Ridge.

            double[][] validX = new double[numSamples][];
            double[] validY = new double[numSamples];

            for (int i = 0; i < numSamples; i++)
            {
                double wSqrt = Math.Sqrt(weights[i]);
                validY[i] = predictions[i] * wSqrt;

                // Add intercept term at the beginning
                double[] row = new double[_featureCount + 1];
                row[0] = 1.0 * wSqrt; // Intercept
                for (int j = 0; j < _featureCount; j++)
                {
                    row[j + 1] = perturbations[i][j] * wSqrt;
                }
                validX[i] = row;
            }

            double[,] matrixX = ToMatrix2D(validX);
            double[] vectorY = validY;

            double lambda = 1.0; // Regularization strength

            // Xt * X
            double[,] Xt = matrixX.Transpose();
            double[,] XtX = Xt.Dot(matrixX);

            // Add lambda to diagonal
            for (int k = 0; k < _featureCount + 1; k++)
                XtX[k, k] += lambda;

            // Xt * Y
            double[] XtY = Xt.Dot(vectorY);

            // Solve (XtX)^-1 * XtY
            try
            {
                double[,] XtX_Inv = XtX.Inverse();
                double[] theta = XtX_Inv.Dot(XtY);

                // Coefficients (exclude intercept at index 0)
                var results = new List<(int, double)>();
                for (int j = 0; j < _featureCount; j++)
                {
                    results.Add((j, theta[j + 1]));
                }

                return results.OrderByDescending(x => Math.Abs(x.Item2)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LIME Error] Singular Matrix? {ex.Message}");
                return new List<(int, double)>();
            }
        }

        private double[,] ToMatrix2D(double[][] source)
        {
            int rows = source.Length;
            int cols = source[0].Length;
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = source[i][j];
            return result;
        }
    }
}
