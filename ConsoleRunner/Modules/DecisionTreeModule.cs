using System;
using System.Linq;
using System.Collections.Generic;
using ML_Project_Windows_App;

namespace ConsoleRunner.Modules
{
    public static class DecisionTreeModule
    {
        public static void Run(List<double[]> inputs, List<double> targets)
        {
            // 1. Split Data (Train/Val)
            Console.WriteLine("[2/4] Splitting Data (80% Train, 20% Validation)...");
            
            // Simple shuffle and split
            var rand = new Random(42);
            int n = inputs.Count;
            var indices = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();
            
            int trainCount = (int)(n * 0.8);
            
            var trainInputs = indices.Take(trainCount).Select(i => inputs[i]).ToArray();
            var trainTargets = indices.Take(trainCount).Select(i => targets[i]).ToArray();
            
            var valInputs = indices.Skip(trainCount).Select(i => inputs[i]).ToArray();
            var valTargets = indices.Skip(trainCount).Select(i => targets[i]).ToArray();
            
            Console.WriteLine($" > Train Set: {trainInputs.Length} samples");
            Console.WriteLine($" > Val Set:   {valInputs.Length} samples");

            // 2. Training
            Console.WriteLine($"\n[3/4] Training Decision Tree...");
            // Using defaults from Python (min_samples_split=2, max_depth=2) OR we probably want slightly deeper?
            // Python code default was 2, 2. I will stick to 10/10 as used in the previous verification run.
            
            var dt = new DecisionTreeRegressor(minSamplesSplit: 10, maxDepth: 10);
            dt.Fit(trainInputs, trainTargets);
            Console.WriteLine(" > Training Complete!");

            // 3. Evaluation
            Console.WriteLine("\n[4/4] Evaluating on Validation Set... ");
            
            double[] predictions = dt.Predict(valInputs);
            
            double mse = 0;
            double sst = 0; // Total Sum of Squares
            double meanVal = valTargets.Average();

            for(int i=0; i<predictions.Length; i++)
            {
                double err = predictions[i] - valTargets[i];
                mse += err * err;
                
                double dev = valTargets[i] - meanVal;
                sst += dev * dev;
            }
            mse /= predictions.Length;
            double rmse = Math.Sqrt(mse);

            double r2 = 1.0 - (mse * predictions.Length) / sst;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($" MSE:      {mse:F4}");
            Console.WriteLine($" RMSE:     {rmse:F4}");
            Console.WriteLine($" R2 Score: {r2:F4}");
            Console.WriteLine("--------------------------------------------------");
            
            // Show a few examples
            Console.WriteLine("\n Sample Predictions:");
            for(int i=0; i<Math.Min(5, predictions.Length); i++)
            {
                 Console.WriteLine($"   Actual: {valTargets[i]:F2} | Predicted: {predictions[i]:F2}");
            }
        }
    }
}
