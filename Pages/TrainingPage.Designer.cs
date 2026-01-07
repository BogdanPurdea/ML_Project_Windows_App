namespace WinForm_RFBN_APP
{
    partial class TrainingPage
    {

        #region Component Designer generated code -----------------------------------

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            InputDocumentTextbox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            FeaturesTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            EpochsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            HiddenNeuronsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            LearningRateTextBox = new MaterialSkin.Controls.MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            TrainButton = new MaterialSkin.Controls.MaterialButton();
            CleanButton = new MaterialSkin.Controls.MaterialButton();
            OutputGroupBox = new GroupBox();
            RichTextBoxOutput = new RichTextBox();
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
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 260F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.Size = new Size(700, 400);
            MainLayout.TabIndex = 0;
            // 
            // ConfigGroupBox
            // 
            ConfigGroupBox.Controls.Add(ConfigLayout);
            ConfigGroupBox.Dock = DockStyle.Fill;
            ConfigGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigGroupBox.Location = new Point(3, 3);
            ConfigGroupBox.Name = "ConfigGroupBox";
            ConfigGroupBox.Size = new Size(694, 254);
            ConfigGroupBox.TabIndex = 0;
            ConfigGroupBox.TabStop = false;
            ConfigGroupBox.Text = "Training Configuration";
            // 
            // ConfigLayout
            // 
            ConfigLayout.ColumnCount = 2;
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ConfigLayout.Controls.Add(materialLabel5, 0, 0);
            ConfigLayout.Controls.Add(InputDocumentTextbox, 1, 0);
            ConfigLayout.Controls.Add(materialLabel4, 0, 1);
            ConfigLayout.Controls.Add(FeaturesTextBox, 1, 1);
            ConfigLayout.Controls.Add(materialLabel1, 0, 2);
            ConfigLayout.Controls.Add(EpochsTextBox, 1, 2);
            ConfigLayout.Controls.Add(materialLabel2, 0, 3);
            ConfigLayout.Controls.Add(HiddenNeuronsTextBox, 1, 3);
            ConfigLayout.Controls.Add(materialLabel3, 0, 4);
            ConfigLayout.Controls.Add(LearningRateTextBox, 1, 4);
            ConfigLayout.Dock = DockStyle.Fill;
            ConfigLayout.Location = new Point(3, 21);
            ConfigLayout.Name = "ConfigLayout";
            ConfigLayout.RowCount = 5;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            ConfigLayout.Size = new Size(688, 230);
            ConfigLayout.TabIndex = 0;
            // 
            // materialLabel5
            // 
            materialLabel5.Anchor = AnchorStyles.Left;
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel5.Location = new Point(3, 13);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(41, 19);
            materialLabel5.TabIndex = 9;
            materialLabel5.Text = "Input:";
            // 
            // InputDocumentTextbox
            // 
            InputDocumentTextbox.AnimateReadOnly = false;
            InputDocumentTextbox.BorderStyle = BorderStyle.None;
            InputDocumentTextbox.Depth = 0;
            InputDocumentTextbox.Dock = DockStyle.Fill;
            InputDocumentTextbox.Font = new Font("Microsoft Sans Serif", 12F);
            InputDocumentTextbox.LeadingIcon = null;
            InputDocumentTextbox.Location = new Point(153, 3);
            InputDocumentTextbox.MaxLength = 50;
            InputDocumentTextbox.MouseState = MaterialSkin.MouseState.OUT;
            InputDocumentTextbox.Multiline = false;
            InputDocumentTextbox.Name = "InputDocumentTextbox";
            InputDocumentTextbox.Size = new Size(532, 50);
            InputDocumentTextbox.TabIndex = 8;
            InputDocumentTextbox.Text = "train_80k.csv";
            InputDocumentTextbox.TrailingIcon = null;
            // 
            // materialLabel4
            // 
            materialLabel4.Anchor = AnchorStyles.Left;
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel4.Location = new Point(3, 58);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(66, 19);
            materialLabel4.TabIndex = 6;
            materialLabel4.Text = "Features:";
            // 
            // FeaturesTextBox
            // 
            FeaturesTextBox.AnimateReadOnly = false;
            FeaturesTextBox.BorderStyle = BorderStyle.None;
            FeaturesTextBox.Depth = 0;
            FeaturesTextBox.Dock = DockStyle.Fill;
            FeaturesTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            FeaturesTextBox.LeadingIcon = null;
            FeaturesTextBox.Location = new Point(153, 48);
            FeaturesTextBox.MaxLength = 50;
            FeaturesTextBox.MouseState = MaterialSkin.MouseState.OUT;
            FeaturesTextBox.Multiline = false;
            FeaturesTextBox.Name = "FeaturesTextBox";
            FeaturesTextBox.Size = new Size(532, 50);
            FeaturesTextBox.TabIndex = 7;
            FeaturesTextBox.Text = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";
            FeaturesTextBox.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            materialLabel1.Anchor = AnchorStyles.Left;
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(3, 103);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(57, 19);
            materialLabel1.TabIndex = 3;
            materialLabel1.Text = "Epochs:";
            // 
            // EpochsTextBox
            // 
            EpochsTextBox.AnimateReadOnly = false;
            EpochsTextBox.BorderStyle = BorderStyle.None;
            EpochsTextBox.Depth = 0;
            EpochsTextBox.Dock = DockStyle.Fill;
            EpochsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EpochsTextBox.LeadingIcon = null;
            EpochsTextBox.Location = new Point(153, 93);
            EpochsTextBox.MaxLength = 50;
            EpochsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EpochsTextBox.Multiline = false;
            EpochsTextBox.Name = "EpochsTextBox";
            EpochsTextBox.Size = new Size(532, 50);
            EpochsTextBox.TabIndex = 0;
            EpochsTextBox.Text = "100";
            EpochsTextBox.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            materialLabel2.Anchor = AnchorStyles.Left;
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(3, 148);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(118, 19);
            materialLabel2.TabIndex = 4;
            materialLabel2.Text = "Hidden Neurons:";
            // 
            // HiddenNeuronsTextBox
            // 
            HiddenNeuronsTextBox.AnimateReadOnly = false;
            HiddenNeuronsTextBox.BorderStyle = BorderStyle.None;
            HiddenNeuronsTextBox.Depth = 0;
            HiddenNeuronsTextBox.Dock = DockStyle.Fill;
            HiddenNeuronsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            HiddenNeuronsTextBox.LeadingIcon = null;
            HiddenNeuronsTextBox.Location = new Point(153, 138);
            HiddenNeuronsTextBox.MaxLength = 50;
            HiddenNeuronsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            HiddenNeuronsTextBox.Multiline = false;
            HiddenNeuronsTextBox.Name = "HiddenNeuronsTextBox";
            HiddenNeuronsTextBox.Size = new Size(532, 50);
            HiddenNeuronsTextBox.TabIndex = 1;
            HiddenNeuronsTextBox.Text = "25";
            HiddenNeuronsTextBox.TrailingIcon = null;
            // 
            // materialLabel3
            // 
            materialLabel3.Anchor = AnchorStyles.Left;
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(3, 195);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(103, 19);
            materialLabel3.TabIndex = 5;
            materialLabel3.Text = "Learning Rate:";
            // 
            // LearningRateTextBox
            // 
            LearningRateTextBox.AnimateReadOnly = false;
            LearningRateTextBox.BorderStyle = BorderStyle.None;
            LearningRateTextBox.Depth = 0;
            LearningRateTextBox.Dock = DockStyle.Fill;
            LearningRateTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            LearningRateTextBox.LeadingIcon = null;
            LearningRateTextBox.Location = new Point(153, 183);
            LearningRateTextBox.MaxLength = 50;
            LearningRateTextBox.MouseState = MaterialSkin.MouseState.OUT;
            LearningRateTextBox.Multiline = false;
            LearningRateTextBox.Name = "LearningRateTextBox";
            LearningRateTextBox.Size = new Size(532, 50);
            LearningRateTextBox.TabIndex = 2;
            LearningRateTextBox.Text = "0.01";
            LearningRateTextBox.TrailingIcon = null;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Fill;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 263);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(694, 64);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(TrainButton);
            ActionsLayout.Controls.Add(CleanButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(688, 40);
            ActionsLayout.TabIndex = 0;
            // 
            // TrainButton
            // 
            TrainButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TrainButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TrainButton.Depth = 0;
            TrainButton.HighEmphasis = true;
            TrainButton.Icon = null;
            TrainButton.Location = new Point(4, 6);
            TrainButton.Margin = new Padding(4, 6, 4, 6);
            TrainButton.MouseState = MaterialSkin.MouseState.HOVER;
            TrainButton.Name = "TrainButton";
            TrainButton.NoAccentTextColor = Color.Empty;
            TrainButton.Size = new Size(64, 36);
            TrainButton.TabIndex = 0;
            TrainButton.Text = "Train";
            TrainButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TrainButton.UseAccentColor = false;
            TrainButton.UseVisualStyleBackColor = true;
            TrainButton.Click += TrainButton_Click;
            // 
            // CleanButton
            // 
            CleanButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CleanButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            CleanButton.Depth = 0;
            CleanButton.HighEmphasis = true;
            CleanButton.Icon = null;
            CleanButton.Location = new Point(76, 6);
            CleanButton.Margin = new Padding(4, 6, 4, 6);
            CleanButton.MouseState = MaterialSkin.MouseState.HOVER;
            CleanButton.Name = "CleanButton";
            CleanButton.NoAccentTextColor = Color.Empty;
            CleanButton.Size = new Size(68, 36);
            CleanButton.TabIndex = 1;
            CleanButton.Text = "Clean";
            CleanButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            CleanButton.UseAccentColor = false;
            CleanButton.UseVisualStyleBackColor = true;
            CleanButton.Click += CleanButton_Click;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(RichTextBoxOutput);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 333);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(694, 64);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Logs";
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Font = new Font("Segoe UI", 9F);
            RichTextBoxOutput.Location = new Point(3, 21);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(688, 40);
            RichTextBoxOutput.TabIndex = 1;
            RichTextBoxOutput.Text = "";
            // 
            // TrainingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "TrainingPage";
            Size = new Size(700, 400);
            MainLayout.ResumeLayout(false);
            ConfigGroupBox.ResumeLayout(false);
            ConfigLayout.ResumeLayout(false);
            ConfigLayout.PerformLayout();
            ActionsGroupBox.ResumeLayout(false);
            ActionsLayout.ResumeLayout(false);
            ActionsLayout.PerformLayout();
            OutputGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private GroupBox ConfigGroupBox;
        private TableLayoutPanel ConfigLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;

        private MaterialSkin.Controls.MaterialButton TrainButton;
        private MaterialSkin.Controls.MaterialButton CleanButton;
        private RichTextBox RichTextBoxOutput;
        
        private MaterialSkin.Controls.MaterialTextBox EpochsTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox LearningRateTextBox;
        private MaterialSkin.Controls.MaterialTextBox HiddenNeuronsTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialTextBox FeaturesTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialTextBox InputDocumentTextbox;
    }
}
