namespace WinForm_RFBN_APP
{
    partial class TestingPage
    {

        #region Component Designer generated code -----------------------------------

        ///<summary> 
        /// Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;

        ///<summary> 
        /// Clean up any resources being used.
        ///</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        ///<summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            InputDocumentTextbox = new MaterialSkin.Controls.MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            TestButton = new MaterialSkin.Controls.MaterialButton();
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
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
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
            ConfigGroupBox.Size = new Size(694, 74);
            ConfigGroupBox.TabIndex = 0;
            ConfigGroupBox.TabStop = false;
            ConfigGroupBox.Text = "Testing Configuration";
            // 
            // ConfigLayout
            // 
            ConfigLayout.ColumnCount = 2;
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ConfigLayout.Controls.Add(materialLabel5, 0, 0);
            ConfigLayout.Controls.Add(InputDocumentTextbox, 1, 0);
            ConfigLayout.Dock = DockStyle.Fill;
            ConfigLayout.Location = new Point(3, 21);
            ConfigLayout.Name = "ConfigLayout";
            ConfigLayout.RowCount = 1;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ConfigLayout.Size = new Size(688, 50);
            ConfigLayout.TabIndex = 0;
            // 
            // materialLabel5
            // 
            materialLabel5.Anchor = AnchorStyles.Left;
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel5.Location = new Point(3, 15);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(41, 19);
            materialLabel5.TabIndex = 0;
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
            InputDocumentTextbox.Location = new Point(103, 3);
            InputDocumentTextbox.MaxLength = 50;
            InputDocumentTextbox.MouseState = MaterialSkin.MouseState.OUT;
            InputDocumentTextbox.Multiline = false;
            InputDocumentTextbox.Name = "InputDocumentTextbox";
            InputDocumentTextbox.Size = new Size(582, 50);
            InputDocumentTextbox.TabIndex = 1;
            InputDocumentTextbox.Text = "test.csv";
            InputDocumentTextbox.TrailingIcon = null;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Fill;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 83);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(694, 64);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(TestButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(688, 40);
            ActionsLayout.TabIndex = 0;
            // 
            // TestButton
            // 
            TestButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestButton.Depth = 0;
            TestButton.HighEmphasis = true;
            TestButton.Icon = null;
            TestButton.Location = new Point(4, 6);
            TestButton.Margin = new Padding(4, 6, 4, 6);
            TestButton.MouseState = MaterialSkin.MouseState.HOVER;
            TestButton.Name = "TestButton";
            TestButton.NoAccentTextColor = Color.Empty;
            TestButton.Size = new Size(64, 36);
            TestButton.TabIndex = 0;
            TestButton.Text = "Test";
            TestButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TestButton.UseAccentColor = false;
            TestButton.UseVisualStyleBackColor = true;
            TestButton.Click += TestButton_Click;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(RichTextBoxOutput);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 153);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(694, 244);
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
            RichTextBoxOutput.Size = new Size(688, 220);
            RichTextBoxOutput.TabIndex = 0;
            RichTextBoxOutput.Text = "";
            // 
            // TestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "TestingPage";
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

        private MaterialSkin.Controls.MaterialButton TestButton;
        private RichTextBox RichTextBoxOutput;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialTextBox InputDocumentTextbox;
    }
}
