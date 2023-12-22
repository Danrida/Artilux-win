
namespace ArtiluxEOL
{
    partial class WindowModal
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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblIds = new System.Windows.Forms.Label();
            this.disp_evse_nr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblLong = new System.Windows.Forms.Label();
            this.panelTestResult = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_work_pos = new System.Windows.Forms.Label();
            this.progressBar_oscilloscope = new System.Windows.Forms.ProgressBar();
            this.progressBar_spectroscope_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_hv_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_load_test = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBar_wifi_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_rfid_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_evse_communication = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panel_main.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelTestResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel2);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Controls.Add(this.panelTestResult);
            this.panel_main.Controls.Add(this.panel4);
            this.panel_main.Location = new System.Drawing.Point(11, 11);
            this.panel_main.Margin = new System.Windows.Forms.Padding(2);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(874, 669);
            this.panel_main.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_work_pos);
            this.panel2.Controls.Add(this.lblIds);
            this.panel2.Controls.Add(this.disp_evse_nr);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(647, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(221, 459);
            this.panel2.TabIndex = 10;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIds.Location = new System.Drawing.Point(8, 158);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(59, 18);
            this.lblIds.TabIndex = 11;
            this.lblIds.Text = "monitor";
            // 
            // disp_evse_nr
            // 
            this.disp_evse_nr.AutoSize = true;
            this.disp_evse_nr.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disp_evse_nr.ForeColor = System.Drawing.Color.DimGray;
            this.disp_evse_nr.Location = new System.Drawing.Point(5, 119);
            this.disp_evse_nr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.disp_evse_nr.Name = "disp_evse_nr";
            this.disp_evse_nr.Size = new System.Drawing.Size(91, 27);
            this.disp_evse_nr.TabIndex = 7;
            this.disp_evse_nr.Text = "EVSE SN:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(10, 271);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 27);
            this.label1.TabIndex = 6;
            this.label1.Text = "INFORMACIJA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label4.Location = new System.Drawing.Point(47, 416);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 27);
            this.label4.TabIndex = 2;
            this.label4.Text = "PASIRUOŠĘS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(32, 379);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "Įrangos būsena";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(2, 233);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Papildoma informacija";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.lblLong);
            this.panel1.Location = new System.Drawing.Point(4, 491);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 170);
            this.panel1.TabIndex = 9;
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(647, 27);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(202, 114);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "PRADĖTI";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.BackColor = System.Drawing.Color.MediumAquamarine;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblResult.Location = new System.Drawing.Point(345, 57);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(133, 44);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Result";
            // 
            // lblLong
            // 
            this.lblLong.AutoSize = true;
            this.lblLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLong.Location = new System.Drawing.Point(61, 54);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new System.Drawing.Size(98, 37);
            this.lblLong.TabIndex = 2;
            this.lblLong.Text = "00:00";
            // 
            // panelTestResult
            // 
            this.panelTestResult.Controls.Add(this.progressBar_evse_communication);
            this.panelTestResult.Controls.Add(this.progressBar_rfid_test);
            this.panelTestResult.Controls.Add(this.progressBar_wifi_test);
            this.panelTestResult.Controls.Add(this.label11);
            this.panelTestResult.Controls.Add(this.label10);
            this.panelTestResult.Controls.Add(this.label9);
            this.panelTestResult.Controls.Add(this.label8);
            this.panelTestResult.Controls.Add(this.label7);
            this.panelTestResult.Controls.Add(this.label6);
            this.panelTestResult.Controls.Add(this.label5);
            this.panelTestResult.Controls.Add(this.progressBar_oscilloscope);
            this.panelTestResult.Controls.Add(this.progressBar_load_test);
            this.panelTestResult.Controls.Add(this.progressBar_spectroscope_test);
            this.panelTestResult.Controls.Add(this.progressBar_hv_test);
            this.panelTestResult.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.panelTestResult.Location = new System.Drawing.Point(8, 34);
            this.panelTestResult.Name = "panelTestResult";
            this.panelTestResult.Size = new System.Drawing.Size(634, 437);
            this.panelTestResult.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(4, 12);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(865, 468);
            this.panel4.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_work_pos
            // 
            this.label_work_pos.BackColor = System.Drawing.Color.Transparent;
            this.label_work_pos.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_work_pos.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_work_pos.Location = new System.Drawing.Point(5, 19);
            this.label_work_pos.Name = "label_work_pos";
            this.label_work_pos.Size = new System.Drawing.Size(213, 73);
            this.label_work_pos.TabIndex = 0;
            this.label_work_pos.Text = "Pos";
            this.label_work_pos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar_oscilloscope
            // 
            this.progressBar_oscilloscope.Location = new System.Drawing.Point(283, 296);
            this.progressBar_oscilloscope.Maximum = 28;
            this.progressBar_oscilloscope.Name = "progressBar_oscilloscope";
            this.progressBar_oscilloscope.Size = new System.Drawing.Size(298, 23);
            this.progressBar_oscilloscope.TabIndex = 29;
            // 
            // progressBar_spectroscope_test
            // 
            this.progressBar_spectroscope_test.Location = new System.Drawing.Point(283, 221);
            this.progressBar_spectroscope_test.Maximum = 28;
            this.progressBar_spectroscope_test.Name = "progressBar_spectroscope_test";
            this.progressBar_spectroscope_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_spectroscope_test.TabIndex = 28;
            // 
            // progressBar_hv_test
            // 
            this.progressBar_hv_test.Location = new System.Drawing.Point(283, 149);
            this.progressBar_hv_test.Maximum = 39;
            this.progressBar_hv_test.Name = "progressBar_hv_test";
            this.progressBar_hv_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_hv_test.TabIndex = 27;
            // 
            // progressBar_load_test
            // 
            this.progressBar_load_test.Location = new System.Drawing.Point(283, 113);
            this.progressBar_load_test.Maximum = 50;
            this.progressBar_load_test.Name = "progressBar_load_test";
            this.progressBar_load_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_load_test.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(32, 139);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 36);
            this.label5.TabIndex = 30;
            this.label5.Text = "HV testas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(32, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 36);
            this.label6.TabIndex = 31;
            this.label6.Text = "Apkrovos testas";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(32, 67);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 36);
            this.label7.TabIndex = 32;
            this.label7.Text = "Stotelės komunikacija";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(32, 175);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(246, 36);
            this.label8.TabIndex = 33;
            this.label8.Text = "WI-FI testas";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(32, 211);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(246, 36);
            this.label9.TabIndex = 34;
            this.label9.Text = "GSM testas";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(32, 247);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(246, 36);
            this.label10.TabIndex = 35;
            this.label10.Text = "RFID testas";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(32, 283);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(246, 36);
            this.label11.TabIndex = 36;
            this.label11.Text = "Srovės nuotekio testas";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar_wifi_test
            // 
            this.progressBar_wifi_test.Location = new System.Drawing.Point(283, 185);
            this.progressBar_wifi_test.Maximum = 28;
            this.progressBar_wifi_test.Name = "progressBar_wifi_test";
            this.progressBar_wifi_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_wifi_test.TabIndex = 37;
            // 
            // progressBar_rfid_test
            // 
            this.progressBar_rfid_test.Location = new System.Drawing.Point(283, 257);
            this.progressBar_rfid_test.Maximum = 28;
            this.progressBar_rfid_test.Name = "progressBar_rfid_test";
            this.progressBar_rfid_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_rfid_test.TabIndex = 38;
            // 
            // progressBar_evse_communication
            // 
            this.progressBar_evse_communication.Location = new System.Drawing.Point(283, 77);
            this.progressBar_evse_communication.Maximum = 50;
            this.progressBar_evse_communication.Name = "progressBar_evse_communication";
            this.progressBar_evse_communication.Size = new System.Drawing.Size(298, 23);
            this.progressBar_evse_communication.TabIndex = 39;
            // 
            // WindowModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1587, 928);
            this.Controls.Add(this.panel_main);
            this.Name = "WindowModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Test Modal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestModal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.panel_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTestResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblLong;
        private System.Windows.Forms.Panel panelTestResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIds;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label disp_evse_nr;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_work_pos;
        public System.Windows.Forms.ProgressBar progressBar_oscilloscope;
        public System.Windows.Forms.ProgressBar progressBar_load_test;
        public System.Windows.Forms.ProgressBar progressBar_spectroscope_test;
        public System.Windows.Forms.ProgressBar progressBar_hv_test;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ProgressBar progressBar_wifi_test;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ProgressBar progressBar_evse_communication;
        public System.Windows.Forms.ProgressBar progressBar_rfid_test;
    }
}