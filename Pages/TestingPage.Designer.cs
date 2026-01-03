namespace WinForm_RFBN_APP
{
    partial class TestingPage
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
            ButtonsPannel = new Panel();
            TestButton = new MaterialSkin.Controls.MaterialButton();
            RichTextBoxOutput = new RichTextBox();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            InputDocumentTextbox = new MaterialSkin.Controls.MaterialTextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            ButtonsPannel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonsPannel
            // 
            ButtonsPannel.Controls.Add(TestButton);
            ButtonsPannel.Dock = DockStyle.Left;
            ButtonsPannel.Location = new Point(0, 0);
            ButtonsPannel.Name = "ButtonsPannel";
            ButtonsPannel.Size = new Size(100, 400);
            ButtonsPannel.TabIndex = 1;
            // 
            // TestButton
            // 
            TestButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestButton.Depth = 0;
            TestButton.HighEmphasis = true;
            TestButton.Icon = null;
            TestButton.Location = new Point(16, 16);
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
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Location = new Point(0, 0);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(600, 322);
            RichTextBoxOutput.TabIndex = 3;
            RichTextBoxOutput.Text = "";
            // 
            // materialLabel5
            // 
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel5.Location = new Point(16, 28);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(41, 19);
            materialLabel5.TabIndex = 11;
            materialLabel5.Text = "Input:";
            // 
            // InputDocumentTextbox
            // 
            InputDocumentTextbox.AnimateReadOnly = false;
            InputDocumentTextbox.BorderStyle = BorderStyle.None;
            InputDocumentTextbox.Depth = 0;
            InputDocumentTextbox.Font = new Font("Microsoft Sans Serif", 12F);
            InputDocumentTextbox.LeadingIcon = null;
            InputDocumentTextbox.Location = new Point(63, 13);
            InputDocumentTextbox.MaxLength = 50;
            InputDocumentTextbox.MouseState = MaterialSkin.MouseState.OUT;
            InputDocumentTextbox.Multiline = false;
            InputDocumentTextbox.Name = "InputDocumentTextbox";
            InputDocumentTextbox.Size = new Size(524, 50);
            InputDocumentTextbox.TabIndex = 10;
            InputDocumentTextbox.Text = "test_20k.csv";
            InputDocumentTextbox.TrailingIcon = null;
            // 
            // panel1
            // 
            panel1.Controls.Add(InputDocumentTextbox);
            panel1.Controls.Add(materialLabel5);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(100, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 78);
            panel1.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.Controls.Add(RichTextBoxOutput);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(100, 78);
            panel2.Name = "panel2";
            panel2.Size = new Size(600, 322);
            panel2.TabIndex = 13;
            // 
            // TestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(ButtonsPannel);
            Name = "TestingPage";
            Size = new Size(700, 400);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel ButtonsPannel;
        private MaterialSkin.Controls.MaterialButton TestButton;
        private RichTextBox RichTextBoxOutput;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialTextBox InputDocumentTextbox;
        private Panel panel1;
        private Panel panel2;
    }
}
