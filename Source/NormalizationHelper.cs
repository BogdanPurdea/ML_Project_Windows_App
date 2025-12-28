using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public static class NormalizationHelper
    {
        // Compute Min and Max for each column from the raw training data
        public static (double[] Min, double[] Max) ComputeStats(List<double[]> data)
        {
            if (data.Count == 0) return (new double[0], new double[0]);

            int dim = data[0].Length;
            double[] min = new double[dim];
            double[] max = new double[dim];

            // Init
            for (int i = 0; i < dim; i++)
            {
                min[i] = double.MaxValue;
                max[i] = double.MinValue;
            }

            // Scan
            foreach (var row in data)
            {
                for (int i = 0; i < dim; i++)
                {
                    if (row[i] < min[i]) min[i] = row[i];
                    if (row[i] > max[i]) max[i] = row[i];
                }
            }

            return (min, max);
        }

        // Apply (x - min) / (max - min) to a single row
        public static double[] NormalizeRow(double[] input, double[] min, double[] max)
        {
            double[] normalized = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                double range = max[i] - min[i];
                if (range == 0)
                {
                    normalized[i] = 0; // Avoid NaN if max == min
                }
                else
                {
                    normalized[i] = (input[i] - min[i]) / range;
                }
            }
            return normalized;
        }

        // Batch version
        public static List<double[]> NormalizeList(List<double[]> data, double[] min, double[] max)
        {
            var result = new List<double[]>(data.Count);
            foreach (var row in data)
            {
                result.Add(NormalizeRow(row, min, max));
            }
            return result;
        }
    }
}