
namespace ArtiluxEOL
{
    public partial class Main
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
            this.panelTestResult = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbAdmin = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.metrel_skip_btn = new System.Windows.Forms.Button();
            this.metrel_break_btn = new System.Windows.Forms.Button();
            this.Met_proceed_btn = new System.Windows.Forms.Button();
            this.metrel_stop_btn = new System.Windows.Forms.Button();
            this.metrel_start_btn = new System.Windows.Forms.Button();
            this.metrel_auto_btn = new System.Windows.Forms.Button();
            this.mtrelTest = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_evse = new System.Windows.Forms.Label();
            this.lbl_load = new System.Windows.Forms.Label();
            this.lbl_specrum = new System.Windows.Forms.Label();
            this.lbl_vald = new System.Windows.Forms.Label();
            this.lbl_hvgen = new System.Windows.Forms.Label();
            this.lbl_osc = new System.Windows.Forms.Label();
            this.lbl_power = new System.Windows.Forms.Label();
            this.lbl_rfid = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveWplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.save_ip = new System.Windows.Forms.Button();
            this.groupBox_Metrel_USB = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox_Valdiklis = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox_Load = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox_Spectr = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox_HVgen = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox_Osc_USB = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.debug_tab = new System.Windows.Forms.TabPage();
            this.list_debug = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serialPort_drd = new System.IO.Ports.SerialPort(this.components);
            this.tmr_1hz = new System.Windows.Forms.Timer(this.components);
            this.tmr_5hz = new System.Windows.Forms.Timer(this.components);
            this.NetworkDevConn = new System.ComponentModel.BackgroundWorker();
            this.MainControllerTCP = new System.ComponentModel.BackgroundWorker();
            this.MainControllerMODBUS = new System.ComponentModel.BackgroundWorker();
            this.btn_popup = new System.Windows.Forms.Button();
            this.HVgen = new System.ComponentModel.BackgroundWorker();
            this.Specroscope = new System.ComponentModel.BackgroundWorker();
            this.Load = new System.ComponentModel.BackgroundWorker();
            this.panelTestResult.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox_Metrel_USB.SuspendLayout();
            this.groupBox_Valdiklis.SuspendLayout();
            this.groupBox_Load.SuspendLayout();
            this.groupBox_Spectr.SuspendLayout();
            this.groupBox_HVgen.SuspendLayout();
            this.groupBox_Osc_USB.SuspendLayout();
            this.debug_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTestResult
            // 
            this.panelTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTestResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTestResult.Controls.Add(this.button4);
            this.panelTestResult.Controls.Add(this.button3);
            this.panelTestResult.Controls.Add(this.button2);
            this.panelTestResult.Controls.Add(this.btnStart);
            this.panelTestResult.Controls.Add(this.cbAdmin);
            this.panelTestResult.Location = new System.Drawing.Point(1, 784);
            this.panelTestResult.Margin = new System.Windows.Forms.Padding(4);
            this.panelTestResult.Name = "panelTestResult";
            this.panelTestResult.Size = new System.Drawing.Size(1113, 198);
            this.panelTestResult.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(252, 66);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 47);
            this.button4.TabIndex = 10;
            this.button4.Text = "Close";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(416, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 47);
            this.button3.TabIndex = 9;
            this.button3.Text = "Send";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(252, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 47);
            this.button2.TabIndex = 8;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(11, 10);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(176, 93);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Pradėti";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbAdmin
            // 
            this.cbAdmin.AutoSize = true;
            this.cbAdmin.Checked = true;
            this.cbAdmin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAdmin.Location = new System.Drawing.Point(562, 109);
            this.cbAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.cbAdmin.Name = "cbAdmin";
            this.cbAdmin.Size = new System.Drawing.Size(179, 20);
            this.cbAdmin.TabIndex = 2;
            this.cbAdmin.Text = "Aadministratoriaus teises";
            this.cbAdmin.UseVisualStyleBackColor = true;
            this.cbAdmin.CheckedChanged += new System.EventHandler(this.cbAdmin_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.debug_tab);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(160, 40);
            this.tabControl1.Location = new System.Drawing.Point(1, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(3544, 2725);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.btn_popup);
            this.tabPage1.Controls.Add(this.metrel_skip_btn);
            this.tabPage1.Controls.Add(this.metrel_break_btn);
            this.tabPage1.Controls.Add(this.Met_proceed_btn);
            this.tabPage1.Controls.Add(this.metrel_stop_btn);
            this.tabPage1.Controls.Add(this.metrel_start_btn);
            this.tabPage1.Controls.Add(this.metrel_auto_btn);
            this.tabPage1.Controls.Add(this.mtrelTest);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(3536, 2677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pagrindinis";
            // 
            // metrel_skip_btn
            // 
            this.metrel_skip_btn.Location = new System.Drawing.Point(383, 545);
            this.metrel_skip_btn.Name = "metrel_skip_btn";
            this.metrel_skip_btn.Size = new System.Drawing.Size(131, 47);
            this.metrel_skip_btn.TabIndex = 8;
            this.metrel_skip_btn.Text = "Skip";
            this.metrel_skip_btn.UseVisualStyleBackColor = true;
            this.metrel_skip_btn.Click += new System.EventHandler(this.metrel_skip_btn_Click);
            // 
            // metrel_break_btn
            // 
            this.metrel_break_btn.Location = new System.Drawing.Point(596, 545);
            this.metrel_break_btn.Name = "metrel_break_btn";
            this.metrel_break_btn.Size = new System.Drawing.Size(131, 47);
            this.metrel_break_btn.TabIndex = 7;
            this.metrel_break_btn.Text = "Break";
            this.metrel_break_btn.UseVisualStyleBackColor = true;
            this.metrel_break_btn.Click += new System.EventHandler(this.metrel_break_btn_Click);
            // 
            // Met_proceed_btn
            // 
            this.Met_proceed_btn.Location = new System.Drawing.Point(596, 486);
            this.Met_proceed_btn.Name = "Met_proceed_btn";
            this.Met_proceed_btn.Size = new System.Drawing.Size(131, 47);
            this.Met_proceed_btn.TabIndex = 6;
            this.Met_proceed_btn.Text = "Proceed";
            this.Met_proceed_btn.UseVisualStyleBackColor = true;
            this.Met_proceed_btn.Click += new System.EventHandler(this.Met_proceed_btn_Click);
            // 
            // metrel_stop_btn
            // 
            this.metrel_stop_btn.Location = new System.Drawing.Point(479, 486);
            this.metrel_stop_btn.Name = "metrel_stop_btn";
            this.metrel_stop_btn.Size = new System.Drawing.Size(90, 47);
            this.metrel_stop_btn.TabIndex = 5;
            this.metrel_stop_btn.Text = "Stop";
            this.metrel_stop_btn.UseVisualStyleBackColor = true;
            this.metrel_stop_btn.Click += new System.EventHandler(this.metrel_stop_btn_Click);
            // 
            // metrel_start_btn
            // 
            this.metrel_start_btn.Location = new System.Drawing.Point(383, 486);
            this.metrel_start_btn.Name = "metrel_start_btn";
            this.metrel_start_btn.Size = new System.Drawing.Size(90, 47);
            this.metrel_start_btn.TabIndex = 4;
            this.metrel_start_btn.Text = "Start";
            this.metrel_start_btn.UseVisualStyleBackColor = true;
            this.metrel_start_btn.Click += new System.EventHandler(this.metrel_start_btn_Click);
            // 
            // metrel_auto_btn
            // 
            this.metrel_auto_btn.Location = new System.Drawing.Point(566, 374);
            this.metrel_auto_btn.Name = "metrel_auto_btn";
            this.metrel_auto_btn.Size = new System.Drawing.Size(161, 95);
            this.metrel_auto_btn.TabIndex = 3;
            this.metrel_auto_btn.Text = "Metrel\r\nAUTO";
            this.metrel_auto_btn.UseVisualStyleBackColor = true;
            this.metrel_auto_btn.Click += new System.EventHandler(this.metrel_auto_btn_Click);
            // 
            // mtrelTest
            // 
            this.mtrelTest.Location = new System.Drawing.Point(383, 373);
            this.mtrelTest.Name = "mtrelTest";
            this.mtrelTest.Size = new System.Drawing.Size(161, 95);
            this.mtrelTest.TabIndex = 2;
            this.mtrelTest.Text = "Metrel\r\nSingle";
            this.mtrelTest.UseVisualStyleBackColor = true;
            this.mtrelTest.Click += new System.EventHandler(this.mtrelTest_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(458, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 355);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label4.Location = new System.Drawing.Point(61, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 35);
            this.label4.TabIndex = 4;
            this.label4.Text = "PASIRUOŠĘS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(41, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 35);
            this.label5.TabIndex = 3;
            this.label5.Text = "Įrangos būsena";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_evse);
            this.panel1.Controls.Add(this.lbl_load);
            this.panel1.Controls.Add(this.lbl_specrum);
            this.panel1.Controls.Add(this.lbl_vald);
            this.panel1.Controls.Add(this.lbl_hvgen);
            this.panel1.Controls.Add(this.lbl_osc);
            this.panel1.Controls.Add(this.lbl_power);
            this.panel1.Controls.Add(this.lbl_rfid);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 657);
            this.panel1.TabIndex = 0;
            // 
            // lbl_evse
            // 
            this.lbl_evse.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse.Location = new System.Drawing.Point(45, 488);
            this.lbl_evse.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_evse.Name = "lbl_evse";
            this.lbl_evse.Size = new System.Drawing.Size(228, 69);
            this.lbl_evse.TabIndex = 9;
            this.lbl_evse.Text = "   EVSE Testas";
            // 
            // lbl_load
            // 
            this.lbl_load.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_load.Location = new System.Drawing.Point(43, 249);
            this.lbl_load.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_load.Name = "lbl_load";
            this.lbl_load.Size = new System.Drawing.Size(230, 69);
            this.lbl_load.TabIndex = 8;
            this.lbl_load.Text = "        Apkrova";
            // 
            // lbl_specrum
            // 
            this.lbl_specrum.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_specrum.Location = new System.Drawing.Point(43, 170);
            this.lbl_specrum.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_specrum.Name = "lbl_specrum";
            this.lbl_specrum.Size = new System.Drawing.Size(230, 69);
            this.lbl_specrum.TabIndex = 7;
            this.lbl_specrum.Text = "         Spektro \r\n    analizatorius";
            // 
            // lbl_vald
            // 
            this.lbl_vald.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_vald.Location = new System.Drawing.Point(43, 12);
            this.lbl_vald.Name = "lbl_vald";
            this.lbl_vald.Size = new System.Drawing.Size(230, 69);
            this.lbl_vald.TabIndex = 6;
            this.lbl_vald.Text = "        Valdiklis      ";
            // 
            // lbl_hvgen
            // 
            this.lbl_hvgen.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_hvgen.Location = new System.Drawing.Point(43, 91);
            this.lbl_hvgen.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_hvgen.Name = "lbl_hvgen";
            this.lbl_hvgen.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_hvgen.Size = new System.Drawing.Size(230, 69);
            this.lbl_hvgen.TabIndex = 5;
            this.lbl_hvgen.Text = "             HV\r\n    Generatorius  ";
            // 
            // lbl_osc
            // 
            this.lbl_osc.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_osc.Location = new System.Drawing.Point(41, 567);
            this.lbl_osc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_osc.Name = "lbl_osc";
            this.lbl_osc.Size = new System.Drawing.Size(232, 69);
            this.lbl_osc.TabIndex = 4;
            this.lbl_osc.Text = "   Osciloskopas";
            // 
            // lbl_power
            // 
            this.lbl_power.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_power.Location = new System.Drawing.Point(43, 407);
            this.lbl_power.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_power.Name = "lbl_power";
            this.lbl_power.Size = new System.Drawing.Size(230, 69);
            this.lbl_power.TabIndex = 3;
            this.lbl_power.Text = "Testavimo lizdas\r\n  (MAITINIMAS)";
            // 
            // lbl_rfid
            // 
            this.lbl_rfid.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid.Location = new System.Drawing.Point(43, 328);
            this.lbl_rfid.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_rfid.Name = "lbl_rfid";
            this.lbl_rfid.Size = new System.Drawing.Size(230, 69);
            this.lbl_rfid.TabIndex = 2;
            this.lbl_rfid.Text = "Testavimo lizdas\r\n         (RF-ID)";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(3536, 2677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nustatymai";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saveWplace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 349);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Darbo vieta";
            // 
            // saveWplace
            // 
            this.saveWplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveWplace.Location = new System.Drawing.Point(223, 283);
            this.saveWplace.Name = "saveWplace";
            this.saveWplace.Size = new System.Drawing.Size(122, 42);
            this.saveWplace.TabIndex = 5;
            this.saveWplace.Text = "Išsaugoti";
            this.saveWplace.UseVisualStyleBackColor = true;
            this.saveWplace.Click += new System.EventHandler(this.saveWplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.save_ip);
            this.tabPage3.Controls.Add(this.groupBox_Metrel_USB);
            this.tabPage3.Controls.Add(this.groupBox_Valdiklis);
            this.tabPage3.Controls.Add(this.groupBox_Load);
            this.tabPage3.Controls.Add(this.groupBox_Spectr);
            this.tabPage3.Controls.Add(this.groupBox_HVgen);
            this.tabPage3.Controls.Add(this.groupBox_Osc_USB);
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(3536, 2677);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Įranga";
            // 
            // save_ip
            // 
            this.save_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_ip.Location = new System.Drawing.Point(309, 548);
            this.save_ip.Name = "save_ip";
            this.save_ip.Size = new System.Drawing.Size(146, 44);
            this.save_ip.TabIndex = 12;
            this.save_ip.Text = "Išsaugoti";
            this.save_ip.UseVisualStyleBackColor = true;
            this.save_ip.Click += new System.EventHandler(this.save_ip_Click);
            // 
            // groupBox_Metrel_USB
            // 
            this.groupBox_Metrel_USB.Controls.Add(this.label16);
            this.groupBox_Metrel_USB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Metrel_USB.Location = new System.Drawing.Point(27, 355);
            this.groupBox_Metrel_USB.Name = "groupBox_Metrel_USB";
            this.groupBox_Metrel_USB.Size = new System.Drawing.Size(758, 75);
            this.groupBox_Metrel_USB.TabIndex = 7;
            this.groupBox_Metrel_USB.TabStop = false;
            this.groupBox_Metrel_USB.Text = "EVSE (Metrel)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(332, 43);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 22);
            this.label16.TabIndex = 2;
            this.label16.Text = "ID_WORD";
            // 
            // groupBox_Valdiklis
            // 
            this.groupBox_Valdiklis.Controls.Add(this.label8);
            this.groupBox_Valdiklis.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Valdiklis.Location = new System.Drawing.Point(27, 31);
            this.groupBox_Valdiklis.Name = "groupBox_Valdiklis";
            this.groupBox_Valdiklis.Size = new System.Drawing.Size(758, 75);
            this.groupBox_Valdiklis.TabIndex = 11;
            this.groupBox_Valdiklis.TabStop = false;
            this.groupBox_Valdiklis.Text = "Valdiklis";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(332, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "IP:PORT";
            // 
            // groupBox_Load
            // 
            this.groupBox_Load.Controls.Add(this.label13);
            this.groupBox_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Load.Location = new System.Drawing.Point(27, 274);
            this.groupBox_Load.Name = "groupBox_Load";
            this.groupBox_Load.Size = new System.Drawing.Size(758, 75);
            this.groupBox_Load.TabIndex = 10;
            this.groupBox_Load.TabStop = false;
            this.groupBox_Load.Text = " AC apkrova";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(332, 43);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 22);
            this.label13.TabIndex = 2;
            this.label13.Text = "IP:PORT";
            // 
            // groupBox_Spectr
            // 
            this.groupBox_Spectr.Controls.Add(this.label14);
            this.groupBox_Spectr.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Spectr.Location = new System.Drawing.Point(27, 193);
            this.groupBox_Spectr.Name = "groupBox_Spectr";
            this.groupBox_Spectr.Size = new System.Drawing.Size(758, 75);
            this.groupBox_Spectr.TabIndex = 9;
            this.groupBox_Spectr.TabStop = false;
            this.groupBox_Spectr.Text = "Spektro analizatorius";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(332, 43);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 22);
            this.label14.TabIndex = 2;
            this.label14.Text = "IP:PORT";
            // 
            // groupBox_HVgen
            // 
            this.groupBox_HVgen.Controls.Add(this.label15);
            this.groupBox_HVgen.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HVgen.Location = new System.Drawing.Point(27, 112);
            this.groupBox_HVgen.Name = "groupBox_HVgen";
            this.groupBox_HVgen.Size = new System.Drawing.Size(758, 75);
            this.groupBox_HVgen.TabIndex = 8;
            this.groupBox_HVgen.TabStop = false;
            this.groupBox_HVgen.Text = "HV generatorius";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(332, 43);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 22);
            this.label15.TabIndex = 2;
            this.label15.Text = "IP:PORT";
            // 
            // groupBox_Osc_USB
            // 
            this.groupBox_Osc_USB.Controls.Add(this.label17);
            this.groupBox_Osc_USB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Osc_USB.Location = new System.Drawing.Point(27, 436);
            this.groupBox_Osc_USB.Name = "groupBox_Osc_USB";
            this.groupBox_Osc_USB.Size = new System.Drawing.Size(758, 75);
            this.groupBox_Osc_USB.TabIndex = 6;
            this.groupBox_Osc_USB.TabStop = false;
            this.groupBox_Osc_USB.Text = "Osciloskopas";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(332, 43);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 22);
            this.label17.TabIndex = 2;
            this.label17.Text = "ID_WORD";
            // 
            // debug_tab
            // 
            this.debug_tab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.debug_tab.Controls.Add(this.list_debug);
            this.debug_tab.Location = new System.Drawing.Point(4, 44);
            this.debug_tab.Name = "debug_tab";
            this.debug_tab.Padding = new System.Windows.Forms.Padding(3);
            this.debug_tab.Size = new System.Drawing.Size(3536, 2677);
            this.debug_tab.TabIndex = 3;
            this.debug_tab.Text = "Debug";
            // 
            // list_debug
            // 
            this.list_debug.BackColor = System.Drawing.Color.WhiteSmoke;
            this.list_debug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.list_debug.GridLines = true;
            this.list_debug.HideSelection = false;
            this.list_debug.Location = new System.Drawing.Point(3, 6);
            this.list_debug.Name = "list_debug";
            this.list_debug.Size = new System.Drawing.Size(685, 563);
            this.list_debug.TabIndex = 0;
            this.list_debug.UseCompatibleStateImageBehavior = false;
            this.list_debug.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 280;
            // 
            // tmr_1hz
            // 
            this.tmr_1hz.Interval = 1000;
            this.tmr_1hz.Tick += new System.EventHandler(this.tmr_1hz_Tick);
            // 
            // tmr_5hz
            // 
            this.tmr_5hz.Interval = 200;
            this.tmr_5hz.Tick += new System.EventHandler(this.tmr_5hz_Tick);
            // 
            // btn_popup
            // 
            this.btn_popup.Location = new System.Drawing.Point(782, 638);
            this.btn_popup.Name = "btn_popup";
            this.btn_popup.Size = new System.Drawing.Size(122, 55);
            this.btn_popup.TabIndex = 9;
            this.btn_popup.Text = "popup";
            this.btn_popup.UseVisualStyleBackColor = true;
            this.btn_popup.Click += new System.EventHandler(this.btn_popup_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 963);
            this.Controls.Add(this.panelTestResult);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Monitors Test v03";
            //this.Load += new System.EventHandler(this.Main_Load);
            this.panelTestResult.ResumeLayout(false);
            this.panelTestResult.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox_Metrel_USB.ResumeLayout(false);
            this.groupBox_Metrel_USB.PerformLayout();
            this.groupBox_Valdiklis.ResumeLayout(false);
            this.groupBox_Valdiklis.PerformLayout();
            this.groupBox_Load.ResumeLayout(false);
            this.groupBox_Load.PerformLayout();
            this.groupBox_Spectr.ResumeLayout(false);
            this.groupBox_Spectr.PerformLayout();
            this.groupBox_HVgen.ResumeLayout(false);
            this.groupBox_HVgen.PerformLayout();
            this.groupBox_Osc_USB.ResumeLayout(false);
            this.groupBox_Osc_USB.PerformLayout();
            this.debug_tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTestResult;
        private System.Windows.Forms.Button btnStart;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox cbAdmin;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveWplace;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_load;
        private System.Windows.Forms.Label lbl_specrum;
        private System.Windows.Forms.Label lbl_vald;
        private System.Windows.Forms.Label lbl_hvgen;
        private System.Windows.Forms.Label lbl_osc;
        private System.Windows.Forms.Label lbl_power;
        private System.Windows.Forms.Label lbl_rfid;
        private System.IO.Ports.SerialPort serialPort_drd;
        public System.Windows.Forms.Timer tmr_1hz;
        public System.Windows.Forms.Timer tmr_5hz;
        private System.Windows.Forms.TabPage debug_tab;
        public System.Windows.Forms.ListView list_debug;
        private System.Windows.Forms.Label lbl_evse;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox_Valdiklis;
        private System.Windows.Forms.GroupBox groupBox_Load;
        private System.Windows.Forms.GroupBox groupBox_Spectr;
        private System.Windows.Forms.GroupBox groupBox_HVgen;
        private System.Windows.Forms.GroupBox groupBox_Metrel_USB;
        private System.Windows.Forms.GroupBox groupBox_Osc_USB;
        private System.Windows.Forms.Button save_ip;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button mtrelTest;
        private System.Windows.Forms.Button metrel_auto_btn;
        private System.Windows.Forms.Button Met_proceed_btn;
        private System.Windows.Forms.Button metrel_stop_btn;
        private System.Windows.Forms.Button metrel_start_btn;
        private System.Windows.Forms.Button metrel_break_btn;
        private System.Windows.Forms.Button metrel_skip_btn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.ComponentModel.BackgroundWorker NetworkDevConn;
        private System.ComponentModel.BackgroundWorker MainControllerTCP;
        private System.ComponentModel.BackgroundWorker MainControllerMODBUS;
        private System.Windows.Forms.Button btn_popup;
        private System.ComponentModel.BackgroundWorker HVgen;
        private System.ComponentModel.BackgroundWorker Specroscope;
        private System.ComponentModel.BackgroundWorker Load;
    }
}

