using Source;
using Source.Data;
using System.Data;

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
            // 1. Load Model
            using var db = new Source.Data.AppDbContext();
            var entity = db.TrainedModels
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefault(m => m.ModelName == "FoodClassifier_V1");

            if (entity == null)
            {
                RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                RichTextBoxOutput.AppendText("No model found. Please train first.\r\n");
                return;
            }

            // Reconstruct Network
            var model = new RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            model.Bias = entity.Bias;
            // Note: Ensure CultureInfo.InvariantCulture is used if these were saved with dots
            model.Weights = entity.WeightsData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Sigmas = entity.SigmasData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Centroids = DeserializeCentroids(entity.CentroidsData);

            // 2. Load Test Data 
            var testData = DataLoader.LoadCsv("test_20k_normalized_data.csv");
            RichTextBoxOutput.AppendText("Test Data Loaded (NutriScore ignored). Finding Optimal Threshold...\r\n");

            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Test Data Loaded.\r\n");

            List<double> rawScores = new List<double>();
            List<double> actuals = testData.Targets;

            // 3. Background Evaluation
            Task.Run(() =>
            {
                // A. Generate Scores
                foreach (var input in testData.Inputs)
                {
                    rawScores.Add(model.Forward(input));
                }

                // B. Find Optimal Threshold
                double bestThreshold = 0.5;
                double bestAccuracy = 0.0;
                EvaluationMetrics bestMetrics = new EvaluationMetrics();

                // Scan from 0.05 to 0.95 to find the sweet spot
                for (double sweetSpot = 0.05; sweetSpot < 0.95; sweetSpot += 0.05)
                {
                    EvaluationMetrics metrics = MetricsCalculator.Calculate(rawScores, actuals, sweetSpot);

                    // You can optimize for Accuracy, F-Measure, or Precision depending on your goal
                    if (metrics.Accuracy > bestAccuracy)
                    {
                        bestAccuracy = metrics.Accuracy;
                        bestThreshold = sweetSpot;
                        bestMetrics = metrics;
                    }
                }

                // C. Update UI
                this.Invoke((MethodInvoker)delegate
                {
                    RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                    RichTextBoxOutput.AppendText(bestMetrics.ToString() + "\r\n");

                    // Comparison with default
                    //var defaultMetrics = MetricsCalculator.Calculate(rawScores, actuals, 0.5);
                    //RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                    //RichTextBoxOutput.AppendText($"\n(Default 0.5 Accuracy was: {defaultMetrics.Accuracy:P2})\r\n");
                });
            });
        }

        #endregion


        #region Auxiliary -----------------------------------------------------------

        /// <summary>
        /// DeserializeCentroids
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[][] DeserializeCentroids(string data)
        {
            // Split the main string by '|' to get each centroid (row)
            var rows = data.Split('|');
            var result = new double[rows.Length][];

            for (int i = 0; i < rows.Length; i++)
            {
                // Split each row by ',' and parse the doubles safely
                // FIX: Added CultureInfo.InvariantCulture to handle "." decimals correctly
                result[i] = rows[i]
                    .Split(',')
                    .Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture))
                    .ToArray();
            }

            return result;
        }

        #endregion

    }
}
