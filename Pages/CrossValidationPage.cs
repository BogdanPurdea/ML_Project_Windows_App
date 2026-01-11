using System.Data;
using System.Globalization;
using Source;
using Source.Data;

namespace WinForm_RFBN_APP
{
    public partial class CrossValidationPage : UserControl
    {
        public CrossValidationPage()
        {
            InitializeComponent();
        }

        private async void RunGridSearchButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int kFolds, out int start, out int end, out int step, out int epochNum, out double learningRate))
                return;

            string csvPath = CsvPathTextBox.Text.Trim();
            string schema = SchemaTextBox.Text.Trim();

            if (string.IsNullOrEmpty(csvPath) || string.IsNullOrEmpty(schema))
            {
                MessageBox.Show("Please provide both CSV Path and Schema.");
                return;
            }

            RunGridSearchButton.Enabled = false;
            LogRichTextBox.Clear();
            LogRichTextBox.AppendText($"Starting Grid Search: {kFolds}-Fold CV, Neurons {start}-{end} (step {step}), LR {learningRate}\r\n");
            LogRichTextBox.AppendText($"File: {csvPath}\r\n");

            try
            {
                // Run in background to keep UI responsive
                await Task.Run(() => PerformGridSearch(kFolds, start, end, step, epochNum, learningRate, csvPath, schema));
            }
            catch (Exception ex)
            {
                Invoke(() => LogRichTextBox.AppendText($"Error: {ex.Message}\r\n"));
            }
            finally
            {
                RunGridSearchButton.Enabled = true;
            }
        }

        private void PerformGridSearch(int kFolds, int startNeurons, int endNeurons, int step, int epochNum, double learningRate, string csvPath, string schema)
        {
            // 1. Load Data
            var (allInputs, allTargets) = DataLoader.LoadCsv(csvPath, schema);

            // 2. Normalize Globally (as per prototype constraints)
            var stats = NormalizationHelper.ComputeZScoreStats(allInputs);
            var normalizedInputs = allInputs.Select(row => NormalizationHelper.NormalizeRow(row, stats.Mean, stats.StdDev)).ToList();

            double bestAccuracy = 0;
            int bestNeuronCount = 0;

            // 3. Grid Search Loop
            for (int neuronCount = startNeurons; neuronCount <= endNeurons; neuronCount += step)
            {
                double totalAccuracy = 0;

                // 4. K-Fold Cross Validation
                // Shuffle indices locally
                var indices = Enumerable.Range(0, normalizedInputs.Count).OrderBy(x => Guid.NewGuid()).ToList();
                int foldSize = normalizedInputs.Count / kFolds;

                for (int fold = 0; fold < kFolds; fold++)
                {
                    int testStart = fold * foldSize;
                    int testEnd = (fold == kFolds - 1) ? normalizedInputs.Count : testStart + foldSize;

                    var trainInputs = new List<double[]>();
                    var trainTargets = new List<double>();
                    var testInputs = new List<double[]>();
                    var testTargets = new List<double>();

                    // Split
                    for (int i = 0; i < normalizedInputs.Count; i++)
                    {
                        int originalIndex = indices[i];
                        if (i >= testStart && i < testEnd)
                        {
                            testInputs.Add(normalizedInputs[originalIndex]);
                            testTargets.Add(allTargets[originalIndex]);
                        }
                        else
                        {
                            trainInputs.Add(normalizedInputs[originalIndex]);
                            trainTargets.Add(allTargets[originalIndex]);
                        }
                    }

                    // Train
                    var trainer = new RbfTrainer();
                    var network = trainer.Train(trainInputs, trainTargets, neuronCount, epochNum, learningRate);

                    // Validation
                    int correct = 0;
                    for (int i = 0; i < testInputs.Count; i++)
                    {
                        double output = network.Forward(testInputs[i]);
                        int predicted = output >= 0.5 ? 1 : 0;
                        int actual = (int)testTargets[i];
                        if (predicted == actual) correct++;
                    }

                    double foldAccuracy = (double)correct / testInputs.Count;
                    totalAccuracy += foldAccuracy;
                }

                double avgAccuracy = totalAccuracy / kFolds;
                
                // Thread-safe UI update
                Invoke(() => LogRichTextBox.AppendText($"Neurons: {neuronCount}, Avg Accuracy: {avgAccuracy:P2}\r\n"));

                if (avgAccuracy > bestAccuracy)
                {
                    bestAccuracy = avgAccuracy;
                    bestNeuronCount = neuronCount;
                }
            }

            Invoke(() =>
            {
                LogRichTextBox.AppendText("--------------------------------------------------\r\n");
                LogRichTextBox.AppendText($"BEST CONFIGURATION: {bestNeuronCount} Neurons with {bestAccuracy:P2} Accuracy.\r\n");
            });
        }

        private bool ValidateInputs(out int kFolds, out int start, out int end, out int step, out int epochNum, out double learningRate)
        {
            kFolds = start = end = step = epochNum = 0;
            learningRate = 0;

            if (!int.TryParse(KFoldsTextBox.Text, out kFolds) || kFolds < 2)
            {
                MessageBox.Show("Invalid K-Folds. Must be >= 2.");
                return false;
            }
            if (!int.TryParse(StartNeuronTextBox.Text, out start) || start < 1)
            {
                MessageBox.Show("Invalid Start Neurons.");
                return false;
            }
            if (!int.TryParse(EndNeuronTextBox.Text, out end) || end < start)
            {
                MessageBox.Show("Invalid End Neurons.");
                return false;
            }
            if (!int.TryParse(StepTextBox.Text, out step) || step < 1)
            {
                MessageBox.Show("Invalid Step.");
                return false;
            }
            if (!int.TryParse(EpochTextBox.Text, out epochNum) || epochNum < 1)
            {
                MessageBox.Show("Invalid Epoch.");
                return false;
            }
            if (!double.TryParse(LearningRateTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out learningRate) || learningRate <= 0)
            {
                MessageBox.Show("Invalid Learning Rate.");
                return false;
            }
            return true;
        }

        private void Invoke(Action action)
        {
            if (this.InvokeRequired)
            {
                base.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
