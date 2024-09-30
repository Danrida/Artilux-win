namespace ArtiluxEOL.Models
{
    public partial class SpectroscopeTestWindow
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.someChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.someChart)).BeginInit();
            this.SuspendLayout();
            // 
            // someChart
            // 
            chartArea2.Name = "ChartArea1";
            this.someChart.ChartAreas.Add(chartArea2);
            this.someChart.Location = new System.Drawing.Point(12, 51);
            this.someChart.Name = "someChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Name = "Series2";
            this.someChart.Series.Add(series3);
            this.someChart.Series.Add(series4);
            this.someChart.Size = new System.Drawing.Size(1576, 537);
            this.someChart.TabIndex = 14;
            this.someChart.Text = "Chart";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(1565, 12);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(23, 23);
            this.Exit.TabIndex = 15;
            this.Exit.Text = "X";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // SpectroscopeTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 600);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.someChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SpectroscopeTestWindow";
            this.Text = "SpectroscopeTestWindow";
            ((System.ComponentModel.ISupportInitialize)(this.someChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart someChart;
        private System.Windows.Forms.Button Exit;
    }
}