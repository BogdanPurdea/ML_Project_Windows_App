
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
            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            CsvPathTextBox = new MaterialSkin.Controls.MaterialTextBox();
            SchemaTextBox = new MaterialSkin.Controls.MaterialTextBox();
            KFoldsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            StartNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            EndNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            StepTextBox = new MaterialSkin.Controls.MaterialTextBox();
            EpochTextBox = new MaterialSkin.Controls.MaterialTextBox();
            LearningRateTextBox = new MaterialSkin.Controls.MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            RunGridSearchButton = new MaterialSkin.Controls.MaterialButton();
            OutputGroupBox = new GroupBox();
            LogRichTextBox = new RichTextBox();
            MainLayout.SuspendLayout();
            ConfigGroupBox.SuspendLayout();
            ConfigLayout.SuspendLayout();
            ActionsGroupBox.SuspendLayout();
            ActionsLayout.SuspendLayout();
            OutputGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.ColumnCount = 1;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayout.Controls.Add(ConfigGroupBox, 0, 0);
            MainLayout.Controls.Add(ActionsGroupBox, 0, 1);
            MainLayout.Controls.Add(OutputGroupBox, 0, 2);
            MainLayout.Dock = DockStyle.Fill;
            MainLayout.Location = new Point(0, 0);
            MainLayout.Name = "MainLayout";
            MainLayout.RowCount = 3;
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 434F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.Size = new Size(800, 600);
            MainLayout.TabIndex = 0;
            // 
            // ConfigGroupBox
            // 
            ConfigGroupBox.Controls.Add(ConfigLayout);
            ConfigGroupBox.Dock = DockStyle.Top;
            ConfigGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigGroupBox.Location = new Point(3, 3);
            ConfigGroupBox.MinimumSize = new Size(0, 50);
            ConfigGroupBox.Name = "ConfigGroupBox";
            ConfigGroupBox.Size = new Size(794, 428);
            ConfigGroupBox.TabIndex = 0;
            ConfigGroupBox.TabStop = false;
            ConfigGroupBox.Text = "Grid Search Configuration";
            // 
            // ConfigLayout
            // 
            ConfigLayout.ColumnCount = 1;
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            ConfigLayout.Controls.Add(CsvPathTextBox, 1, 0);
            ConfigLayout.Controls.Add(SchemaTextBox, 1, 1);
            ConfigLayout.Controls.Add(KFoldsTextBox, 1, 2);
            ConfigLayout.Controls.Add(StartNeuronTextBox, 1, 3);
            ConfigLayout.Controls.Add(EndNeuronTextBox, 1, 4);
            ConfigLayout.Controls.Add(StepTextBox, 1, 5);
            ConfigLayout.Controls.Add(EpochTextBox, 1, 6);
            ConfigLayout.Controls.Add(LearningRateTextBox, 1, 7);
            ConfigLayout.Dock = DockStyle.Fill;
            ConfigLayout.Location = new Point(3, 21);
            ConfigLayout.MinimumSize = new Size(50, 50);
            ConfigLayout.Name = "ConfigLayout";
            ConfigLayout.RowCount = 8;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.Size = new Size(788, 404);
            ConfigLayout.TabIndex = 0;
            // 
            // CsvPathTextBox
            // 
            CsvPathTextBox.AnimateReadOnly = false;
            CsvPathTextBox.BorderStyle = BorderStyle.None;
            CsvPathTextBox.Depth = 0;
            CsvPathTextBox.Dock = DockStyle.Fill;
            CsvPathTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CsvPathTextBox.Hint = "CSV File Path";
            CsvPathTextBox.LeadingIcon = null;
            CsvPathTextBox.Location = new Point(3, 3);
            CsvPathTextBox.MaxLength = 500;
            CsvPathTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CsvPathTextBox.Multiline = false;
            CsvPathTextBox.Name = "CsvPathTextBox";
            CsvPathTextBox.Size = new Size(782, 50);
            CsvPathTextBox.TabIndex = 0;
            CsvPathTextBox.Text = "train.csv";
            CsvPathTextBox.TrailingIcon = null;
            // 
            // SchemaTextBox
            // 
            SchemaTextBox.AnimateReadOnly = false;
            SchemaTextBox.BorderStyle = BorderStyle.None;
            SchemaTextBox.Depth = 0;
            SchemaTextBox.Dock = DockStyle.Fill;
            SchemaTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SchemaTextBox.Hint = "Schema";
            SchemaTextBox.LeadingIcon = null;
            SchemaTextBox.Location = new Point(3, 53);
            SchemaTextBox.MaxLength = 500;
            SchemaTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SchemaTextBox.Multiline = false;
            SchemaTextBox.Name = "SchemaTextBox";
            SchemaTextBox.Size = new Size(782, 50);
            SchemaTextBox.TabIndex = 1;
            SchemaTextBox.Text = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";
            SchemaTextBox.TrailingIcon = null;
            // 
            // KFoldsTextBox
            // 
            KFoldsTextBox.AnimateReadOnly = false;
            KFoldsTextBox.BorderStyle = BorderStyle.None;
            KFoldsTextBox.Depth = 0;
            KFoldsTextBox.Dock = DockStyle.Fill;
            KFoldsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            KFoldsTextBox.Hint = "K-Folds (e.g. 5)";
            KFoldsTextBox.LeadingIcon = null;
            KFoldsTextBox.Location = new Point(3, 103);
            KFoldsTextBox.MaxLength = 50;
            KFoldsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            KFoldsTextBox.Multiline = false;
            KFoldsTextBox.Name = "KFoldsTextBox";
            KFoldsTextBox.Size = new Size(782, 50);
            KFoldsTextBox.TabIndex = 2;
            KFoldsTextBox.Text = "5";
            KFoldsTextBox.TrailingIcon = null;
            // 
            // StartNeuronTextBox
            // 
            StartNeuronTextBox.AnimateReadOnly = false;
            StartNeuronTextBox.BorderStyle = BorderStyle.None;
            StartNeuronTextBox.Depth = 0;
            StartNeuronTextBox.Dock = DockStyle.Fill;
            StartNeuronTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            StartNeuronTextBox.Hint = "Start Neurons";
            StartNeuronTextBox.LeadingIcon = null;
            StartNeuronTextBox.Location = new Point(3, 153);
            StartNeuronTextBox.MaxLength = 50;
            StartNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            StartNeuronTextBox.Multiline = false;
            StartNeuronTextBox.Name = "StartNeuronTextBox";
            StartNeuronTextBox.Size = new Size(782, 50);
            StartNeuronTextBox.TabIndex = 3;
            StartNeuronTextBox.Text = "10";
            StartNeuronTextBox.TrailingIcon = null;
            // 
            // EndNeuronTextBox
            // 
            EndNeuronTextBox.AnimateReadOnly = false;
            EndNeuronTextBox.BorderStyle = BorderStyle.None;
            EndNeuronTextBox.Depth = 0;
            EndNeuronTextBox.Dock = DockStyle.Fill;
            EndNeuronTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EndNeuronTextBox.Hint = "End Neurons";
            EndNeuronTextBox.LeadingIcon = null;
            EndNeuronTextBox.Location = new Point(3, 203);
            EndNeuronTextBox.MaxLength = 50;
            EndNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EndNeuronTextBox.Multiline = false;
            EndNeuronTextBox.Name = "EndNeuronTextBox";
            EndNeuronTextBox.Size = new Size(782, 50);
            EndNeuronTextBox.TabIndex = 4;
            EndNeuronTextBox.Text = "50";
            EndNeuronTextBox.TrailingIcon = null;
            // 
            // StepTextBox
            // 
            StepTextBox.AnimateReadOnly = false;
            StepTextBox.BorderStyle = BorderStyle.None;
            StepTextBox.Depth = 0;
            StepTextBox.Dock = DockStyle.Fill;
            StepTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            StepTextBox.Hint = "Step";
            StepTextBox.LeadingIcon = null;
            StepTextBox.Location = new Point(3, 253);
            StepTextBox.MaxLength = 50;
            StepTextBox.MouseState = MaterialSkin.MouseState.OUT;
            StepTextBox.Multiline = false;
            StepTextBox.Name = "StepTextBox";
            StepTextBox.Size = new Size(782, 50);
            StepTextBox.TabIndex = 5;
            StepTextBox.Text = "10";
            StepTextBox.TrailingIcon = null;
            // 
            // EpochTextBox
            // 
            EpochTextBox.AnimateReadOnly = false;
            EpochTextBox.BorderStyle = BorderStyle.None;
            EpochTextBox.Depth = 0;
            EpochTextBox.Dock = DockStyle.Fill;
            EpochTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EpochTextBox.Hint = "Epoch";
            EpochTextBox.LeadingIcon = null;
            EpochTextBox.Location = new Point(3, 303);
            EpochTextBox.MaxLength = 50;
            EpochTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EpochTextBox.Multiline = false;
            EpochTextBox.Name = "EpochTextBox";
            EpochTextBox.Size = new Size(782, 50);
            EpochTextBox.TabIndex = 6;
            EpochTextBox.Text = "20";
            EpochTextBox.TrailingIcon = null;
            // 
            // LearningRateTextBox
            // 
            LearningRateTextBox.AnimateReadOnly = false;
            LearningRateTextBox.BorderStyle = BorderStyle.None;
            LearningRateTextBox.Depth = 0;
            LearningRateTextBox.Dock = DockStyle.Fill;
            LearningRateTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            LearningRateTextBox.Hint = "Learning Rate (e.g. 0.01)";
            LearningRateTextBox.LeadingIcon = null;
            LearningRateTextBox.Location = new Point(3, 353);
            LearningRateTextBox.MaxLength = 50;
            LearningRateTextBox.MouseState = MaterialSkin.MouseState.OUT;
            LearningRateTextBox.Multiline = false;
            LearningRateTextBox.Name = "LearningRateTextBox";
            LearningRateTextBox.Size = new Size(782, 50);
            LearningRateTextBox.TabIndex = 7;
            LearningRateTextBox.Text = "0.01";
            LearningRateTextBox.TrailingIcon = null;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Top;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 437);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(794, 70);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(RunGridSearchButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(788, 46);
            ActionsLayout.TabIndex = 0;
            // 
            // RunGridSearchButton
            // 
            RunGridSearchButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            RunGridSearchButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            RunGridSearchButton.Depth = 0;
            RunGridSearchButton.HighEmphasis = true;
            RunGridSearchButton.Icon = null;
            RunGridSearchButton.Location = new Point(4, 6);
            RunGridSearchButton.Margin = new Padding(4, 6, 4, 6);
            RunGridSearchButton.MouseState = MaterialSkin.MouseState.HOVER;
            RunGridSearchButton.Name = "RunGridSearchButton";
            RunGridSearchButton.NoAccentTextColor = Color.Empty;
            RunGridSearchButton.Size = new Size(147, 36);
            RunGridSearchButton.TabIndex = 0;
            RunGridSearchButton.Text = "Run Grid Search";
            RunGridSearchButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            RunGridSearchButton.UseAccentColor = false;
            RunGridSearchButton.UseVisualStyleBackColor = true;
            RunGridSearchButton.Click += RunGridSearchButton_Click;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(LogRichTextBox);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 513);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(794, 84);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Logs";
            // 
            // LogRichTextBox
            // 
            LogRichTextBox.Dock = DockStyle.Fill;
            LogRichTextBox.Font = new Font("Segoe UI", 9F);
            LogRichTextBox.Location = new Point(3, 21);
            LogRichTextBox.Name = "LogRichTextBox";
            LogRichTextBox.ReadOnly = true;
            LogRichTextBox.Size = new Size(788, 60);
            LogRichTextBox.TabIndex = 0;
            LogRichTextBox.Text = "Grid Search Results will appear here...";
            // 
            // CrossValidationPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "CrossValidationPage";
            Size = new Size(800, 600);
            MainLayout.ResumeLayout(false);
            ConfigGroupBox.ResumeLayout(false);
            ConfigLayout.ResumeLayout(false);
            ActionsGroupBox.ResumeLayout(false);
            ActionsLayout.ResumeLayout(false);
            ActionsLayout.PerformLayout();
            OutputGroupBox.ResumeLayout(false);
            ResumeLayout(false);

        }

        private TableLayoutPanel MainLayout;
        private GroupBox ConfigGroupBox;
        private TableLayoutPanel ConfigLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;
        
        private RichTextBox LogRichTextBox;
        private MaterialSkin.Controls.MaterialTextBox CsvPathTextBox;
        private MaterialSkin.Controls.MaterialTextBox SchemaTextBox;
        private MaterialSkin.Controls.MaterialTextBox KFoldsTextBox;
        private MaterialSkin.Controls.MaterialTextBox StartNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox EndNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox StepTextBox;
        private MaterialSkin.Controls.MaterialTextBox EpochTextBox;
        private MaterialSkin.Controls.MaterialTextBox LearningRateTextBox;
        private MaterialSkin.Controls.MaterialButton RunGridSearchButton;
    }
}
