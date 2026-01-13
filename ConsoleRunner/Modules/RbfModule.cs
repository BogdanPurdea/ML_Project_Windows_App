using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Source;
using Source.Data;

namespace ConsoleRunner.Modules
{
    public static class RbfModule
    {
        public static FoodClassifier Run(
            List<double[]> trainInputs,
            List<double> trainTargets,
            List<double[]> valInputs,
            List<double> valTargets,
            int hiddenNeurons,
            int epochs,
            double learningRate,
            string schema
        )
        {
            Console.WriteLine($" > Train Set: {trainInputs.Count} samples");
            Console.WriteLine($" > Val Set:   {valInputs.Count} samples");

            // 1. Normalization
            Console.Write("[2/4] Computing Normalization Stats... ");
            int dim = trainInputs[0].Length;
            double[] means = new double[dim];
            double[] stdDevs = new double[dim];

            for (int d = 0; d < dim; d++)
            {
                var colValues = trainInputs.Select(row => row[d]).ToList();
                means[d] = colValues.Average();

                double sumSq = colValues.Sum(v => Math.Pow(v - means[d], 2));
                stdDevs[d] = Math.Sqrt(sumSq / colValues.Count);
                if (stdDevs[d] == 0)
                    stdDevs[d] = 1; // Prevent div/0
            }

            var normalizedInputs = new List<double[]>();
            foreach (var row in trainInputs)
            {
                double[] normRow = new double[dim];
                for (int d = 0; d < dim; d++)
                    normRow[d] = (row[d] - means[d]) / stdDevs[d];
                normalizedInputs.Add(normRow);
            }
            Console.WriteLine("Done");

            // 2. Training
            Console.WriteLine(
                $"\n[3/4] Training Model ({epochs} epochs, {hiddenNeurons} neurons)..."
            );
            var trainer = new RbfTrainer();
            var network = trainer.Train(
                normalizedInputs,
                trainTargets,
                hiddenNeurons,
                epochs,
                learningRate
            );
            Console.WriteLine(" > Training Complete!");

            // 3. Evaluation
            Console.Write("\n[4/4] Evaluating on Validation Set... ");

            int correct = 0;
            int total = valInputs.Count;

            var classifier = new FoodClassifier(network, means, stdDevs, schema);

            var predictions = new List<double>();
            var actuals = new List<double>();

            for (int i = 0; i < total; i++)
            {
                double prob = classifier.Predict(valInputs[i]);
                predictions.Add(prob);
                actuals.Add(valTargets[i]);
            }

            Console.WriteLine("Done\n");

            var metrics = MetricsCalculator.Calculate(predictions, actuals);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(metrics.ToString());
            Console.WriteLine("--------------------------------------------------");
            
            return new FoodClassifier(network, means, stdDevs, schema);
        }
    }
}
