
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_print_label = new System.Windows.Forms.Button();
            this.button_reset_modular_III = new System.Windows.Forms.Button();
            this.button_reset_modular_II = new System.Windows.Forms.Button();
            this.button_reset_modular_I = new System.Windows.Forms.Button();
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
            this.lbl_printer = new System.Windows.Forms.Label();
            this.lbl_evse = new System.Windows.Forms.Label();
            this.lbl_load = new System.Windows.Forms.Label();
            this.lbl_specrum = new System.Windows.Forms.Label();
            this.lbl_vald = new System.Windows.Forms.Label();
            this.lbl_hvgen = new System.Windows.Forms.Label();
            this.lbl_osc = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label47 = new System.Windows.Forms.Label();
            this.tb_test_param_ihold_32a_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_ihold_32a_min = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.tb_test_param_ihold_6a_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_ihold_6a_min = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.tb_test_param_firmware_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_firmware_min = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.tb_test_param_rfid_range_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_rfid_range_min = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.tb_test_param_gsm_speed_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_gsm_speed_min = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tb_test_param_wifi_speed_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_wifi_speed_min = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.tb_test_param_resid_dc_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_resid_dc_min = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.tb_test_param_r_gnd_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_r_gnd_min = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tb_test_param_insul_dc_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_insul_dc_min = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tb_test_param_insul_ac_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_insul_ac_min = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tb_test_param_pow_mrelay_on_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_pow_mrelay_on_min = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_test_param_pow_mrelay_off_max = new System.Windows.Forms.TextBox();
            this.tb_test_param_pow_mrelay_off_min = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveWplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox_Printer = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_RCD_L3 = new System.Windows.Forms.RadioButton();
            this.radioButton_RCD_L2 = new System.Windows.Forms.RadioButton();
            this.radioButton_RCD_L1 = new System.Windows.Forms.RadioButton();
            this.label_RCD_current = new System.Windows.Forms.Label();
            this.button_RCD_set = new System.Windows.Forms.Button();
            this.RCD_textBox = new System.Windows.Forms.TextBox();
            this.RCD_sel_plus = new System.Windows.Forms.Button();
            this.RCD_sel_minus = new System.Windows.Forms.Button();
            this.groupBox_evse_state = new System.Windows.Forms.GroupBox();
            this.groupBox_main_relay = new System.Windows.Forms.GroupBox();
            this.groupBox_pp_select = new System.Windows.Forms.GroupBox();
            this.groupBox_tp_select = new System.Windows.Forms.GroupBox();
            this.groupBox_checks = new System.Windows.Forms.GroupBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rad_btn_spect_EVSE_sel_3 = new System.Windows.Forms.RadioButton();
            this.rad_btn_spect_EVSE_sel_2 = new System.Windows.Forms.RadioButton();
            this.rad_btn_spect_EVSE_sel_1 = new System.Windows.Forms.RadioButton();
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
            this.PrinterTCP = new System.ComponentModel.BackgroundWorker();
            this.MainControllerMODBUS = new System.ComponentModel.BackgroundWorker();
            this.HVgen = new System.ComponentModel.BackgroundWorker();
            this.Specroscope = new System.ComponentModel.BackgroundWorker();
            this.Load = new System.ComponentModel.BackgroundWorker();
            this.Barcode1 = new System.ComponentModel.BackgroundWorker();
            this.Barcode2 = new System.ComponentModel.BackgroundWorker();
            this.Barcode3 = new System.ComponentModel.BackgroundWorker();
            this.cbAdmin = new System.Windows.Forms.CheckBox();
            this.panelTestResult = new System.Windows.Forms.Panel();
            this.button_load_test_start = new System.Windows.Forms.Button();
            this.progressBar_load_test = new System.Windows.Forms.ProgressBar();
            this.button_load_test_cancel = new System.Windows.Forms.Button();
            this.progressBar_HV_Test = new System.Windows.Forms.ProgressBar();
            this.button_HV_Test_Cancel = new System.Windows.Forms.Button();
            this.button_HV_Test_Start = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button_Spectroscope_Test_Cancel = new System.Windows.Forms.Button();
            this.progressBar_Spectroscope_Test = new System.Windows.Forms.ProgressBar();
            this.button_Oscilloscope_Test = new System.Windows.Forms.Button();
            this.button_Cancel_Oscilloscope = new System.Windows.Forms.Button();
            this.progressBar_Oscilloscope = new System.Windows.Forms.ProgressBar();
            this.button_oscilloscope_pulse = new System.Windows.Forms.Button();
            this.textBox_pulse_length = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label_Estop = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Test_lizdas_3.SuspendLayout();
            this.Test_lizdas_2.SuspendLayout();
            this.Test_lizdas_1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox_Printer.SuspendLayout();
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
            this.groupBox3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_test)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(4, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 661);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.button_print_label);
            this.tabPage1.Controls.Add(this.button_reset_modular_III);
            this.tabPage1.Controls.Add(this.button_reset_modular_II);
            this.tabPage1.Controls.Add(this.button_reset_modular_I);
            this.tabPage1.Controls.Add(this.Test_lizdas_3);
            this.tabPage1.Controls.Add(this.Test_lizdas_2);
            this.tabPage1.Controls.Add(this.Test_lizdas_1);
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
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(984, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pagrindinis";
            // 
            // button_print_label
            // 
            this.button_print_label.BackColor = System.Drawing.Color.LightCyan;
            this.button_print_label.Location = new System.Drawing.Point(322, 401);
            this.button_print_label.Name = "button_print_label";
            this.button_print_label.Size = new System.Drawing.Size(203, 113);
            this.button_print_label.TabIndex = 17;
            this.button_print_label.Text = "Print label";
            this.button_print_label.UseVisualStyleBackColor = false;
            this.button_print_label.Click += new System.EventHandler(this.button_print_label_Click);
            // 
            // button_reset_modular_III
            // 
            this.button_reset_modular_III.Location = new System.Drawing.Point(760, 243);
            this.button_reset_modular_III.Name = "button_reset_modular_III";
            this.button_reset_modular_III.Size = new System.Drawing.Size(117, 37);
            this.button_reset_modular_III.TabIndex = 16;
            this.button_reset_modular_III.Text = "Reset";
            this.button_reset_modular_III.UseVisualStyleBackColor = true;
            this.button_reset_modular_III.Click += new System.EventHandler(this.button_reset_modular_III_Click);
            // 
            // button_reset_modular_II
            // 
            this.button_reset_modular_II.Location = new System.Drawing.Point(548, 243);
            this.button_reset_modular_II.Name = "button_reset_modular_II";
            this.button_reset_modular_II.Size = new System.Drawing.Size(117, 37);
            this.button_reset_modular_II.TabIndex = 15;
            this.button_reset_modular_II.Text = "Reset";
            this.button_reset_modular_II.UseVisualStyleBackColor = true;
            this.button_reset_modular_II.Click += new System.EventHandler(this.button_reset_modular_II_Click);
            // 
            // button_reset_modular_I
            // 
            this.button_reset_modular_I.Location = new System.Drawing.Point(357, 243);
            this.button_reset_modular_I.Name = "button_reset_modular_I";
            this.button_reset_modular_I.Size = new System.Drawing.Size(117, 37);
            this.button_reset_modular_I.TabIndex = 14;
            this.button_reset_modular_I.Text = "Reset";
            this.button_reset_modular_I.UseVisualStyleBackColor = true;
            this.button_reset_modular_I.Click += new System.EventHandler(this.button_reset_modular_I_Click);
            // 
            // Test_lizdas_3
            // 
            this.Test_lizdas_3.Controls.Add(this.lbl_evse3);
            this.Test_lizdas_3.Controls.Add(this.lbl_barcode_3);
            this.Test_lizdas_3.Controls.Add(this.lbl_rfid_3);
            this.Test_lizdas_3.Location = new System.Drawing.Point(725, 15);
            this.Test_lizdas_3.Margin = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_3.Name = "Test_lizdas_3";
            this.Test_lizdas_3.Padding = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_3.Size = new System.Drawing.Size(198, 223);
            this.Test_lizdas_3.TabIndex = 12;
            this.Test_lizdas_3.TabStop = false;
            this.Test_lizdas_3.Text = "3";
            // 
            // lbl_evse3
            // 
            this.lbl_evse3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse3.Location = new System.Drawing.Point(4, 75);
            this.lbl_evse3.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_evse3.Name = "lbl_evse3";
            this.lbl_evse3.Size = new System.Drawing.Size(178, 30);
            this.lbl_evse3.TabIndex = 13;
            this.lbl_evse3.Text = "           EVSE";
            // 
            // lbl_barcode_3
            // 
            this.lbl_barcode_3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_3.Location = new System.Drawing.Point(4, 150);
            this.lbl_barcode_3.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_barcode_3.Name = "lbl_barcode_3";
            this.lbl_barcode_3.Size = new System.Drawing.Size(178, 56);
            this.lbl_barcode_3.TabIndex = 10;
            this.lbl_barcode_3.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_3
            // 
            this.lbl_rfid_3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_3.Location = new System.Drawing.Point(4, 113);
            this.lbl_rfid_3.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_rfid_3.Name = "lbl_rfid_3";
            this.lbl_rfid_3.Size = new System.Drawing.Size(178, 29);
            this.lbl_rfid_3.TabIndex = 11;
            this.lbl_rfid_3.Text = "           RF-ID";
            // 
            // Test_lizdas_2
            // 
            this.Test_lizdas_2.Controls.Add(this.lbl_evse2);
            this.Test_lizdas_2.Controls.Add(this.lbl_barcode_2);
            this.Test_lizdas_2.Controls.Add(this.lbl_rfid_2);
            this.Test_lizdas_2.Location = new System.Drawing.Point(520, 15);
            this.Test_lizdas_2.Margin = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_2.Name = "Test_lizdas_2";
            this.Test_lizdas_2.Padding = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_2.Size = new System.Drawing.Size(198, 223);
            this.Test_lizdas_2.TabIndex = 11;
            this.Test_lizdas_2.TabStop = false;
            this.Test_lizdas_2.Text = "2";
            // 
            // lbl_evse2
            // 
            this.lbl_evse2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse2.Location = new System.Drawing.Point(4, 75);
            this.lbl_evse2.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_evse2.Name = "lbl_evse2";
            this.lbl_evse2.Size = new System.Drawing.Size(178, 30);
            this.lbl_evse2.TabIndex = 13;
            this.lbl_evse2.Text = "           EVSE";
            // 
            // lbl_barcode_2
            // 
            this.lbl_barcode_2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_2.Location = new System.Drawing.Point(4, 150);
            this.lbl_barcode_2.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_barcode_2.Name = "lbl_barcode_2";
            this.lbl_barcode_2.Size = new System.Drawing.Size(178, 56);
            this.lbl_barcode_2.TabIndex = 10;
            this.lbl_barcode_2.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_2
            // 
            this.lbl_rfid_2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_2.Location = new System.Drawing.Point(4, 113);
            this.lbl_rfid_2.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_rfid_2.Name = "lbl_rfid_2";
            this.lbl_rfid_2.Size = new System.Drawing.Size(178, 29);
            this.lbl_rfid_2.TabIndex = 11;
            this.lbl_rfid_2.Text = "           RF-ID";
            // 
            // Test_lizdas_1
            // 
            this.Test_lizdas_1.BackColor = System.Drawing.Color.Transparent;
            this.Test_lizdas_1.Controls.Add(this.lbl_evse1);
            this.Test_lizdas_1.Controls.Add(this.lbl_barcode_1);
            this.Test_lizdas_1.Controls.Add(this.lbl_rfid_1);
            this.Test_lizdas_1.Location = new System.Drawing.Point(314, 15);
            this.Test_lizdas_1.Margin = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_1.Name = "Test_lizdas_1";
            this.Test_lizdas_1.Padding = new System.Windows.Forms.Padding(2);
            this.Test_lizdas_1.Size = new System.Drawing.Size(198, 223);
            this.Test_lizdas_1.TabIndex = 10;
            this.Test_lizdas_1.TabStop = false;
            this.Test_lizdas_1.Text = "1";
            // 
            // lbl_evse1
            // 
            this.lbl_evse1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse1.Location = new System.Drawing.Point(4, 75);
            this.lbl_evse1.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_evse1.Name = "lbl_evse1";
            this.lbl_evse1.Size = new System.Drawing.Size(178, 30);
            this.lbl_evse1.TabIndex = 12;
            this.lbl_evse1.Text = "           EVSE";
            // 
            // lbl_barcode_1
            // 
            this.lbl_barcode_1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_barcode_1.Location = new System.Drawing.Point(4, 150);
            this.lbl_barcode_1.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_barcode_1.Name = "lbl_barcode_1";
            this.lbl_barcode_1.Size = new System.Drawing.Size(178, 56);
            this.lbl_barcode_1.TabIndex = 10;
            this.lbl_barcode_1.Text = "      Skaitytuvas";
            // 
            // lbl_rfid_1
            // 
            this.lbl_rfid_1.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_rfid_1.Location = new System.Drawing.Point(4, 113);
            this.lbl_rfid_1.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_rfid_1.Name = "lbl_rfid_1";
            this.lbl_rfid_1.Size = new System.Drawing.Size(178, 29);
            this.lbl_rfid_1.TabIndex = 11;
            this.lbl_rfid_1.Text = "           RF-ID";
            // 
            // metrel_skip_btn
            // 
            this.metrel_skip_btn.Enabled = false;
            this.metrel_skip_btn.Location = new System.Drawing.Point(760, 525);
            this.metrel_skip_btn.Margin = new System.Windows.Forms.Padding(2);
            this.metrel_skip_btn.Name = "metrel_skip_btn";
            this.metrel_skip_btn.Size = new System.Drawing.Size(98, 38);
            this.metrel_skip_btn.TabIndex = 8;
            this.metrel_skip_btn.Text = "Skip";
            this.metrel_skip_btn.UseVisualStyleBackColor = true;
            this.metrel_skip_btn.Visible = false;
            this.metrel_skip_btn.Click += new System.EventHandler(this.metrel_skip_btn_Click);
            // 
            // metrel_break_btn
            // 
            this.metrel_break_btn.Enabled = false;
            this.metrel_break_btn.Location = new System.Drawing.Point(763, 476);
            this.metrel_break_btn.Margin = new System.Windows.Forms.Padding(2);
            this.metrel_break_btn.Name = "metrel_break_btn";
            this.metrel_break_btn.Size = new System.Drawing.Size(98, 38);
            this.metrel_break_btn.TabIndex = 7;
            this.metrel_break_btn.Text = "Break";
            this.metrel_break_btn.UseVisualStyleBackColor = true;
            this.metrel_break_btn.Visible = false;
            this.metrel_break_btn.Click += new System.EventHandler(this.metrel_break_btn_Click);
            // 
            // Met_proceed_btn
            // 
            this.Met_proceed_btn.Enabled = false;
            this.Met_proceed_btn.Location = new System.Drawing.Point(763, 428);
            this.Met_proceed_btn.Margin = new System.Windows.Forms.Padding(2);
            this.Met_proceed_btn.Name = "Met_proceed_btn";
            this.Met_proceed_btn.Size = new System.Drawing.Size(98, 38);
            this.Met_proceed_btn.TabIndex = 6;
            this.Met_proceed_btn.Text = "Proceed";
            this.Met_proceed_btn.UseVisualStyleBackColor = true;
            this.Met_proceed_btn.Visible = false;
            this.Met_proceed_btn.Click += new System.EventHandler(this.Met_proceed_btn_Click);
            // 
            // metrel_stop_btn
            // 
            this.metrel_stop_btn.Enabled = false;
            this.metrel_stop_btn.Location = new System.Drawing.Point(899, 480);
            this.metrel_stop_btn.Margin = new System.Windows.Forms.Padding(2);
            this.metrel_stop_btn.Name = "metrel_stop_btn";
            this.metrel_stop_btn.Size = new System.Drawing.Size(68, 38);
            this.metrel_stop_btn.TabIndex = 5;
            this.metrel_stop_btn.Text = "Stop";
            this.metrel_stop_btn.UseVisualStyleBackColor = true;
            this.metrel_stop_btn.Visible = false;
            this.metrel_stop_btn.Click += new System.EventHandler(this.metrel_stop_btn_Click);
            // 
            // metrel_start_btn
            // 
            this.metrel_start_btn.Enabled = false;
            this.metrel_start_btn.Location = new System.Drawing.Point(899, 434);
            this.metrel_start_btn.Margin = new System.Windows.Forms.Padding(2);
            this.metrel_start_btn.Name = "metrel_start_btn";
            this.metrel_start_btn.Size = new System.Drawing.Size(68, 38);
            this.metrel_start_btn.TabIndex = 4;
            this.metrel_start_btn.Text = "Start";
            this.metrel_start_btn.UseVisualStyleBackColor = true;
            this.metrel_start_btn.Visible = false;
            this.metrel_start_btn.Click += new System.EventHandler(this.metrel_start_btn_Click);
            // 
            // metrel_auto_btn
            // 
            this.metrel_auto_btn.Enabled = false;
            this.metrel_auto_btn.Location = new System.Drawing.Point(593, 401);
            this.metrel_auto_btn.Margin = new System.Windows.Forms.Padding(2);
            this.metrel_auto_btn.Name = "metrel_auto_btn";
            this.metrel_auto_btn.Size = new System.Drawing.Size(121, 77);
            this.metrel_auto_btn.TabIndex = 3;
            this.metrel_auto_btn.Text = "Metrel\r\nAUTO";
            this.metrel_auto_btn.UseVisualStyleBackColor = true;
            this.metrel_auto_btn.Visible = false;
            this.metrel_auto_btn.Click += new System.EventHandler(this.metrel_auto_btn_Click);
            // 
            // mtrelTest
            // 
            this.mtrelTest.Enabled = false;
            this.mtrelTest.Location = new System.Drawing.Point(591, 496);
            this.mtrelTest.Margin = new System.Windows.Forms.Padding(2);
            this.mtrelTest.Name = "mtrelTest";
            this.mtrelTest.Size = new System.Drawing.Size(121, 77);
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
            this.panel2.Location = new System.Drawing.Point(41, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(202, 134);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label4.Location = new System.Drawing.Point(46, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 27);
            this.label4.TabIndex = 4;
            this.label4.Text = "PASIRUOŠĘS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(31, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 27);
            this.label5.TabIndex = 3;
            this.label5.Text = "Įrangos būsena";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_printer);
            this.panel1.Controls.Add(this.lbl_evse);
            this.panel1.Controls.Add(this.lbl_load);
            this.panel1.Controls.Add(this.lbl_specrum);
            this.panel1.Controls.Add(this.lbl_vald);
            this.panel1.Controls.Add(this.lbl_hvgen);
            this.panel1.Controls.Add(this.lbl_osc);
            this.panel1.Location = new System.Drawing.Point(6, 153);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 456);
            this.panel1.TabIndex = 0;
            // 
            // lbl_printer
            // 
            this.lbl_printer.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_printer.Location = new System.Drawing.Point(40, 263);
            this.lbl_printer.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_printer.Name = "lbl_printer";
            this.lbl_printer.Size = new System.Drawing.Size(178, 56);
            this.lbl_printer.TabIndex = 10;
            this.lbl_printer.Text = "Spausdintuvas";
            this.lbl_printer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_evse
            // 
            this.lbl_evse.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_evse.Location = new System.Drawing.Point(40, 327);
            this.lbl_evse.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_evse.Name = "lbl_evse";
            this.lbl_evse.Size = new System.Drawing.Size(178, 56);
            this.lbl_evse.TabIndex = 9;
            this.lbl_evse.Text = "EVSE Testas";
            this.lbl_evse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_load
            // 
            this.lbl_load.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_load.Location = new System.Drawing.Point(40, 199);
            this.lbl_load.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_load.Name = "lbl_load";
            this.lbl_load.Size = new System.Drawing.Size(178, 56);
            this.lbl_load.TabIndex = 8;
            this.lbl_load.Text = "Apkrova";
            this.lbl_load.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_specrum
            // 
            this.lbl_specrum.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_specrum.Location = new System.Drawing.Point(40, 135);
            this.lbl_specrum.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_specrum.Name = "lbl_specrum";
            this.lbl_specrum.Size = new System.Drawing.Size(178, 56);
            this.lbl_specrum.TabIndex = 7;
            this.lbl_specrum.Text = "Spektro analizatorius";
            this.lbl_specrum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_vald
            // 
            this.lbl_vald.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_vald.Location = new System.Drawing.Point(40, 7);
            this.lbl_vald.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_vald.Name = "lbl_vald";
            this.lbl_vald.Size = new System.Drawing.Size(178, 56);
            this.lbl_vald.TabIndex = 6;
            this.lbl_vald.Text = "Valdiklis";
            this.lbl_vald.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_hvgen
            // 
            this.lbl_hvgen.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_hvgen.Location = new System.Drawing.Point(40, 71);
            this.lbl_hvgen.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_hvgen.Name = "lbl_hvgen";
            this.lbl_hvgen.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lbl_hvgen.Size = new System.Drawing.Size(178, 56);
            this.lbl_hvgen.TabIndex = 5;
            this.lbl_hvgen.Text = "HV Generatorius";
            this.lbl_hvgen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_osc
            // 
            this.lbl_osc.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_osc.Location = new System.Drawing.Point(40, 391);
            this.lbl_osc.Margin = new System.Windows.Forms.Padding(2, 8, 2, 0);
            this.lbl_osc.Name = "lbl_osc";
            this.lbl_osc.Size = new System.Drawing.Size(178, 56);
            this.lbl_osc.TabIndex = 4;
            this.lbl_osc.Text = "Osciloskopas";
            this.lbl_osc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(984, 613);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nustatymai";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label47);
            this.groupBox4.Controls.Add(this.tb_test_param_ihold_32a_max);
            this.groupBox4.Controls.Add(this.tb_test_param_ihold_32a_min);
            this.groupBox4.Controls.Add(this.label48);
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.tb_test_param_ihold_6a_max);
            this.groupBox4.Controls.Add(this.tb_test_param_ihold_6a_min);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.label43);
            this.groupBox4.Controls.Add(this.tb_test_param_firmware_max);
            this.groupBox4.Controls.Add(this.tb_test_param_firmware_min);
            this.groupBox4.Controls.Add(this.label44);
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Controls.Add(this.tb_test_param_rfid_range_max);
            this.groupBox4.Controls.Add(this.tb_test_param_rfid_range_min);
            this.groupBox4.Controls.Add(this.label42);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.tb_test_param_gsm_speed_max);
            this.groupBox4.Controls.Add(this.tb_test_param_gsm_speed_min);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.label37);
            this.groupBox4.Controls.Add(this.tb_test_param_wifi_speed_max);
            this.groupBox4.Controls.Add(this.tb_test_param_wifi_speed_min);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label35);
            this.groupBox4.Controls.Add(this.tb_test_param_resid_dc_max);
            this.groupBox4.Controls.Add(this.tb_test_param_resid_dc_min);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.tb_test_param_r_gnd_max);
            this.groupBox4.Controls.Add(this.tb_test_param_r_gnd_min);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.tb_test_param_insul_dc_max);
            this.groupBox4.Controls.Add(this.tb_test_param_insul_dc_min);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.tb_test_param_insul_ac_max);
            this.groupBox4.Controls.Add(this.tb_test_param_insul_ac_min);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.tb_test_param_pow_mrelay_on_max);
            this.groupBox4.Controls.Add(this.tb_test_param_pow_mrelay_on_min);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.tb_test_param_pow_mrelay_off_max);
            this.groupBox4.Controls.Add(this.tb_test_param_pow_mrelay_off_min);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Location = new System.Drawing.Point(343, 19);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(637, 464);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Testų parametrai";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(566, 429);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(27, 26);
            this.label47.TabIndex = 55;
            this.label47.Text = "A";
            // 
            // tb_test_param_ihold_32a_max
            // 
            this.tb_test_param_ihold_32a_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_ihold_32a_max.Location = new System.Drawing.Point(497, 428);
            this.tb_test_param_ihold_32a_max.Name = "tb_test_param_ihold_32a_max";
            this.tb_test_param_ihold_32a_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_ihold_32a_max.TabIndex = 54;
            this.tb_test_param_ihold_32a_max.Text = "33,6";
            this.tb_test_param_ihold_32a_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_ihold_32a_max.TextChanged += new System.EventHandler(this.tb_test_param_ihold_32a_max_TextChanged);
            // 
            // tb_test_param_ihold_32a_min
            // 
            this.tb_test_param_ihold_32a_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_ihold_32a_min.Location = new System.Drawing.Point(428, 428);
            this.tb_test_param_ihold_32a_min.Name = "tb_test_param_ihold_32a_min";
            this.tb_test_param_ihold_32a_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_ihold_32a_min.TabIndex = 53;
            this.tb_test_param_ihold_32a_min.Text = "30,4";
            this.tb_test_param_ihold_32a_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_ihold_32a_min.TextChanged += new System.EventHandler(this.tb_test_param_ihold_32a_min_TextChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(5, 429);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(314, 26);
            this.label48.TabIndex = 52;
            this.label48.Text = "Krovimo srovės testas prie 32A";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(566, 394);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(27, 26);
            this.label45.TabIndex = 51;
            this.label45.Text = "A";
            // 
            // tb_test_param_ihold_6a_max
            // 
            this.tb_test_param_ihold_6a_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_ihold_6a_max.Location = new System.Drawing.Point(497, 393);
            this.tb_test_param_ihold_6a_max.Name = "tb_test_param_ihold_6a_max";
            this.tb_test_param_ihold_6a_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_ihold_6a_max.TabIndex = 50;
            this.tb_test_param_ihold_6a_max.Text = "6,3";
            this.tb_test_param_ihold_6a_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_ihold_6a_max.TextChanged += new System.EventHandler(this.tb_test_param_ihold_6a_max_TextChanged);
            // 
            // tb_test_param_ihold_6a_min
            // 
            this.tb_test_param_ihold_6a_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_ihold_6a_min.Location = new System.Drawing.Point(428, 393);
            this.tb_test_param_ihold_6a_min.Name = "tb_test_param_ihold_6a_min";
            this.tb_test_param_ihold_6a_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_ihold_6a_min.TabIndex = 49;
            this.tb_test_param_ihold_6a_min.Text = "5,7";
            this.tb_test_param_ihold_6a_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_ihold_6a_min.TextChanged += new System.EventHandler(this.tb_test_param_ihold_6a_min_TextChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(5, 394);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(302, 26);
            this.label46.TabIndex = 48;
            this.label46.Text = "Krovimo srovės testas prie 6A";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(566, 359);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(23, 26);
            this.label43.TabIndex = 47;
            this.label43.Text = "v";
            // 
            // tb_test_param_firmware_max
            // 
            this.tb_test_param_firmware_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_firmware_max.Location = new System.Drawing.Point(497, 358);
            this.tb_test_param_firmware_max.Name = "tb_test_param_firmware_max";
            this.tb_test_param_firmware_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_firmware_max.TabIndex = 46;
            this.tb_test_param_firmware_max.Text = "1.2.1";
            this.tb_test_param_firmware_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_firmware_max.TextChanged += new System.EventHandler(this.tb_test_param_firmware_max_TextChanged);
            // 
            // tb_test_param_firmware_min
            // 
            this.tb_test_param_firmware_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_firmware_min.Location = new System.Drawing.Point(428, 358);
            this.tb_test_param_firmware_min.Name = "tb_test_param_firmware_min";
            this.tb_test_param_firmware_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_firmware_min.TabIndex = 45;
            this.tb_test_param_firmware_min.Text = "1.2.1";
            this.tb_test_param_firmware_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_firmware_min.TextChanged += new System.EventHandler(this.tb_test_param_firmware_min_TextChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(5, 359);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(282, 26);
            this.label44.TabIndex = 44;
            this.label44.Text = "Programinės įrangos versija";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(566, 324);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(50, 26);
            this.label41.TabIndex = 43;
            this.label41.Text = "mm";
            // 
            // tb_test_param_rfid_range_max
            // 
            this.tb_test_param_rfid_range_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_rfid_range_max.Location = new System.Drawing.Point(497, 323);
            this.tb_test_param_rfid_range_max.Name = "tb_test_param_rfid_range_max";
            this.tb_test_param_rfid_range_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_rfid_range_max.TabIndex = 42;
            this.tb_test_param_rfid_range_max.Text = "120";
            this.tb_test_param_rfid_range_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_rfid_range_max.TextChanged += new System.EventHandler(this.tb_test_param_rfid_range_max_TextChanged);
            // 
            // tb_test_param_rfid_range_min
            // 
            this.tb_test_param_rfid_range_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_rfid_range_min.Location = new System.Drawing.Point(428, 323);
            this.tb_test_param_rfid_range_min.Name = "tb_test_param_rfid_range_min";
            this.tb_test_param_rfid_range_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_rfid_range_min.TabIndex = 41;
            this.tb_test_param_rfid_range_min.Text = "20";
            this.tb_test_param_rfid_range_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_rfid_range_min.TextChanged += new System.EventHandler(this.tb_test_param_rfid_range_min_TextChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(5, 324);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(251, 26);
            this.label42.TabIndex = 40;
            this.label42.Text = "RFID skaitymo atstumas";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(566, 289);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(55, 26);
            this.label39.TabIndex = 39;
            this.label39.Text = "kB/s";
            // 
            // tb_test_param_gsm_speed_max
            // 
            this.tb_test_param_gsm_speed_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_gsm_speed_max.Location = new System.Drawing.Point(497, 288);
            this.tb_test_param_gsm_speed_max.Name = "tb_test_param_gsm_speed_max";
            this.tb_test_param_gsm_speed_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_gsm_speed_max.TabIndex = 38;
            this.tb_test_param_gsm_speed_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_gsm_speed_max.TextChanged += new System.EventHandler(this.tb_test_param_gsm_speed_max_TextChanged);
            // 
            // tb_test_param_gsm_speed_min
            // 
            this.tb_test_param_gsm_speed_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_gsm_speed_min.Location = new System.Drawing.Point(428, 288);
            this.tb_test_param_gsm_speed_min.Name = "tb_test_param_gsm_speed_min";
            this.tb_test_param_gsm_speed_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_gsm_speed_min.TabIndex = 37;
            this.tb_test_param_gsm_speed_min.Text = "50";
            this.tb_test_param_gsm_speed_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_gsm_speed_min.TextChanged += new System.EventHandler(this.tb_test_param_gsm_speed_min_TextChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(5, 289);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(289, 26);
            this.label40.TabIndex = 36;
            this.label40.Text = "GSM duomenų srauto greitis";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(566, 254);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(62, 26);
            this.label37.TabIndex = 35;
            this.label37.Text = "MB/s";
            // 
            // tb_test_param_wifi_speed_max
            // 
            this.tb_test_param_wifi_speed_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_wifi_speed_max.Location = new System.Drawing.Point(497, 253);
            this.tb_test_param_wifi_speed_max.Name = "tb_test_param_wifi_speed_max";
            this.tb_test_param_wifi_speed_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_wifi_speed_max.TabIndex = 34;
            this.tb_test_param_wifi_speed_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_wifi_speed_max.TextChanged += new System.EventHandler(this.tb_test_param_wifi_speed_max_TextChanged);
            // 
            // tb_test_param_wifi_speed_min
            // 
            this.tb_test_param_wifi_speed_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_wifi_speed_min.Location = new System.Drawing.Point(428, 253);
            this.tb_test_param_wifi_speed_min.Name = "tb_test_param_wifi_speed_min";
            this.tb_test_param_wifi_speed_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_wifi_speed_min.TabIndex = 33;
            this.tb_test_param_wifi_speed_min.Text = "1";
            this.tb_test_param_wifi_speed_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_wifi_speed_min.TextChanged += new System.EventHandler(this.tb_test_param_wifi_speed_min_TextChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(5, 254);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(290, 26);
            this.label38.TabIndex = 32;
            this.label38.Text = "Wi-Fi duomenų srauto greitis";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(566, 219);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(46, 26);
            this.label35.TabIndex = 31;
            this.label35.Text = "mA";
            // 
            // tb_test_param_resid_dc_max
            // 
            this.tb_test_param_resid_dc_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_resid_dc_max.Location = new System.Drawing.Point(497, 218);
            this.tb_test_param_resid_dc_max.Name = "tb_test_param_resid_dc_max";
            this.tb_test_param_resid_dc_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_resid_dc_max.TabIndex = 30;
            this.tb_test_param_resid_dc_max.Text = "6";
            this.tb_test_param_resid_dc_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_resid_dc_max.TextChanged += new System.EventHandler(this.tb_test_param_resid_dc_max_TextChanged);
            // 
            // tb_test_param_resid_dc_min
            // 
            this.tb_test_param_resid_dc_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_resid_dc_min.Location = new System.Drawing.Point(428, 218);
            this.tb_test_param_resid_dc_min.Name = "tb_test_param_resid_dc_min";
            this.tb_test_param_resid_dc_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_resid_dc_min.TabIndex = 29;
            this.tb_test_param_resid_dc_min.Text = "3";
            this.tb_test_param_resid_dc_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_resid_dc_min.TextChanged += new System.EventHandler(this.tb_test_param_resid_dc_min_TextChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 219);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(196, 26);
            this.label36.TabIndex = 28;
            this.label36.Text = "DC nuotekio testas";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(566, 184);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(55, 26);
            this.label33.TabIndex = 27;
            this.label33.Text = "ohm";
            // 
            // tb_test_param_r_gnd_max
            // 
            this.tb_test_param_r_gnd_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_r_gnd_max.Location = new System.Drawing.Point(497, 183);
            this.tb_test_param_r_gnd_max.Name = "tb_test_param_r_gnd_max";
            this.tb_test_param_r_gnd_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_r_gnd_max.TabIndex = 26;
            this.tb_test_param_r_gnd_max.Text = "0,1";
            this.tb_test_param_r_gnd_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_r_gnd_max.TextChanged += new System.EventHandler(this.tb_test_param_r_gnd_max_TextChanged);
            // 
            // tb_test_param_r_gnd_min
            // 
            this.tb_test_param_r_gnd_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_r_gnd_min.Location = new System.Drawing.Point(428, 183);
            this.tb_test_param_r_gnd_min.Name = "tb_test_param_r_gnd_min";
            this.tb_test_param_r_gnd_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_r_gnd_min.TabIndex = 25;
            this.tb_test_param_r_gnd_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_r_gnd_min.TextChanged += new System.EventHandler(this.tb_test_param_r_gnd_min_TextChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 184);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(259, 26);
            this.label34.TabIndex = 24;
            this.label34.Text = "Įžeminimo varža prie 10A";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(566, 151);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(54, 26);
            this.label31.TabIndex = 23;
            this.label31.Text = "Mon";
            // 
            // tb_test_param_insul_dc_max
            // 
            this.tb_test_param_insul_dc_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_insul_dc_max.Location = new System.Drawing.Point(497, 148);
            this.tb_test_param_insul_dc_max.Name = "tb_test_param_insul_dc_max";
            this.tb_test_param_insul_dc_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_insul_dc_max.TabIndex = 22;
            this.tb_test_param_insul_dc_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_insul_dc_max.TextChanged += new System.EventHandler(this.tb_test_param_insul_dc_max_TextChanged);
            // 
            // tb_test_param_insul_dc_min
            // 
            this.tb_test_param_insul_dc_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_insul_dc_min.Location = new System.Drawing.Point(428, 148);
            this.tb_test_param_insul_dc_min.Name = "tb_test_param_insul_dc_min";
            this.tb_test_param_insul_dc_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_insul_dc_min.TabIndex = 21;
            this.tb_test_param_insul_dc_min.Text = "1";
            this.tb_test_param_insul_dc_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_insul_dc_min.TextChanged += new System.EventHandler(this.tb_test_param_insul_dc_min_TextChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 151);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(308, 26);
            this.label32.TabIndex = 20;
            this.label32.Text = "Izoliacijos testas prie 550V DC";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(566, 114);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(54, 26);
            this.label29.TabIndex = 19;
            this.label29.Text = "Mon";
            // 
            // tb_test_param_insul_ac_max
            // 
            this.tb_test_param_insul_ac_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_insul_ac_max.Location = new System.Drawing.Point(497, 113);
            this.tb_test_param_insul_ac_max.Name = "tb_test_param_insul_ac_max";
            this.tb_test_param_insul_ac_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_insul_ac_max.TabIndex = 18;
            this.tb_test_param_insul_ac_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_insul_ac_max.TextChanged += new System.EventHandler(this.tb_test_param_insul_ac_max_TextChanged);
            // 
            // tb_test_param_insul_ac_min
            // 
            this.tb_test_param_insul_ac_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_insul_ac_min.Location = new System.Drawing.Point(428, 113);
            this.tb_test_param_insul_ac_min.Name = "tb_test_param_insul_ac_min";
            this.tb_test_param_insul_ac_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_insul_ac_min.TabIndex = 17;
            this.tb_test_param_insul_ac_min.Text = "1";
            this.tb_test_param_insul_ac_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_insul_ac_min.TextChanged += new System.EventHandler(this.tb_test_param_insul_ac_min_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 114);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(319, 26);
            this.label30.TabIndex = 16;
            this.label30.Text = "Izoliacijos testas prie 1500V AC";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(566, 79);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(33, 26);
            this.label27.TabIndex = 15;
            this.label27.Text = "W";
            // 
            // tb_test_param_pow_mrelay_on_max
            // 
            this.tb_test_param_pow_mrelay_on_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_pow_mrelay_on_max.Location = new System.Drawing.Point(497, 78);
            this.tb_test_param_pow_mrelay_on_max.Name = "tb_test_param_pow_mrelay_on_max";
            this.tb_test_param_pow_mrelay_on_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_pow_mrelay_on_max.TabIndex = 14;
            this.tb_test_param_pow_mrelay_on_max.Text = "5.5";
            this.tb_test_param_pow_mrelay_on_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_pow_mrelay_on_max.TextChanged += new System.EventHandler(this.tb_test_param_pow_mrelay_on_max_TextChanged);
            // 
            // tb_test_param_pow_mrelay_on_min
            // 
            this.tb_test_param_pow_mrelay_on_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_pow_mrelay_on_min.Location = new System.Drawing.Point(428, 78);
            this.tb_test_param_pow_mrelay_on_min.Name = "tb_test_param_pow_mrelay_on_min";
            this.tb_test_param_pow_mrelay_on_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_pow_mrelay_on_min.TabIndex = 13;
            this.tb_test_param_pow_mrelay_on_min.Text = "4.5";
            this.tb_test_param_pow_mrelay_on_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_pow_mrelay_on_min.TextChanged += new System.EventHandler(this.tb_test_param_pow_mrelay_on_min_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 79);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(322, 26);
            this.label28.TabIndex = 12;
            this.label28.Text = "Energijos sąnaudos pagr. rėlė įj.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(566, 44);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(33, 26);
            this.label26.TabIndex = 11;
            this.label26.Text = "W";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(435, 19);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 20);
            this.label25.TabIndex = 10;
            this.label25.Text = "NUO        IKI";
            // 
            // tb_test_param_pow_mrelay_off_max
            // 
            this.tb_test_param_pow_mrelay_off_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_pow_mrelay_off_max.Location = new System.Drawing.Point(497, 43);
            this.tb_test_param_pow_mrelay_off_max.Name = "tb_test_param_pow_mrelay_off_max";
            this.tb_test_param_pow_mrelay_off_max.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_pow_mrelay_off_max.TabIndex = 9;
            this.tb_test_param_pow_mrelay_off_max.Text = "4";
            this.tb_test_param_pow_mrelay_off_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_pow_mrelay_off_max.TextChanged += new System.EventHandler(this.tb_test_param_pow_mrelay_off_max_TextChanged);
            // 
            // tb_test_param_pow_mrelay_off_min
            // 
            this.tb_test_param_pow_mrelay_off_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_test_param_pow_mrelay_off_min.Location = new System.Drawing.Point(428, 43);
            this.tb_test_param_pow_mrelay_off_min.Name = "tb_test_param_pow_mrelay_off_min";
            this.tb_test_param_pow_mrelay_off_min.Size = new System.Drawing.Size(63, 29);
            this.tb_test_param_pow_mrelay_off_min.TabIndex = 8;
            this.tb_test_param_pow_mrelay_off_min.Text = "3";
            this.tb_test_param_pow_mrelay_off_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_test_param_pow_mrelay_off_min.TextChanged += new System.EventHandler(this.tb_test_param_pow_mrelay_off_min_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 44);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(333, 26);
            this.label24.TabIndex = 0;
            this.label24.Text = "Energijos sąnaudos pagr. rėlė išj.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.saveWplace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(322, 284);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Darbo vieta";
            // 
            // saveWplace
            // 
            this.saveWplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveWplace.Location = new System.Drawing.Point(123, 246);
            this.saveWplace.Margin = new System.Windows.Forms.Padding(2);
            this.saveWplace.Name = "saveWplace";
            this.saveWplace.Size = new System.Drawing.Size(92, 34);
            this.saveWplace.TabIndex = 5;
            this.saveWplace.Text = "Išsaugoti";
            this.saveWplace.UseVisualStyleBackColor = true;
            this.saveWplace.Click += new System.EventHandler(this.saveWplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 165);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.groupBox_Printer);
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
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(984, 613);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Įranga";
            // 
            // groupBox_Printer
            // 
            this.groupBox_Printer.Controls.Add(this.label23);
            this.groupBox_Printer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Printer.Location = new System.Drawing.Point(472, 214);
            this.groupBox_Printer.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Printer.Name = "groupBox_Printer";
            this.groupBox_Printer.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Printer.Size = new System.Drawing.Size(442, 90);
            this.groupBox_Printer.TabIndex = 15;
            this.groupBox_Printer.TabStop = false;
            this.groupBox_Printer.Text = "Spausdintuvas";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(14, 49);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 18);
            this.label23.TabIndex = 2;
            this.label23.Text = "IP:PORT";
            // 
            // groupBox_Rfid
            // 
            this.groupBox_Rfid.Controls.Add(this.label19);
            this.groupBox_Rfid.Controls.Add(this.label18);
            this.groupBox_Rfid.Controls.Add(this.label10);
            this.groupBox_Rfid.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Rfid.Location = new System.Drawing.Point(472, 419);
            this.groupBox_Rfid.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Rfid.Name = "groupBox_Rfid";
            this.groupBox_Rfid.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Rfid.Size = new System.Drawing.Size(442, 158);
            this.groupBox_Rfid.TabIndex = 14;
            this.groupBox_Rfid.TabStop = false;
            this.groupBox_Rfid.Text = "Testavimo lizdai";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(9, 125);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 18);
            this.label19.TabIndex = 4;
            this.label19.Text = "3  ADDR";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(9, 84);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 18);
            this.label18.TabIndex = 3;
            this.label18.Text = "2  ADDR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 18);
            this.label10.TabIndex = 2;
            this.label10.Text = "1  ADDR";
            // 
            // groupBox_Barcode
            // 
            this.groupBox_Barcode.Controls.Add(this.label12);
            this.groupBox_Barcode.Controls.Add(this.label11);
            this.groupBox_Barcode.Controls.Add(this.label9);
            this.groupBox_Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Barcode.Location = new System.Drawing.Point(20, 419);
            this.groupBox_Barcode.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Barcode.Name = "groupBox_Barcode";
            this.groupBox_Barcode.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Barcode.Size = new System.Drawing.Size(438, 158);
            this.groupBox_Barcode.TabIndex = 13;
            this.groupBox_Barcode.TabStop = false;
            this.groupBox_Barcode.Text = " Skaitytuvai";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 125);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "3  IP:PORT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 87);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 18);
            this.label11.TabIndex = 3;
            this.label11.Text = "2  IP:PORT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 49);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "1  IP:PORT";
            // 
            // save_ip
            // 
            this.save_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_ip.Location = new System.Drawing.Point(627, 345);
            this.save_ip.Margin = new System.Windows.Forms.Padding(2);
            this.save_ip.Name = "save_ip";
            this.save_ip.Size = new System.Drawing.Size(110, 36);
            this.save_ip.TabIndex = 12;
            this.save_ip.Text = "Išsaugoti";
            this.save_ip.UseVisualStyleBackColor = true;
            this.save_ip.Click += new System.EventHandler(this.save_ip_Click);
            // 
            // groupBox_Metrel_USB
            // 
            this.groupBox_Metrel_USB.Controls.Add(this.label16);
            this.groupBox_Metrel_USB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Metrel_USB.Location = new System.Drawing.Point(472, 25);
            this.groupBox_Metrel_USB.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Metrel_USB.Name = "groupBox_Metrel_USB";
            this.groupBox_Metrel_USB.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Metrel_USB.Size = new System.Drawing.Size(442, 89);
            this.groupBox_Metrel_USB.TabIndex = 7;
            this.groupBox_Metrel_USB.TabStop = false;
            this.groupBox_Metrel_USB.Text = "EVSE (Metrel)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(14, 49);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 18);
            this.label16.TabIndex = 2;
            this.label16.Text = "ID_WORD";
            // 
            // groupBox_Valdiklis
            // 
            this.groupBox_Valdiklis.Controls.Add(this.label8);
            this.groupBox_Valdiklis.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Valdiklis.Location = new System.Drawing.Point(20, 25);
            this.groupBox_Valdiklis.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Valdiklis.Name = "groupBox_Valdiklis";
            this.groupBox_Valdiklis.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Valdiklis.Size = new System.Drawing.Size(438, 89);
            this.groupBox_Valdiklis.TabIndex = 11;
            this.groupBox_Valdiklis.TabStop = false;
            this.groupBox_Valdiklis.Text = "Valdiklis";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "IP:PORT";
            // 
            // groupBox_Load
            // 
            this.groupBox_Load.Controls.Add(this.label13);
            this.groupBox_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Load.Location = new System.Drawing.Point(20, 314);
            this.groupBox_Load.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Load.Name = "groupBox_Load";
            this.groupBox_Load.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Load.Size = new System.Drawing.Size(438, 89);
            this.groupBox_Load.TabIndex = 10;
            this.groupBox_Load.TabStop = false;
            this.groupBox_Load.Text = " AC apkrova";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 49);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 18);
            this.label13.TabIndex = 2;
            this.label13.Text = "IP:PORT";
            // 
            // groupBox_Spectr
            // 
            this.groupBox_Spectr.Controls.Add(this.label14);
            this.groupBox_Spectr.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Spectr.Location = new System.Drawing.Point(20, 214);
            this.groupBox_Spectr.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Spectr.Name = "groupBox_Spectr";
            this.groupBox_Spectr.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Spectr.Size = new System.Drawing.Size(438, 90);
            this.groupBox_Spectr.TabIndex = 9;
            this.groupBox_Spectr.TabStop = false;
            this.groupBox_Spectr.Text = "Spektro analizatorius";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 49);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 18);
            this.label14.TabIndex = 2;
            this.label14.Text = "IP:PORT";
            // 
            // groupBox_HVgen
            // 
            this.groupBox_HVgen.Controls.Add(this.label15);
            this.groupBox_HVgen.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HVgen.Location = new System.Drawing.Point(20, 119);
            this.groupBox_HVgen.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_HVgen.Name = "groupBox_HVgen";
            this.groupBox_HVgen.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_HVgen.Size = new System.Drawing.Size(438, 90);
            this.groupBox_HVgen.TabIndex = 8;
            this.groupBox_HVgen.TabStop = false;
            this.groupBox_HVgen.Text = "HV generatorius";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(9, 49);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 18);
            this.label15.TabIndex = 2;
            this.label15.Text = "IP:PORT";
            // 
            // groupBox_Osc_USB
            // 
            this.groupBox_Osc_USB.Controls.Add(this.label17);
            this.groupBox_Osc_USB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Osc_USB.Location = new System.Drawing.Point(472, 119);
            this.groupBox_Osc_USB.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Osc_USB.Name = "groupBox_Osc_USB";
            this.groupBox_Osc_USB.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Osc_USB.Size = new System.Drawing.Size(442, 90);
            this.groupBox_Osc_USB.TabIndex = 6;
            this.groupBox_Osc_USB.TabStop = false;
            this.groupBox_Osc_USB.Text = "Osciloskopas";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(14, 49);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 18);
            this.label17.TabIndex = 2;
            this.label17.Text = "ID_WORD";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tabControl2);
            this.tabPage4.Location = new System.Drawing.Point(4, 44);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(984, 613);
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
            this.tabControl2.Location = new System.Drawing.Point(6, 20);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(975, 566);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Controls.Add(this.groupBox_evse_state);
            this.tabPage5.Controls.Add(this.groupBox_main_relay);
            this.tabPage5.Controls.Add(this.groupBox_pp_select);
            this.tabPage5.Controls.Add(this.groupBox_tp_select);
            this.tabPage5.Controls.Add(this.groupBox_checks);
            this.tabPage5.Location = new System.Drawing.Point(4, 31);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(967, 531);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "  CONTROL_BOARD  ";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_RCD_L3);
            this.groupBox3.Controls.Add(this.radioButton_RCD_L2);
            this.groupBox3.Controls.Add(this.radioButton_RCD_L1);
            this.groupBox3.Controls.Add(this.label_RCD_current);
            this.groupBox3.Controls.Add(this.button_RCD_set);
            this.groupBox3.Controls.Add(this.RCD_textBox);
            this.groupBox3.Controls.Add(this.RCD_sel_plus);
            this.groupBox3.Controls.Add(this.RCD_sel_minus);
            this.groupBox3.Location = new System.Drawing.Point(32, 342);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(888, 60);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RCD";
            // 
            // radioButton_RCD_L3
            // 
            this.radioButton_RCD_L3.AutoSize = true;
            this.radioButton_RCD_L3.Location = new System.Drawing.Point(820, 21);
            this.radioButton_RCD_L3.Name = "radioButton_RCD_L3";
            this.radioButton_RCD_L3.Size = new System.Drawing.Size(48, 28);
            this.radioButton_RCD_L3.TabIndex = 12;
            this.radioButton_RCD_L3.TabStop = true;
            this.radioButton_RCD_L3.Text = "L3";
            this.radioButton_RCD_L3.UseVisualStyleBackColor = true;
            this.radioButton_RCD_L3.CheckedChanged += new System.EventHandler(this.radioButton_RCD_L3_CheckedChanged);
            // 
            // radioButton_RCD_L2
            // 
            this.radioButton_RCD_L2.AutoSize = true;
            this.radioButton_RCD_L2.Location = new System.Drawing.Point(746, 21);
            this.radioButton_RCD_L2.Name = "radioButton_RCD_L2";
            this.radioButton_RCD_L2.Size = new System.Drawing.Size(48, 28);
            this.radioButton_RCD_L2.TabIndex = 11;
            this.radioButton_RCD_L2.TabStop = true;
            this.radioButton_RCD_L2.Text = "L2";
            this.radioButton_RCD_L2.UseVisualStyleBackColor = true;
            this.radioButton_RCD_L2.CheckedChanged += new System.EventHandler(this.radioButton_RCD_L2_CheckedChanged);
            // 
            // radioButton_RCD_L1
            // 
            this.radioButton_RCD_L1.AutoSize = true;
            this.radioButton_RCD_L1.Location = new System.Drawing.Point(672, 21);
            this.radioButton_RCD_L1.Name = "radioButton_RCD_L1";
            this.radioButton_RCD_L1.Size = new System.Drawing.Size(48, 28);
            this.radioButton_RCD_L1.TabIndex = 10;
            this.radioButton_RCD_L1.TabStop = true;
            this.radioButton_RCD_L1.Text = "L1";
            this.radioButton_RCD_L1.UseVisualStyleBackColor = true;
            this.radioButton_RCD_L1.CheckedChanged += new System.EventHandler(this.radioButton_RCD_L1_CheckedChanged);
            // 
            // label_RCD_current
            // 
            this.label_RCD_current.BackColor = System.Drawing.Color.Gainsboro;
            this.label_RCD_current.Location = new System.Drawing.Point(141, 18);
            this.label_RCD_current.Name = "label_RCD_current";
            this.label_RCD_current.Size = new System.Drawing.Size(91, 37);
            this.label_RCD_current.TabIndex = 9;
            this.label_RCD_current.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_RCD_set
            // 
            this.button_RCD_set.Location = new System.Drawing.Point(493, 17);
            this.button_RCD_set.Name = "button_RCD_set";
            this.button_RCD_set.Size = new System.Drawing.Size(55, 38);
            this.button_RCD_set.TabIndex = 8;
            this.button_RCD_set.Text = "SET";
            this.button_RCD_set.UseVisualStyleBackColor = true;
            this.button_RCD_set.Click += new System.EventHandler(this.button_RCD_set_Click);
            // 
            // RCD_textBox
            // 
            this.RCD_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RCD_textBox.Location = new System.Drawing.Point(424, 18);
            this.RCD_textBox.Name = "RCD_textBox";
            this.RCD_textBox.Size = new System.Drawing.Size(63, 35);
            this.RCD_textBox.TabIndex = 7;
            this.RCD_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RCD_sel_plus
            // 
            this.RCD_sel_plus.Location = new System.Drawing.Point(238, 17);
            this.RCD_sel_plus.Name = "RCD_sel_plus";
            this.RCD_sel_plus.Size = new System.Drawing.Size(55, 38);
            this.RCD_sel_plus.TabIndex = 6;
            this.RCD_sel_plus.Text = ">";
            this.RCD_sel_plus.UseVisualStyleBackColor = true;
            this.RCD_sel_plus.Click += new System.EventHandler(this.RCD_sel_plus_Click);
            // 
            // RCD_sel_minus
            // 
            this.RCD_sel_minus.Location = new System.Drawing.Point(79, 17);
            this.RCD_sel_minus.Name = "RCD_sel_minus";
            this.RCD_sel_minus.Size = new System.Drawing.Size(55, 38);
            this.RCD_sel_minus.TabIndex = 5;
            this.RCD_sel_minus.Text = "<";
            this.RCD_sel_minus.UseVisualStyleBackColor = true;
            this.RCD_sel_minus.Click += new System.EventHandler(this.RCD_sel_minus_Click);
            // 
            // groupBox_evse_state
            // 
            this.groupBox_evse_state.Location = new System.Drawing.Point(32, 100);
            this.groupBox_evse_state.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_evse_state.Name = "groupBox_evse_state";
            this.groupBox_evse_state.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_evse_state.Size = new System.Drawing.Size(700, 60);
            this.groupBox_evse_state.TabIndex = 3;
            this.groupBox_evse_state.TabStop = false;
            this.groupBox_evse_state.Text = "EV_MODE";
            // 
            // groupBox_main_relay
            // 
            this.groupBox_main_relay.Location = new System.Drawing.Point(32, 258);
            this.groupBox_main_relay.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_main_relay.Name = "groupBox_main_relay";
            this.groupBox_main_relay.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_main_relay.Size = new System.Drawing.Size(888, 60);
            this.groupBox_main_relay.TabIndex = 2;
            this.groupBox_main_relay.TabStop = false;
            this.groupBox_main_relay.Text = "PWR";
            // 
            // groupBox_pp_select
            // 
            this.groupBox_pp_select.Location = new System.Drawing.Point(32, 178);
            this.groupBox_pp_select.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_pp_select.Name = "groupBox_pp_select";
            this.groupBox_pp_select.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_pp_select.Size = new System.Drawing.Size(700, 60);
            this.groupBox_pp_select.TabIndex = 2;
            this.groupBox_pp_select.TabStop = false;
            this.groupBox_pp_select.Text = "CABLE";
            // 
            // groupBox_tp_select
            // 
            this.groupBox_tp_select.Location = new System.Drawing.Point(750, 22);
            this.groupBox_tp_select.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_tp_select.Name = "groupBox_tp_select";
            this.groupBox_tp_select.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_tp_select.Size = new System.Drawing.Size(170, 216);
            this.groupBox_tp_select.TabIndex = 2;
            this.groupBox_tp_select.TabStop = false;
            this.groupBox_tp_select.Text = "POSITION";
            // 
            // groupBox_checks
            // 
            this.groupBox_checks.Location = new System.Drawing.Point(32, 22);
            this.groupBox_checks.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_checks.Name = "groupBox_checks";
            this.groupBox_checks.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_checks.Size = new System.Drawing.Size(700, 60);
            this.groupBox_checks.TabIndex = 3;
            this.groupBox_checks.TabStop = false;
            this.groupBox_checks.Text = "CHECKS";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btn_stop);
            this.tabPage7.Controls.Add(this.button5);
            this.tabPage7.Controls.Add(this.dataGrid_HV_result);
            this.tabPage7.Controls.Add(this.dataGrid_HV_test);
            this.tabPage7.Location = new System.Drawing.Point(4, 31);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(967, 531);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "  HV_GEN  ";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(658, 219);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(123, 34);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(532, 219);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 34);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid_HV_result.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid_HV_result.Location = new System.Drawing.Point(2, 270);
            this.dataGrid_HV_result.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_HV_result.Name = "dataGrid_HV_result";
            this.dataGrid_HV_result.RowHeadersVisible = false;
            this.dataGrid_HV_result.RowHeadersWidth = 51;
            this.dataGrid_HV_result.RowTemplate.Height = 24;
            this.dataGrid_HV_result.Size = new System.Drawing.Size(797, 138);
            this.dataGrid_HV_result.TabIndex = 2;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "DATE";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.Width = 96;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.HeaderText = "TEST";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 94;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewButtonColumn1.FillWeight = 80F;
            this.dataGridViewButtonColumn1.HeaderText = "RESULT";
            this.dataGridViewButtonColumn1.MinimumWidth = 6;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.Width = 118;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "PARAM1";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 122;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.HeaderText = "PARAM2";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 122;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.HeaderText = "TIME";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 90;
            // 
            // dataGrid_HV_test
            // 
            this.dataGrid_HV_test.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
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
            this.dataGrid_HV_test.Location = new System.Drawing.Point(2, 12);
            this.dataGrid_HV_test.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_HV_test.Name = "dataGrid_HV_test";
            this.dataGrid_HV_test.RowHeadersVisible = false;
            this.dataGrid_HV_test.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGrid_HV_test.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_HV_test.RowTemplate.Height = 35;
            this.dataGrid_HV_test.Size = new System.Drawing.Size(797, 188);
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
            // 
            // Column0
            // 
            this.Column0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column0.HeaderText = "TEST";
            this.Column0.MinimumWidth = 6;
            this.Column0.Name = "Column0";
            this.Column0.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column0.Width = 84;
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
            this.Column8.Width = 74;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.HeaderText = "SET";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 53;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "V, kV";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "HI SET";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 95;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "LO SET";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 102;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "REF";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 73;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "TIME";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Controls.Add(this.dataGrid_Spectrum);
            this.tabPage6.Controls.Add(this.button7);
            this.tabPage6.Controls.Add(this.chart1);
            this.tabPage6.Location = new System.Drawing.Point(4, 31);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(967, 531);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "  SPECTRUM  ";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rad_btn_spect_EVSE_sel_3);
            this.groupBox2.Controls.Add(this.rad_btn_spect_EVSE_sel_2);
            this.groupBox2.Controls.Add(this.rad_btn_spect_EVSE_sel_1);
            this.groupBox2.Location = new System.Drawing.Point(239, 422);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 68);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EVSE";
            // 
            // rad_btn_spect_EVSE_sel_3
            // 
            this.rad_btn_spect_EVSE_sel_3.AutoSize = true;
            this.rad_btn_spect_EVSE_sel_3.Location = new System.Drawing.Point(286, 30);
            this.rad_btn_spect_EVSE_sel_3.Name = "rad_btn_spect_EVSE_sel_3";
            this.rad_btn_spect_EVSE_sel_3.Size = new System.Drawing.Size(94, 28);
            this.rad_btn_spect_EVSE_sel_3.TabIndex = 8;
            this.rad_btn_spect_EVSE_sel_3.TabStop = true;
            this.rad_btn_spect_EVSE_sel_3.Text = "EVSE 3";
            this.rad_btn_spect_EVSE_sel_3.UseVisualStyleBackColor = true;
            this.rad_btn_spect_EVSE_sel_3.CheckedChanged += new System.EventHandler(this.rad_btn_spect_EVSE_sel_3_CheckedChanged);
            // 
            // rad_btn_spect_EVSE_sel_2
            // 
            this.rad_btn_spect_EVSE_sel_2.AutoSize = true;
            this.rad_btn_spect_EVSE_sel_2.Location = new System.Drawing.Point(143, 30);
            this.rad_btn_spect_EVSE_sel_2.Name = "rad_btn_spect_EVSE_sel_2";
            this.rad_btn_spect_EVSE_sel_2.Size = new System.Drawing.Size(94, 28);
            this.rad_btn_spect_EVSE_sel_2.TabIndex = 7;
            this.rad_btn_spect_EVSE_sel_2.TabStop = true;
            this.rad_btn_spect_EVSE_sel_2.Text = "EVSE 2";
            this.rad_btn_spect_EVSE_sel_2.UseVisualStyleBackColor = true;
            this.rad_btn_spect_EVSE_sel_2.CheckedChanged += new System.EventHandler(this.rad_btn_spect_EVSE_sel_2_CheckedChanged);
            // 
            // rad_btn_spect_EVSE_sel_1
            // 
            this.rad_btn_spect_EVSE_sel_1.AutoSize = true;
            this.rad_btn_spect_EVSE_sel_1.Location = new System.Drawing.Point(9, 30);
            this.rad_btn_spect_EVSE_sel_1.Name = "rad_btn_spect_EVSE_sel_1";
            this.rad_btn_spect_EVSE_sel_1.Size = new System.Drawing.Size(94, 28);
            this.rad_btn_spect_EVSE_sel_1.TabIndex = 6;
            this.rad_btn_spect_EVSE_sel_1.TabStop = true;
            this.rad_btn_spect_EVSE_sel_1.Text = "EVSE 1";
            this.rad_btn_spect_EVSE_sel_1.UseVisualStyleBackColor = true;
            this.rad_btn_spect_EVSE_sel_1.CheckedChanged += new System.EventHandler(this.rad_btn_spect_EVSE_sel_1_CheckedChanged);
            // 
            // dataGrid_Spectrum
            // 
            this.dataGrid_Spectrum.AllowUserToDeleteRows = false;
            this.dataGrid_Spectrum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid_Spectrum.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_Spectrum.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_Spectrum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGrid_Spectrum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Spectrum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column18,
            this.Column19});
            this.dataGrid_Spectrum.Location = new System.Drawing.Point(575, 25);
            this.dataGrid_Spectrum.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Spectrum.Name = "dataGrid_Spectrum";
            this.dataGrid_Spectrum.RowHeadersVisible = false;
            this.dataGrid_Spectrum.RowHeadersWidth = 51;
            this.dataGrid_Spectrum.RowTemplate.Height = 24;
            this.dataGrid_Spectrum.Size = new System.Drawing.Size(267, 351);
            this.dataGrid_Spectrum.TabIndex = 5;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "PARAM";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.Width = 102;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "VALUE";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.Width = 97;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(7, 375);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 59);
            this.button7.TabIndex = 1;
            this.button7.Text = "GET DATA";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(5, 6);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(563, 370);
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
            this.tabPage8.Location = new System.Drawing.Point(4, 31);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(967, 531);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "  Load  ";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(685, 236);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(104, 41);
            this.button6.TabIndex = 6;
            this.button6.Text = "GET";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 236);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 26);
            this.label7.TabIndex = 5;
            this.label7.Text = "LINE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 26);
            this.label6.TabIndex = 4;
            this.label6.Text = "LOAD";
            // 
            // dataGrid_Load_Line
            // 
            this.dataGrid_Load_Line.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
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
            this.dataGrid_Load_Line.Location = new System.Drawing.Point(2, 264);
            this.dataGrid_Load_Line.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Load_Line.Name = "dataGrid_Load_Line";
            this.dataGrid_Load_Line.RowHeadersVisible = false;
            this.dataGrid_Load_Line.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGrid_Load_Line.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_Line.RowTemplate.Height = 35;
            this.dataGrid_Load_Line.Size = new System.Drawing.Size(443, 171);
            this.dataGrid_Load_Line.TabIndex = 3;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "PHASE";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.Width = 99;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.HeaderText = "U";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.Width = 48;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn8.HeaderText = "I";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn8.Width = 39;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn9.HeaderText = "P";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 28;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "POWER";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.Width = 106;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "-----------";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 101;
            // 
            // dataGrid_Load_load
            // 
            this.dataGrid_Load_load.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
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
            this.dataGrid_Load_load.Location = new System.Drawing.Point(2, 47);
            this.dataGrid_Load_load.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Load_load.Name = "dataGrid_Load_load";
            this.dataGrid_Load_load.RowHeadersVisible = false;
            this.dataGrid_Load_load.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGrid_Load_load.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_Load_load.RowTemplate.Height = 35;
            this.dataGrid_Load_load.Size = new System.Drawing.Size(737, 179);
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
            this.dataGridViewButtonColumn2.Width = 99;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "U";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.Width = 48;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "I";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.Width = 39;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "P";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.Width = 47;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "POWER";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.Width = 106;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "MODE";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.Width = 92;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "SET I, A";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 104;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "STATE";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn1.Width = 97;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.groupBoxBarcode3);
            this.tabPage9.Controls.Add(this.groupBoxBarcode2);
            this.tabPage9.Controls.Add(this.groupBoxBarcode1);
            this.tabPage9.Location = new System.Drawing.Point(4, 31);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage9.Size = new System.Drawing.Size(967, 531);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "  EVSE  ";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // groupBoxBarcode3
            // 
            this.groupBoxBarcode3.Controls.Add(this.label21);
            this.groupBoxBarcode3.Controls.Add(this.dataGrid_Barcode3);
            this.groupBoxBarcode3.Enabled = false;
            this.groupBoxBarcode3.Location = new System.Drawing.Point(653, 13);
            this.groupBoxBarcode3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode3.Name = "groupBoxBarcode3";
            this.groupBoxBarcode3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode3.Size = new System.Drawing.Size(298, 514);
            this.groupBoxBarcode3.TabIndex = 1;
            this.groupBoxBarcode3.TabStop = false;
            this.groupBoxBarcode3.Text = "3";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(119, 15);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 24);
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
            this.dataGrid_Barcode3.Location = new System.Drawing.Point(9, 63);
            this.dataGrid_Barcode3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Barcode3.Name = "dataGrid_Barcode3";
            this.dataGrid_Barcode3.RowHeadersVisible = false;
            this.dataGrid_Barcode3.RowHeadersWidth = 51;
            this.dataGrid_Barcode3.RowTemplate.Height = 24;
            this.dataGrid_Barcode3.Size = new System.Drawing.Size(285, 306);
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
            this.dataGridViewTextBoxColumn12.Width = 77;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "RESULT";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 108;
            // 
            // groupBoxBarcode2
            // 
            this.groupBoxBarcode2.Controls.Add(this.evse2_params);
            this.groupBoxBarcode2.Controls.Add(this.label20);
            this.groupBoxBarcode2.Controls.Add(this.dataGrid_Barcode2);
            this.groupBoxBarcode2.Enabled = false;
            this.groupBoxBarcode2.Location = new System.Drawing.Point(328, 13);
            this.groupBoxBarcode2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode2.Name = "groupBoxBarcode2";
            this.groupBoxBarcode2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode2.Size = new System.Drawing.Size(298, 514);
            this.groupBoxBarcode2.TabIndex = 1;
            this.groupBoxBarcode2.TabStop = false;
            this.groupBoxBarcode2.Text = "2";
            // 
            // evse2_params
            // 
            this.evse2_params.AutoSize = true;
            this.evse2_params.Location = new System.Drawing.Point(16, 379);
            this.evse2_params.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.evse2_params.Name = "evse2_params";
            this.evse2_params.Size = new System.Drawing.Size(28, 24);
            this.evse2_params.TabIndex = 2;
            this.evse2_params.Text = "---";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(115, 15);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 24);
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
            this.dataGrid_Barcode2.Location = new System.Drawing.Point(9, 63);
            this.dataGrid_Barcode2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Barcode2.Name = "dataGrid_Barcode2";
            this.dataGrid_Barcode2.RowHeadersVisible = false;
            this.dataGrid_Barcode2.RowHeadersWidth = 51;
            this.dataGrid_Barcode2.RowTemplate.Height = 24;
            this.dataGrid_Barcode2.Size = new System.Drawing.Size(285, 306);
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
            this.dataGridViewTextBoxColumn10.Width = 77;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "RESULT";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 108;
            // 
            // groupBoxBarcode1
            // 
            this.groupBoxBarcode1.Controls.Add(this.sta);
            this.groupBoxBarcode1.Controls.Add(this.dataGrid_Barcode1);
            this.groupBoxBarcode1.Enabled = false;
            this.groupBoxBarcode1.Location = new System.Drawing.Point(4, 13);
            this.groupBoxBarcode1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode1.Name = "groupBoxBarcode1";
            this.groupBoxBarcode1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxBarcode1.Size = new System.Drawing.Size(298, 514);
            this.groupBoxBarcode1.TabIndex = 0;
            this.groupBoxBarcode1.TabStop = false;
            this.groupBoxBarcode1.Text = "1";
            // 
            // sta
            // 
            this.sta.AutoSize = true;
            this.sta.Location = new System.Drawing.Point(110, 15);
            this.sta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sta.Name = "sta";
            this.sta.Size = new System.Drawing.Size(66, 24);
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
            this.dataGrid_Barcode1.Location = new System.Drawing.Point(9, 63);
            this.dataGrid_Barcode1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid_Barcode1.Name = "dataGrid_Barcode1";
            this.dataGrid_Barcode1.RowHeadersVisible = false;
            this.dataGrid_Barcode1.RowHeadersWidth = 51;
            this.dataGrid_Barcode1.RowTemplate.Height = 35;
            this.dataGrid_Barcode1.Size = new System.Drawing.Size(285, 306);
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
            this.Column20.Width = 77;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "RESULT";
            this.Column21.MinimumWidth = 6;
            this.Column21.Name = "Column21";
            this.Column21.Width = 108;
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
            this.debug_tab.Margin = new System.Windows.Forms.Padding(2);
            this.debug_tab.Name = "debug_tab";
            this.debug_tab.Padding = new System.Windows.Forms.Padding(2);
            this.debug_tab.Size = new System.Drawing.Size(984, 613);
            this.debug_tab.TabIndex = 3;
            this.debug_tab.Text = "Debug";
            // 
            // debug_ping_cbox
            // 
            this.debug_ping_cbox.AutoSize = true;
            this.debug_ping_cbox.Location = new System.Drawing.Point(709, 327);
            this.debug_ping_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_ping_cbox.Name = "debug_ping_cbox";
            this.debug_ping_cbox.Size = new System.Drawing.Size(73, 28);
            this.debug_ping_cbox.TabIndex = 9;
            this.debug_ping_cbox.Text = "PING";
            this.debug_ping_cbox.UseVisualStyleBackColor = true;
            this.debug_ping_cbox.CheckedChanged += new System.EventHandler(this.debug_ping_cbox_CheckedChanged);
            // 
            // debug_load_cbox
            // 
            this.debug_load_cbox.AutoSize = true;
            this.debug_load_cbox.Location = new System.Drawing.Point(709, 249);
            this.debug_load_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_load_cbox.Name = "debug_load_cbox";
            this.debug_load_cbox.Size = new System.Drawing.Size(177, 28);
            this.debug_load_cbox.TabIndex = 8;
            this.debug_load_cbox.Text = "EVSE/BARCODE";
            this.debug_load_cbox.UseVisualStyleBackColor = true;
            this.debug_load_cbox.CheckedChanged += new System.EventHandler(this.debug_load_cbox_CheckedChanged);
            // 
            // debug_siglent_cbox
            // 
            this.debug_siglent_cbox.AutoSize = true;
            this.debug_siglent_cbox.Location = new System.Drawing.Point(709, 203);
            this.debug_siglent_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_siglent_cbox.Name = "debug_siglent_cbox";
            this.debug_siglent_cbox.Size = new System.Drawing.Size(203, 28);
            this.debug_siglent_cbox.TabIndex = 7;
            this.debug_siglent_cbox.Text = "SPECTRO/SIGLENT";
            this.debug_siglent_cbox.UseVisualStyleBackColor = true;
            this.debug_siglent_cbox.CheckedChanged += new System.EventHandler(this.debug_siglent_cbox_CheckedChanged);
            // 
            // debug_gwinstek_cbox
            // 
            this.debug_gwinstek_cbox.AutoSize = true;
            this.debug_gwinstek_cbox.Location = new System.Drawing.Point(709, 163);
            this.debug_gwinstek_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_gwinstek_cbox.Name = "debug_gwinstek_cbox";
            this.debug_gwinstek_cbox.Size = new System.Drawing.Size(160, 28);
            this.debug_gwinstek_cbox.TabIndex = 6;
            this.debug_gwinstek_cbox.Text = "HV/GWINSTEK";
            this.debug_gwinstek_cbox.UseVisualStyleBackColor = true;
            this.debug_gwinstek_cbox.CheckedChanged += new System.EventHandler(this.debug_gwinstek_cbox_CheckedChanged);
            // 
            // debug_evse_cbox
            // 
            this.debug_evse_cbox.AutoSize = true;
            this.debug_evse_cbox.Location = new System.Drawing.Point(709, 123);
            this.debug_evse_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_evse_cbox.Name = "debug_evse_cbox";
            this.debug_evse_cbox.Size = new System.Drawing.Size(177, 28);
            this.debug_evse_cbox.TabIndex = 5;
            this.debug_evse_cbox.Text = "EVSE/BARCODE";
            this.debug_evse_cbox.UseVisualStyleBackColor = true;
            this.debug_evse_cbox.CheckedChanged += new System.EventHandler(this.debug_evse_cbox_CheckedChanged);
            // 
            // debug_main_cbox
            // 
            this.debug_main_cbox.AutoSize = true;
            this.debug_main_cbox.Location = new System.Drawing.Point(709, 5);
            this.debug_main_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_main_cbox.Name = "debug_main_cbox";
            this.debug_main_cbox.Size = new System.Drawing.Size(70, 28);
            this.debug_main_cbox.TabIndex = 4;
            this.debug_main_cbox.Text = "Main";
            this.debug_main_cbox.UseVisualStyleBackColor = true;
            this.debug_main_cbox.CheckedChanged += new System.EventHandler(this.debug_main_cbox_CheckedChanged);
            // 
            // dbg_list_clear
            // 
            this.dbg_list_clear.Location = new System.Drawing.Point(709, 375);
            this.dbg_list_clear.Margin = new System.Windows.Forms.Padding(2);
            this.dbg_list_clear.Name = "dbg_list_clear";
            this.dbg_list_clear.Size = new System.Drawing.Size(150, 32);
            this.dbg_list_clear.TabIndex = 3;
            this.dbg_list_clear.Text = "Clear";
            this.dbg_list_clear.UseVisualStyleBackColor = true;
            this.dbg_list_clear.Click += new System.EventHandler(this.dbg_list_clear_Click);
            // 
            // debug_usb_cbox
            // 
            this.debug_usb_cbox.AutoSize = true;
            this.debug_usb_cbox.Location = new System.Drawing.Point(709, 82);
            this.debug_usb_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_usb_cbox.Name = "debug_usb_cbox";
            this.debug_usb_cbox.Size = new System.Drawing.Size(66, 28);
            this.debug_usb_cbox.TabIndex = 2;
            this.debug_usb_cbox.Text = "USB";
            this.debug_usb_cbox.UseVisualStyleBackColor = true;
            this.debug_usb_cbox.CheckedChanged += new System.EventHandler(this.debug_usb_CheckedChanged);
            // 
            // debug_network_cbox
            // 
            this.debug_network_cbox.AutoSize = true;
            this.debug_network_cbox.Location = new System.Drawing.Point(709, 43);
            this.debug_network_cbox.Margin = new System.Windows.Forms.Padding(2);
            this.debug_network_cbox.Name = "debug_network_cbox";
            this.debug_network_cbox.Size = new System.Drawing.Size(98, 28);
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
            this.list_debug.Location = new System.Drawing.Point(2, 5);
            this.list_debug.Margin = new System.Windows.Forms.Padding(2);
            this.list_debug.Name = "list_debug";
            this.list_debug.Size = new System.Drawing.Size(696, 488);
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
            this.cbAdmin.Location = new System.Drawing.Point(44, 76);
            this.cbAdmin.Name = "cbAdmin";
            this.cbAdmin.Size = new System.Drawing.Size(135, 17);
            this.cbAdmin.TabIndex = 2;
            this.cbAdmin.Text = "Administratoriaus teises";
            this.cbAdmin.UseVisualStyleBackColor = true;
            // 
            // panelTestResult
            // 
            this.panelTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTestResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTestResult.Controls.Add(this.cbAdmin);
            this.panelTestResult.Location = new System.Drawing.Point(705, 677);
            this.panelTestResult.Name = "panelTestResult";
            this.panelTestResult.Size = new System.Drawing.Size(286, 156);
            this.panelTestResult.TabIndex = 0;
            // 
            // button_load_test_start
            // 
            this.button_load_test_start.Location = new System.Drawing.Point(289, 677);
            this.button_load_test_start.Name = "button_load_test_start";
            this.button_load_test_start.Size = new System.Drawing.Size(117, 23);
            this.button_load_test_start.TabIndex = 13;
            this.button_load_test_start.Text = "Start load test";
            this.button_load_test_start.UseVisualStyleBackColor = true;
            this.button_load_test_start.Click += new System.EventHandler(this.button_load_test_start_Click);
            // 
            // progressBar_load_test
            // 
            this.progressBar_load_test.Location = new System.Drawing.Point(473, 677);
            this.progressBar_load_test.Maximum = 50;
            this.progressBar_load_test.Name = "progressBar_load_test";
            this.progressBar_load_test.Size = new System.Drawing.Size(226, 23);
            this.progressBar_load_test.TabIndex = 14;
            // 
            // button_load_test_cancel
            // 
            this.button_load_test_cancel.Location = new System.Drawing.Point(412, 677);
            this.button_load_test_cancel.Name = "button_load_test_cancel";
            this.button_load_test_cancel.Size = new System.Drawing.Size(55, 23);
            this.button_load_test_cancel.TabIndex = 15;
            this.button_load_test_cancel.Text = "Cancel";
            this.button_load_test_cancel.UseVisualStyleBackColor = true;
            this.button_load_test_cancel.Click += new System.EventHandler(this.button_load_test_cancel_Click);
            // 
            // progressBar_HV_Test
            // 
            this.progressBar_HV_Test.Location = new System.Drawing.Point(473, 706);
            this.progressBar_HV_Test.Maximum = 39;
            this.progressBar_HV_Test.Name = "progressBar_HV_Test";
            this.progressBar_HV_Test.Size = new System.Drawing.Size(226, 23);
            this.progressBar_HV_Test.TabIndex = 17;
            // 
            // button_HV_Test_Cancel
            // 
            this.button_HV_Test_Cancel.Location = new System.Drawing.Point(412, 706);
            this.button_HV_Test_Cancel.Name = "button_HV_Test_Cancel";
            this.button_HV_Test_Cancel.Size = new System.Drawing.Size(55, 23);
            this.button_HV_Test_Cancel.TabIndex = 18;
            this.button_HV_Test_Cancel.Text = "Cancel";
            this.button_HV_Test_Cancel.UseVisualStyleBackColor = true;
            this.button_HV_Test_Cancel.Click += new System.EventHandler(this.button_HV_Test_Cancel_Click);
            // 
            // button_HV_Test_Start
            // 
            this.button_HV_Test_Start.Location = new System.Drawing.Point(289, 706);
            this.button_HV_Test_Start.Name = "button_HV_Test_Start";
            this.button_HV_Test_Start.Size = new System.Drawing.Size(117, 23);
            this.button_HV_Test_Start.TabIndex = 19;
            this.button_HV_Test_Start.Text = "Start HV test";
            this.button_HV_Test_Start.UseVisualStyleBackColor = true;
            this.button_HV_Test_Start.Click += new System.EventHandler(this.button_HV_Test_Start_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 735);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Spectroscope test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_Spectroscope_Test_Start);
            // 
            // button_Spectroscope_Test_Cancel
            // 
            this.button_Spectroscope_Test_Cancel.Location = new System.Drawing.Point(412, 735);
            this.button_Spectroscope_Test_Cancel.Name = "button_Spectroscope_Test_Cancel";
            this.button_Spectroscope_Test_Cancel.Size = new System.Drawing.Size(55, 23);
            this.button_Spectroscope_Test_Cancel.TabIndex = 21;
            this.button_Spectroscope_Test_Cancel.Text = "Cancel";
            this.button_Spectroscope_Test_Cancel.UseVisualStyleBackColor = true;
            this.button_Spectroscope_Test_Cancel.Click += new System.EventHandler(this.button_Spectroscope_Test_Cancel_Click);
            // 
            // progressBar_Spectroscope_Test
            // 
            this.progressBar_Spectroscope_Test.Location = new System.Drawing.Point(473, 735);
            this.progressBar_Spectroscope_Test.Maximum = 28;
            this.progressBar_Spectroscope_Test.Name = "progressBar_Spectroscope_Test";
            this.progressBar_Spectroscope_Test.Size = new System.Drawing.Size(226, 23);
            this.progressBar_Spectroscope_Test.TabIndex = 22;
            // 
            // button_Oscilloscope_Test
            // 
            this.button_Oscilloscope_Test.Location = new System.Drawing.Point(289, 764);
            this.button_Oscilloscope_Test.Name = "button_Oscilloscope_Test";
            this.button_Oscilloscope_Test.Size = new System.Drawing.Size(117, 23);
            this.button_Oscilloscope_Test.TabIndex = 23;
            this.button_Oscilloscope_Test.Text = "Oscilloscope test";
            this.button_Oscilloscope_Test.UseVisualStyleBackColor = true;
            this.button_Oscilloscope_Test.Click += new System.EventHandler(this.button_Oscilloscope_Test_Click);
            // 
            // button_Cancel_Oscilloscope
            // 
            this.button_Cancel_Oscilloscope.Location = new System.Drawing.Point(412, 764);
            this.button_Cancel_Oscilloscope.Name = "button_Cancel_Oscilloscope";
            this.button_Cancel_Oscilloscope.Size = new System.Drawing.Size(55, 23);
            this.button_Cancel_Oscilloscope.TabIndex = 24;
            this.button_Cancel_Oscilloscope.Text = "Cancel";
            this.button_Cancel_Oscilloscope.UseVisualStyleBackColor = true;
            this.button_Cancel_Oscilloscope.Click += new System.EventHandler(this.button_Cancel_Oscilloscope_Click);
            // 
            // progressBar_Oscilloscope
            // 
            this.progressBar_Oscilloscope.Location = new System.Drawing.Point(473, 764);
            this.progressBar_Oscilloscope.Maximum = 28;
            this.progressBar_Oscilloscope.Name = "progressBar_Oscilloscope";
            this.progressBar_Oscilloscope.Size = new System.Drawing.Size(226, 23);
            this.progressBar_Oscilloscope.TabIndex = 25;
            // 
            // button_oscilloscope_pulse
            // 
            this.button_oscilloscope_pulse.Location = new System.Drawing.Point(289, 793);
            this.button_oscilloscope_pulse.Name = "button_oscilloscope_pulse";
            this.button_oscilloscope_pulse.Size = new System.Drawing.Size(327, 23);
            this.button_oscilloscope_pulse.TabIndex = 26;
            this.button_oscilloscope_pulse.Text = "Oscilloscope pulse";
            this.button_oscilloscope_pulse.UseVisualStyleBackColor = true;
            this.button_oscilloscope_pulse.Click += new System.EventHandler(this.button_osc_pulse);
            // 
            // textBox_pulse_length
            // 
            this.textBox_pulse_length.Location = new System.Drawing.Point(622, 793);
            this.textBox_pulse_length.Name = "textBox_pulse_length";
            this.textBox_pulse_length.Size = new System.Drawing.Size(51, 20);
            this.textBox_pulse_length.TabIndex = 27;
            this.textBox_pulse_length.Text = "100000";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(679, 790);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(20, 23);
            this.label22.TabIndex = 28;
            this.label22.Text = "ns";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Estop
            // 
            this.label_Estop.BackColor = System.Drawing.Color.IndianRed;
            this.label_Estop.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Estop.Location = new System.Drawing.Point(12, 673);
            this.label_Estop.Name = "label_Estop";
            this.label_Estop.Size = new System.Drawing.Size(271, 143);
            this.label_Estop.TabIndex = 29;
            this.label_Estop.Text = "STOP";
            this.label_Estop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 833);
            this.Controls.Add(this.label_Estop);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.textBox_pulse_length);
            this.Controls.Add(this.button_oscilloscope_pulse);
            this.Controls.Add(this.progressBar_Oscilloscope);
            this.Controls.Add(this.button_Cancel_Oscilloscope);
            this.Controls.Add(this.button_Oscilloscope_Test);
            this.Controls.Add(this.progressBar_Spectroscope_Test);
            this.Controls.Add(this.button_Spectroscope_Test_Cancel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_HV_Test_Start);
            this.Controls.Add(this.button_HV_Test_Cancel);
            this.Controls.Add(this.progressBar_HV_Test);
            this.Controls.Add(this.button_load_test_cancel);
            this.Controls.Add(this.progressBar_load_test);
            this.Controls.Add(this.button_load_test_start);
            this.Controls.Add(this.panelTestResult);
            this.Controls.Add(this.tabControl1);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox_Printer.ResumeLayout(false);
            this.groupBox_Printer.PerformLayout();
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_HV_test)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
            this.PerformLayout();

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
        public System.ComponentModel.BackgroundWorker PrinterTCP;
        private System.ComponentModel.BackgroundWorker MainControllerMODBUS;
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
        private CheckBox debug_ping_cbox;
        private CheckBox debug_load_cbox;
        private CheckBox debug_siglent_cbox;
        private CheckBox debug_gwinstek_cbox;
        private GroupBox groupBox_main_relay;
        private GroupBox groupBox_pp_select;
        private GroupBox groupBox_tp_select;
        private GroupBox groupBox_evse_state;
        private GroupBox groupBox_checks;
        private System.Windows.Forms.Button button_load_test_start;
        private System.Windows.Forms.ProgressBar progressBar_load_test;
        private System.Windows.Forms.Button button_load_test_cancel;
        private System.Windows.Forms.ProgressBar progressBar_HV_Test;
        private System.Windows.Forms.Button button_HV_Test_Cancel;
        private System.Windows.Forms.Button button_HV_Test_Start;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_Spectroscope_Test_Cancel;
        private System.Windows.Forms.ProgressBar progressBar_Spectroscope_Test;
        private System.Windows.Forms.Button button_Oscilloscope_Test;
        private System.Windows.Forms.Button button_Cancel_Oscilloscope;
        private System.Windows.Forms.ProgressBar progressBar_Oscilloscope;
        private System.Windows.Forms.Button button_oscilloscope_pulse;
        private System.Windows.Forms.TextBox textBox_pulse_length;
        private Label label22;
        private Label label_Estop;
        private System.Windows.Forms.Button button_reset_modular_III;
        private System.Windows.Forms.Button button_reset_modular_II;
        private System.Windows.Forms.Button button_reset_modular_I;
        private GroupBox groupBox2;
        private RadioButton rad_btn_spect_EVSE_sel_3;
        private RadioButton rad_btn_spect_EVSE_sel_2;
        private RadioButton rad_btn_spect_EVSE_sel_1;
        private GroupBox groupBox3;
        private System.Windows.Forms.Button RCD_sel_plus;
        private System.Windows.Forms.Button RCD_sel_minus;
        private System.Windows.Forms.TextBox RCD_textBox;
        private Label label_RCD_current;
        private System.Windows.Forms.Button button_RCD_set;
        private RadioButton radioButton_RCD_L3;
        private RadioButton radioButton_RCD_L2;
        private RadioButton radioButton_RCD_L1;
        private GroupBox groupBox_Printer;
        private Label label23;
        private Label lbl_printer;
        private System.Windows.Forms.Button button_print_label;
        private GroupBox groupBox4;
        private Label label24;
        private System.Windows.Forms.TextBox tb_test_param_pow_mrelay_off_min;
        private System.Windows.Forms.TextBox tb_test_param_pow_mrelay_off_max;
        private Label label27;
        private System.Windows.Forms.TextBox tb_test_param_pow_mrelay_on_max;
        private System.Windows.Forms.TextBox tb_test_param_pow_mrelay_on_min;
        private Label label28;
        private Label label26;
        private Label label25;
        private Label label29;
        private System.Windows.Forms.TextBox tb_test_param_insul_ac_max;
        private System.Windows.Forms.TextBox tb_test_param_insul_ac_min;
        private Label label30;
        private Label label31;
        private System.Windows.Forms.TextBox tb_test_param_insul_dc_max;
        private System.Windows.Forms.TextBox tb_test_param_insul_dc_min;
        private Label label32;
        private Label label33;
        private System.Windows.Forms.TextBox tb_test_param_r_gnd_max;
        private System.Windows.Forms.TextBox tb_test_param_r_gnd_min;
        private Label label34;
        private Label label35;
        private System.Windows.Forms.TextBox tb_test_param_resid_dc_max;
        private System.Windows.Forms.TextBox tb_test_param_resid_dc_min;
        private Label label36;
        private Label label37;
        private System.Windows.Forms.TextBox tb_test_param_wifi_speed_max;
        private System.Windows.Forms.TextBox tb_test_param_wifi_speed_min;
        private Label label38;
        private Label label41;
        private System.Windows.Forms.TextBox tb_test_param_rfid_range_max;
        private System.Windows.Forms.TextBox tb_test_param_rfid_range_min;
        private Label label42;
        private Label label39;
        private System.Windows.Forms.TextBox tb_test_param_gsm_speed_max;
        private System.Windows.Forms.TextBox tb_test_param_gsm_speed_min;
        private Label label40;
        private Label label43;
        private System.Windows.Forms.TextBox tb_test_param_firmware_max;
        private System.Windows.Forms.TextBox tb_test_param_firmware_min;
        private Label label44;
        private Label label45;
        private System.Windows.Forms.TextBox tb_test_param_ihold_6a_max;
        private System.Windows.Forms.TextBox tb_test_param_ihold_6a_min;
        private Label label46;
        private Label label47;
        private System.Windows.Forms.TextBox tb_test_param_ihold_32a_max;
        private System.Windows.Forms.TextBox tb_test_param_ihold_32a_min;
        private Label label48;
    }
}

