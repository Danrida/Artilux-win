namespace ArtiluxEOL
{
    public partial class OscilloscopeTestWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Exit_Oscilloscope = new System.Windows.Forms.Button();
            this.chart_Oscilloscope = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_Channel = new System.Windows.Forms.ComboBox();
            this.comboBox_Voltage = new System.Windows.Forms.ComboBox();
            this.Pico_Cancel = new System.Windows.Forms.Button();
            this.button_Time_Block = new System.Windows.Forms.Button();
            this.button_Trigger_Block = new System.Windows.Forms.Button();
            this.label_Time = new System.Windows.Forms.Label();
            this.label_Measuring1 = new System.Windows.Forms.Label();
            this.label_Measuring2 = new System.Windows.Forms.Label();
            this.textBox_TimeBase = new System.Windows.Forms.TextBox();
            this.textBox_Treashold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oscilloscope)).BeginInit();
            this.SuspendLayout();
            // 
            // Exit_Oscilloscope
            // 
            this.Exit_Oscilloscope.Location = new System.Drawing.Point(1565, 12);
            this.Exit_Oscilloscope.Name = "Exit_Oscilloscope";
            this.Exit_Oscilloscope.Size = new System.Drawing.Size(23, 23);
            this.Exit_Oscilloscope.TabIndex = 16;
            this.Exit_Oscilloscope.Text = "X";
            this.Exit_Oscilloscope.UseVisualStyleBackColor = true;
            this.Exit_Oscilloscope.Click += new System.EventHandler(this.Exit_Oscilloscope_Click);
            // 
            // chart_Oscilloscope
            // 
            chartArea11.Name = "ChartArea1";
            this.chart_Oscilloscope.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chart_Oscilloscope.Legends.Add(legend11);
            this.chart_Oscilloscope.Location = new System.Drawing.Point(12, 42);
            this.chart_Oscilloscope.Name = "chart_Oscilloscope";
            series21.ChartArea = "ChartArea1";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series21.Legend = "Legend1";
            series21.Name = "Channel A";
            series22.ChartArea = "ChartArea1";
            series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series22.Legend = "Legend1";
            series22.Name = "Channel B";
            this.chart_Oscilloscope.Series.Add(series21);
            this.chart_Oscilloscope.Series.Add(series22);
            this.chart_Oscilloscope.Size = new System.Drawing.Size(1576, 619);
            this.chart_Oscilloscope.TabIndex = 17;
            this.chart_Oscilloscope.Text = "chart";
            // 
            // comboBox_Channel
            // 
            this.comboBox_Channel.FormattingEnabled = true;
            this.comboBox_Channel.Items.AddRange(new object[] {
            "Channel A",
            "Channel B"});
            this.comboBox_Channel.Location = new System.Drawing.Point(12, 667);
            this.comboBox_Channel.Name = "comboBox_Channel";
            this.comboBox_Channel.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Channel.TabIndex = 18;
            this.comboBox_Channel.SelectedIndexChanged += new System.EventHandler(this.comboBox_Channel_SelectedIndexChanged);
            // 
            // comboBox_Voltage
            // 
            this.comboBox_Voltage.FormattingEnabled = true;
            this.comboBox_Voltage.Items.AddRange(new object[] {
            "10 mV",
            "20 mV",
            "50 mV",
            "100 mV",
            "200 mV",
            "500 mV",
            "1 V",
            "2 V",
            "5 V",
            "10 V",
            "20 V"});
            this.comboBox_Voltage.Location = new System.Drawing.Point(139, 667);
            this.comboBox_Voltage.Name = "comboBox_Voltage";
            this.comboBox_Voltage.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Voltage.TabIndex = 19;
            this.comboBox_Voltage.SelectedIndexChanged += new System.EventHandler(this.comboBox_Voltage_SelectedIndexChanged);
            // 
            // Pico_Cancel
            // 
            this.Pico_Cancel.Location = new System.Drawing.Point(1492, 667);
            this.Pico_Cancel.Name = "Pico_Cancel";
            this.Pico_Cancel.Size = new System.Drawing.Size(96, 21);
            this.Pico_Cancel.TabIndex = 22;
            this.Pico_Cancel.Text = "Cancel";
            this.Pico_Cancel.UseVisualStyleBackColor = true;
            this.Pico_Cancel.Click += new System.EventHandler(this.Pico_Cancel_Click);
            // 
            // button_Time_Block
            // 
            this.button_Time_Block.Location = new System.Drawing.Point(1390, 667);
            this.button_Time_Block.Name = "button_Time_Block";
            this.button_Time_Block.Size = new System.Drawing.Size(96, 21);
            this.button_Time_Block.TabIndex = 23;
            this.button_Time_Block.Text = "Time block";
            this.button_Time_Block.UseVisualStyleBackColor = true;
            this.button_Time_Block.Click += new System.EventHandler(this.button_Time_Block_Click);
            // 
            // button_Trigger_Block
            // 
            this.button_Trigger_Block.Location = new System.Drawing.Point(1288, 667);
            this.button_Trigger_Block.Name = "button_Trigger_Block";
            this.button_Trigger_Block.Size = new System.Drawing.Size(96, 21);
            this.button_Trigger_Block.TabIndex = 24;
            this.button_Trigger_Block.Text = "Trigger block";
            this.button_Trigger_Block.UseVisualStyleBackColor = true;
            this.button_Trigger_Block.Click += new System.EventHandler(this.button_Trigger_Block_Click);
            // 
            // label_Time
            // 
            this.label_Time.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_Time.Location = new System.Drawing.Point(869, 666);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(112, 21);
            this.label_Time.TabIndex = 25;
            this.label_Time.Text = "Time";
            this.label_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Measuring1
            // 
            this.label_Measuring1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_Measuring1.Location = new System.Drawing.Point(571, 667);
            this.label_Measuring1.Name = "label_Measuring1";
            this.label_Measuring1.Size = new System.Drawing.Size(292, 21);
            this.label_Measuring1.TabIndex = 26;
            this.label_Measuring1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Measuring2
            // 
            this.label_Measuring2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_Measuring2.Location = new System.Drawing.Point(987, 666);
            this.label_Measuring2.Name = "label_Measuring2";
            this.label_Measuring2.Size = new System.Drawing.Size(295, 21);
            this.label_Measuring2.TabIndex = 27;
            this.label_Measuring2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_TimeBase
            // 
            this.textBox_TimeBase.Location = new System.Drawing.Point(331, 668);
            this.textBox_TimeBase.Name = "textBox_TimeBase";
            this.textBox_TimeBase.Size = new System.Drawing.Size(39, 20);
            this.textBox_TimeBase.TabIndex = 28;
            this.textBox_TimeBase.Text = "1000";
            this.textBox_TimeBase.TextChanged += new System.EventHandler(this.textBox_TimeBase_TextChanged);
            // 
            // textBox_Treashold
            // 
            this.textBox_Treashold.Location = new System.Drawing.Point(489, 667);
            this.textBox_Treashold.Name = "textBox_Treashold";
            this.textBox_Treashold.Size = new System.Drawing.Size(39, 20);
            this.textBox_Treashold.TabIndex = 29;
            this.textBox_Treashold.Text = "2000";
            this.textBox_Treashold.TextChanged += new System.EventHandler(this.textBox_Treashold_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(266, 667);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 30;
            this.label1.Text = "Time base:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(376, 667);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 31;
            this.label2.Text = "ms        Threashold:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(534, 666);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 21);
            this.label3.TabIndex = 32;
            this.label3.Text = "mV";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OscilloscopeTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 700);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Treashold);
            this.Controls.Add(this.textBox_TimeBase);
            this.Controls.Add(this.label_Measuring2);
            this.Controls.Add(this.label_Measuring1);
            this.Controls.Add(this.label_Time);
            this.Controls.Add(this.button_Trigger_Block);
            this.Controls.Add(this.button_Time_Block);
            this.Controls.Add(this.Pico_Cancel);
            this.Controls.Add(this.comboBox_Voltage);
            this.Controls.Add(this.comboBox_Channel);
            this.Controls.Add(this.chart_Oscilloscope);
            this.Controls.Add(this.Exit_Oscilloscope);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OscilloscopeTestWindow";
            this.Text = "OscilloscopeTestWindow";
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oscilloscope)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Exit_Oscilloscope;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart_Oscilloscope;
        public System.Windows.Forms.ComboBox comboBox_Channel;
        public System.Windows.Forms.ComboBox comboBox_Voltage;
        public System.Windows.Forms.Button Pico_Cancel;
        public System.Windows.Forms.Button button_Time_Block;
        public System.Windows.Forms.Button button_Trigger_Block;
        public System.Windows.Forms.Label label_Time;
        public System.Windows.Forms.Label label_Measuring1;
        public System.Windows.Forms.Label label_Measuring2;
        private System.Windows.Forms.TextBox textBox_TimeBase;
        private System.Windows.Forms.TextBox textBox_Treashold;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
    }
}