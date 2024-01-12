
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
            this.label_EVSE_state = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_work_pos = new System.Windows.Forms.Label();
            this.lblIds = new System.Windows.Forms.Label();
            this.disp_evse_nr = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar_total = new System.Windows.Forms.ProgressBar();
            this.label_Time_elapsed = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_Test_State = new System.Windows.Forms.Label();
            this.panelTestResult = new System.Windows.Forms.Panel();
            this.progressBar_evse_communication = new System.Windows.Forms.ProgressBar();
            this.progressBar_RFID_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_Wifi_test = new System.Windows.Forms.ProgressBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar_RCD_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_load_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_GSM_test = new System.Windows.Forms.ProgressBar();
            this.progressBar_HV_test = new System.Windows.Forms.ProgressBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            this.panel2.Controls.Add(this.label_EVSE_state);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label_work_pos);
            this.panel2.Controls.Add(this.lblIds);
            this.panel2.Controls.Add(this.disp_evse_nr);
            this.panel2.Location = new System.Drawing.Point(647, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(221, 456);
            this.panel2.TabIndex = 10;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label_EVSE_state
            // 
            this.label_EVSE_state.AutoSize = true;
            this.label_EVSE_state.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_EVSE_state.Location = new System.Drawing.Point(8, 218);
            this.label_EVSE_state.Name = "label_EVSE_state";
            this.label_EVSE_state.Size = new System.Drawing.Size(40, 18);
            this.label_EVSE_state.TabIndex = 13;
            this.label_EVSE_state.Text = "state";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(5, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 27);
            this.label1.TabIndex = 12;
            this.label1.Text = "EVSE būsena:";
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
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIds.Location = new System.Drawing.Point(8, 148);
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar_total);
            this.panel1.Controls.Add(this.label_Time_elapsed);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label_Test_State);
            this.panel1.Location = new System.Drawing.Point(4, 491);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 170);
            this.panel1.TabIndex = 9;
            // 
            // progressBar_total
            // 
            this.progressBar_total.Location = new System.Drawing.Point(108, 105);
            this.progressBar_total.Maximum = 28;
            this.progressBar_total.Name = "progressBar_total";
            this.progressBar_total.Size = new System.Drawing.Size(718, 23);
            this.progressBar_total.TabIndex = 30;
            // 
            // label_Time_elapsed
            // 
            this.label_Time_elapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Time_elapsed.Location = new System.Drawing.Point(29, 105);
            this.label_Time_elapsed.Name = "label_Time_elapsed";
            this.label_Time_elapsed.Size = new System.Drawing.Size(73, 23);
            this.label_Time_elapsed.TabIndex = 2;
            this.label_Time_elapsed.Text = "00:00";
            this.label_Time_elapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(29, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "Bandymo būsena:";
            // 
            // label_Test_State
            // 
            this.label_Test_State.AutoSize = true;
            this.label_Test_State.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Test_State.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label_Test_State.Location = new System.Drawing.Point(205, 59);
            this.label_Test_State.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Test_State.Name = "label_Test_State";
            this.label_Test_State.Size = new System.Drawing.Size(79, 27);
            this.label_Test_State.TabIndex = 2;
            this.label_Test_State.Text = "Būsena";
            // 
            // panelTestResult
            // 
            this.panelTestResult.Controls.Add(this.progressBar_evse_communication);
            this.panelTestResult.Controls.Add(this.progressBar_RFID_test);
            this.panelTestResult.Controls.Add(this.progressBar_Wifi_test);
            this.panelTestResult.Controls.Add(this.label11);
            this.panelTestResult.Controls.Add(this.label10);
            this.panelTestResult.Controls.Add(this.label9);
            this.panelTestResult.Controls.Add(this.label8);
            this.panelTestResult.Controls.Add(this.label7);
            this.panelTestResult.Controls.Add(this.label6);
            this.panelTestResult.Controls.Add(this.label5);
            this.panelTestResult.Controls.Add(this.progressBar_RCD_test);
            this.panelTestResult.Controls.Add(this.progressBar_load_test);
            this.panelTestResult.Controls.Add(this.progressBar_GSM_test);
            this.panelTestResult.Controls.Add(this.progressBar_HV_test);
            this.panelTestResult.Font = new System.Drawing.Font("Webdings", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.panelTestResult.Location = new System.Drawing.Point(8, 15);
            this.panelTestResult.Name = "panelTestResult";
            this.panelTestResult.Size = new System.Drawing.Size(634, 456);
            this.panelTestResult.TabIndex = 7;
            // 
            // progressBar_evse_communication
            // 
            this.progressBar_evse_communication.Location = new System.Drawing.Point(283, 104);
            this.progressBar_evse_communication.Maximum = 15;
            this.progressBar_evse_communication.Name = "progressBar_evse_communication";
            this.progressBar_evse_communication.Size = new System.Drawing.Size(298, 23);
            this.progressBar_evse_communication.TabIndex = 39;
            // 
            // progressBar_RFID_test
            // 
            this.progressBar_RFID_test.Location = new System.Drawing.Point(283, 284);
            this.progressBar_RFID_test.Maximum = 15;
            this.progressBar_RFID_test.Name = "progressBar_RFID_test";
            this.progressBar_RFID_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_RFID_test.TabIndex = 38;
            // 
            // progressBar_Wifi_test
            // 
            this.progressBar_Wifi_test.Location = new System.Drawing.Point(283, 212);
            this.progressBar_Wifi_test.Maximum = 15;
            this.progressBar_Wifi_test.Name = "progressBar_Wifi_test";
            this.progressBar_Wifi_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_Wifi_test.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(32, 313);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(246, 36);
            this.label11.TabIndex = 36;
            this.label11.Text = "Srovės nuotekio testas";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(32, 274);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(246, 36);
            this.label10.TabIndex = 35;
            this.label10.Text = "RFID testas";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(32, 238);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(246, 36);
            this.label9.TabIndex = 34;
            this.label9.Text = "GSM testas";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(32, 202);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(246, 36);
            this.label8.TabIndex = 33;
            this.label8.Text = "WI-FI testas";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(32, 93);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 36);
            this.label7.TabIndex = 32;
            this.label7.Text = "Stotelės komunikacija";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(32, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 36);
            this.label6.TabIndex = 31;
            this.label6.Text = "Apkrovos testas";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(32, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 36);
            this.label5.TabIndex = 30;
            this.label5.Text = "HV testas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar_RCD_test
            // 
            this.progressBar_RCD_test.Location = new System.Drawing.Point(283, 323);
            this.progressBar_RCD_test.Maximum = 15;
            this.progressBar_RCD_test.Name = "progressBar_RCD_test";
            this.progressBar_RCD_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_RCD_test.TabIndex = 29;
            // 
            // progressBar_load_test
            // 
            this.progressBar_load_test.Location = new System.Drawing.Point(283, 140);
            this.progressBar_load_test.Maximum = 15;
            this.progressBar_load_test.Name = "progressBar_load_test";
            this.progressBar_load_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_load_test.TabIndex = 26;
            // 
            // progressBar_GSM_test
            // 
            this.progressBar_GSM_test.Location = new System.Drawing.Point(283, 248);
            this.progressBar_GSM_test.Maximum = 15;
            this.progressBar_GSM_test.Name = "progressBar_GSM_test";
            this.progressBar_GSM_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_GSM_test.TabIndex = 28;
            // 
            // progressBar_HV_test
            // 
            this.progressBar_HV_test.Location = new System.Drawing.Point(283, 176);
            this.progressBar_HV_test.Maximum = 15;
            this.progressBar_HV_test.Name = "progressBar_HV_test";
            this.progressBar_HV_test.Size = new System.Drawing.Size(298, 23);
            this.progressBar_HV_test.TabIndex = 27;
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
            // WindowModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
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
        private System.Windows.Forms.Label label_Time_elapsed;
        private System.Windows.Forms.Panel panelTestResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_Test_State;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIds;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label disp_evse_nr;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_work_pos;
        public System.Windows.Forms.ProgressBar progressBar_RCD_test;
        public System.Windows.Forms.ProgressBar progressBar_load_test;
        public System.Windows.Forms.ProgressBar progressBar_GSM_test;
        public System.Windows.Forms.ProgressBar progressBar_HV_test;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ProgressBar progressBar_Wifi_test;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ProgressBar progressBar_evse_communication;
        public System.Windows.Forms.ProgressBar progressBar_RFID_test;
        private System.Windows.Forms.Label label_EVSE_state;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ProgressBar progressBar_total;
    }
}