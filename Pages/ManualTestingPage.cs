using Source;
using Source.Data;
using System.Data;
using System.Globalization;

namespace WinForm_RFBN_APP
{
    public partial class ManualTestingPage : UserControl
    {

        #region Constructor ---------------------------------------------------------

        public ManualTestingPage()
        {
            InitializeComponent();
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        private void TestFullButton_Click(object sender, EventArgs e)
        {
            string inputRaw = FullInputTextBox.Text.Trim();
            RunPrediction(inputRaw);
        }

        private void TestCustomButton_Click(object sender, EventArgs e)
        {
            // Helper to safely parse text boxes to "0" if empty
            string GetVal(string text) => string.IsNullOrWhiteSpace(text) ? "0" : text.Trim();

            string p = GetVal(ProteinTextBox.Text);
            string tf = GetVal(TotalFatTextBox.Text);
            string c = GetVal(CarbohydratesTextBox.Text);
            string en = GetVal(KiloCaloriesTextBox.Text);
            string fib = GetVal(FiberTextBox.Text);
            string sf = GetVal(SaturatedFatTextBox.Text);
            string s = GetVal(SugarTextBox.Text);

            string inputRaw = $"{p};{tf};{c};{en};{fib};{sf};{s}";
            RunPrediction(inputRaw);
        }

        #endregion


        #region Logics --------------------------------------------------------------

        private void RunPrediction(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return;

            // 1. Load the Wrapper
            // Note: In a real app, you might cache this field so you don't reload DB on every click.
            FoodClassifier classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");

            if (classifier == null)
            {
                RichTextBoxOutput.AppendText("Model not found. Please train the model first.\r\n");
                return;
            }

            // 2. Parse User Input (Raw numbers)
            double[] rawInput;
            try
            {
                rawInput = inputString.Split(';')
                    .Select(val => double.Parse(val.Trim(), CultureInfo.InvariantCulture))
                    .ToArray();

                if (rawInput.Length != 7)
                {
                    MessageBox.Show("Expected 7 values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Predict
            // The classifier handles normalization internally now!
            double score = classifier.Predict(rawInput);

            string label = score >= 0.5 ? "HEALTHY (1)" : "UNHEALTHY (0)";

            // 4. Output
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText($"Input: {inputString}\n");
            RichTextBoxOutput.AppendText($"Score: {score:F4}\n");
            RichTextBoxOutput.AppendText($"Prediction: {label}\n");
        }

        #endregion

    }
}
