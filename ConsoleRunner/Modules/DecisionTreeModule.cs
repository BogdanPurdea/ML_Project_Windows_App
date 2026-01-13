using System;
using System.Linq;
using System.Collections.Generic;
using ML_Project_Windows_App;
using Source;
using System.IO;

namespace ConsoleRunner.Modules
{
    public static class DecisionTreeModule
    {
        public static DecisionTreeRegressor Run(
            List<double[]> trainInputs, 
            List<double> trainTargets, 
            List<double[]> valInputs, 
            List<double> valTargets,
            int minSamplesSplit, 
            int minSamplesLeaf, 
            int maxDepth, 
            string schema)
        {
            Console.WriteLine($" > Train Set: {trainInputs.Count} samples");
            Console.WriteLine($" > Val Set:   {valInputs.Count} samples");

            // 1. Training
            Console.WriteLine($"\n[3/4] Training Decision Tree (MinSamplesSplit={minSamplesSplit}, MinSamplesLeaf={minSamplesLeaf}, MaxDepth={maxDepth})...");
            
            // Convert List to Array for internal use if needed (DecisionTree implementation takes arrays)
            var trainInputsArray = trainInputs.ToArray();
            var trainTargetsArray = trainTargets.ToArray();

            var dt = new DecisionTreeRegressor(minSamplesSplit: minSamplesSplit, minSamplesLeaf: minSamplesLeaf, maxDepth: maxDepth);
            dt.Fit(trainInputsArray, trainTargetsArray);
            Console.WriteLine(" > Training Complete!");
            
            // 2. Evaluation
            Console.WriteLine("\n[4/4] Evaluating on Validation Set... ");
            
            var predictor = new ScorePredictor(dt, schema);
            
            var predictions = new List<double>();
            foreach (var input in valInputs)
            {
                // Explicitly typed as double[] to avoid any ambiguity
                double[] inputVec = input;
                predictions.Add(predictor.Predict(inputVec));
            }
            
            var metrics = Source.RegressionMetricsCalculator.Calculate(predictions.ToList(), valTargets.ToList());

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(metrics.ToString());
            Console.WriteLine("--------------------------------------------------");
            
            // Show a few examples
            Console.WriteLine("\n Sample Predictions:");
            for(int i=0; i<Math.Min(5, predictions.Count); i++)
            {
                 Console.WriteLine($"   Actual: {valTargets[i]:F2} | Predicted: {predictions[i]:F2}");
            }

            return dt;
        }

    }
}
