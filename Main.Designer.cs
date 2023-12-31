﻿
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Test_lizdas_3 = new System.Windows.Forms.GroupBox();
            this.lbl_evse3 = new System.Windows.Forms.Label();
            this.lbl_barcode_3 = new System.Windows.Forms.Label();
            this.lbl_rfid_3 = new System.Windows.Forms.Label();
            this.Test_lizdas_2 = new System.Windows.Forms.GroupBox();
            this.lbl_evse2 = new System.Windows.Forms.Label();
            this.lbl_barcode_2 = new System.Windows.Forms.Label();
            this.lbl_rfid_2 = new System.Windows.Forms.Label();
            this.Test_lizdas_1 = new System.Windows.Forms.GroupBox();
            this.lbl_evse1 = new System.Windows.Forms.Label();
            this.lbl_barcode_1 = new System.Windows.Forms.Label();
            this.lbl_rfid_1 = new System.Windows.Forms.Label();
            this.btn_popup = new System.Windows.Forms.Button();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveWplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox_Rfid = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox_Barcode = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.data_grid_main_board = new System.Windows.Forms.DataGridView();
            this.CMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btn_stop = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGrid_HV_result = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_HV_test = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column0 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGrid_Spectrum = new System.Windows.Forms.DataGridView();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button7 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGrid_Load_Line = new System.Windows.Forms.DataGridView();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_Load_load = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.groupBoxBarcode3 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.dataGrid_Barcode3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxBarcode2 = new System.Windows.Forms.GroupBox();
            this.evse2_params = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dataGrid_Barcode2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxBarcode1 = new System.Windows.Forms.GroupBox();
            this.sta = new System.Windows.Forms.Label();
            this.dataGrid_Barcode1 = new System.Windows.Forms.DataGridView();
            this.Column20 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debug_tab = new System.Windows.Forms.TabPage();
            this.debug_ping_cbox = new System.Windows.Forms.CheckBox();
            this.debug_load_cbox = new System.Windows.Forms.CheckBox();
            this.debug_siglent_cbox = new System.Windows.Forms.CheckBox();
            this.debug_gwinstek_cbox = new System.Windows.Forms.CheckBox();
            this.debug_evse_cbox = new System.Windows.Forms.CheckBox();
            this.debug_main_cbox = new System.Windows.Forms.CheckBox();
            this.dbg_list_clear = new System.Windows.Forms.Button();
            this.debug_usb_cbox = new System.Windows.Forms.CheckBox();
            this.debug_network_cbox = new System.Windows.Forms.CheckBox();
            this.list_debug = new System.Windows.Forms.ListView();
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serialPort_drd = new System.IO.Ports.SerialPort(this.components);
            this.tmr_1hz = new System.Windows.Forms.Timer(this.components);
            this.tmr_5hz = new System.Windows.Forms.Timer(this.components);
            this.NetworkDevConn = new System.ComponentModel.BackgroundWorker();
            this.MainControllerTCP = new System.ComponentModel.BackgroundWorker();
            this.MainControllerMODBUS = new System.ComponentModel.BackgroundWorker();
            this.HVgen = new System.ComponentModel.BackgroundWorker();
            this.Specroscope = new System.ComponentModel.BackgroundWorker();
            this.Load = new System.ComponentModel.BackgroundWorker();
            this.Barcode1 = new System.ComponentModel.BackgroundWorker();
            this.Barcode2 = new System.ComponentModel.BackgroundWorker();
            this.Barcode3 = new System.ComponentModel.BackgroundWorker();
            this.cbAdmin = new System.Windows.Forms.CheckBox();
            this.panelTestResult = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox_main_relay = new System.Windows.Forms.GroupBox();
            this.groupBox_evse_state = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Test_lizdas_3.SuspendLayout();
            this.Test_lizdas_2.SuspendLayout();
            this.Test_lizdas_1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox_Rfid.SuspendLayout();
            this.groupBox_Barcode.SuspendLayout();
            this.groupBox_Metrel_USB.SuspendLayout();
            this.groupBox_Valdiklis.SuspendLayout();
            this.groupBox_Load.SuspendLayout();
            this.groupBox_Spectr.SuspendLayout();
            this.groupBox_HVgen.SuspendLayout();
            this.groupBox_Osc_USB.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_main_board)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_test)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Spectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Load_Line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Load_load)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.groupBoxBarcode3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode3)).BeginInit();
            this.groupBoxBarcode2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode2)).BeginInit();
            this.groupBoxBarcode1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode1)).BeginInit();
            this.debug_tab.SuspendLayout();
            this.panelTestResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.debug_tab);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(160, 40);
            this.tabControl1.Location = new System.Drawing.Point(5, 12);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1323, 814);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.Test_lizdas_3);
            this.tabPage1.Controls.Add(this.Test_lizdas_2);
            this.tabPage1.Controls.Add(this.Test_lizdas_1);
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
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1315, 766);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pagrindinis";
            // 
            // Test_lizdas_3
            // 
            this.Test_lizdas_3.Controls.Add(this.lbl_evse3);
            this.Test_lizdas_3.Controls.Add(this.lbl_barcode_3);
            this.Test_lizdas_3.Controls.Add(this.lbl_rfid_3);
            this.Test_lizdas_3.Location = new System.Drawing.Point(967, 18);
            this.Test_lizdas_3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_3.Name = "Test_lizdas_3";
            this.Test_lizdas_3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_3.Size = new System.Drawing.Size(264, 274);
            this.Test_lizdas_3.TabIndex = 12;
            this.Test_lizdas_3.TabStop = false;
            this.Test_lizdas_3.Text = "3";
            // 
            // lbl_evse3
            // 
            this.lbl_evse3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse3.Location = new System.Drawing.Point(5, 92);
            this.lbl_evse3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_evse3.Name = "lbl_evse3";
            this.lbl_evse3.Size = new System.Drawing.Size(237, 37);
            this.lbl_evse3.TabIndex = 13;
            this.lbl_evse3.Text = "           EVSE";
            // 
            // lbl_barcode_3
            // 
            this.lbl_barcode_3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_3.Location = new System.Drawing.Point(5, 185);
            this.lbl_barcode_3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_barcode_3.Name = "lbl_barcode_3";
            this.lbl_barcode_3.Size = new System.Drawing.Size(237, 69);
            this.lbl_barcode_3.TabIndex = 10;
            this.lbl_barcode_3.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_3
            // 
            this.lbl_rfid_3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_3.Location = new System.Drawing.Point(5, 139);
            this.lbl_rfid_3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_rfid_3.Name = "lbl_rfid_3";
            this.lbl_rfid_3.Size = new System.Drawing.Size(237, 36);
            this.lbl_rfid_3.TabIndex = 11;
            this.lbl_rfid_3.Text = "           RF-ID";
            // 
            // Test_lizdas_2
            // 
            this.Test_lizdas_2.Controls.Add(this.lbl_evse2);
            this.Test_lizdas_2.Controls.Add(this.lbl_barcode_2);
            this.Test_lizdas_2.Controls.Add(this.lbl_rfid_2);
            this.Test_lizdas_2.Location = new System.Drawing.Point(693, 18);
            this.Test_lizdas_2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_2.Name = "Test_lizdas_2";
            this.Test_lizdas_2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_2.Size = new System.Drawing.Size(264, 274);
            this.Test_lizdas_2.TabIndex = 11;
            this.Test_lizdas_2.TabStop = false;
            this.Test_lizdas_2.Text = "2";
            // 
            // lbl_evse2
            // 
            this.lbl_evse2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse2.Location = new System.Drawing.Point(5, 92);
            this.lbl_evse2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_evse2.Name = "lbl_evse2";
            this.lbl_evse2.Size = new System.Drawing.Size(237, 37);
            this.lbl_evse2.TabIndex = 13;
            this.lbl_evse2.Text = "           EVSE";
            // 
            // lbl_barcode_2
            // 
            this.lbl_barcode_2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_2.Location = new System.Drawing.Point(5, 185);
            this.lbl_barcode_2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_barcode_2.Name = "lbl_barcode_2";
            this.lbl_barcode_2.Size = new System.Drawing.Size(237, 69);
            this.lbl_barcode_2.TabIndex = 10;
            this.lbl_barcode_2.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_2
            // 
            this.lbl_rfid_2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_2.Location = new System.Drawing.Point(5, 139);
            this.lbl_rfid_2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_rfid_2.Name = "lbl_rfid_2";
            this.lbl_rfid_2.Size = new System.Drawing.Size(237, 36);
            this.lbl_rfid_2.TabIndex = 11;
            this.lbl_rfid_2.Text = "           RF-ID";
            // 
            // Test_lizdas_1
            // 
            this.Test_lizdas_1.Controls.Add(this.lbl_evse1);
            this.Test_lizdas_1.Controls.Add(this.lbl_barcode_1);
            this.Test_lizdas_1.Controls.Add(this.lbl_rfid_1);
            this.Test_lizdas_1.Location = new System.Drawing.Point(419, 18);
            this.Test_lizdas_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_1.Name = "Test_lizdas_1";
            this.Test_lizdas_1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Test_lizdas_1.Size = new System.Drawing.Size(264, 274);
            this.Test_lizdas_1.TabIndex = 10;
            this.Test_lizdas_1.TabStop = false;
            this.Test_lizdas_1.Text = "1";
            // 
            // lbl_evse1
            // 
            this.lbl_evse1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse1.Location = new System.Drawing.Point(5, 92);
            this.lbl_evse1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_evse1.Name = "lbl_evse1";
            this.lbl_evse1.Size = new System.Drawing.Size(237, 37);
            this.lbl_evse1.TabIndex = 12;
            this.lbl_evse1.Text = "           EVSE";
            // 
            // lbl_barcode_1
            // 
            this.lbl_barcode_1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_1.Location = new System.Drawing.Point(5, 185);
            this.lbl_barcode_1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_barcode_1.Name = "lbl_barcode_1";
            this.lbl_barcode_1.Size = new System.Drawing.Size(237, 69);
            this.lbl_barcode_1.TabIndex = 10;
            this.lbl_barcode_1.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_1
            // 
            this.lbl_rfid_1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_1.Location = new System.Drawing.Point(5, 139);
            this.lbl_rfid_1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_rfid_1.Name = "lbl_rfid_1";
            this.lbl_rfid_1.Size = new System.Drawing.Size(237, 36);
            this.lbl_rfid_1.TabIndex = 11;
            this.lbl_rfid_1.Text = "           RF-ID";
            // 
            // btn_popup
            // 
            this.btn_popup.Location = new System.Drawing.Point(1167, 705);
            this.btn_popup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_popup.Name = "btn_popup";
            this.btn_popup.Size = new System.Drawing.Size(123, 55);
            this.btn_popup.TabIndex = 9;
            this.btn_popup.Text = "popup";
            this.btn_popup.UseVisualStyleBackColor = true;
            this.btn_popup.Click += new System.EventHandler(this.btn_popup_Click);
            // 
            // metrel_skip_btn
            // 
            this.metrel_skip_btn.Enabled = false;
            this.metrel_skip_btn.Location = new System.Drawing.Point(1013, 646);
            this.metrel_skip_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metrel_skip_btn.Name = "metrel_skip_btn";
            this.metrel_skip_btn.Size = new System.Drawing.Size(131, 47);
            this.metrel_skip_btn.TabIndex = 8;
            this.metrel_skip_btn.Text = "Skip";
            this.metrel_skip_btn.UseVisualStyleBackColor = true;
            this.metrel_skip_btn.Visible = false;
            this.metrel_skip_btn.Click += new System.EventHandler(this.metrel_skip_btn_Click);
            // 
            // metrel_break_btn
            // 
            this.metrel_break_btn.Enabled = false;
            this.metrel_break_btn.Location = new System.Drawing.Point(1017, 586);
            this.metrel_break_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metrel_break_btn.Name = "metrel_break_btn";
            this.metrel_break_btn.Size = new System.Drawing.Size(131, 47);
            this.metrel_break_btn.TabIndex = 7;
            this.metrel_break_btn.Text = "Break";
            this.metrel_break_btn.UseVisualStyleBackColor = true;
            this.metrel_break_btn.Visible = false;
            this.metrel_break_btn.Click += new System.EventHandler(this.metrel_break_btn_Click);
            // 
            // Met_proceed_btn
            // 
            this.Met_proceed_btn.Enabled = false;
            this.Met_proceed_btn.Location = new System.Drawing.Point(1017, 527);
            this.Met_proceed_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Met_proceed_btn.Name = "Met_proceed_btn";
            this.Met_proceed_btn.Size = new System.Drawing.Size(131, 47);
            this.Met_proceed_btn.TabIndex = 6;
            this.Met_proceed_btn.Text = "Proceed";
            this.Met_proceed_btn.UseVisualStyleBackColor = true;
            this.Met_proceed_btn.Visible = false;
            this.Met_proceed_btn.Click += new System.EventHandler(this.Met_proceed_btn_Click);
            // 
            // metrel_stop_btn
            // 
            this.metrel_stop_btn.Enabled = false;
            this.metrel_stop_btn.Location = new System.Drawing.Point(1199, 591);
            this.metrel_stop_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metrel_stop_btn.Name = "metrel_stop_btn";
            this.metrel_stop_btn.Size = new System.Drawing.Size(91, 47);
            this.metrel_stop_btn.TabIndex = 5;
            this.metrel_stop_btn.Text = "Stop";
            this.metrel_stop_btn.UseVisualStyleBackColor = true;
            this.metrel_stop_btn.Visible = false;
            this.metrel_stop_btn.Click += new System.EventHandler(this.metrel_stop_btn_Click);
            // 
            // metrel_start_btn
            // 
            this.metrel_start_btn.Enabled = false;
            this.metrel_start_btn.Location = new System.Drawing.Point(1199, 534);
            this.metrel_start_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metrel_start_btn.Name = "metrel_start_btn";
            this.metrel_start_btn.Size = new System.Drawing.Size(91, 47);
            this.metrel_start_btn.TabIndex = 4;
            this.metrel_start_btn.Text = "Start";
            this.metrel_start_btn.UseVisualStyleBackColor = true;
            this.metrel_start_btn.Visible = false;
            this.metrel_start_btn.Click += new System.EventHandler(this.metrel_start_btn_Click);
            // 
            // metrel_auto_btn
            // 
            this.metrel_auto_btn.Enabled = false;
            this.metrel_auto_btn.Location = new System.Drawing.Point(791, 494);
            this.metrel_auto_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metrel_auto_btn.Name = "metrel_auto_btn";
            this.metrel_auto_btn.Size = new System.Drawing.Size(161, 95);
            this.metrel_auto_btn.TabIndex = 3;
            this.metrel_auto_btn.Text = "Metrel\r\nAUTO";
            this.metrel_auto_btn.UseVisualStyleBackColor = true;
            this.metrel_auto_btn.Visible = false;
            this.metrel_auto_btn.Click += new System.EventHandler(this.metrel_auto_btn_Click);
            // 
            // mtrelTest
            // 
            this.mtrelTest.Enabled = false;
            this.mtrelTest.Location = new System.Drawing.Point(788, 610);
            this.mtrelTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtrelTest.Name = "mtrelTest";
            this.mtrelTest.Size = new System.Drawing.Size(161, 95);
            this.mtrelTest.TabIndex = 2;
            this.mtrelTest.Text = "Metrel\r\nSingle";
            this.mtrelTest.UseVisualStyleBackColor = true;
            this.mtrelTest.Visible = false;
            this.mtrelTest.Click += new System.EventHandler(this.mtrelTest_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(55, 18);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 165);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label4.Location = new System.Drawing.Point(61, 71);
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
            this.label5.Location = new System.Drawing.Point(41, 27);
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
            this.panel1.Location = new System.Drawing.Point(8, 230);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 530);
            this.panel1.TabIndex = 0;
            // 
            // lbl_evse
            // 
            this.lbl_evse.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse.Location = new System.Drawing.Point(45, 369);
            this.lbl_evse.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_evse.Name = "lbl_evse";
            this.lbl_evse.Size = new System.Drawing.Size(235, 69);
            this.lbl_evse.TabIndex = 9;
            this.lbl_evse.Text = "   EVSE Testas";
            // 
            // lbl_load
            // 
            this.lbl_load.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_load.Location = new System.Drawing.Point(43, 255);
            this.lbl_load.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_load.Name = "lbl_load";
            this.lbl_load.Size = new System.Drawing.Size(237, 69);
            this.lbl_load.TabIndex = 8;
            this.lbl_load.Text = "        Apkrova";
            // 
            // lbl_specrum
            // 
            this.lbl_specrum.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_specrum.Location = new System.Drawing.Point(43, 176);
            this.lbl_specrum.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_specrum.Name = "lbl_specrum";
            this.lbl_specrum.Size = new System.Drawing.Size(237, 69);
            this.lbl_specrum.TabIndex = 7;
            this.lbl_specrum.Text = "         Spektro \r\n    analizatorius";
            // 
            // lbl_vald
            // 
            this.lbl_vald.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_vald.Location = new System.Drawing.Point(43, 18);
            this.lbl_vald.Name = "lbl_vald";
            this.lbl_vald.Size = new System.Drawing.Size(237, 69);
            this.lbl_vald.TabIndex = 6;
            this.lbl_vald.Text = "        Valdiklis      ";
            // 
            // lbl_hvgen
            // 
            this.lbl_hvgen.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_hvgen.Location = new System.Drawing.Point(43, 97);
            this.lbl_hvgen.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_hvgen.Name = "lbl_hvgen";
            this.lbl_hvgen.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_hvgen.Size = new System.Drawing.Size(237, 69);
            this.lbl_hvgen.TabIndex = 5;
            this.lbl_hvgen.Text = "            HV\r\n   Generatorius  ";
            // 
            // lbl_osc
            // 
            this.lbl_osc.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_osc.Location = new System.Drawing.Point(41, 448);
            this.lbl_osc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lbl_osc.Name = "lbl_osc";
            this.lbl_osc.Size = new System.Drawing.Size(239, 69);
            this.lbl_osc.TabIndex = 4;
            this.lbl_osc.Text = "   Osciloskopas";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1315, 766);
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
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(429, 350);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Darbo vieta";
            // 
            // saveWplace
            // 
            this.saveWplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveWplace.Location = new System.Drawing.Point(223, 283);
            this.saveWplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveWplace.Name = "saveWplace";
            this.saveWplace.Size = new System.Drawing.Size(123, 42);
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
            this.tabPage3.Controls.Add(this.groupBox_Rfid);
            this.tabPage3.Controls.Add(this.groupBox_Barcode);
            this.tabPage3.Controls.Add(this.save_ip);
            this.tabPage3.Controls.Add(this.groupBox_Metrel_USB);
            this.tabPage3.Controls.Add(this.groupBox_Valdiklis);
            this.tabPage3.Controls.Add(this.groupBox_Load);
            this.tabPage3.Controls.Add(this.groupBox_Spectr);
            this.tabPage3.Controls.Add(this.groupBox_HVgen);
            this.tabPage3.Controls.Add(this.groupBox_Osc_USB);
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(1315, 766);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Įranga";
            // 
            // groupBox_Rfid
            // 
            this.groupBox_Rfid.Controls.Add(this.label19);
            this.groupBox_Rfid.Controls.Add(this.label18);
            this.groupBox_Rfid.Controls.Add(this.label10);
            this.groupBox_Rfid.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Rfid.Location = new System.Drawing.Point(629, 516);
            this.groupBox_Rfid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Rfid.Name = "groupBox_Rfid";
            this.groupBox_Rfid.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Rfid.Size = new System.Drawing.Size(589, 194);
            this.groupBox_Rfid.TabIndex = 14;
            this.groupBox_Rfid.TabStop = false;
            this.groupBox_Rfid.Text = "Testavimo lizdai";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(12, 154);
            this.label19.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 22);
            this.label19.TabIndex = 4;
            this.label19.Text = "3  ADDR";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 103);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 22);
            this.label18.TabIndex = 3;
            this.label18.Text = "2  ADDR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 60);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 22);
            this.label10.TabIndex = 2;
            this.label10.Text = "1  ADDR";
            // 
            // groupBox_Barcode
            // 
            this.groupBox_Barcode.Controls.Add(this.label12);
            this.groupBox_Barcode.Controls.Add(this.label11);
            this.groupBox_Barcode.Controls.Add(this.label9);
            this.groupBox_Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Barcode.Location = new System.Drawing.Point(27, 516);
            this.groupBox_Barcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Barcode.Name = "groupBox_Barcode";
            this.groupBox_Barcode.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Barcode.Size = new System.Drawing.Size(584, 194);
            this.groupBox_Barcode.TabIndex = 13;
            this.groupBox_Barcode.TabStop = false;
            this.groupBox_Barcode.Text = " Skaitytuvai";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 154);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 22);
            this.label12.TabIndex = 4;
            this.label12.Text = "3  IP:PORT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 107);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 22);
            this.label11.TabIndex = 3;
            this.label11.Text = "2  IP:PORT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 22);
            this.label9.TabIndex = 2;
            this.label9.Text = "1  IP:PORT";
            // 
            // save_ip
            // 
            this.save_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_ip.Location = new System.Drawing.Point(1041, 378);
            this.save_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save_ip.Name = "save_ip";
            this.save_ip.Size = new System.Drawing.Size(147, 44);
            this.save_ip.TabIndex = 12;
            this.save_ip.Text = "Išsaugoti";
            this.save_ip.UseVisualStyleBackColor = true;
            this.save_ip.Click += new System.EventHandler(this.save_ip_Click);
            // 
            // groupBox_Metrel_USB
            // 
            this.groupBox_Metrel_USB.Controls.Add(this.label16);
            this.groupBox_Metrel_USB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Metrel_USB.Location = new System.Drawing.Point(629, 31);
            this.groupBox_Metrel_USB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Metrel_USB.Name = "groupBox_Metrel_USB";
            this.groupBox_Metrel_USB.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Metrel_USB.Size = new System.Drawing.Size(589, 110);
            this.groupBox_Metrel_USB.TabIndex = 7;
            this.groupBox_Metrel_USB.TabStop = false;
            this.groupBox_Metrel_USB.Text = "EVSE (Metrel)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(19, 60);
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
            this.groupBox_Valdiklis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Valdiklis.Name = "groupBox_Valdiklis";
            this.groupBox_Valdiklis.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Valdiklis.Size = new System.Drawing.Size(584, 110);
            this.groupBox_Valdiklis.TabIndex = 11;
            this.groupBox_Valdiklis.TabStop = false;
            this.groupBox_Valdiklis.Text = "Valdiklis";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 60);
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
            this.groupBox_Load.Location = new System.Drawing.Point(27, 386);
            this.groupBox_Load.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Load.Name = "groupBox_Load";
            this.groupBox_Load.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Load.Size = new System.Drawing.Size(584, 110);
            this.groupBox_Load.TabIndex = 10;
            this.groupBox_Load.TabStop = false;
            this.groupBox_Load.Text = " AC apkrova";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(12, 60);
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
            this.groupBox_Spectr.Location = new System.Drawing.Point(27, 263);
            this.groupBox_Spectr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Spectr.Name = "groupBox_Spectr";
            this.groupBox_Spectr.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Spectr.Size = new System.Drawing.Size(584, 106);
            this.groupBox_Spectr.TabIndex = 9;
            this.groupBox_Spectr.TabStop = false;
            this.groupBox_Spectr.Text = "Spektro analizatorius";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 60);
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
            this.groupBox_HVgen.Location = new System.Drawing.Point(27, 146);
            this.groupBox_HVgen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_HVgen.Name = "groupBox_HVgen";
            this.groupBox_HVgen.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_HVgen.Size = new System.Drawing.Size(584, 111);
            this.groupBox_HVgen.TabIndex = 8;
            this.groupBox_HVgen.TabStop = false;
            this.groupBox_HVgen.Text = "HV generatorius";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 60);
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
            this.groupBox_Osc_USB.Location = new System.Drawing.Point(629, 146);
            this.groupBox_Osc_USB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Osc_USB.Name = "groupBox_Osc_USB";
            this.groupBox_Osc_USB.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_Osc_USB.Size = new System.Drawing.Size(589, 111);
            this.groupBox_Osc_USB.TabIndex = 6;
            this.groupBox_Osc_USB.TabStop = false;
            this.groupBox_Osc_USB.Text = "Osciloskopas";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(19, 60);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 22);
            this.label17.TabIndex = 2;
            this.label17.Text = "ID_WORD";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tabControl2);
            this.tabPage4.Location = new System.Drawing.Point(4, 44);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(1315, 766);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Valdymas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Location = new System.Drawing.Point(8, 25);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1300, 697);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox_evse_state);
            this.tabPage5.Controls.Add(this.groupBox_main_relay);
            this.tabPage5.Controls.Add(this.data_grid_main_board);
            this.tabPage5.Controls.Add(this.button1);
            this.tabPage5.Location = new System.Drawing.Point(4, 38);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Size = new System.Drawing.Size(1292, 655);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "  CONTROL_BOARD  ";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // data_grid_main_board
            // 
            this.data_grid_main_board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_grid_main_board.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CMD,
            this.Column23,
            this.Column24,
            this.Column25});
            this.data_grid_main_board.Location = new System.Drawing.Point(43, 296);
            this.data_grid_main_board.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.data_grid_main_board.Name = "data_grid_main_board";
            this.data_grid_main_board.RowHeadersWidth = 51;
            this.data_grid_main_board.Size = new System.Drawing.Size(933, 185);
            this.data_grid_main_board.TabIndex = 1;
            this.data_grid_main_board.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_main_board_click);
            // 
            // CMD
            // 
            this.CMD.HeaderText = "Name";
            this.CMD.MinimumWidth = 6;
            this.CMD.Name = "CMD";
            this.CMD.Width = 125;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "Value";
            this.Column23.MinimumWidth = 6;
            this.Column23.Name = "Column23";
            this.Column23.Width = 125;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "ON";
            this.Column24.MinimumWidth = 6;
            this.Column24.Name = "Column24";
            this.Column24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column24.Width = 125;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "OFF";
            this.Column25.MinimumWidth = 6;
            this.Column25.Name = "Column25";
            this.Column25.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column25.Width = 125;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1061, 535);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 112);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btn_stop);
            this.tabPage7.Controls.Add(this.button5);
            this.tabPage7.Controls.Add(this.dataGrid_HV_result);
            this.tabPage7.Controls.Add(this.dataGrid_HV_test);
            this.tabPage7.Location = new System.Drawing.Point(4, 38);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1292, 655);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "  HV_GEN  ";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(877, 270);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(164, 42);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(709, 270);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 42);
            this.button5.TabIndex = 3;
            this.button5.Text = "get_res";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // dataGrid_HV_result
            // 
            this.dataGrid_HV_result.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_HV_result.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGrid_HV_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_HV_result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column9,
            this.dataGridViewButtonColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid_HV_result.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGrid_HV_result.Location = new System.Drawing.Point(3, 332);
            this.dataGrid_HV_result.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_HV_result.Name = "dataGrid_HV_result";
            this.dataGrid_HV_result.RowHeadersVisible = false;
            this.dataGrid_HV_result.RowHeadersWidth = 51;
            this.dataGrid_HV_result.RowTemplate.Height = 24;
            this.dataGrid_HV_result.Size = new System.Drawing.Size(1063, 170);
            this.dataGrid_HV_result.TabIndex = 2;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "DATE";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.Width = 116;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.HeaderText = "TEST";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 116;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewButtonColumn1.FillWeight = 80F;
            this.dataGridViewButtonColumn1.HeaderText = "RESULT";
            this.dataGridViewButtonColumn1.MinimumWidth = 6;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.Width = 147;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "PARAM1";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 148;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.HeaderText = "PARAM2";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 148;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.HeaderText = "TIME";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 110;
            // 
            // dataGrid_HV_test
            // 
            this.dataGrid_HV_test.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGrid_HV_test.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_HV_test.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGrid_HV_test.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid_HV_test.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_HV_test.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column0,
            this.Column8,
            this.Column7,
            this.Column2,
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column5});
            this.dataGrid_HV_test.Location = new System.Drawing.Point(3, 15);
            this.dataGrid_HV_test.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_HV_test.Name = "dataGrid_HV_test";
            this.dataGrid_HV_test.RowHeadersVisible = false;
            this.dataGrid_HV_test.RowHeadersWidth = 51;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGrid_HV_test.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.RowTemplate.Height = 35;
            this.dataGrid_HV_test.Size = new System.Drawing.Size(1063, 231);
            this.dataGrid_HV_test.TabIndex = 1;
            this.dataGrid_HV_test.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGrid_HV_test.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_HV_test_CellValueChanged);
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.HeaderText = "READY";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // Column0
            // 
            this.Column0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column0.HeaderText = "TEST";
            this.Column0.MinimumWidth = 6;
            this.Column0.Name = "Column0";
            this.Column0.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column0.Width = 106;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.HeaderText = "GET";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column8.UseColumnTextForButtonValue = true;
            this.Column8.Width = 92;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.HeaderText = "SET";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 67;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "V, kV";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 96;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "HI SET";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 119;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "LO SET";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 128;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "REF";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 90;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "TIME";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dataGrid_Spectrum);
            this.tabPage6.Controls.Add(this.button7);
            this.tabPage6.Controls.Add(this.chart1);
            this.tabPage6.Location = new System.Drawing.Point(4, 38);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Size = new System.Drawing.Size(1292, 655);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "  SPECTRUM  ";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGrid_Spectrum
            // 
            this.dataGrid_Spectrum.AllowUserToDeleteRows = false;
            this.dataGrid_Spectrum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Spectrum.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Spectrum.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_Spectrum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGrid_Spectrum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Spectrum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column18,
            this.Column19});
            this.dataGrid_Spectrum.Location = new System.Drawing.Point(767, 31);
            this.dataGrid_Spectrum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Spectrum.Name = "dataGrid_Spectrum";
            this.dataGrid_Spectrum.RowHeadersVisible = false;
            this.dataGrid_Spectrum.RowHeadersWidth = 51;
            this.dataGrid_Spectrum.RowTemplate.Height = 24;
            this.dataGrid_Spectrum.Size = new System.Drawing.Size(356, 432);
            this.dataGrid_Spectrum.TabIndex = 5;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "PARAM";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.Width = 125;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "VALUE";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.Width = 118;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(9, 462);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(149, 73);
            this.button7.TabIndex = 1;
            this.button7.Text = "GET DATA";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(7, 7);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(751, 455);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.button6);
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Controls.Add(this.label6);
            this.tabPage8.Controls.Add(this.dataGrid_Load_Line);
            this.tabPage8.Controls.Add(this.dataGrid_Load_load);
            this.tabPage8.Location = new System.Drawing.Point(4, 38);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1292, 655);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "  Load  ";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(913, 290);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(139, 50);
            this.button6.TabIndex = 6;
            this.button6.Text = "GET";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 32);
            this.label7.TabIndex = 5;
            this.label7.Text = "LINE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 32);
            this.label6.TabIndex = 4;
            this.label6.Text = "LOAD";
            // 
            // dataGrid_Load_Line
            // 
            this.dataGrid_Load_Line.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGrid_Load_Line.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Load_Line.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGrid_Load_Line.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid_Load_Line.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Load_Line.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column17,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.Column12,
            this.dataGridViewTextBoxColumn2});
            this.dataGrid_Load_Line.Location = new System.Drawing.Point(3, 325);
            this.dataGrid_Load_Line.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Load_Line.Name = "dataGrid_Load_Line";
            this.dataGrid_Load_Line.RowHeadersVisible = false;
            this.dataGrid_Load_Line.RowHeadersWidth = 51;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGrid_Load_Line.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.RowTemplate.Height = 35;
            this.dataGrid_Load_Line.Size = new System.Drawing.Size(591, 210);
            this.dataGrid_Load_Line.TabIndex = 3;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "PHASE";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.Width = 122;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.HeaderText = "U";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.Width = 59;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn8.HeaderText = "I";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn8.Width = 48;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn9.HeaderText = "P";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 35;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "POWER";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.Width = 132;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "-----------";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGrid_Load_load
            // 
            this.dataGrid_Load_load.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGrid_Load_load.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Load_load.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGrid_Load_load.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid_Load_load.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Load_load.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn2,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column11,
            this.Column13,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn1});
            this.dataGrid_Load_load.Location = new System.Drawing.Point(3, 58);
            this.dataGrid_Load_load.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Load_load.Name = "dataGrid_Load_load";
            this.dataGrid_Load_load.RowHeadersVisible = false;
            this.dataGrid_Load_load.RowHeadersWidth = 51;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGrid_Load_load.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.RowTemplate.Height = 35;
            this.dataGrid_Load_load.Size = new System.Drawing.Size(983, 220);
            this.dataGrid_Load_load.TabIndex = 2;
            this.dataGrid_Load_load.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Load_load_CellContentClick);
            this.dataGrid_Load_load.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Load_load_CellValueChanged);
            this.dataGrid_Load_load.Enter += new System.EventHandler(this.dataGrid_Load_load_Enter);
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewButtonColumn2.HeaderText = "PHASE";
            this.dataGridViewButtonColumn2.MinimumWidth = 6;
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn2.Width = 122;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "U";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.Width = 59;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "I";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.Width = 48;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "P";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.Width = 58;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "POWER";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.Width = 132;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "MODE";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.Width = 114;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "SET I, A";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 129;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "STATE";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn1.Width = 121;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.groupBoxBarcode3);
            this.tabPage9.Controls.Add(this.groupBoxBarcode2);
            this.tabPage9.Controls.Add(this.groupBoxBarcode1);
            this.tabPage9.Location = new System.Drawing.Point(4, 38);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Size = new System.Drawing.Size(1292, 655);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "  EVSE  ";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // groupBoxBarcode3
            // 
            this.groupBoxBarcode3.Controls.Add(this.label21);
            this.groupBoxBarcode3.Controls.Add(this.dataGrid_Barcode3);
            this.groupBoxBarcode3.Enabled = false;
            this.groupBoxBarcode3.Location = new System.Drawing.Point(871, 16);
            this.groupBoxBarcode3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode3.Name = "groupBoxBarcode3";
            this.groupBoxBarcode3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode3.Size = new System.Drawing.Size(397, 633);
            this.groupBoxBarcode3.TabIndex = 1;
            this.groupBoxBarcode3.TabStop = false;
            this.groupBoxBarcode3.Text = "3";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(159, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(87, 29);
            this.label21.TabIndex = 3;
            this.label21.Text = "SSID 3";
            // 
            // dataGrid_Barcode3
            // 
            this.dataGrid_Barcode3.AllowUserToDeleteRows = false;
            this.dataGrid_Barcode3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Barcode3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Barcode3.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGrid_Barcode3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Barcode3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13});
            this.dataGrid_Barcode3.Location = new System.Drawing.Point(12, 78);
            this.dataGrid_Barcode3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Barcode3.Name = "dataGrid_Barcode3";
            this.dataGrid_Barcode3.RowHeadersVisible = false;
            this.dataGrid_Barcode3.RowHeadersWidth = 51;
            this.dataGrid_Barcode3.RowTemplate.Height = 24;
            this.dataGrid_Barcode3.Size = new System.Drawing.Size(380, 377);
            this.dataGrid_Barcode3.TabIndex = 0;
            this.dataGrid_Barcode3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Barcode3_CellContentClick);
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "CMD";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn12.Width = 96;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "RESULT";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 137;
            // 
            // groupBoxBarcode2
            // 
            this.groupBoxBarcode2.Controls.Add(this.evse2_params);
            this.groupBoxBarcode2.Controls.Add(this.label20);
            this.groupBoxBarcode2.Controls.Add(this.dataGrid_Barcode2);
            this.groupBoxBarcode2.Enabled = false;
            this.groupBoxBarcode2.Location = new System.Drawing.Point(437, 16);
            this.groupBoxBarcode2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode2.Name = "groupBoxBarcode2";
            this.groupBoxBarcode2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode2.Size = new System.Drawing.Size(397, 633);
            this.groupBoxBarcode2.TabIndex = 1;
            this.groupBoxBarcode2.TabStop = false;
            this.groupBoxBarcode2.Text = "2";
            // 
            // evse2_params
            // 
            this.evse2_params.AutoSize = true;
            this.evse2_params.Location = new System.Drawing.Point(21, 466);
            this.evse2_params.Name = "evse2_params";
            this.evse2_params.Size = new System.Drawing.Size(37, 29);
            this.evse2_params.TabIndex = 2;
            this.evse2_params.Text = "---";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(153, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 29);
            this.label20.TabIndex = 2;
            this.label20.Text = "SSID 2";
            // 
            // dataGrid_Barcode2
            // 
            this.dataGrid_Barcode2.AllowUserToDeleteRows = false;
            this.dataGrid_Barcode2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Barcode2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Barcode2.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGrid_Barcode2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Barcode2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.dataGrid_Barcode2.Location = new System.Drawing.Point(12, 78);
            this.dataGrid_Barcode2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Barcode2.Name = "dataGrid_Barcode2";
            this.dataGrid_Barcode2.RowHeadersVisible = false;
            this.dataGrid_Barcode2.RowHeadersWidth = 51;
            this.dataGrid_Barcode2.RowTemplate.Height = 24;
            this.dataGrid_Barcode2.Size = new System.Drawing.Size(380, 377);
            this.dataGrid_Barcode2.TabIndex = 0;
            this.dataGrid_Barcode2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Barcode2_CellContentClick);
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "CMD";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn10.Width = 96;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "RESULT";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 137;
            // 
            // groupBoxBarcode1
            // 
            this.groupBoxBarcode1.Controls.Add(this.sta);
            this.groupBoxBarcode1.Controls.Add(this.dataGrid_Barcode1);
            this.groupBoxBarcode1.Enabled = false;
            this.groupBoxBarcode1.Location = new System.Drawing.Point(5, 16);
            this.groupBoxBarcode1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode1.Name = "groupBoxBarcode1";
            this.groupBoxBarcode1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBarcode1.Size = new System.Drawing.Size(397, 633);
            this.groupBoxBarcode1.TabIndex = 0;
            this.groupBoxBarcode1.TabStop = false;
            this.groupBoxBarcode1.Text = "1";
            // 
            // sta
            // 
            this.sta.AutoSize = true;
            this.sta.Location = new System.Drawing.Point(147, 18);
            this.sta.Name = "sta";
            this.sta.Size = new System.Drawing.Size(87, 29);
            this.sta.TabIndex = 1;
            this.sta.Text = "SSID 1";
            // 
            // dataGrid_Barcode1
            // 
            this.dataGrid_Barcode1.AllowUserToDeleteRows = false;
            this.dataGrid_Barcode1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Barcode1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Barcode1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGrid_Barcode1.ColumnHeadersHeight = 45;
            this.dataGrid_Barcode1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid_Barcode1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column20,
            this.Column21});
            this.dataGrid_Barcode1.Location = new System.Drawing.Point(12, 78);
            this.dataGrid_Barcode1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid_Barcode1.Name = "dataGrid_Barcode1";
            this.dataGrid_Barcode1.RowHeadersVisible = false;
            this.dataGrid_Barcode1.RowHeadersWidth = 51;
            this.dataGrid_Barcode1.RowTemplate.Height = 35;
            this.dataGrid_Barcode1.Size = new System.Drawing.Size(380, 377);
            this.dataGrid_Barcode1.TabIndex = 0;
            this.dataGrid_Barcode1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Barcode1_CellContentClick);
            // 
            // Column20
            // 
            this.Column20.HeaderText = "CMD";
            this.Column20.MinimumWidth = 6;
            this.Column20.Name = "Column20";
            this.Column20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column20.Width = 96;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "RESULT";
            this.Column21.MinimumWidth = 6;
            this.Column21.Name = "Column21";
            this.Column21.Width = 137;
            // 
            // debug_tab
            // 
            this.debug_tab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.debug_tab.Controls.Add(this.debug_ping_cbox);
            this.debug_tab.Controls.Add(this.debug_load_cbox);
            this.debug_tab.Controls.Add(this.debug_siglent_cbox);
            this.debug_tab.Controls.Add(this.debug_gwinstek_cbox);
            this.debug_tab.Controls.Add(this.debug_evse_cbox);
            this.debug_tab.Controls.Add(this.debug_main_cbox);
            this.debug_tab.Controls.Add(this.dbg_list_clear);
            this.debug_tab.Controls.Add(this.debug_usb_cbox);
            this.debug_tab.Controls.Add(this.debug_network_cbox);
            this.debug_tab.Controls.Add(this.list_debug);
            this.debug_tab.Location = new System.Drawing.Point(4, 44);
            this.debug_tab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_tab.Name = "debug_tab";
            this.debug_tab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_tab.Size = new System.Drawing.Size(1315, 766);
            this.debug_tab.TabIndex = 3;
            this.debug_tab.Text = "Debug";
            // 
            // debug_ping_cbox
            // 
            this.debug_ping_cbox.AutoSize = true;
            this.debug_ping_cbox.Location = new System.Drawing.Point(945, 402);
            this.debug_ping_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_ping_cbox.Name = "debug_ping_cbox";
            this.debug_ping_cbox.Size = new System.Drawing.Size(93, 33);
            this.debug_ping_cbox.TabIndex = 9;
            this.debug_ping_cbox.Text = "PING";
            this.debug_ping_cbox.UseVisualStyleBackColor = true;
            this.debug_ping_cbox.CheckedChanged += new System.EventHandler(this.debug_ping_cbox_CheckedChanged);
            // 
            // debug_load_cbox
            // 
            this.debug_load_cbox.AutoSize = true;
            this.debug_load_cbox.Location = new System.Drawing.Point(945, 306);
            this.debug_load_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_load_cbox.Name = "debug_load_cbox";
            this.debug_load_cbox.Size = new System.Drawing.Size(222, 33);
            this.debug_load_cbox.TabIndex = 8;
            this.debug_load_cbox.Text = "EVSE/BARCODE";
            this.debug_load_cbox.UseVisualStyleBackColor = true;
            this.debug_load_cbox.CheckedChanged += new System.EventHandler(this.debug_load_cbox_CheckedChanged);
            // 
            // debug_siglent_cbox
            // 
            this.debug_siglent_cbox.AutoSize = true;
            this.debug_siglent_cbox.Location = new System.Drawing.Point(945, 250);
            this.debug_siglent_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_siglent_cbox.Name = "debug_siglent_cbox";
            this.debug_siglent_cbox.Size = new System.Drawing.Size(262, 33);
            this.debug_siglent_cbox.TabIndex = 7;
            this.debug_siglent_cbox.Text = "SPECTRO/SIGLENT";
            this.debug_siglent_cbox.UseVisualStyleBackColor = true;
            this.debug_siglent_cbox.CheckedChanged += new System.EventHandler(this.debug_siglent_cbox_CheckedChanged);
            // 
            // debug_gwinstek_cbox
            // 
            this.debug_gwinstek_cbox.AutoSize = true;
            this.debug_gwinstek_cbox.Location = new System.Drawing.Point(945, 201);
            this.debug_gwinstek_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_gwinstek_cbox.Name = "debug_gwinstek_cbox";
            this.debug_gwinstek_cbox.Size = new System.Drawing.Size(202, 33);
            this.debug_gwinstek_cbox.TabIndex = 6;
            this.debug_gwinstek_cbox.Text = "HV/GWINSTEK";
            this.debug_gwinstek_cbox.UseVisualStyleBackColor = true;
            this.debug_gwinstek_cbox.CheckedChanged += new System.EventHandler(this.debug_gwinstek_cbox_CheckedChanged);
            // 
            // debug_evse_cbox
            // 
            this.debug_evse_cbox.AutoSize = true;
            this.debug_evse_cbox.Location = new System.Drawing.Point(945, 151);
            this.debug_evse_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_evse_cbox.Name = "debug_evse_cbox";
            this.debug_evse_cbox.Size = new System.Drawing.Size(222, 33);
            this.debug_evse_cbox.TabIndex = 5;
            this.debug_evse_cbox.Text = "EVSE/BARCODE";
            this.debug_evse_cbox.UseVisualStyleBackColor = true;
            this.debug_evse_cbox.CheckedChanged += new System.EventHandler(this.debug_evse_cbox_CheckedChanged);
            // 
            // debug_main_cbox
            // 
            this.debug_main_cbox.AutoSize = true;
            this.debug_main_cbox.Location = new System.Drawing.Point(945, 6);
            this.debug_main_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_main_cbox.Name = "debug_main_cbox";
            this.debug_main_cbox.Size = new System.Drawing.Size(87, 33);
            this.debug_main_cbox.TabIndex = 4;
            this.debug_main_cbox.Text = "Main";
            this.debug_main_cbox.UseVisualStyleBackColor = true;
            this.debug_main_cbox.CheckedChanged += new System.EventHandler(this.debug_main_cbox_CheckedChanged);
            // 
            // dbg_list_clear
            // 
            this.dbg_list_clear.Location = new System.Drawing.Point(945, 462);
            this.dbg_list_clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dbg_list_clear.Name = "dbg_list_clear";
            this.dbg_list_clear.Size = new System.Drawing.Size(200, 39);
            this.dbg_list_clear.TabIndex = 3;
            this.dbg_list_clear.Text = "Clear";
            this.dbg_list_clear.UseVisualStyleBackColor = true;
            this.dbg_list_clear.Click += new System.EventHandler(this.dbg_list_clear_Click);
            // 
            // debug_usb_cbox
            // 
            this.debug_usb_cbox.AutoSize = true;
            this.debug_usb_cbox.Location = new System.Drawing.Point(945, 101);
            this.debug_usb_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_usb_cbox.Name = "debug_usb_cbox";
            this.debug_usb_cbox.Size = new System.Drawing.Size(84, 33);
            this.debug_usb_cbox.TabIndex = 2;
            this.debug_usb_cbox.Text = "USB";
            this.debug_usb_cbox.UseVisualStyleBackColor = true;
            this.debug_usb_cbox.CheckedChanged += new System.EventHandler(this.debug_usb_CheckedChanged);
            // 
            // debug_network_cbox
            // 
            this.debug_network_cbox.AutoSize = true;
            this.debug_network_cbox.Location = new System.Drawing.Point(945, 53);
            this.debug_network_cbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debug_network_cbox.Name = "debug_network_cbox";
            this.debug_network_cbox.Size = new System.Drawing.Size(125, 33);
            this.debug_network_cbox.TabIndex = 1;
            this.debug_network_cbox.Text = "Network";
            this.debug_network_cbox.UseVisualStyleBackColor = true;
            this.debug_network_cbox.CheckedChanged += new System.EventHandler(this.debug_cbox_CheckedChanged);
            // 
            // list_debug
            // 
            this.list_debug.BackColor = System.Drawing.Color.WhiteSmoke;
            this.list_debug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1});
            this.list_debug.GridLines = true;
            this.list_debug.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.list_debug.HideSelection = false;
            this.list_debug.Location = new System.Drawing.Point(3, 6);
            this.list_debug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.list_debug.Name = "list_debug";
            this.list_debug.Size = new System.Drawing.Size(927, 600);
            this.list_debug.TabIndex = 0;
            this.list_debug.UseCompatibleStateImageBehavior = false;
            this.list_debug.View = System.Windows.Forms.View.Details;
            // 
            // col1
            // 
            this.col1.Name = "col1";
            this.col1.Text = "";
            this.col1.Width = 600;
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
            // cbAdmin
            // 
            this.cbAdmin.AutoSize = true;
            this.cbAdmin.Checked = true;
            this.cbAdmin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAdmin.Location = new System.Drawing.Point(59, 94);
            this.cbAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAdmin.Name = "cbAdmin";
            this.cbAdmin.Size = new System.Drawing.Size(179, 20);
            this.cbAdmin.TabIndex = 2;
            this.cbAdmin.Text = "Aadministratoriaus teises";
            this.cbAdmin.UseVisualStyleBackColor = true;
            // 
            // panelTestResult
            // 
            this.panelTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTestResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTestResult.Controls.Add(this.cbAdmin);
            this.panelTestResult.Location = new System.Drawing.Point(940, 833);
            this.panelTestResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTestResult.Name = "panelTestResult";
            this.panelTestResult.Size = new System.Drawing.Size(381, 192);
            this.panelTestResult.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(17, 884);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(176, 94);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Pradėti";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox_main_relay
            // 
            this.groupBox_main_relay.Location = new System.Drawing.Point(43, 27);
            this.groupBox_main_relay.Name = "groupBox_main_relay";
            this.groupBox_main_relay.Size = new System.Drawing.Size(1184, 104);
            this.groupBox_main_relay.TabIndex = 2;
            this.groupBox_main_relay.TabStop = false;
            this.groupBox_main_relay.Text = "PWR";
            // 
            // groupBox_evse_state
            // 
            this.groupBox_evse_state.Location = new System.Drawing.Point(43, 158);
            this.groupBox_evse_state.Name = "groupBox_evse_state";
            this.groupBox_evse_state.Size = new System.Drawing.Size(1184, 104);
            this.groupBox_evse_state.TabIndex = 3;
            this.groupBox_evse_state.TabStop = false;
            this.groupBox_evse_state.Text = "EV_MODE";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 1025);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panelTestResult);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.Text = "Monitors Test v03";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.Test_lizdas_3.ResumeLayout(false);
            this.Test_lizdas_2.ResumeLayout(false);
            this.Test_lizdas_1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox_Rfid.ResumeLayout(false);
            this.groupBox_Rfid.PerformLayout();
            this.groupBox_Barcode.ResumeLayout(false);
            this.groupBox_Barcode.PerformLayout();
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
            this.tabPage4.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_grid_main_board)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_test)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Spectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Load_Line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Load_load)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.groupBoxBarcode3.ResumeLayout(false);
            this.groupBoxBarcode3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode3)).EndInit();
            this.groupBoxBarcode2.ResumeLayout(false);
            this.groupBoxBarcode2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode2)).EndInit();
            this.groupBoxBarcode1.ResumeLayout(false);
            this.groupBoxBarcode1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Barcode1)).EndInit();
            this.debug_tab.ResumeLayout(false);
            this.debug_tab.PerformLayout();
            this.panelTestResult.ResumeLayout(false);
            this.panelTestResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
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
        private System.ComponentModel.BackgroundWorker NetworkDevConn;
        public System.ComponentModel.BackgroundWorker MainControllerTCP;
        private System.ComponentModel.BackgroundWorker MainControllerMODBUS;
        private System.Windows.Forms.Button btn_popup;
        public System.ComponentModel.BackgroundWorker HVgen;
        public System.ComponentModel.BackgroundWorker Specroscope;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        public System.Windows.Forms.DataGridView dataGrid_HV_test;
        public System.Windows.Forms.DataGridView dataGrid_HV_result;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox debug_network_cbox;
        private System.Windows.Forms.CheckBox debug_usb_cbox;
        private ColumnHeader col1;
        private System.Windows.Forms.Button dbg_list_clear;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn dataGridViewButtonColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewButtonColumn Column0;
        private DataGridViewButtonColumn Column8;
        private DataGridViewButtonColumn Column7;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btn_stop;
        public DataGridView dataGrid_Load_load;
        private Label label7;
        private Label label6;
        public DataGridView dataGrid_Load_Line;
        private System.Windows.Forms.Button button6;
        public System.ComponentModel.BackgroundWorker Load;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewButtonColumn2;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewButtonColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button7;
        public DataGridView dataGrid_Spectrum;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private Label lbl_barcode_1;
        private GroupBox groupBox_Barcode;
        private Label label9;
        private GroupBox Test_lizdas_1;
        private GroupBox Test_lizdas_2;
        private Label lbl_barcode_2;
        private Label lbl_rfid_2;
        private Label lbl_rfid_1;
        private GroupBox Test_lizdas_3;
        private Label lbl_barcode_3;
        private Label lbl_rfid_3;
        private CheckBox debug_main_cbox;
        private GroupBox groupBox_Rfid;
        private Label label19;
        private Label label18;
        private Label label10;
        private Label label12;
        private Label label11;
        private TabPage tabPage9;
        public GroupBox groupBoxBarcode1;
        public DataGridView dataGrid_Barcode1;
        public GroupBox groupBoxBarcode3;
        public DataGridView dataGrid_Barcode3;
        public GroupBox groupBoxBarcode2;
        public DataGridView dataGrid_Barcode2;
        private Label sta;
        private Label label21;
        private Label label20;
        private DataGridViewButtonColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewButtonColumn Column20;
        private DataGridViewTextBoxColumn Column21;
        private DataGridViewButtonColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        public System.ComponentModel.BackgroundWorker Barcode1;
        public System.ComponentModel.BackgroundWorker Barcode2;
        public System.ComponentModel.BackgroundWorker Barcode3;
        public Label evse2_params;
        public Label lbl_evse3;
        public Label lbl_evse2;
        public Label lbl_evse1;
        private CheckBox debug_evse_cbox;
        private CheckBox cbAdmin;
        private Panel panelTestResult;
        private System.Windows.Forms.Button btnStart;
        private CheckBox debug_ping_cbox;
        private CheckBox debug_load_cbox;
        private CheckBox debug_siglent_cbox;
        private CheckBox debug_gwinstek_cbox;
        private System.Windows.Forms.Button button1;
        private DataGridView data_grid_main_board;
        private DataGridViewTextBoxColumn CMD;
        private DataGridViewTextBoxColumn Column23;
        private DataGridViewButtonColumn Column24;
        private DataGridViewButtonColumn Column25;
        private GroupBox groupBox_main_relay;
        private GroupBox groupBox_evse_state;
    }
}

