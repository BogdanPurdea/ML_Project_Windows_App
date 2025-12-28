using Source;
using Source.Data;
using System.Data;
using System.Globalization;

namespace WinForm_RFBN_APP
{
    public partial class TrainingPage : UserControl
    {

        #region Constructor ---------------------------------------------------------

        /// <summary>
        /// Constructor
        /// </summary>
        public TrainingPage()
        {
            InitializeComponent();
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        /// <summary>
        /// Train Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TrainButton_Click(object sender, EventArgs e)
        {
            var data = LoadCsv("train_80k_normalized_data.csv");
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Data Loaded. Starting Training...\n");

            // Normalize data (Crucial for RBF/K-Means)
            // NOTE: In production, save Min/Max stats to DB to normalize new inputs similarly
            // For brevity, skipping explicit normalization code here, but HIGHLY recommended.

            // Input data from TextBoxes
            bool successRead = true;

            int hiddenNeurons, epochs;
            successRead |= int.TryParse(HiddenNeuronsTextBox.Text.ToString(), out hiddenNeurons);
            successRead |= int.TryParse(EpochsTextBox.Text.ToString(), out epochs);

            double learningRate;
            successRead |= double.TryParse(LearningRateTextBox.Text.ToString(), out learningRate);

            // 25 Hidden Neurons, 100 Epochs, 0.01 Learning Rate
            if (!successRead)
            {
                hiddenNeurons = 25;
                epochs = 100;
                learningRate = 0.01d;
            }

            var trainer = new RbfTrainer();

            // Run on background thread to keep UI responsive
            RbfNetwork model = await Task.Run(() =>
            {
                return trainer.Train(data.Inputs, data.Targets, hiddenNeurons, epochs, learningRate);
            });
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Training Complete!\n");

            ModelRepository.SaveModel("FoodClassifier_V1", model);
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Model Saved to Repository.\n");
        }

        /// <summary>
        /// Clean Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CleanButton_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to delete all trained models? This cannot be undone.",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    ModelRepository.ClearModel();

                    RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                    RichTextBoxOutput.AppendText($"[System]: Database cleared at {DateTime.Now.ToShortTimeString()}.\n");
                    RichTextBoxOutput.AppendText("Ready for clean training.\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error clearing database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                    RichTextBoxOutput.AppendText($"Error clearing database: {ex.Message}");
                }
            }
        }

        #endregion


        #region Data Access ---------------------------------------------------------

        /// <summary>
        /// LoadCsv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            // Read all lines
            var lines = File.ReadAllLines(filePath);

            // Skip header if it exists (assuming header is purely text)
            // If your CSV strictly has no header, remove the .Skip(1)
            var dataLines = lines.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            List<double[]> inputs = new List<double[]>();
            List<double> targets = new List<double>();

            if (dataLines.Count == 0) return (inputs, targets);

            // Detect dimension from the first data row
            // We assume the LAST column is the target, so InputDim = TotalColumns - 1
            int totalColumns = dataLines[0].Split(';').Length;
            int inputDim = totalColumns - 1;

            foreach (var line in dataLines)
            {
                var parts = line.Split(';');

                // Safety check to ensure row consistency
                if (parts.Length != totalColumns) continue;

                double[] rowInput = new double[inputDim];

                // Loop dynamically up to inputDim
                for (int i = 0; i < inputDim; i++)
                {
                    if (double.TryParse(parts[i], CultureInfo.InvariantCulture, out double val))
                    {
                        rowInput[i] = val;
                    }
                    else
                    {
                        rowInput[i] = 0.0; // Handle missing/bad data safely
                    }
                }

                // Parse Target (Last Column)
                if (double.TryParse(parts[inputDim], CultureInfo.InvariantCulture, out double target))
                {
                    targets.Add(target);
                }
                else
                {
                    // If target is invalid, you might want to skip this row entirely
                    continue;
                }

                inputs.Add(rowInput);
            }

            return (inputs, targets);
        }

        #endregion

    }
}
