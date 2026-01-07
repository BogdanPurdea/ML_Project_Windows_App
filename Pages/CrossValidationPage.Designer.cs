
using System.Drawing;
using System.Windows.Forms;

namespace WinForm_RFBN_APP
{
    partial class CrossValidationPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ConfigPanel = new Panel();
            EpochTextBox = new MaterialSkin.Controls.MaterialTextBox();
            CsvPathTextBox = new MaterialSkin.Controls.MaterialTextBox();
            SchemaTextBox = new MaterialSkin.Controls.MaterialTextBox();
            LearningRateTextBox = new MaterialSkin.Controls.MaterialTextBox();
            StepTextBox = new MaterialSkin.Controls.MaterialTextBox();
            EndNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            StartNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            KFoldsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            RunGridSearchButton = new MaterialSkin.Controls.MaterialButton();
            ConfigLabel = new MaterialSkin.Controls.MaterialLabel();
            LogRichTextBox = new RichTextBox();
            ConfigPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ConfigPanel
            // 
            ConfigPanel.Controls.Add(EpochTextBox);
            ConfigPanel.Controls.Add(CsvPathTextBox);
            ConfigPanel.Controls.Add(SchemaTextBox);
            ConfigPanel.Controls.Add(LearningRateTextBox);
            ConfigPanel.Controls.Add(StepTextBox);
            ConfigPanel.Controls.Add(EndNeuronTextBox);
            ConfigPanel.Controls.Add(StartNeuronTextBox);
            ConfigPanel.Controls.Add(KFoldsTextBox);
            ConfigPanel.Controls.Add(RunGridSearchButton);
            ConfigPanel.Controls.Add(ConfigLabel);
            ConfigPanel.Dock = DockStyle.Top;
            ConfigPanel.Location = new Point(0, 0);
            ConfigPanel.Name = "ConfigPanel";
            ConfigPanel.Size = new Size(800, 260);
            ConfigPanel.TabIndex = 0;
            // 
            // EpochTextBox
            // 
            EpochTextBox.AnimateReadOnly = false;
            EpochTextBox.BorderStyle = BorderStyle.None;
            EpochTextBox.Depth = 0;
            EpochTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EpochTextBox.Hint = "Epoch";
            EpochTextBox.LeadingIcon = null;
            EpochTextBox.Location = new Point(446, 160);
            EpochTextBox.MaxLength = 50;
            EpochTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EpochTextBox.Multiline = false;
            EpochTextBox.Name = "EpochTextBox";
            EpochTextBox.Size = new Size(100, 50);
            EpochTextBox.TabIndex = 9;
            EpochTextBox.Text = "100";
            EpochTextBox.TrailingIcon = null;
            // 
            // CsvPathTextBox
            // 
            CsvPathTextBox.AnimateReadOnly = false;
            CsvPathTextBox.BorderStyle = BorderStyle.None;
            CsvPathTextBox.Depth = 0;
            CsvPathTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CsvPathTextBox.Hint = "CSV File Path (e.g. train_80k.csv)";
            CsvPathTextBox.LeadingIcon = null;
            CsvPathTextBox.Location = new Point(10, 40);
            CsvPathTextBox.MaxLength = 500;
            CsvPathTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CsvPathTextBox.Multiline = false;
            CsvPathTextBox.Name = "CsvPathTextBox";
            CsvPathTextBox.Size = new Size(780, 50);
            CsvPathTextBox.TabIndex = 1;
            CsvPathTextBox.Text = "train_80k.csv";
            CsvPathTextBox.TrailingIcon = null;
            // 
            // SchemaTextBox
            // 
            SchemaTextBox.AnimateReadOnly = false;
            SchemaTextBox.BorderStyle = BorderStyle.None;
            SchemaTextBox.Depth = 0;
            SchemaTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SchemaTextBox.Hint = "Schema (Features;Target)";
            SchemaTextBox.LeadingIcon = null;
            SchemaTextBox.Location = new Point(10, 100);
            SchemaTextBox.MaxLength = 500;
            SchemaTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SchemaTextBox.Multiline = false;
            SchemaTextBox.Name = "SchemaTextBox";
            SchemaTextBox.Size = new Size(780, 50);
            SchemaTextBox.TabIndex = 2;
            SchemaTextBox.Text = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";
            SchemaTextBox.TrailingIcon = null;
            // 
            // LearningRateTextBox
            // 
            LearningRateTextBox.AnimateReadOnly = false;
            LearningRateTextBox.BorderStyle = BorderStyle.None;
            LearningRateTextBox.Depth = 0;
            LearningRateTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            LearningRateTextBox.Hint = "Learning Rate (e.g. 0.01)";
            LearningRateTextBox.LeadingIcon = null;
            LearningRateTextBox.Location = new Point(552, 160);
            LearningRateTextBox.MaxLength = 50;
            LearningRateTextBox.MouseState = MaterialSkin.MouseState.OUT;
            LearningRateTextBox.Multiline = false;
            LearningRateTextBox.Name = "LearningRateTextBox";
            LearningRateTextBox.Size = new Size(155, 50);
            LearningRateTextBox.TabIndex = 7;
            LearningRateTextBox.Text = "0.01";
            LearningRateTextBox.TrailingIcon = null;
            // 
            // StepTextBox
            // 
            StepTextBox.AnimateReadOnly = false;
            StepTextBox.BorderStyle = BorderStyle.None;
            StepTextBox.Depth = 0;
            StepTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            StepTextBox.Hint = "Step";
            StepTextBox.LeadingIcon = null;
            StepTextBox.Location = new Point(340, 160);
            StepTextBox.MaxLength = 50;
            StepTextBox.MouseState = MaterialSkin.MouseState.OUT;
            StepTextBox.Multiline = false;
            StepTextBox.Name = "StepTextBox";
            StepTextBox.Size = new Size(100, 50);
            StepTextBox.TabIndex = 6;
            StepTextBox.Text = "5";
            StepTextBox.TrailingIcon = null;
            // 
            // EndNeuronTextBox
            // 
            EndNeuronTextBox.AnimateReadOnly = false;
            EndNeuronTextBox.BorderStyle = BorderStyle.None;
            EndNeuronTextBox.Depth = 0;
            EndNeuronTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EndNeuronTextBox.Hint = "End Neurons";
            EndNeuronTextBox.LeadingIcon = null;
            EndNeuronTextBox.Location = new Point(230, 160);
            EndNeuronTextBox.MaxLength = 50;
            EndNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EndNeuronTextBox.Multiline = false;
            EndNeuronTextBox.Name = "EndNeuronTextBox";
            EndNeuronTextBox.Size = new Size(100, 50);
            EndNeuronTextBox.TabIndex = 5;
            EndNeuronTextBox.Text = "50";
            EndNeuronTextBox.TrailingIcon = null;
            // 
            // StartNeuronTextBox
            // 
            StartNeuronTextBox.AnimateReadOnly = false;
            StartNeuronTextBox.BorderStyle = BorderStyle.None;
            StartNeuronTextBox.Depth = 0;
            StartNeuronTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            StartNeuronTextBox.Hint = "Start Neurons";
            StartNeuronTextBox.LeadingIcon = null;
            StartNeuronTextBox.Location = new Point(120, 160);
            StartNeuronTextBox.MaxLength = 50;
            StartNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            StartNeuronTextBox.Multiline = false;
            StartNeuronTextBox.Name = "StartNeuronTextBox";
            StartNeuronTextBox.Size = new Size(100, 50);
            StartNeuronTextBox.TabIndex = 4;
            StartNeuronTextBox.Text = "5";
            StartNeuronTextBox.TrailingIcon = null;
            // 
            // KFoldsTextBox
            // 
            KFoldsTextBox.AnimateReadOnly = false;
            KFoldsTextBox.BorderStyle = BorderStyle.None;
            KFoldsTextBox.Depth = 0;
            KFoldsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            KFoldsTextBox.Hint = "K-Folds (e.g. 5)";
            KFoldsTextBox.LeadingIcon = null;
            KFoldsTextBox.Location = new Point(10, 160);
            KFoldsTextBox.MaxLength = 50;
            KFoldsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            KFoldsTextBox.Multiline = false;
            KFoldsTextBox.Name = "KFoldsTextBox";
            KFoldsTextBox.Size = new Size(100, 50);
            KFoldsTextBox.TabIndex = 3;
            KFoldsTextBox.Text = "5";
            KFoldsTextBox.TrailingIcon = null;
            // 
            // RunGridSearchButton
            // 
            RunGridSearchButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            RunGridSearchButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            RunGridSearchButton.Depth = 0;
            RunGridSearchButton.HighEmphasis = true;
            RunGridSearchButton.Icon = null;
            RunGridSearchButton.Location = new Point(10, 220);
            RunGridSearchButton.Margin = new Padding(4, 6, 4, 6);
            RunGridSearchButton.MouseState = MaterialSkin.MouseState.HOVER;
            RunGridSearchButton.Name = "RunGridSearchButton";
            RunGridSearchButton.NoAccentTextColor = Color.Empty;
            RunGridSearchButton.Size = new Size(147, 36);
            RunGridSearchButton.TabIndex = 8;
            RunGridSearchButton.Text = "Run Grid Search";
            RunGridSearchButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            RunGridSearchButton.UseAccentColor = false;
            RunGridSearchButton.UseVisualStyleBackColor = true;
            RunGridSearchButton.Click += RunGridSearchButton_Click;
            // 
            // ConfigLabel
            // 
            ConfigLabel.AutoSize = true;
            ConfigLabel.Depth = 0;
            ConfigLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            ConfigLabel.Location = new Point(10, 10);
            ConfigLabel.MouseState = MaterialSkin.MouseState.HOVER;
            ConfigLabel.Name = "ConfigLabel";
            ConfigLabel.Size = new Size(98, 19);
            ConfigLabel.TabIndex = 0;
            ConfigLabel.Text = "Configuration";
            // 
            // LogRichTextBox
            // 
            LogRichTextBox.Dock = DockStyle.Fill;
            LogRichTextBox.Location = new Point(0, 260);
            LogRichTextBox.Name = "LogRichTextBox";
            LogRichTextBox.ReadOnly = true;
            LogRichTextBox.Size = new Size(800, 340);
            LogRichTextBox.TabIndex = 1;
            LogRichTextBox.Text = "Grid Search Results will appear here...";
            // 
            // CrossValidationPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LogRichTextBox);
            Controls.Add(ConfigPanel);
            Name = "CrossValidationPage";
            Size = new Size(800, 600);
            ConfigPanel.ResumeLayout(false);
            ConfigPanel.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel ConfigPanel;
        private MaterialSkin.Controls.MaterialTextBox KFoldsTextBox;
        private MaterialSkin.Controls.MaterialTextBox StartNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox EndNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox StepTextBox;
        private MaterialSkin.Controls.MaterialTextBox LearningRateTextBox;
        private MaterialSkin.Controls.MaterialButton RunGridSearchButton;
        private MaterialSkin.Controls.MaterialLabel ConfigLabel;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private MaterialSkin.Controls.MaterialTextBox CsvPathTextBox;
        private MaterialSkin.Controls.MaterialTextBox SchemaTextBox;
        private MaterialSkin.Controls.MaterialTextBox EpochTextBox;
    }
}
