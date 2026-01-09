using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ML_Project_Windows_App;
using Source;
using Source.Data;

namespace ConsoleRunner
{
    class Program
    {
        // Default Configuration
        static string _trainFile = Path.Combine("DataSet", "train_80k.csv");
        static string _testFile = Path.Combine("DataSet", "test_20k.csv");
        static string _schema =
            "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;NUTRI_SCORE;CLASSIFICATION";

        // RBF Params
        static int _hiddenNeurons = 25;
        static int _epochs = 50;
        static double _learningRate = 0.01;

        // Decision Tree Params
        static int _dtTotalSamples = 0; // 0 = All
        static int _minSamplesSplit = 10;
        static int _minSamplesLeaf = 1;
        static int _maxDepth = 10;

        // Trained Models
        static FoodClassifier? _trainedRbfNetwork;
        static DecisionTreeRegressor? _trainedDecisionTree;
        static List<double[]>? _trainingInputs;

        static int _modelType = 1; // 1: RBF Network, 2: Decision Tree

        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                string? input = Console.ReadLine();

                switch (input?.Trim())
                {
                    case "0":
                        _modelType = _modelType == 1 ? 2 : 1;
                        if (_modelType == 2)
                        {
                            // Output a warning about path if not absolute, but let's try to find it
                            // Using absolute path from context for reliability in this environment
                            _trainFile = Path.Combine("DataSet", "ML_Training_Data.csv");
                            _schema =
                                "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;final_score";
                            Console.WriteLine("\n[Switched to Decision Tree]");
                            Console.WriteLine($"Default Train File set to: {_trainFile}");
                            Console.WriteLine($"Default Schema set to: {_schema}");
                        }
                        else
                        {
                            _trainFile = Path.Combine("DataSet", "train_80k.csv");
                            _schema =
                                "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;NUTRI_SCORE;CLASSIFICATION";
                            Console.WriteLine("\n[Switched to RBF Network]");
                            Console.WriteLine("Defaults restored.");
                        }
                        Console.WriteLine("Press Enter...");
                        Console.ReadLine();
                        break;
                    case "1":
                        if (_modelType == 1) // RBF - Epochs
                        {
                            Console.Write($"Enter Epochs (Current: {_epochs}): ");
                            if (int.TryParse(Console.ReadLine(), out int ep))
                                _epochs = ep;
                        }
                        else // DT - Total Sample Size
                        {
                            Console.Write(
                                $"Enter Total Sample Size (0 = All, Current: {_dtTotalSamples}): "
                            );
                            if (int.TryParse(Console.ReadLine(), out int ts))
                                _dtTotalSamples = ts;
                        }
                        break;
                    case "2":
                        if (_modelType == 1) // RBF - LR
                        {
                            Console.Write($"Enter Learning Rate (Current: {_learningRate}): ");
                            if (double.TryParse(Console.ReadLine(), out double lr))
                                _learningRate = lr;
                        }
                        else // DT - Min Samples Split
                        {
                            Console.Write(
                                $"Enter Min Samples Per Split (Current: {_minSamplesSplit}): "
                            );
                            if (int.TryParse(Console.ReadLine(), out int ms))
                                _minSamplesSplit = ms;
                        }
                        break;
                        break;
                    case "3":
                        if (_modelType == 2)
                        {
                            // DT - Min Samples Leaf
                            Console.Write($"Enter Min Samples Per Leaf (Current: {_minSamplesLeaf}): ");
                            if (int.TryParse(Console.ReadLine(), out int ml))
                                _minSamplesLeaf = ml;
                        }
                        else
                        {
                            // RBF - Hidden Neurons
                            Console.Write($"Enter Hidden Neurons (Current: {_hiddenNeurons}): ");
                            if (int.TryParse(Console.ReadLine(), out int hn))
                                _hiddenNeurons = hn;
                        }
                        break;
                    case "4":
                        if (_modelType == 2)
                        {
                            // DT - Max Depth
                            Console.Write($"Enter Max Depth (Current: {_maxDepth}): ");
                            if (int.TryParse(Console.ReadLine(), out int md))
                                _maxDepth = md;
                        }
                        else
                        {
                            // RBF - Training File
                            Console.Write($"Enter Training File Path (Current: {_trainFile}): ");
                            string? tf = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tf))
                                _trainFile = tf;
                        }
                        break;
                    case "5":
                        if (_modelType == 2)
                        {
                            // DT - Training File
                            Console.Write($"Enter Training File Path (Current: {_trainFile}): ");
                            string? tf = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tf))
                                _trainFile = tf;
                        }
                        else
                        {
                            // RBF - Schema
                            Console.Write($"Enter Schema (Current: {_schema}): ");
                            string? sch = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(sch))
                                _schema = sch;
                        }
                        break;
                    case "6":
                        if (_modelType == 2)
                        {
                            // DT - Schema
                            Console.Write($"Enter Schema (Current: {_schema}): ");
                            string? sch = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(sch))
                                _schema = sch;
                        }
                        else
                        {
                            // RBF - Run
                            RunTraining();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        break;
                    case "7":
                        if (_modelType == 2)
                        {
                            // DT - Run
                            RunTraining();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        else
                        {
                            // RBF - Predict
                            RunPrediction();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        break;
                    case "8":
                        if (_modelType == 2)
                        {
                            // DT - Predict
                            RunPrediction();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        else
                        {
                            // RBF - Optimize
                            RunOptimization();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        break;
                    case "9":
                        if (_modelType == 2)
                        {
                            // DT - Optimize
                            RunOptimization();
                            Console.WriteLine("\n[Press Enter to return to menu]");
                            Console.ReadLine();
                        }
                        else
                        {
                            return; // RBF - Exit
                        }
                        break;
                    case "10":
                        if (_modelType == 2)
                            return; // DT - Exit
                        else
                            Console.WriteLine("Invalid option. Press Enter...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("             ML Console Runner");
            Console.WriteLine("==================================================");
            Console.WriteLine(
                $" [0] Model Type:       {(_modelType == 1 ? "RBF Network (Classification)" : "Decision Tree (Regression)")}"
            );
            Console.WriteLine("--------------------------------------------------");

            if (_modelType == 1)
            {
                Console.WriteLine($" [1] Epochs:           {_epochs}");
                Console.WriteLine($" [2] Learning Rate:    {_learningRate}");
                Console.WriteLine($" [3] Hidden Neurons:   {_hiddenNeurons}");
            }
            else
            {
                Console.WriteLine(
                    $" [1] Total Dataset Size: {(_dtTotalSamples == 0 ? "All" : _dtTotalSamples.ToString())}"
                );
                Console.WriteLine($" [2] Min Samples/Split:  {_minSamplesSplit}");
                Console.WriteLine($" [3] Min Samples/Leaf:   {_minSamplesLeaf}");
                Console.WriteLine($" [4] Max Depth:          {_maxDepth}");
            }

            Console.WriteLine($" [5] Training File:    {_trainFile}");
            Console.WriteLine($" [6] Schema:           {_schema}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" [7] RUN TRAINING");
            Console.WriteLine(" [8] PREDICT (Using trained model)");
            Console.WriteLine(" [9] OPTIMIZE (Grid Search)");
            Console.WriteLine(" [10] EXIT");
            Console.WriteLine("==================================================");
            Console.Write(" >> Select Option: ");
        }

        static void RunOptimization()
        {
            if (_modelType == 1)
            {
                Console.WriteLine("Optimization currently implemented for Decision Tree only.");
                return;
            }

            Console.WriteLine("\n--- Hyperparameter Grid Search ---");

            if (!File.Exists(_trainFile))
            {
                Console.WriteLine(
                    $"[Error] Train file not found at: {Path.GetFullPath(_trainFile)}"
                );
                return;
            }

            // Load Data
            Console.Write("Loading Data... ");
            var (inputs, targets) = DataLoader.LoadCsv(_trainFile, _schema);
            _trainingInputs = inputs;

            // Optional: limit data for speed during optimization if dataset is huge
            if (_dtTotalSamples > 0 && _dtTotalSamples < inputs.Count)
            {
                inputs = inputs.Take(_dtTotalSamples).ToList();
                targets = targets.Take(_dtTotalSamples).ToList();
            }
            Console.WriteLine($"Done ({inputs.Count} records)");

            // Define Grid
            int[] depthGrid = { 5, 10, 30};
            int[] splitGrid = { 2, 5 };
            int[] leafGrid = { 1, 5 };

            Console.WriteLine(
                $"\nGrid: MaxDepth={string.Join(",", depthGrid)}, MinSamplesSplit={string.Join(",", splitGrid)}, MinSamplesLeaf={string.Join(",", leafGrid)}"
            );
            Console.WriteLine($"Total Combinations: {depthGrid.Length * splitGrid.Length * leafGrid.Length}");
            Console.WriteLine("Running 3-Fold Cross Validation for each combination...\n");

            double bestScore = double.MinValue;
            int bestDepth = -1;
            int bestSplit = -1;
            int bestLeaf = -1;

            int count = 1;
            int total = depthGrid.Length * splitGrid.Length * leafGrid.Length;

            foreach (var d in depthGrid)
            {
                foreach (var s in splitGrid)
                {
                    foreach (var l in leafGrid)
                    {
                        Console.Write($"[{count}/{total}] Depth={d}, Split={s}, Leaf={l} ... ");

                        var (meanR2, stdDev) = CrossValidator.CrossValidate(
                            inputs,
                            targets,
                            3,
                            (trainX, trainY, valX) =>
                            {
                                var dt = new DecisionTreeRegressor(s, l, d);
                                dt.Fit(trainX.ToArray(), trainY.ToArray());
                                return dt.Predict(valX.ToArray());
                            }
                        );

                        Console.WriteLine($"R2={meanR2:F4} (+/- {stdDev:F4})");

                        if (meanR2 > bestScore)
                        {
                            bestScore = meanR2;
                            bestDepth = d;
                            bestSplit = s;
                            bestLeaf = l;
                        }
                        count++;
                    }
                }
            }

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"Optimization Complete!");
            Console.WriteLine($"Best R2: {bestScore:F4}");
            Console.WriteLine($"Best Params: MaxDepth={bestDepth}, MinSamplesSplit={bestSplit}, MinSamplesLeaf={bestLeaf}");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Update current configuration with these parameters? (Y/n): ");
            var resp = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(resp) || resp.Trim().ToLower() == "y")
            {
                _maxDepth = bestDepth;
                _minSamplesSplit = bestSplit;
                _minSamplesLeaf = bestLeaf;
                Console.WriteLine("Configuration updated.");
            }
        }

        static void RunTraining()
        {
            Console.WriteLine("\nStarting Training Pipeline...");

            if (!File.Exists(_trainFile))
            {
                Console.WriteLine(
                    $"[Error] Train file not found at: {Path.GetFullPath(_trainFile)}"
                );
                return;
            }

            try
            {
                // 1. Load Data
                Console.Write("\n[1/4] Loading Training Data... ");
                var (inputs, targets) = DataLoader.LoadCsv(_trainFile, _schema);
                _trainingInputs = inputs;
                Console.WriteLine($"Done ({inputs.Count} records)");

                if (_modelType == 1) // RBF Network
                {
                    _trainedRbfNetwork = ConsoleRunner.Modules.RbfModule.Run(
                        inputs,
                        targets,
                        _hiddenNeurons,
                        _epochs,
                        _learningRate,
                        _testFile,
                        _schema
                    );
                }
                else // Decision Tree
                {
                    if (_dtTotalSamples > 0 && _dtTotalSamples < inputs.Count)
                    {
                        Console.WriteLine(
                            $"[Config] Limiting dataset to {_dtTotalSamples} samples."
                        );
                        inputs = inputs.Take(_dtTotalSamples).ToList();
                        targets = targets.Take(_dtTotalSamples).ToList();
                    }
                    _trainedDecisionTree = ConsoleRunner.Modules.DecisionTreeModule.Run(
                        inputs,
                        targets,
                        _minSamplesSplit,
                        _minSamplesLeaf,
                        _maxDepth,
                        _schema
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Fatal Error] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void RunPrediction()
        {
            // Check if model exists
            if (_modelType == 1 && _trainedRbfNetwork == null)
            {
                Console.WriteLine("[Error] No RBF Network trained yet. Please Option 6 first.");
                return;
            }
            if (_modelType == 2 && _trainedDecisionTree == null)
            {
                // Check both just in case, though only one should be used.
                // Actually _trainedDecisionTree was used in RunTraining in turn 206.
                // But wait, in turn 206 I used _trainedDecisionTree.
                // In turn 216+ user might have changed things?
                // Let's rely on standard logic.
                Console.WriteLine("[Error] No Decision Tree trained yet. Please Option 6 first.");
                // Note: user edits in 219 changed RunTraining to NOT assign to _trainedDecisionTree static field?
                // Let's check RunTraining in current file.
                return;
            }

            Console.WriteLine("\n--- Manual Prediction ---");
            Console.WriteLine($"Using {(_modelType == 1 ? "RBF Network" : "Decision Tree")}");

            var columns = _schema.Split(';');
            var featureNames = new List<string>();

            int targetCount = _modelType == 1 ? 2 : 1;
            int featureCount = columns.Length - targetCount;

            for (int i = 0; i < featureCount; i++)
            {
                featureNames.Add(columns[i]);
            }

            double[] userInputs = new double[featureCount];

            for (int i = 0; i < featureCount; i++)
            {
                while (true)
                {
                    Console.Write($"Enter {featureNames[i]}: ");
                    if (double.TryParse(Console.ReadLine(), out double val))
                    {
                        userInputs[i] = val;
                        break;
                    }
                    Console.WriteLine("Invalid number.");
                }
            }

            // Predict
            try
            {
                double result = 0;
                if (_modelType == 1)
                {
                    result = _trainedRbfNetwork!.Predict(userInputs);
                    Console.WriteLine($"\n>> Predicted Value: {result:F4}");
                    Console.WriteLine(
                        $">> Class: {(result >= 0.5 ? "Healthy (1)" : "Unhealthy (0)")}"
                    );
                }
                else
                {
                    var predictions = _trainedDecisionTree!.Predict(new double[][] { userInputs });
                    result = predictions[0];
                    Console.WriteLine($"\n>> Predicted Score: {result:F4}");
                }

                // LIME Explanation
                if (_trainingInputs != null && _trainingInputs.Count > 0)
                {
                    Console.WriteLine("\nGenerating LIME Explanation...");
                    var explainer = new LimeExplainer(_trainingInputs);

                    Func<double[][], double[]> predictFn = (batch) =>
                    {
                        var outputs = new double[batch.Length];
                        for (int k = 0; k < batch.Length; k++)
                        {
                            if (_modelType == 1)
                                outputs[k] = _trainedRbfNetwork!.Predict(batch[k]);
                            else
                                outputs[k] = _trainedDecisionTree!.PredictSingle(batch[k]);
                        }
                        return outputs;
                    };

                    // Explain
                    var explanation = explainer.Explain(userInputs, predictFn, numSamples: 500);

                    Console.WriteLine("Top 3 Influential Features:");
                    foreach (var (idx, weight) in explanation.Take(3))
                    {
                        string name =
                            (idx < featureNames.Count) ? featureNames[idx] : $"Feat {idx}";
                        Console.WriteLine($"  {name}: {weight:F4}");
                    }
                }
                else
                {
                    Console.WriteLine("\n(LIME Explanation skipped: Training data not loaded)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during prediction: {ex.Message}");
            }
        }
    }
}
