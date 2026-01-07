using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinForm_RFBN_APP
{
    partial class ShapPage
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.InputPanel = new System.Windows.Forms.Panel();
            this.ShapChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ExplainButton = new MaterialSkin.Controls.MaterialButton();

            // Initialize Controls
            this.CsvPathTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.SchemaTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.ProteinBox = new MaterialSkin.Controls.MaterialTextBox();
            this.FatBox = new MaterialSkin.Controls.MaterialTextBox();
            this.CarbBox = new MaterialSkin.Controls.MaterialTextBox();
            this.KcalBox = new MaterialSkin.Controls.MaterialTextBox();
            this.FiberBox = new MaterialSkin.Controls.MaterialTextBox();
            this.SatFatBox = new MaterialSkin.Controls.MaterialTextBox();
            this.SugarBox = new MaterialSkin.Controls.MaterialTextBox();

            this.InputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShapChart)).BeginInit();
            this.SuspendLayout();

            // 
            // InputPanel
            // 
            this.InputPanel.Controls.Add(this.SugarBox);
            this.InputPanel.Controls.Add(this.SatFatBox);
            this.InputPanel.Controls.Add(this.FiberBox);
            this.InputPanel.Controls.Add(this.KcalBox);
            this.InputPanel.Controls.Add(this.CarbBox);
            this.InputPanel.Controls.Add(this.FatBox);
            this.InputPanel.Controls.Add(this.ProteinBox);
            this.InputPanel.Controls.Add(this.SchemaTextBox);
            this.InputPanel.Controls.Add(this.CsvPathTextBox);
            this.InputPanel.Controls.Add(this.ExplainButton);
            this.InputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.InputPanel.Location = new System.Drawing.Point(0, 0);
            this.InputPanel.Name = "InputPanel";
            this.InputPanel.Size = new System.Drawing.Size(250, 750);
            this.InputPanel.TabIndex = 0;

            // 
            // CsvPathTextBox
            // 
            this.CsvPathTextBox.Hint = "CSV Path";
            this.CsvPathTextBox.Location = new System.Drawing.Point(10, 10);
            this.CsvPathTextBox.Size = new System.Drawing.Size(220, 50);
            this.CsvPathTextBox.Text = "train_80k.csv";

            // 
            // SchemaTextBox
            // 
            this.SchemaTextBox.Hint = "Schema";
            this.SchemaTextBox.Location = new System.Drawing.Point(10, 70);
            this.SchemaTextBox.Size = new System.Drawing.Size(220, 50);
            this.SchemaTextBox.Text = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";

            // 
            // ProteinBox
            // 
            this.ProteinBox.Hint = "Protein";
            this.ProteinBox.Location = new System.Drawing.Point(10, 130);
            this.ProteinBox.Size = new System.Drawing.Size(220, 50);

            // 
            // FatBox
            // 
            this.FatBox.Hint = "Total Fat";
            this.FatBox.Location = new System.Drawing.Point(10, 190);
            this.FatBox.Size = new System.Drawing.Size(220, 50);

            // 
            // CarbBox
            // 
            this.CarbBox.Hint = "Carbohydrate";
            this.CarbBox.Location = new System.Drawing.Point(10, 250);
            this.CarbBox.Size = new System.Drawing.Size(220, 50);

            // 
            // KcalBox
            // 
            this.KcalBox.Hint = "Energy (Kcal)";
            this.KcalBox.Location = new System.Drawing.Point(10, 310);
            this.KcalBox.Size = new System.Drawing.Size(220, 50);

            // 
            // FiberBox
            // 
            this.FiberBox.Hint = "Fiber";
            this.FiberBox.Location = new System.Drawing.Point(10, 370);
            this.FiberBox.Size = new System.Drawing.Size(220, 50);

            // 
            // SatFatBox
            // 
            this.SatFatBox.Hint = "Saturated Fat";
            this.SatFatBox.Location = new System.Drawing.Point(10, 430);
            this.SatFatBox.Size = new System.Drawing.Size(220, 50);

            // 
            // SugarBox
            // 
            this.SugarBox.Hint = "Sugar";
            this.SugarBox.Location = new System.Drawing.Point(10, 490);
            this.SugarBox.Size = new System.Drawing.Size(220, 50);

            // 
            // ExplainButton
            // 
            this.ExplainButton.Text = "Explain Prediction";
            this.ExplainButton.Location = new System.Drawing.Point(10, 560);
            this.ExplainButton.Size = new System.Drawing.Size(220, 36);
            this.ExplainButton.Click += new System.EventHandler(this.ExplainButton_Click);

            // 
            // ShapChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ShapChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Enabled = false;
            this.ShapChart.Legends.Add(legend1);
            this.ShapChart.Location = new System.Drawing.Point(260, 20);
            this.ShapChart.Name = "ShapChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = SeriesChartType.Bar;
            series1.Name = "ShapValues";
            this.ShapChart.Series.Add(series1);
            this.ShapChart.Size = new System.Drawing.Size(500, 500);
            this.ShapChart.TabIndex = 1;
            this.ShapChart.Text = "SHAP Values";
            this.ShapChart.Dock = DockStyle.Fill;

            // 
            // ShapPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ShapChart);
            this.Controls.Add(this.InputPanel);
            this.Name = "ShapPage";
            this.Size = new System.Drawing.Size(800, 750);
            this.InputPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShapChart)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel InputPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ShapChart;
        private MaterialSkin.Controls.MaterialButton ExplainButton;
        private MaterialSkin.Controls.MaterialTextBox CsvPathTextBox;
        private MaterialSkin.Controls.MaterialTextBox SchemaTextBox;
        private MaterialSkin.Controls.MaterialTextBox ProteinBox;
        private MaterialSkin.Controls.MaterialTextBox FatBox;
        private MaterialSkin.Controls.MaterialTextBox CarbBox;
        private MaterialSkin.Controls.MaterialTextBox KcalBox;
        private MaterialSkin.Controls.MaterialTextBox FiberBox;
        private MaterialSkin.Controls.MaterialTextBox SatFatBox;
        private MaterialSkin.Controls.MaterialTextBox SugarBox;
    }
}