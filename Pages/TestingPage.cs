using Source;
using Source.Data;

namespace WinForm_RFBN_APP
{
    public partial class TestingPage : UserControl
    {

        #region Constructor ---------------------------------------------------------

        public TestingPage()
        {
            InitializeComponent();
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        private void TestButton_Click(object sender, EventArgs e)
        {
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");

            // 1. Load the Wrapper (Network + Normalization Stats)
            // We use the new LoadClassifier method which returns the ready-to-use object.
            FoodClassifier classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");

            if (classifier == null)
            {
                RichTextBoxOutput.AppendText("No model found. Please train first.\r\n");
                return;
            }

            // 2. Load RAW Test Data

            string inputDocumentName = InputDocumentTextbox.Text.Trim();

            if (inputDocumentName.Length <= 1)
            {
                inputDocumentName = "test.csv";
            }

            var rawTestData = DataLoader.LoadCsv(inputDocumentName, classifier.InputSchema);

            if (rawTestData.Inputs.Count == 0)
            {
                RichTextBoxOutput.AppendText("Error: No test data found in CSV!\r\n");
                return;
            }
            if (classifier.InputSchema.Length < 1)
            {
                RichTextBoxOutput.AppendText($"Error: Invalid user input features schema {classifier.InputSchema}!\r\n");
                return;
            }

            RichTextBoxOutput.AppendText($"Loaded {rawTestData.Inputs.Count} raw test records.\n");
            RichTextBoxOutput.AppendText("Starting Batch Prediction...\n");

            // 3. Run Predictions in Background
            List<double> rawScores = new List<double>();
            List<double> actuals = rawTestData.Targets;

            Task.Run(() =>
            {
                foreach (var rawInput in rawTestData.Inputs)
                {
                    // The wrapper automatically:
                    // 1. Applies the Z-Score normalization (using stats from Training).
                    // 2. Runs the RBF Network forward pass.
                    double score = classifier.Predict(rawInput);

                    rawScores.Add(score);
                }

                // 4. Find Optimal Threshold & Metrics (Same logic as before)
                double bestThreshold = 0.5;
                double bestAccuracy = 0.0;
                EvaluationMetrics bestMetrics = new EvaluationMetrics();

                // Scan thresholds to find the "sweet spot"
                for (double sweetSpot = 0.05; sweetSpot < 0.95; sweetSpot += 0.05)
                {
                    EvaluationMetrics metrics = MetricsCalculator.Calculate(rawScores, actuals, sweetSpot);
                    if (metrics.Accuracy > bestAccuracy)
                    {
                        bestAccuracy = metrics.Accuracy;
                        bestThreshold = sweetSpot;
                        bestMetrics = metrics;
                    }
                }

                // 5. Update UI safely
                this.Invoke((MethodInvoker)delegate
                {
                    RichTextBoxOutput.AppendText($"Batch Prediction Complete.\n");
                    RichTextBoxOutput.AppendText($"Optimal Threshold: {bestThreshold:F2}\n");
                    RichTextBoxOutput.AppendText(bestMetrics.ToString() + "\r\n");
                });
            });
        }

        #endregion

    }
}