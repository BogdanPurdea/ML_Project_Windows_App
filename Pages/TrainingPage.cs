using Source;
using Source.Data;

namespace WinForm_RFBN_APP
{
    public partial class TrainingPage : UserControl
    {

        #region Constructor ---------------------------------------------------------

        public TrainingPage()
        {
            InitializeComponent();
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            try
            {
                TrainButton.Enabled = false;
                RichTextBoxOutput.Clear();
                RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                RichTextBoxOutput.AppendText("Starting Training Pipeline...\n");

                // 1. Get the Schema from your TextBox (assuming you named it SchemaInputTextBox)
                // Example text: "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy"
                string userFeatureSchema = FeaturesTextBox.Text.Trim();

                if (userFeatureSchema.Length <= 1)
                {
                    userFeatureSchema = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";
                }

                RichTextBoxOutput.AppendText($"Using Schema: {userFeatureSchema}\n");

                // 2. Load RAW Data
                // The DataLoader reads the CSV. Ensure "train_80k.csv" exists in your bin folder.
                string inputDocumentName = InputDocumentTextbox.Text.Trim();
                
                if (inputDocumentName.Length <= 1)
                {
                    inputDocumentName = "train.csv";
                }

                var rawData = await Task.Run(() => DataLoader.LoadCsv(inputDocumentName, userFeatureSchema));

                if (rawData.Inputs.Count == 0)
                {
                    RichTextBoxOutput.AppendText("Error: No training data found.\n");
                    return;
                }

                RichTextBoxOutput.AppendText($"Raw Data Loaded: {rawData.Inputs.Count} records.\n");

                // 3. Compute Normalization Stats (Z-Score)
                // We MUST compute these on the TRAINING set. These exact values will be saved 
                // and used to normalize future user inputs in the FoodClassifier wrapper.
                var (means, stdDevs) = NormalizationHelper.ComputeZScoreStats(rawData.Inputs);

                RichTextBoxOutput.AppendText("Normalization Stats Computed (Mean & StdDev).\n");

                // 4. Normalize the Training Data
                // The network never sees raw data, only data scaled ~ -3 to +3
                var normalizedInputs = new List<double[]>();
                foreach (var row in rawData.Inputs)
                {
                    normalizedInputs.Add(NormalizationHelper.NormalizeRow(row, means, stdDevs));
                }

                // 5. Parse UI Parameters
                int hiddenNeurons = 25;
                int epochs = 100;
                double learningRate = 0.01d;

                int.TryParse(HiddenNeuronsTextBox.Text, out hiddenNeurons);
                int.TryParse(EpochsTextBox.Text, out epochs);
                double.TryParse(LearningRateTextBox.Text, out learningRate);

                RichTextBoxOutput.AppendText($"Training with {hiddenNeurons} hidden neurons, {epochs} epochs, LR: {learningRate}...\n");

                // 6. Train Model (Using NORMALIZED inputs)
                var trainer = new RbfTrainer();
                RbfNetwork network = await Task.Run(() =>
                {
                    return trainer.Train(normalizedInputs, rawData.Targets, hiddenNeurons, epochs, learningRate);
                });

                RichTextBoxOutput.AppendText("Training Complete!\n");

                // 7. Save Model AND Normalization Stats
                // Serialize the arrays to strings (e.g., "0.5;1.2;-0.3") to store in SQLite.
                string meanStr = NormalizationHelper.SerializeArray(means);
                string stdStr = NormalizationHelper.SerializeArray(stdDevs);

                // Saves everything the FoodClassifier wrapper needs to rebuild itself later.
                ModelRepository.SaveModel("FoodClassifier_V1", network, meanStr, stdStr, userFeatureSchema);

                RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                RichTextBoxOutput.AppendText("SUCCESS: Model and Normalization Stats saved to DB.\n");
                RichTextBoxOutput.AppendText("You can now go to 'Manual Testing' and test with raw inputs.\n");
            }
            catch (Exception ex)
            {
                RichTextBoxOutput.AppendText($"\nCRITICAL ERROR: {ex.Message}\n");
            }
            finally
            {
                TrainButton.Enabled = true;
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
               "Are you sure you want to delete all trained models? This cannot be undone.",
               "Confirm Clear",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                ModelRepository.ClearModel();
                RichTextBoxOutput.AppendText("Database cleared.\n");
            }
        }

        #endregion

    }
}