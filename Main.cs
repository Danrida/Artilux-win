using MonitorsTest.Models;
using MonitorsTest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using static Ion.Tools.Models.XmlDataExport.Graph;
using Newtonsoft.Json.Converters;
using System.Text.RegularExpressions;
using System.Windows;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using static Ion.Sdk.Idi.Value.Constraint;

namespace ArtiluxEOL
{
    public partial class Main : Form
    {
        public static Main main;

        static RegistryKey Config_reg;
        static RegistryKey Workplaces_reg;
        static RegistryKey Periphery_reg;

        bool debug_network = false;
        bool debug_usb = false;

        public bool init_done = false;
        //public static Main main_win;

        Label[,] EvseParmsInTable;



        #region <<< KINTAMIEJI SERIAL PORT >>>

        public static int portsFoundBefore = 0;

        public const int PORT_SVARST_A1 = 0;
        public const int PORT_SVARST_A2 = 1;
        public const int PORT_SVARST_B2 = 2;
        public const int PORT_SVARST_B1 = 3;
        public const int PORT_ARM_BOARD = 4;
        public const int PORT_ALKOTEST = 5;
        public const int PORT_NONE = 255;

        int found_ser_nr = 0;
        int METERL_PORT = 0;
        int OSCIL_PORT = 0;

        public string[] serial_temp = new string[7];
        public static List<string> uart_buff = new List<string>();
        public byte[] uart_data;

        //Metrel_bb Met_bbox_test = new Metrel_bb();
        SocketClient Socket_ = new SocketClient();
        NetworkThreads NetworkThreads = new NetworkThreads();

        public void deb(string str)
        {
            //get { return list_debug; }
            //set { Console.WriteLine($"Port0:{str}"); }
        }

        bool load_param_enable_edit = false;





        public class portas // BUTINAI CLASS NES NORIM PRIEITI PRIE VARIABLU
        {
            public SerialPort port;
            public bool port_active;
            public int timeout = 0;
            public string id;
            public string dev_name;
            public List<string> cmd;
            public string NewLine;



        }

        List<portas> SerPorts = new List<portas>();
        #endregion

        int serial_dev_count = 2;
        public string[] devices_info = new string[8];

        UInt16 WorkplacesCount = 0;

        //UInt64[] SavedWorkplaces;

        System.Windows.Forms.ComboBox[] cBoxWplace = new System.Windows.Forms.ComboBox[4];
        System.Windows.Forms.TextBox[] TextBox_dev_info = new System.Windows.Forms.TextBox[13];
        CheckBox[] CheckBox_dev_info = new CheckBox[12];
        CheckBox[] CheckBox_lizdai = new CheckBox[3];

        public List<MonitorTest> mtlist = new List<MonitorTest>();
        List<MonitorTest> ml = new List<MonitorTest>();//monitor list

        public List<WorkplaceList> SavedWorkplaces = new List<WorkplaceList>();
        public List<SocketDevList> network_dev = new List<SocketDevList>();

        public DevList devList;
        public Test_struc[] Test = new Test_struc[3];

        public Main()
        {
            Config_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Configs");
            Workplaces_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Workplaces");
            Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Devices");

            //SavedWorkplaces = new UInt64[5];
            //workplace
            cBoxWplace[0] = new System.Windows.Forms.ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 40), Size = new Size(200, 37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200 };
            cBoxWplace[1] = new System.Windows.Forms.ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 100), Size = new Size(200, 37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200 };
            cBoxWplace[2] = new System.Windows.Forms.ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 165), Size = new Size(200, 37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200 };

            InitializeComponent();
            main = this;

            this.groupBox1.Controls.Add(cBoxWplace[0]); //Darbo vietos nr prikyrimas
            this.groupBox1.Controls.Add(cBoxWplace[1]);
            this.groupBox1.Controls.Add(cBoxWplace[2]);

            #region <<< Serial Ports INIT >>>
            // suinitinam serial portus
            int a;
            for (a = 0; a < 7; a++)
            {
                portas p = new portas();
                p.port = new SerialPort();
                p.port_active = false;
                p.cmd = new List<string>();
                p.NewLine = "\r";
                SerPorts.Add(p);
            }
            // sukonfiguruojam objektus
            /*SerPorts[0].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP0_DataRx);
            SerPorts[0].id = tb_nust_svarA1ID;//.Text.Trim().ToUpper();
            SerPorts[1].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP1_DataRx);
            SerPorts[1].id = tb_nust_svarA2ID;//.Text.Trim().ToUpper();
            SerPorts[2].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP2_DataRx);
            SerPorts[2].id = tb_nust_svarB2ID;//.Text.Trim().ToUpper();
            SerPorts[3].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP3_DataRx);
            SerPorts[3].id = tb_nust_svarB1ID;//.Text.Trim().ToUpper();
            SerPorts[4].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP4_DataRx);
            SerPorts[4].id = tb_nust_mcuID;//.Text;//.Trim().ToUpper();
            SerPorts[5].port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SP5_DataRx);
            SerPorts[5].id = tb_nust_alkotestID;//.Text.Trim().ToUpper();
            SerPorts[6].id = new TextBox(); // sukurima tuscia*/

            //tabControl1.SelectTab(3); //pradzioje rodom debug langa
            #endregion

            DbgType.NETWORK = true;
            debug_network_cbox.Checked = true;
            DbgType.MAIN = true;
            debug_main_cbox.Checked = true;
            DbgType.EVSE = true;
            debug_evse_cbox.Checked = true;
            DbgType.HV_GEN = false;
            debug_gwinstek_cbox.Checked = false;
            DbgType.SPECTRO = false;
            debug_siglent_cbox.Checked = false;
            DbgType.LOAD = false;
            debug_load_cbox.Checked = false;
            DbgType.PING = true;
            debug_ping_cbox.Checked = true;

            WorkplaceList wplace;
            wplace = new WorkplaceList { WorplaceMonitorID = 123456789, Enable = true, BarcodePort = 1111 };
            SavedWorkplaces.Add(wplace);
            wplace = new WorkplaceList { WorplaceMonitorID = 123456789, Enable = true, BarcodePort = 1111 };
            SavedWorkplaces.Add(wplace);
            wplace = new WorkplaceList { WorplaceMonitorID = 123456789, Enable = true, BarcodePort = 1111 };
            SavedWorkplaces.Add(wplace);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            get_all_monitors();

            //if (Workplaces_reg != null)
            //{
            regReadWplace();
            //}

            ip_texbox_show();//irenginiu IP adresu nustatymas
            //serial port init

            load_dev_control();

            //dbg_print(DbgType.MAIN, "NetworkThread", Color.MediumSeaGreen);

            devList.DevEvse = new DevEvse_struc[3];//label pointeris

            for (int a = 0; a < 3; a++)//initinam structuras
            {
                Test[a].test_type = new TestType_struc[9];
                Test[a].evse_barcode = "-";
                for (int x = 0; x < 9; x++)
                {
                    Test[a].test_type[x].name = "a";
                }
                devList.DevEvse[a].barcode = "- - *** - -";
                devList.DevEvse[a].voltage = new UInt32[3];
                devList.DevEvse[a].current = new UInt32[3];
            }
        }

        #region Debug list
        public void dbg_print(bool dbg_type, string str, Color color)
        {
            if (!dbg_type)//einam lauk jei dbg isjungtas
            {
                return;
            }
            ListViewItem lv = null;

            list_debug.Invoke((MethodInvoker)(() => lv = list_debug.Items.Add(str)));
            // nutrinam visus senesnius nei XXX irasu
            while (list_debug.Items.Count > 80)
            {
                list_debug.Invoke((MethodInvoker)(() => list_debug.Items.RemoveAt(0)));
            }
            int nr = (list_debug.Items.Count - 1);
            lv.UseItemStyleForSubItems = false;
            lv.ForeColor = color;

            list_debug.Invoke((MethodInvoker)(() => list_debug.Update()));
        }

        private void debug_main_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.MAIN = debug_main_cbox.Checked;
        }

        private void debug_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.NETWORK = debug_network_cbox.Checked;
        }

        private void debug_usb_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.USB = debug_usb_cbox.Checked;
        }

        private void debug_evse_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.EVSE = debug_evse_cbox.Checked;
        }

        private void debug_gwinstek_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.HV_GEN = debug_gwinstek_cbox.Checked;
        }

        private void debug_siglent_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.SPECTRO = debug_siglent_cbox.Checked;
        }

        private void debug_load_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.LOAD = debug_load_cbox.Checked;
        }

        private void debug_ping_cbox_CheckedChanged(object sender, EventArgs e)
        {
            DbgType.PING = debug_ping_cbox.Checked;
        }

        private void dbg_list_clear_Click(object sender, EventArgs e)
        {
            this.list_debug.Items.Clear();
        }

        #endregion

        #region IP ivedimo laukai, checkboxai...

        public void update_all_device_ctrl_access()
        {
            for (int a = 0; a < network_dev.Count; a++)
            {
                switch (a)
                {
                    case NetDev_Tab.MAIN_CONTROLLER:
                        break;
                    case NetDev_Tab.HW_TESTER:
                        break;
                    case NetDev_Tab.SIGLENT:
                        //groupBox_Spectr.Enabled = network_dev[a].Connected;
                        break;
                    case NetDev_Tab.ITECH_LOAD:
                        //groupBox_Load.Enabled = network_dev[a].Connected;
                        //this.tabPage8.
                        break;
                    case NetDev_Tab.BARCODE_1:
                        this.groupBoxBarcode1.Enabled = network_dev[a].Connected;
                        dataGrid_Barcode1.Enabled = network_dev[a].Connected;
                        break;
                    case NetDev_Tab.BARCODE_2:
                        this.groupBoxBarcode2.Enabled = network_dev[a].Connected;
                        dataGrid_Barcode2.Enabled = network_dev[a].Connected;
                        break;
                    case NetDev_Tab.BARCODE_3:
                        this.groupBoxBarcode3.Enabled = network_dev[a].Connected;
                        dataGrid_Barcode3.Enabled = network_dev[a].Connected;
                        break;
                    case NetDev_Tab.OSCILOSCOPE:
                        break;
                }

            }
        }


        void lizdai_checbox_change()//EVSE test lizdai, indijuojam busena
        {
            for (int a = 0; a < CheckBox_lizdai.Length; a++)
            {
                bool state;
                state = CheckBox_lizdai[a].Checked;
                if (SavedWorkplaces[a].Enable != state)
                {
                    network_dev[NetDev_Tab.BARCODE_1 + a].Enable = state;
                    network_dev[NetDev_Tab.RFID_1 + a].Enable = state;
                    SavedWorkplaces[a].Enable = state;
                    regUpdatePeriphery(a);
                    regUpdateWplace();
                }

                if (state)
                {
                    if (network_dev[NetDev_Tab.BARCODE_1 + a].Connected || network_dev[NetDev_Tab.RFID_1 + a].Connected)
                    {
                        device_state_indication(NetDev_Tab.BARCODE_1 + a, Color.SpringGreen);//pazymim raudonai, jei rasim pakeisim i zalia
                        device_state_indication(NetDev_Tab.RFID_1 + a, Color.SpringGreen);//pazymim raudonai, jei rasim pakeisim i zalia
                    }
                    else
                    {
                        device_state_indication(NetDev_Tab.BARCODE_1 + a, Color.LightCoral);//pazymim raudonai, jei rasim pakeisim i zalia
                        device_state_indication(NetDev_Tab.RFID_1 + a, Color.LightCoral);//pazymim raudonai, jei rasim pakeisim i zalia
                    }
                }
                else
                {
                    device_state_indication(NetDev_Tab.BARCODE_1 + a, Color.Gainsboro);//pazymim zymim pilkai
                    device_state_indication(NetDev_Tab.RFID_1 + a, Color.Gainsboro);//pazymim zymim pilkai
                }
            }

        }

        private void Checkboxes_lizdai_handler(object sender, EventArgs e)
        {
            lizdai_checbox_change();
        }
        private void ShowCheckedCheckboxes(object sender, EventArgs e)//indijuojam irangos busena
        {
            bool state = false;
            if (init_done)
            {

                for (int a = 0; a < CheckBox_dev_info.Length; a++)
                {
                    if (a < 4 || a > 9)
                    {
                        state = CheckBox_dev_info[a].Checked;

                        if (network_dev[a].Enable != state)
                        {
                            network_dev[a].Enable = state;
                            regUpdatePeriphery(a);
                        }


                        if (state)
                        {
                            if (network_dev[a].Connected)
                            {
                                device_state_indication(a, Color.SpringGreen);//pazymim raudonai, jei rasim pakeisim i zalia
                            }
                            else
                            {
                                device_state_indication(a, Color.LightCoral);//pazymim raudonai, jei rasim pakeisim i zalia
                            }
                        }
                        else
                        {
                            device_state_indication(a, Color.Gainsboro);//pazymim zymim pilkai 
                        }
                    }
                }
            }
        }

        public void ip_texbox_show()
        {//dev ip ivedino laukeliai ir devaisu enable checbox
            int cbox_y_location = 30;
            Font textbox_font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            SocketDevList dev;
            dev = new SocketDevList { Name = "MAIN_CONTROLLER", TestMsg = "TEST?", client = null, Ip = "192.168.11.85", Port_0 = 5566, Port_1 = 5567, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "HV_TEST", TestMsg = "SYSTEM:TIME?", client = null, Ip = "192.168.11.150", Port_0 = 12312, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "SIGLENT", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 50252, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "ITECH", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 11311, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "BARCODE_1", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 11311, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "BARCODE_2", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 11311, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "BARCODE_3", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 11311, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "RFID_1", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "RFID_2", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "RFID_3", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            //dev = new SocketDevList { Name = "Power", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            //network_dev.Add(dev);
            dev = new SocketDevList { Name = "METREL", TestMsg = "BB;", client = null, Ip = "0", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "OSCIL", TestMsg = "---", client = null, Ip = "0", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);

            EvseParmsInTable = new Label[3, 9];

            regReadPeriphery();

            for (int x = 0; x < network_dev.Count; x++)
            {
                //////IP ivedimo laukai, barcode ir rfid turim po tris todel reikia keisti ivedimo laukelio pozicija, tam sis case.
                TextBox_dev_info[x] = new System.Windows.Forms.TextBox { Enabled = true, Font = textbox_font, Location = new Point(x: 100, y: 45), Size = new Size(220, 24) };
                switch (x)
                {
                    case 4:
                    case 7:
                        TextBox_dev_info[x] = new System.Windows.Forms.TextBox { Enabled = true, Font = textbox_font, Location = new Point(x: 100, y: 45), Size = new Size(220, 24) };
                        break;
                    case 5:
                    case 8:
                        TextBox_dev_info[x] = new System.Windows.Forms.TextBox { Enabled = true, Font = textbox_font, Location = new Point(x: 100, y: 85), Size = new Size(220, 24) };
                        break;
                    case 6:
                    case 9:
                        TextBox_dev_info[x] = new System.Windows.Forms.TextBox { Enabled = true, Font = textbox_font, Location = new Point(x: 100, y: 120), Size = new Size(220, 24) };
                        break;
                }
                //////////////////////

                //kuram devaisu en checbox, bet skipinam barcode ir rfid, juos enablinam su testo vietos chebox, Saved_workplaces variable laikom visa info
                if (x < 4 || x > 9)
                {
                    CheckBox_dev_info[x] = new CheckBox { Checked = true, Location = new Point(x: 225, y: cbox_y_location) };
                    CheckBox_dev_info[x].CheckedChanged += new EventHandler(ShowCheckedCheckboxes);

                    this.panel1.Controls.Add(CheckBox_dev_info[x]);

                    if (x < 3)
                    {
                        CheckBox_lizdai[x] = new CheckBox { Text = "", Checked = true, Location = new Point(x: 90, y: 35) };
                        CheckBox_lizdai[x].Checked = SavedWorkplaces[x].Enable;
                        CheckBox_lizdai[x].CheckedChanged += new EventHandler(Checkboxes_lizdai_handler);
                        switch (x)
                        {
                            case 0:
                                this.Test_lizdas_1.Controls.Add(CheckBox_lizdai[x]);
                                break;
                            case 1:
                                this.Test_lizdas_2.Controls.Add(CheckBox_lizdai[x]);
                                break;
                            case 2:
                                this.Test_lizdas_3.Controls.Add(CheckBox_lizdai[x]);
                                break;
                        }

                    }

                    CheckBox_dev_info[x].Checked = network_dev[x].Enable;//setinam kurie enable

                    if (CheckBox_dev_info[x].Checked)
                    {
                        device_state_indication(x, Color.LightCoral);//pazymim raudonai, jei rasim pakeisim i zalia 
                    }

                    cbox_y_location += 64;
                }
                else
                {

                }


                // priskiram ip ir porta i laukus
                if (network_dev[x].Port_1 == 0)
                {

                    TextBox_dev_info[x].Text = network_dev[x].Ip + ':' + network_dev[x].Port_0;//ip port setings
                }
                else
                {
                    TextBox_dev_info[x].Text = network_dev[x].Ip + ':' + network_dev[x].Port_0 + ':' + network_dev[x].Port_1;//ip port setings
                }


            }

            lizdai_checbox_change();

            this.groupBox_Valdiklis.Controls.Add(TextBox_dev_info[0]);
            this.groupBox_HVgen.Controls.Add(TextBox_dev_info[1]);
            this.groupBox_Spectr.Controls.Add(TextBox_dev_info[2]);
            this.groupBox_Load.Controls.Add(TextBox_dev_info[3]);
            this.groupBox_Barcode.Controls.Add(TextBox_dev_info[4]);
            this.groupBox_Barcode.Controls.Add(TextBox_dev_info[5]);
            this.groupBox_Barcode.Controls.Add(TextBox_dev_info[6]);

            this.groupBox_Rfid.Controls.Add(TextBox_dev_info[7]);
            this.groupBox_Rfid.Controls.Add(TextBox_dev_info[8]);
            this.groupBox_Rfid.Controls.Add(TextBox_dev_info[9]);
            this.groupBox_Metrel_USB.Controls.Add(TextBox_dev_info[10]);
            this.groupBox_Osc_USB.Controls.Add(TextBox_dev_info[11]);

            groupBoxBarcode2.Controls.Add(evse2_params);

            //NetworkDevConn.WorkerSupportsCancellation = true;
            NetworkDevConn.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.NetworkDevConn_DoWork);
            NetworkDevConn.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(NetworkThreads.NetworkDevConn_RunWorkerCompleted);
            NetworkDevConn.RunWorkerAsync();
            NetworkDevConn.WorkerSupportsCancellation = true;
            MainControllerTCP.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.MainControllerTCP_DoWork);
            //MainControllerTCP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(MainControllerTCP_RunWorkerCompleted);
            //MainControllerTCP.RunWorkerAsync();
            //NetworkDevConn.WorkerSupportsCancellation = true;
            //MainControllerMODBUS.DoWork += new System.ComponentModel.DoWorkEventHandler(MainControllerMODBUS_DoWork);
            //MainControllerMODBUS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(MainControllerMODBUS_RunWorkerCompleted);
            //MainControllerMODBUS.RunWorkerAsync();

            HVgen.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.HVgen_DoWork);
            Specroscope.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.Specroscope_DoWork);
            Load.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.Load_DoWork);

            Barcode1.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.Barcode1_DoWork);
            Barcode2.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.Barcode2_DoWork);
            Barcode3.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkThreads.Barcode3_DoWork);

            init_done = true;
        }

        private void save_ip_Click(object sender, EventArgs e)
        {
            string str;
            string[] subs;
            //string[] ip;
            string ip;
            bool update = false;
            int port;

            for (int i = 0; i < network_dev.Count; i++)
            {
                str = TextBox_dev_info[i].Text.ToString();
                subs = str.Split(':');//subs[0]=ip,subs[1]=port_0,subs[2]=port_1,

                if ((subs.Length > 1) || i > 5)// turim turet nors viena porta)
                {
                    ip = Convert.ToString(subs[0]);

                    if (!String.Equals(network_dev[i].Ip, ip))
                    {
                        network_dev[i].Ip = ip;
                        update = true;
                    }

                    port = Convert.ToInt32(subs[1]);

                    if (port > 0)
                    {
                        if (network_dev[i].Port_0 != port)
                        {
                            network_dev[i].Port_0 = port;
                            update = true;
                        }

                    }
                    else
                    {
                        System.Diagnostics.Debug.Print($"PORT_0 ERROR: = {port}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Print($"PORT_NOT_FOUND: = {0}");
                }

                if (subs.Length > 2)// jei turim ivesta 2 porta tikrinam ar ne 0
                {
                    port = Convert.ToInt16(subs[2]);

                    if (port > 0)
                    {
                        if (network_dev[i].Port_0 != port)
                        {
                            network_dev[i].Port_1 = port;
                            update = true;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.Print($"PORT_1 ERROR: = {port}");
                    }
                }
                else
                {
                    network_dev[i].Port_1 = 0;
                }

                if (update)
                {
                    update = false;
                    regUpdatePeriphery(i);
                }
            }
        }
        #endregion

        public void device_state_indication(int dev_nr, Color color)
        {
            switch (dev_nr)
            {
                case 0:
                    lbl_vald.BackColor = color;
                    break;

                case 1:
                    lbl_hvgen.BackColor = color;
                    break;
                case 2:
                    lbl_specrum.BackColor = color;
                    break;
                case 3:
                    lbl_load.BackColor = color;
                    break;
                case 4:
                    lbl_barcode_1.BackColor = color;
                    break;
                case 5:
                    lbl_barcode_2.BackColor = color;
                    break;
                case 6:
                    lbl_barcode_3.BackColor = color;
                    break;
                case 7:
                    lbl_rfid_1.BackColor = color;
                    break;
                case 8:
                    lbl_rfid_2.BackColor = color;
                    break;
                case 9:
                    lbl_rfid_3.BackColor = color;
                    break;
                case 10:
                    lbl_evse.BackColor = color;
                    break;
                case 11:
                    lbl_osc.BackColor = color;
                    break;
            }
        }


        #region ---SERIAL PORTS ROUTINES---

        // tikrinam/handlinam serial portus
        int send_err = 5;
        private void tmr_1hz_Tick(object sender, EventArgs e)
        {

            string[] PCportai = SerialPort.GetPortNames();
            string msg = "";
            int nr = (SerPorts.Count - 1); // paskutinio netikrinam, jis temporary
            //Console.WriteLine("PCports=" + PCportai.Length + " before=" + portsFoundBefore);
            // jai sumazejo portu
            if (portsFoundBefore > PCportai.Length)
            {
                dbg_print(DbgType.USB, "", Color.DimGray);
                dbg_print(DbgType.USB, "-DISCONNECTED " + (portsFoundBefore - PCportai.Length) + "-\n", Color.LightCoral);
                for (int a = 0; a < nr; a++)
                {
                    //dbg_print("\r\n"+PCportai.Count().ToString());
                    if (PCportai.Count() >= 0)
                    {
                        bool toks_yra = false;
                        if (SerPorts[a].port_active)
                        {
                            // prasukam visus ir patikrinam ar toks dar yra
                            foreach (string pp in PCportai)
                            {
                                if (SerPorts[a].port.PortName.ToString().Equals(pp))
                                {
                                    toks_yra = true;

                                }
                            }
                            // jai tokio jau nebera, uzdarom
                            if (!toks_yra)
                            {
                                SerPorts[a].port_active = false;
                                dbg_print(DbgType.USB, "  close port " + SerPorts[a].port.PortName.ToString(), Color.LightCoral);
                                if (a == PORT_ALKOTEST)
                                {
                                    //Alko_serial_close_end();
                                    lbl_evse.BackColor = Color.LightCoral;
                                }
                            }

                        }
                    }
                    // viska diseiblinam nes nera portu
                    else
                    {
                        SerPorts[a].port_active = false;
                    }
                }
                portsFoundBefore = PCportai.Length;
            }
            // atsirado naujas(ji) portas(ai)
            else if (portsFoundBefore < PCportai.Length)
            {
                dbg_print(DbgType.USB, "", Color.DimGray);
                dbg_print(DbgType.USB, "-CONNECTED " + (PCportai.Length - portsFoundBefore) + "-", Color.SpringGreen);
                for (int b = 0; b < PCportai.Count(); b++)
                {
                    bool nenaudojamas = true;
                    // tikrinam ar toks portas jau naudojamas
                    for (int a = 0; a < SerPorts.Count; a++)
                    {
                        if (PCportai[b].Equals(SerPorts[a].port.PortName))
                        {
                            if (SerPorts[a].port_active)
                            {
                                // portas naudojamas, pazymim, baigiam tikrinima
                                nenaudojamas = false;
                                a = SerPorts.Count;
                            }
                        }
                    }


                    // jai neradom kad sitas portas butu naudojamas, tikrinam ar galim naudoti ir ar gaunam koki nors ID
                    if (nenaudojamas)
                    {
                        // jai paskutinis neuzimtas, bandom (sito reikia jai taimeris jau kazka tikrina ir uztrukus ilgiau vel suveikai taimeris ir paleidzia tikrinti per nauja)
                        if (!SerPorts[nr].port_active)
                        {
                            if (try_port(PCportai[b]))
                            {
                                // radom neuzimta porta kuris grazina ID, tikrinam ar jis kam nors tinkamas, jai taip tada priskiriam
                                bool priskirtas_sekmingai = false;
                                string id = SerPorts[nr].id.Trim().ToUpper();

                                for (int a = 0; a < nr; a++)
                                {
                                    // radau atitinkama ID
                                    //if (SerPorts[a].id.Trim().ToUpper().Equals(id))
                                    //{
                                    //msg += "  radau " + SerPorts[nr].port.PortName + "_id = " + SerPorts[a].id.Trim().ToUpper();
                                    if (!SerPorts[a].port_active)
                                    {
                                        //SerPorts[a].port.Dispose();
                                        //SerPorts[nr].port.Dispose();

                                        try
                                        {


                                            SerPorts[a].port.Close();
                                            //  this.BeginInvoke(new EventHandler(delegate { SerPorts[a].port.Close(); }));f
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);

                                        }
                                        //SerPorts[nr].port.Dispose();
                                        try
                                        {

                                            SerPorts[nr].port.Close();
                                            //this.BeginInvoke(new EventHandler(delegate { SerPorts[nr].port.Close(); }));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);

                                        }
                                        //

                                        switch (found_ser_nr)
                                        {
                                            case 4:
                                                SerPorts[found_ser_nr].dev_name = "METREL";
                                                METERL_PORT = found_ser_nr;
                                                break;
                                            case 5:
                                                SerPorts[found_ser_nr].dev_name = "OSCIL";
                                                OSCIL_PORT = found_ser_nr;
                                                break;
                                        }

                                        SerPorts[found_ser_nr].port.PortName = SerPorts[nr].port.PortName;
                                        SerPorts[found_ser_nr].port.BaudRate = SerPorts[nr].port.BaudRate;
                                        //SerPorts[found_ser_nr].port.Open();
                                        SerPorts[found_ser_nr].port_active = true;
                                        SerPorts[found_ser_nr].timeout = 10; // sitas butinai nes timeoutins
                                        SerPorts[nr].port_active = false;
                                        a = nr; // baigiam tikrinaima



                                        msg += " ... RADOM " + SerPorts[found_ser_nr].dev_name;
                                        priskirtas_sekmingai = true;
                                    }
                                    else
                                    {
                                        msg += " ... ! KLAIDA - jau atidarytas !";
                                    }
                                    dbg_print(DbgType.USB, msg, Color.LightCoral);
                                    //}

                                }
                                if (!priskirtas_sekmingai) // jai niekam nepavyko priskirti bet portas grazino ID, informuojam apie tai
                                {
                                    dbg_print(DbgType.USB, "  neatpazintas " + SerPorts[nr].port.PortName + " ID ... skip", Color.LightCoral);
                                    //SerPorts[nr].port.Dispose();
                                    try
                                    {

                                        SerPorts[nr].port.Close();
                                        //this.BeginInvoke(new EventHandler(delegate { SerPorts[nr].port.Close(); }));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);

                                    }
                                    SerPorts[nr].port_active = false;
                                }
                            }
                        }
                    }
                    // jai pasibaige startup seka perjugiam puslapi i pirma svarstykliu
                    /* if ((startup) && (b == (PCportai.Count() - 1)))
                     {
                         tabControl.SelectTab(0); // portai pravaziavo, rodom pirma puslapi
                         startup = false; //daugiau niekada i cia nebegryztam
                     }*/
                }
                portsFoundBefore = PCportai.Length;
            }


            // keiciam serial portu spalva pagal statusa
            for (int a = 0; a < 4; a++)
            {
                if (SerPorts[a].port_active)
                {
                    /*svarstykles[a].connected = true;
                    Tara[a].connected = true;
                    startupas_svr(a);*/
                }
                else
                {
                    /* svarstykles[a].connected = false;
                     Tara[a].connected = false;*/
                }
            }

            //if (a == 5)
            // {
            if (SerPorts[OSCIL_PORT].port_active)
            {
                //ijungiam mikrovaldiklis urasas zaliai
                lbl_osc.BackColor = Color.SpringGreen;

            }
            else
            {
                //isjungiam mikrovaldiklis uzrasa zaliai
                lbl_osc.BackColor = Color.LightCoral;
                /*Tara[PORT_SVARST_A1].reset_temp();
                Tara[PORT_SVARST_A2].reset_temp();
                Tara[PORT_SVARST_B1].reset_temp();
                Tara[PORT_SVARST_B2].reset_temp();*/

            }

            if (SerPorts[METERL_PORT].port_active)
            {
                lbl_evse.BackColor = Color.SpringGreen;
            }
            else
            {
                //if (a == PORT_ALKOTEST)
                //{
                //Alko_serial_close_end();
                lbl_evse.BackColor = Color.LightCoral;
                //}

            }

            // }
            if (send_err > 0)
            {
                send_err--;
            }
            else
            {

                SerPort_transmit(PORT_ALKOTEST, "ER 255\r\n");
                send_err = 5;
            }

        }

        private bool SerPort_transmit(int nr, string str)
        {
            if (SerPorts[nr].port_active)
            {
                if (SerPorts[nr].port.IsOpen)
                {
                    try
                    {
                        SerPorts[nr].port.Write(str);

                        //Console.Write(str);
                        // Console.WriteLine(nr.ToString());
                    }
                    catch (Exception ex)
                    {
                        dbg_print(DbgType.USB, "Exc!_USB_SEND  :", Color.LightCoral);
                        Console.WriteLine(ex);
                        SerPorts[nr].port_active = false;
                        //SerPorts[nr].port.Close();
                        //SerPorts[nr].port.Dispose();

                        try
                        {

                            SerPorts[nr].port.Close();
                            //this.BeginInvoke(new EventHandler(delegate { SerPorts[nr].port.Close(); }));
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1);

                        }
                        dbg_print(DbgType.USB, "! ERROR port " + SerPorts[nr].port.PortName + " lost", Color.LightCoral);
                    }
                }
                else
                {

                }
                return true;
            }
            else
            {
                return false;
                // dbg_print("inactive" + SerPorts[nr].port.PortName + " lost");
            }
        }

        private void tmr_5hz_Tick(object sender, EventArgs e)
        {
            //   this.myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            //   Pen blackPen = new Pen(Color.Black, 3);
            //   Point point1 = new Point(0, 0);
            //   Point point2 = new Point(1000, 1000);

            //   this.g = this.CreateGraphics();
            //   this.g.DrawLine(blackPen, point1, point2);
            //    myPen.Dispose();
            //    this.g.Dispose();

            for (int a = 0; a < 6; a++)
            {
                if (SerPorts[a].port_active)
                {
                    if (SerPorts[a].timeout > 0)
                    {
                        SerPorts[a].timeout--;
                        if (SerPorts[a].timeout == 0)
                        {
                            dbg_print(DbgType.USB, "! Port " + SerPorts[a].port.PortName + " timeout", Color.DimGray);
                            SerPorts[a].port_active = false;
                            //SerPorts[a].port.Dispose();
                            try
                            {
                                //frezina softas del close;
                                SerPorts[a].port.Close();
                                // this.BeginInvoke(new EventHandler(delegate { SerPorts[a].port.Close(); }));

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("timeout error");
                                Console.WriteLine(ex);
                            }

                        }
                    }
                }
            }
        }


        // tik [6] paskutiniam portui
        private bool try_port(string prt)
        {
            int search_dev_nr = 0;
            string d = "Try port:  " + prt, id = "";
            string get;
            int nr = SerPorts.Count - 1;
            if (!SerPorts[nr].port_active)
            {
                SerPorts[nr].port.PortName = prt;
                SerPorts[nr].port.BaudRate = 115200;
                //SerPorts[nr].port.DataBits = 8;
                //SerPorts[nr].port.Parity = Parity.None;
                //SerPorts[nr].port.StopBits = StopBits.One;

                // bandom atidaryti porta 
                try
                {

                    SerPorts[nr].port.Open();

                    //SerPorts[nr].port.DiscardInBuffer(); // isvalom buferi
                }
                catch (Exception) { dbg_print(DbgType.USB, d + " ...FAIL !", Color.LightCoral); return false; }

                //jeigu atidareme isvalome bufferi
                if (SerPorts[nr].port.IsOpen)
                {
                    SerPorts[nr].port.DiscardInBuffer(); // isvalom buferi
                }
                // atidarem sekmingai, siunciam ID uzklausa
                SerPorts[nr].port_active = true;
                //string get = send_receive("ID ?\r\n");

                int all_dev_count = devices_info.GetLength(0);//kiek turim devaisu liste
                int usb_dev_ptr = all_dev_count - serial_dev_count;//nuo katro ptr prasideda usb devaisai

                while (usb_dev_ptr <= all_dev_count)
                {
                    search_dev_nr++;
                    get = send_receive(devices_info[usb_dev_ptr] + "\r\n");
                    dbg_print(DbgType.USB, get, Color.DimGray);
                    //System.Diagnostics.Debug.Print($"METREL:: = {get}");
                    if (get.Length > 2)
                    {
                        id = get.Substring(2, (get.Length - 2)).ToUpper().Trim();
                        dbg_print(DbgType.USB, d + " ...OK, " + " ID=\"" + id + "\"", Color.DimGray);
                        SerPorts[nr].id = id;
                        found_ser_nr = usb_dev_ptr;
                        return true;
                    }
                    else
                    {

                        if (usb_dev_ptr == (all_dev_count - 1))
                        {
                            dbg_print(DbgType.USB, d + " ...TIMEOUT !", Color.Gold);
                            //SerPorts[nr].port.Close();
                            //SerPorts[nr].port.Dispose();
                            try
                            {
                                SerPorts[nr].port.Close();
                                //SerPorts[nr].port.Close();
                                //this.BeginInvoke(new EventHandler(delegate { SerPorts[nr].port.Close(); }));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);

                            }
                            SerPorts[nr].port_active = false;
                            return false;
                        }
                        else
                        {
                            dbg_print(DbgType.USB, d + " ...FIND NEXT !", Color.DimGray);
                        }
                    }
                    usb_dev_ptr++;
                }
                return false;
            }
            else
            {
                dbg_print(DbgType.USB, d + " ...jau aktyvus! kas per...UAZDAROM!", Color.LightCoral);
                SerPorts[nr].port_active = false;
                return false;
            }

        }

        // tik [6] paskutiniam portui
        private string send_receive(string s)
        {
            string ret = "";

            int bytesCount = 0;
            if (SerPorts[SerPorts.Count - 1].port_active)
            {
                if (!SerPort_transmit((SerPorts.Count - 1), s))
                {

                    return "";
                }

                int timeout = 30;
                string line;
                string newLine;
                // laukiam iki sekundes laiko kol gausim ID atsakyma
                while (timeout > 0)
                {
                    Thread.Sleep(30);
                    timeout--;
                    //dbg_print(timeout.ToString());
                    if (SerPorts[SerPorts.Count - 1].port.IsOpen == false)
                        return "";

                    bytesCount = SerPorts[SerPorts.Count - 1].port.BytesToRead;
                    if (bytesCount > 2)
                    {
                        try
                        {
                            uart_data = new byte[bytesCount];
                            SerPorts[SerPorts.Count - 1].port.Read(uart_data, 0, uart_data.Length);
                            line = System.Text.Encoding.UTF8.GetString(uart_data);

                            dbg_print(DbgType.USB, "getting answer.." + bytesCount.ToString(), Color.DimGray);
                            dbg_print(DbgType.USB, line, Color.DimGray);
                        }
                        catch (Exception ex)
                        {
                            dbg_print(DbgType.USB, "! ERROR reading port " + SerPorts[SerPorts.Count - 1].port.PortName, Color.LightCoral);
                            Console.WriteLine(ex);
                            return "";
                        }
                        if (line.Length > 2)
                        {
                            if ((line.Substring(0, 2).Equals("BB")) || (line.Substring(0, 2).Equals("ID")))
                            {
                                ret = line;
                                timeout = 0;
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                }
                return ret;
            }
            else
            {
                return ret;
            }
        }

        private void read_serial_data(int pnr)
        {
            string temp = "";
            try
            {
                temp = SerPorts[pnr].port.ReadExisting();

                // string s = SerPorts[pnr].port.ReadLine().Trim().ToUpper();


                // if (s.Length > 1)
                // {
                //    SerPorts[pnr].cmd.Add(s);
                //    SerPorts[pnr].timeout = 10;
                //}
            }
            catch (Exception ex)
            {
                try
                {
                    SerPorts[pnr].port.Close();
                    //this.BeginInvoke(new EventHandler(delegate { SerPorts[pnr].port.Close(); }));
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1);

                }
                SerPorts[pnr].port_active = false;
                dbg_print(DbgType.USB, "! EROOR ! port " + SerPorts[pnr].port.PortName + " disconnected", Color.LightCoral);
                Console.WriteLine(ex);
            }

            serial_temp[pnr] += temp.ToUpper();
            // Console.Write(serial_temp[pnr]);

            int timeoutwhile = 3000;
            while (serial_temp[pnr].Contains("\r\n") && timeoutwhile > 0)
            {
                if (serial_temp[pnr].Count() > 3)
                {
                    StringReader strReader = new StringReader(serial_temp[pnr]);
                    string line = strReader.ReadLine();
                    //Console.Write("{0}, c:{1}", line.Replace("\r\n", "AA"), line.Count());
                    if (line != null)
                    {
                        SerPorts[pnr].cmd.Add(line);
                        SerPorts[pnr].timeout = 16;

                    }
                    serial_temp[pnr] = serial_temp[pnr].Replace(line + "\r\n", "");
                    timeoutwhile--;

                }

            }
        }

        // sitas blokas butinas kitaip luzta nes accesinam is atskiro thread kuris generuojamas OnDataReceived
        // kodel? http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        delegate void ShowWeightCallback(int svarst, float svoris);
        /*private void display_weight(int svarst, float svoris)
        {
            if (this.svarstykles[svarst].InvokeRequired)
            {
                ShowWeightCallback d = new ShowWeightCallback(display_weight);
                this.Invoke(d, new object[] { svarst, svoris });
            }
            else
            {
                this.svarstykles[svarst].weight = svoris;
            }
        }*/

        delegate void ShowStatusCallback(int svarst, string busena);

        private void display_status(int svarst, string busena)
        {
            /*
            if (this.gB_svartikles[svarst].InvokeRequired)
            {
                ShowStatusCallback d = new ShowStatusCallback(display_status);
                this.Invoke(d, new object[] { svarst, busena });
            }
            else
            {
               // this.Tara[svarst].weight = svoris;
               if (svarst== PORT_SVARST_A1)
                { this.gB_svartikles[svarst].Text = "A1 [" + busena +"]"; }
                if (svarst == PORT_SVARST_A2)
                { this.gB_svartikles[svarst].Text = "A1 [" + busena + "]"; }
                if (svarst == PORT_SVARST_B1)
                { this.gB_svartikles[svarst].Text = "B1 [" + busena + "]"; }
                if (svarst == PORT_SVARST_B2)
                { this.gB_svartikles[svarst].Text = "B2 [" + busena + "]"; }

            }
            */
            //this.InvokeEx(alcoWin.gB_svartikles[svarst].Text = "A5");

        }

        private void SP0_DataRx(object sender, SerialDataReceivedEventArgs e)
        {
            int xprt = PORT_SVARST_A1;
            read_serial_data(xprt); // skaitom visas komandas i buferi


            // iskarto tikrinam ir traukiam duomenis, viska ignoruojam naudojam tik wg
            while (SerPorts[xprt].cmd.Count > 0)
            {
                //siunciam svarstikliu dekodavimui ir handlinimmui
                // paimam senaiusia ir istrinam
                string c = SerPorts[xprt].cmd[0].ToString();
                SerPorts[xprt].cmd.RemoveAt(0);
                // parsinam ir apdorojam tik jai WG visa kita drop
                if (c.Substring(0, 2).Equals("WG"))
                {
                    string tempstring = c.Substring(2, (c.Length - 2)).Trim();
                    float temp;
                    /*if (float.TryParse(tempstring, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    {
                        display_weight(xprt, temp);
                    }*/
                    // display_weight(xprt, (double) double.Parse(c)); // butinai per isorini callback nes luztam atskiru thread
                }
                //Tara[PORT_SVARST_A1].parse_command(c);
                //Tara[PORT_SVARST_A1].svr_command_handler(xprt);
            }

        }



        /*private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dbg_print("cmd:");
            string cmd;
            SerialPort sp = (SerialPort)sender;
            if (sp.IsOpen)
            {
                try
                {
                    uart_data = new byte[sp.BytesToWrite];

                    sp.Read(uart_data, 0, uart_data.Length);

                    cmd = System.Text.Encoding.UTF8.GetString(uart_data);

                    //cmd = sp.ReadLine();

                    //uart_buff_circl[command_wp++] = cmd.Trim().ToString();
                    uart_buff.Add(cmd.Trim().ToString());

                }
                catch (TimeoutException) { }


            }
        }*/

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            //get_all_monitors();

            try
            {
                int i = 0;
                int py = 10;

                int w = 0;
                int h = 0;
                foreach (MonitorTest ids in ml)//pridedam reikiamus atributus ir kiekvinam monitoriuje atverciam TESTAVIMO langa
                {
                    string labelName = "label" + i;

                    Label label = new Label { Name = labelName, AutoSize = true, Text = ids.MonitorIds, Location = new Point(x: 5, y: py) };
                    System.Windows.Forms.ProgressBar progress = new System.Windows.Forms.ProgressBar();
                    progress.Name = Name = "progres" + i;
                    progress.Style = ProgressBarStyle.Continuous;
                    progress.Location = new Point(x: 150, y: py);
                    panelTestResult.Controls.Add(label);
                    panelTestResult.Controls.Add(progress);
                    py = py + 35;

                    w = Screen.AllScreens[i].WorkingArea.Width;
                    h = Screen.AllScreens[i].WorkingArea.Height;

                    MonitorTest mt = new MonitorTest { Id = i, MonitorIds = ids.MonitorIds, Width = w, Height = h, WorkPlaceNr = 0, testList = new List<TestList>() };
                    //System.Diagnostics.Debug.WriteLine("Method1");
                    //System.Diagnostics.Debug.Print($"ID: = {mt.Id}" + $" RES: = {w}");
                    mtlist.Add(mt);
                    WindowModal tm = new WindowModal(mt);
                    tm.Show(this);
                    progress.Visible = true;
                    i++;
                    tm.FormClosing += Tm_FormClosing;
                }
            }
            catch (Exception err)
            {
                //DialogResult dialog = MessageBox.Show(err.Message, err.InnerException.Message, MessageBoxButtons.OK);
                foreach (MonitorTest mtx in mtlist)
                {
                    //key.SetValue("Workplace" + mtl.Id, mtl.MonitorIds);
                }
            }

            if (mtlist.Count > 0)
            {
                regReadWplace();
            }

        }

        private void Tm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string x = ((Form)sender).Text;
            var mt = mtlist.FirstOrDefault(it => it.MonitorIds.Contains(x.Substring(5)));
            if (mt != null)
            {

                Label lbl_text = this.Controls.Find("label" + mt.Id, true).FirstOrDefault() as Label;
                lbl_text.Text = lbl_text.Text + " " + x.Substring(0, 4);
            }
        }

        private string ParseUid(string str)
        {
            string prs = "unknow";
            try
            {
                int fsl = str.IndexOf("\\");
                prs = str.Substring(fsl + 1);
                int fsl2 = prs.IndexOf("\\");
                prs = prs.Substring(0, fsl2);
            }
            catch (Exception err)
            {
                var msg = err;
            }
            return prs;
        }

        #region -=Param save/load from reg=-
        private void regUpdateWplace()
        {
            int id = 0;
            Workplaces_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Workplaces", true);

            for (int a = 0; a < WorkplacesCount; a++)
            {
                id = a + 1;
                try
                {
                    Workplaces_reg.SetValue("Workplace" + id, cBoxWplace[a].SelectedItem.ToString());
                    Workplaces_reg.SetValue("WP_" + id + "_EN", SavedWorkplaces[id].Enable);
                    System.Diagnostics.Debug.Print($"WPLACE {id}" + $": = {SavedWorkplaces[a].WorplaceMonitorID}");
                }
                catch (Exception e)
                {
                    dbg_print(DbgType.MAIN, "Update_work_place_exception, a: " + a, Color.LightCoral);
                }


            }

            Workplaces_reg.Close();
        }

        private void regReadWplace()
        {
            int wp_count = 0;
            int id = 0;

            string monitor_id;

            Workplaces_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Workplaces");

            if (Workplaces_reg != null)
            {
                WorkplacesCount = UInt16.Parse(Workplaces_reg.GetValue("NumWorkPlaces").ToString());
                //int height = int.Parse(key.GetValue("Height").ToString());
                System.Diagnostics.Debug.Print($"REGISTRY: = {WorkplacesCount}");

                for (int a = 0; a < 3; a++)
                {
                    try
                    {
                        id = a + 1;
                        monitor_id = Workplaces_reg.GetValue("Workplace" + id).ToString();
                        if (monitor_id.Equals("NONE"))
                        {
                            SavedWorkplaces[a].WorplaceMonitorID = 0;
                        }
                        else
                        {
                            SavedWorkplaces[a].WorplaceMonitorID = UInt64.Parse(monitor_id);
                        }

                        SavedWorkplaces[a].Enable = Boolean.Parse(Workplaces_reg.GetValue("WP_" + id + "_EN").ToString());
                        System.Diagnostics.Debug.Print($"WPLACE: = {SavedWorkplaces[a].WorplaceMonitorID}" + $" en: = {SavedWorkplaces[a].Enable}");
                        cBoxWplace[a].Text = SavedWorkplaces[a].WorplaceMonitorID.ToString();
                    }
                    catch (Exception e)
                    {
                        dbg_print(DbgType.MAIN, "Read_work_place_exception", Color.LightCoral);
                    }


                }

                Workplaces_reg.Close();
            }
            else
            {
                Workplaces_reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Artilux\Workplaces");

                wp_count = ml.Count;
                dbg_print(DbgType.MAIN, "wp_count: " + wp_count, Color.Gray);
                //System.Diagnostics.Debug.Print($"wp_count: = {wp_count}");
                Workplaces_reg.SetValue("NumWorkPlaces", wp_count);
                for (int a = 0; a < 3; a++)
                //foreach (MonitorTest mtx in ml)
                {
                    try
                    {
                        id = ml[a].Id + 1;
                        System.Diagnostics.Debug.Print($"wp_id: = {id}");
                        Workplaces_reg.SetValue("Workplace" + id, ml[a].MonitorIds);
                        Workplaces_reg.SetValue("WP_" + id + "_EN", SavedWorkplaces[a].Enable);

                        cBoxWplace[a].Text = ml[a].MonitorIds.ToString();
                    }
                    catch (Exception e)
                    {
                        dbg_print(DbgType.MAIN, "WPlace create fail", Color.LightCoral);
                        Workplaces_reg.Close();
                        return;
                    }

                }
                //System.Diagnostics.Debug.Print($"REG_not_found: = {"create"}");
                dbg_print(DbgType.MAIN, "REG_not_found: Create", Color.Violet);
                Workplaces_reg.Close();

                //cBoxWplace[0].Text = SavedWorkplaces[0].WorplaceMonitorID.ToString();
                //cBoxWplace[1].Text = SavedWorkplaces[1].WorplaceMonitorID.ToString();
                //cBoxWplace[2].Text = SavedWorkplaces[2].WorplaceMonitorID.ToString();
            }
        }

        private void get_reg_Click(object sender, EventArgs e)
        {
            regReadWplace();
        }

        private void cBoxWplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print($"sel1: = {cBoxWplace[0].SelectedItem}");
        }


        private void saveWplace_Click(object sender, EventArgs e)
        {
            int id = 0;
            int a = 0;
            int b = 0;
            int kiek_liko = WorkplacesCount;

            bool found_same_entry = false;

            UInt64[] monitor;
            monitor = new UInt64[4];
            System.Diagnostics.Debug.Print($"sel1: = {cBoxWplace[0].SelectedItem}");
            System.Diagnostics.Debug.Print($"sel2: = {cBoxWplace[1].SelectedItem}");
            System.Diagnostics.Debug.Print($"sel3: = {cBoxWplace[2].SelectedItem}");

            for (a = 0; a < WorkplacesCount - 1; a++)
            {

                monitor[a] = UInt64.Parse(cBoxWplace[a].SelectedItem.ToString());

                if (WorkplacesCount > 2)
                {
                    for (b = a + 1; b < WorkplacesCount; b++)
                    {
                        monitor[b] = UInt64.Parse(cBoxWplace[b].SelectedItem.ToString());

                        if (monitor[a] == monitor[b])
                        {
                            found_same_entry = true;
                            System.Diagnostics.Debug.Print($"Same entry!");
                        }
                    }
                }
                else
                {
                    monitor[a + 1] = UInt64.Parse(cBoxWplace[a + 1].SelectedItem.ToString());
                    if (monitor[a] == monitor[a + 1])
                    {
                        found_same_entry = true;
                        System.Diagnostics.Debug.Print($"Same entry!");
                    }
                }

                if (found_same_entry)
                {
                    string box_msg = "Pasirinkti monitoriai sutampa !";
                    string box_title = "Klaida";
                    MessageBox.Show(box_msg, box_title);
                }
                else
                {
                    regUpdateWplace();
                    regReadWplace();
                }

            }
        }
        #endregion


        #region -=Periphery save/load=-
        private void regReadPeriphery()
        {
            int phe_count = 0;
            int id = 0;
            int a = 0;

            Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Periphery");

            if (Periphery_reg != null)
            {
                phe_count = UInt16.Parse(Periphery_reg.GetValue("NumPeriphery").ToString());
                //int height = int.Parse(key.GetValue("Height").ToString());
                System.Diagnostics.Debug.Print($"REGISTRY: = {phe_count}");

                for (a = 0; a < phe_count; a++)
                {
                    try
                    {
                        Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Periphery\Phe_" + a);

                        //network_dev[a].Name = UInt64.Parse(Periphery_reg.GetValue("Name").ToString());
                        network_dev[a].Name = (Periphery_reg.GetValue("Name").ToString());
                        network_dev[a].Ip = (Periphery_reg.GetValue("Ip").ToString());
                        network_dev[a].Port_0 = (int)(Periphery_reg.GetValue("Port0"));
                        network_dev[a].Port_1 = (int)(Periphery_reg.GetValue("Port1"));
                        network_dev[a].Enable = Convert.ToBoolean((Periphery_reg.GetValue("Enable")));
                        Periphery_reg.Close();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.Print($"read_phe fail: = {a}");
                    }

                }

                /*System.Diagnostics.Debug.Print($"Name: = {network_dev[0].Name}");
                System.Diagnostics.Debug.Print($"Ip: = {network_dev[0].Ip}");
                System.Diagnostics.Debug.Print($"Port: = {network_dev[0].Port_0}");
                System.Diagnostics.Debug.Print($"Enable: = {network_dev[0].Enable}");*/
            }
            else
            {
                Periphery_reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Artilux\Periphery");

                phe_count = network_dev.Count;
                System.Diagnostics.Debug.Print($"wp_count: = {phe_count}");
                Periphery_reg.SetValue("NumPeriphery", phe_count);

                foreach (SocketDevList dev in network_dev)
                {
                    Periphery_reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Artilux\Periphery\Phe_" + a);

                    Periphery_reg.SetValue("Name", dev.Name);
                    Periphery_reg.SetValue("Ip", dev.Ip);
                    Periphery_reg.SetValue("Port0", dev.Port_0);
                    Periphery_reg.SetValue("Port1", dev.Port_1);
                    Periphery_reg.SetValue("Enable", Convert.ToInt32(dev.Enable));
                    Periphery_reg.Close();
                    a++;
                }

                System.Diagnostics.Debug.Print($"REG_not_found: = {"create"}");
                Periphery_reg.Close();

            }
        }

        private void regUpdatePeriphery(int phe_nr)
        {
            int phe_count = 0;
            int id = 0;

            Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Periphery\Phe_" + phe_nr, true);


            Periphery_reg.SetValue("Name", network_dev[phe_nr].Name);
            Periphery_reg.SetValue("Ip", network_dev[phe_nr].Ip);
            Periphery_reg.SetValue("Port0", network_dev[phe_nr].Port_0);
            Periphery_reg.SetValue("Port1", network_dev[phe_nr].Port_1);
            Periphery_reg.SetValue("Enable", Convert.ToInt32(network_dev[phe_nr].Enable));
            Periphery_reg.Close();

            System.Diagnostics.Debug.Print($"UPDATE: = {phe_nr} ip: = {network_dev[phe_nr].Ip} port_0: = {network_dev[phe_nr].Port_0} port_1: = {network_dev[phe_nr].Port_1} en: = {network_dev[phe_nr].Enable}");
        }

        #endregion

        public void get_all_monitors()
        {

            //set the class name and namespace
            string NamespacePath = "\\\\.\\ROOT\\WMI";
            string ClassName = "WmiMonitorID";
            //Create ManagementClass
            ManagementClass oClass = new ManagementClass(NamespacePath + ":" + ClassName);
            int a = 0;
            //Get all instances of the class and enumerate them
            foreach (ManagementObject oObject in oClass.GetInstances())
            {
                //access a property of the Management object
                //String MonitorId = oObject["SerialNumberID"].ToString();
                UInt16[] idd = new UInt16[24];
                dynamic serial_nr = oObject["SerialNumberID"];
                string instance = (String)oObject["InstanceName"];
                string id = string.Join("", serial_nr);

                System.Diagnostics.Debug.Print($"MONITORe: = {id}");
                dbg_print(DbgType.MAIN, "MONITOR--" + id, Color.MediumPurple);
                id = id.Substring(0, 16);
                //Console.WriteLine("Active : {0}", oObject["SerialNumberID"].ToString());
                MonitorTest mt = new MonitorTest { Id = a, MonitorIds = id };
                ml.Add(mt);
                cBoxWplace[0].Items.Add(id);
                cBoxWplace[1].Items.Add(id);
                cBoxWplace[2].Items.Add(id);

                cBoxWplace[a].Enabled = true;
                a++;
            }
            if (a < 3)
            {
                cBoxWplace[2].Enabled = true;
                MonitorTest mt = new MonitorTest { Id = 2, MonitorIds = "0" };
                cBoxWplace[2].Items.Add("0");
                ml.Add(mt);
            }

            panelTestResult.Controls.Clear();
        }


        #region -=Metrel test btn=-

        private void mtrelTest_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.PolarityNoInteraction();
        }

        private void metrel_auto_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.StartAutoSequence("COM6");
        }

        private void metrel_start_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.ExecuteAction("Start_test");
        }

        private void metrel_stop_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.ExecuteAction("Stop_test");
        }

        private void Met_proceed_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.ExecuteAction("Proceed");
        }

        private void metrel_break_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.ExecuteAction("Break");
        }

        private void metrel_skip_btn_Click(object sender, EventArgs e)
        {
            //Met_bbox_test.ExecuteAction("Skip");
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

            Socket_.start_socket(network_dev[DevType.GWINSTEK_HV_TESTER], 0);
            //Socket_.socket_ping(network_dev[DevType.GWINSTEK_HV_TESTER]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], "SYSTEM:TIME?");

            //network_dev[DevType.GWINSTEK_HV_TESTER].NewSendData = true;
            network_dev[DevType.GWINSTEK_HV_TESTER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Socket_.close_socket(network_dev[DevType.GWINSTEK_HV_TESTER]);
        }



        public void show_msg(string msg, Color _color)
        {
            Popup_msg popup = new Popup_msg("", msg, _color, 2);
            popup.Show();
        }

        private void btn_popup_Click(object sender, EventArgs e)
        {

            dbg_print(DbgType.MAIN, "TEST", Color.MediumPurple);

            Popup_msg popup = new Popup_msg("TEST", "asdsd", Color.SpringGreen, 2);
            popup.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Socket_.send_socket(network_dev[DevType.MAIN_CONTROLLER], "A");
        }

        #region GWinstek HV tester
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.Print($"e.RowIndex: = {e.RowIndex} e.ColumnIndex: = {e.ColumnIndex}");

            network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamCount = 5;
            network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamLeft = 5;
            network_dev[DevType.GWINSTEK_HV_TESTER].TestType = e.RowIndex;

            switch (e.ColumnIndex)
            {
                case 1://TEST
                    //network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamCount = 0;
                    network_dev[DevType.GWINSTEK_HV_TESTER].State = NetDev_State.START_TEST;
                    network_dev[DevType.GWINSTEK_HV_TESTER].SubState = NetDev_Test.TEST_SELECT;
                    break;

                case 2://GET
                    //ITECH_HV_handle_get_params(e.ColumnIndex, e.RowIndex);
                    //network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamCount = 5;
                    DataGridViewRow Row = (DataGridViewRow)dataGrid_HV_test.Rows[e.RowIndex];//
                    Row.Cells[4].Value = "--";
                    Row.Cells[5].Value = "--";
                    Row.Cells[6].Value = "--";
                    Row.Cells[7].Value = "--";
                    Row.Cells[8].Value = "--";
                    network_dev[DevType.GWINSTEK_HV_TESTER].State = NetDev_State.SELECT_TEST;
                    break;
                case 3://SET
                    //network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamCount = 5;
                    network_dev[DevType.GWINSTEK_HV_TESTER].State = NetDev_State.SET_PARAM;
                    break;

            }
            NetworkThreads.GW_HV_handle_get_params();
        }

        public void gwinstek_handle_test_result()
        {
            bool test_pass = false;
            DataGridViewCellStyle style = new DataGridViewCellStyle();

            network_dev[DevType.GWINSTEK_HV_TESTER].State = NetDev_State.READY;
            string[] split_result;
            split_result = network_dev[DevType.GWINSTEK_HV_TESTER].Resp.Split(',');//subs[0]=ip,sub

            //DataGridViewRow Row0 = (DataGridViewRow)dataGridView2.Rows[0].Clone();
            dataGrid_HV_result.Invoke((MethodInvoker)(() => dataGrid_HV_result.Rows.Insert(0)));

            var dateTime3 = DateTimeOffset.FromUnixTimeSeconds(Socket_.UnixTimeNow()).LocalDateTime;
            dataGrid_HV_result.Invoke((MethodInvoker)(() => dataGrid_HV_result.Rows[0].Cells[0].Value = dateTime3));

            if (String.Equals("PASS ", split_result[1]))//lyginam stringus, ar uzsetinom parametra
            {
                test_pass = true;
                style.BackColor = Color.SpringGreen;
                dataGrid_HV_result.Rows[0].Cells[2].Style = style;
                System.Diagnostics.Debug.Print($"TEST_PASS");
            }
            else
                style.BackColor = Color.LightCoral;
            {
                dataGrid_HV_result.Rows[0].Cells[2].Style = style;
            }

            for (int i = 0; i < split_result.Length; i++)
            {
                //Row0.Cells[i].Value = split_result[i];
                dataGrid_HV_result.Invoke((MethodInvoker)(() => dataGrid_HV_result.Rows[0].Cells[i + 1].Value = split_result[i]));
                dataGrid_HV_result.Invoke((MethodInvoker)(() => dataGrid_HV_result.AutoResizeColumns()));
            }
        }
        private void dataGrid_HV_test_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.Print($"CHANGED Row: = {e.RowIndex} Column: = {e.ColumnIndex}");
        }
        #endregion

        #region ITECH load set param
        private void dataGrid_Load_load_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            System.Diagnostics.Debug.Print($"e.RowIndex: = {e.RowIndex} e.ColumnIndex: = {e.ColumnIndex}");

            network_dev[DevType.ITECH_LOAD].GetSetParamCount = NetworkThreads.Itech_load_param_get_type.Length;
            network_dev[DevType.ITECH_LOAD].GetSetParamLeft = NetworkThreads.Itech_load_param_get_type.Length;
            network_dev[DevType.ITECH_LOAD].TestType = e.RowIndex;
            DataGridViewRow Row_load = (DataGridViewRow)dataGrid_Load_load.Rows[0];//get table row by test type

            //const int state_cell = Row_load.Cells.Count - 1;// paskutinio stulpelio state pozicija

            switch (e.ColumnIndex)
            {
                case 7://SET STATE (ON/OFF load)
                    if (NetworkThreads.load_state == false)
                    {
                        network_dev[DevType.ITECH_LOAD].State = NetDev_State.START_TEST;
                        network_dev[DevType.ITECH_LOAD].SubState = NetDev_Test.TEST_START;
                        break;
                    }
                    else
                    {
                        network_dev[DevType.ITECH_LOAD].State = NetDev_State.READY;
                        break;
                    }

                    break;
            }

            NetworkThreads.ITECH_LOAD_handle_get_params();
        }



        private void dataGrid_Load_load_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            if (load_param_enable_edit)
            {
                load_param_enable_edit = false;
                int load_current_cell = dataGrid_Load_load.Rows[0].Cells.Count - 2;// load current set cell
                int load_current_value = Convert.ToInt32(dataGrid_Load_load.Rows[0].Cells[load_current_cell].Value);

                if (e.RowIndex == 0 && e.ColumnIndex == (load_current_cell))
                {

                    System.Diagnostics.Debug.Print($"CHANGED CURRENT: = {load_current_value}");
                    if (load_current_value > 0 && load_current_value < 40)
                    {
                        network_dev[DevType.ITECH_LOAD].GetSetParamCount = 3;
                        network_dev[DevType.ITECH_LOAD].GetSetParamLeft = 3;
                        network_dev[DevType.ITECH_LOAD].Cmd = "CURR " + load_current_value;
                        devList.DevLoad.load_current = load_current_value;
                        network_dev[DevType.ITECH_LOAD].State = NetDev_State.SET_PARAM;
                        NetworkThreads.ITECH_LOAD_handle_get_params();
                    }
                    else
                    {
                        show_msg("Blogi parametrai, galimos vertes 1-40A", Color.LightCoral);
                    }
                }
            }
        }

        private void dataGrid_Load_load_Enter(object sender, EventArgs e)
        {
            load_param_enable_edit = true;
        }
        #endregion


        #region Device control tab
        void load_dev_control()//irenginiu valdymo skirtukai ir lenteliu pradines reiksmes
        {
            DataGridViewRow Row0 = (DataGridViewRow)data_grid_main_board.Rows[0].Clone();
            //dataGrid_HV_result.Rows.Insert(0);

            //MAIN BOARD
            Row0.Cells[0].Value = "Relay 11";
            data_grid_main_board.Rows.Add(Row0);
            Row0 = (DataGridViewRow)data_grid_main_board.Rows[0].Clone();
            Row0.Cells[0].Value = "Relay 12";
            data_grid_main_board.Rows.Add(Row0);
            Row0 = (DataGridViewRow)data_grid_main_board.Rows[0].Clone();
            Row0.Cells[0].Value = "Relay 13";
            data_grid_main_board.Rows.Add(Row0);
            Row0 = (DataGridViewRow)data_grid_main_board.Rows[0].Clone();
            Row0.Cells[0].Value = "Relay 14";
            data_grid_main_board.Rows.Add(Row0);

            // GWINSTEK 
            Row0 = (DataGridViewRow)dataGrid_HV_test.Rows[0].Clone();
            Row0.Cells[1].Value = "ACW";
            dataGrid_HV_test.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_HV_test.Rows[0].Clone();
            Row0.Cells[1].Value = "IR";
            dataGrid_HV_test.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_HV_test.Rows[0].Clone();
            Row0.Cells[1].Value = "CONT";
            dataGrid_HV_test.Rows.Add(Row0);
            // ITECH LOAD 
            Row0 = (DataGridViewRow)dataGrid_Load_load.Rows[0].Clone();
            Row0.Cells[0].Value = "A";
            dataGrid_Load_load.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_load.Rows[0].Clone();
            Row0.Cells[0].Value = "B";
            dataGrid_Load_load.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_load.Rows[0].Clone();
            Row0.Cells[0].Value = "C";
            dataGrid_Load_load.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_load.Rows[0].Clone();
            // ITECH LINE
            Row0 = (DataGridViewRow)dataGrid_Load_Line.Rows[0].Clone();
            Row0.Cells[0].Value = "A";
            dataGrid_Load_Line.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_Line.Rows[0].Clone();
            Row0.Cells[0].Value = "B";
            dataGrid_Load_Line.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_Line.Rows[0].Clone();
            Row0.Cells[0].Value = "C";
            dataGrid_Load_Line.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Load_Line.Rows[0].Clone();

            //Spectrocope main values
            Row0 = (DataGridViewRow)dataGrid_Spectrum.Rows[0].Clone();
            Row0.Cells[0].Value = NetworkThreads.Siglent_param_type[0];
            dataGrid_Spectrum.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Spectrum.Rows[0].Clone();
            Row0.Cells[0].Value = NetworkThreads.Siglent_param_type[1];
            dataGrid_Spectrum.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Spectrum.Rows[0].Clone();
            Row0.Cells[0].Value = NetworkThreads.Siglent_param_type[2];
            dataGrid_Spectrum.Rows.Add(Row0);
            network_dev[DevType.ANALYSER_SIGLENT].GetSetParamLeft = 3;
            network_dev[DevType.ANALYSER_SIGLENT].GetSetParamCount = 3;

            network_dev[DevType.ANALYSER_SIGLENT].device_param = new String[25];

            //BARCODE_1 EVSE
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "BARCODE";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "WIFI SIGNAL";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "LTE SIGNAL";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY ON";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY OFF";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "METER DATA";
            dataGrid_Barcode1.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode1.Rows[0].Clone();
            Row0.Cells[0].Value = "RFID";
            dataGrid_Barcode1.Rows.Add(Row0);
            //BARCODE_2 EVSE
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "GET DATE";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "SET DATE";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "BARCODE";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "WIFI SIGNAL";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "LTE SIGNAL";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY ON";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY OFF";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "METER DATA";
            dataGrid_Barcode2.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode2.Rows[0].Clone();
            Row0.Cells[0].Value = "RFID";
            dataGrid_Barcode2.Rows.Add(Row0);
            //BARCODE_3 EVSE
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "BARCODE";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "WIFI SIGNAL";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "LTE SIGNAL";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY ON";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "RELAY OFF";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "METER DATA";
            dataGrid_Barcode3.Rows.Add(Row0);
            Row0 = (DataGridViewRow)dataGrid_Barcode3.Rows[0].Clone();
            Row0.Cells[0].Value = "RFID";
            dataGrid_Barcode3.Rows.Add(Row0);

        }
        #endregion

        #region Siglent analyser chart
        private void button7_Click(object sender, EventArgs e)
        {
            var net_dev = network_dev[DevType.ANALYSER_SIGLENT];

            chart1.Series.Clear();
            chart1.Series.Add("spectrum");
            chart1.Series["spectrum"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["spectrum"].BorderWidth = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

            double start = Convert.ToDouble(net_dev.device_param[Siglent_param.START_X]);
            double stop = Convert.ToDouble(net_dev.device_param[Siglent_param.STOP_X]);
            double range;

            if (start > 0)
            {
                range = stop / start;
            }
            else
            {
                range = stop;
            }

            net_dev.device_param[Siglent_param.X_RANGE] = Convert.ToString(range);

            chart1.ChartAreas[0].AxisX.Minimum = start;
            chart1.ChartAreas[0].AxisX.Maximum = stop;
            chart1.ChartAreas[0].AxisX.Title = net_dev.device_param[Siglent_param.X_UNIT];
            chart1.ChartAreas[0].AxisY.Minimum = -10;
            chart1.ChartAreas[0].AxisY.Maximum = 0;
            chart1.ChartAreas[0].AxisY.Interval = 1;
            //chart1.ChartAreas[0].AxisY.IsReversed = true;
            this.chart1.Series["spectrum"].Points.AddXY(start, 5);

            net_dev.State = NetDev_State.START_TEST;//prasom grafiko duomenu
            net_dev.SubState = NetDev_Test.GET_CHART_DATA;
            NetworkThreads.Sprectroscope_handle_get_params();
        }

        public string get_siglent_unit(string val)
        {

            string[] split;
            int unit = 0;
            string unit_str = "";

            split = val.Split('E');
            System.Diagnostics.Debug.Print($"Split: = {split[0][1]}");
            int.TryParse(split[1], out unit);
            System.Diagnostics.Debug.Print($"unit: = {unit}");
            switch (unit)
            {
                case 9:
                    unit_str = "Ghz";
                    break;
                case 8:
                case 7:
                case 6:
                    unit_str = "Mhz";
                    break;
                case 5:
                case 4:
                case 3:
                    unit_str = "Khz";
                    break;
                case 2:
                case 1:
                case 0:
                    unit_str = "Hz";
                    break;
            }

            return unit_str;
        }

        public void update_chart()
        {
            string[] split;
            string str;
            double y_min = 0;
            double y_max = 1;
            double point;
            var netdev = network_dev[DevType.ANALYSER_SIGLENT];

            netdev.State = NetDev_State.READY;
            netdev.SubState = NetDev_Test.TEST_NONE;

            double point_x = chart1.Series["spectrum"].Points[0].XValue;
            var net_dev = network_dev[DevType.ANALYSER_SIGLENT];

            double start = Convert.ToDouble(net_dev.device_param[Siglent_param.START_X]);

            split = netdev.Resp.Split(',');//subs[0]=ip,subs[1]=port_0,subs[2]=port_1,
            int points_cnt = split.Length;

            Double range = Convert.ToDouble(netdev.device_param[Siglent_param.X_RANGE]);
            Double interval = range / points_cnt;

            int i = 0;
            for (i = 0; i < split.Length; i++)
            {
                str = split[i];
                //System.Diagnostics.Debug.Print($"str: = {str} split: = {split[i]}");
                split[i] = str.Remove(7, 4);//triminam gala 3.20000000000E+09

                if (i == 0)
                {
                    //chart1.Invoke((MethodInvoker)(() => chart1.Series["spectrum"].Points[0].XValue = start));
                    //chart1.Invoke((MethodInvoker)(() => chart1.Series["spectrum"].Points[0].SetValueY(split[i])));

                    Invoke
                    (
                        new Action
                        (
                            () =>
                            {
                                chart1.Series["spectrum"].Points[0].XValue = start;
                                chart1.Series["spectrum"].Points[0].SetValueY(split[i]);
                            }
                        )
                    );

                    y_min = Convert.ToDouble(split[i].Replace('.', ','));
                    y_max = Convert.ToDouble(split[i].Replace('.', ','));
                }
                else
                {
                    point_x = chart1.Series["spectrum"].Points[i - 1].XValue;
                    chart1.Invoke((MethodInvoker)(() => chart1.Series["spectrum"].Points.AddXY(point_x + interval, split[i])));
                    point = Convert.ToDouble(split[i].Replace('.', ','));
                    if (point < y_min)
                    {
                        y_min = point;
                    }
                    if (point > y_max)
                    {
                        y_max = point;
                    }
                }
            }


            //System.Diagnostics.Debug.Print($"points_cnt: = {points_cnt} interval: = {interval} min: = {y_min} max: = {y_max} 0: = {split[0]} 1: = {split[1]}");

            chart1.Invoke((MethodInvoker)(() => chart1.ChartAreas[0].AxisY.Minimum = y_min));
            chart1.Invoke((MethodInvoker)(() => chart1.ChartAreas[0].AxisY.Maximum = 0));
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            network_dev[DevType.GWINSTEK_HV_TESTER].Cmd = "MEAS?";
            Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], network_dev[DevType.GWINSTEK_HV_TESTER].Cmd);
            network_dev[DevType.GWINSTEK_HV_TESTER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
        }



        private void btn_stop_Click(object sender, EventArgs e)
        {
            network_dev[DevType.GWINSTEK_HV_TESTER].Cmd = "FUNC:TEST OFF";
            Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], network_dev[DevType.GWINSTEK_HV_TESTER].Cmd);
            network_dev[DevType.GWINSTEK_HV_TESTER].SendReceiveState = NetDev_SendState.SEND_BEGIN;

            show_msg("TEST STOP", Color.LightSalmon);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*network_dev[DevType.ITECH_LOAD].Cmd = "MEAS?";//test select komandos formavimas
            Socket_.send_socket(network_dev[DevType.ITECH_LOAD], network_dev[DevType.ITECH_LOAD].Cmd);
            network_dev[DevType.ITECH_LOAD].SendReceiveState = NetDev_SendState.SEND_BEGIN;*/

            network_dev[DevType.ITECH_LOAD].State = NetDev_State.GET_PARAM_ALL;
            network_dev[DevType.ITECH_LOAD].GetSetParamLeft = NetworkThreads.Itech_load_param_get_type.Length;
            NetworkThreads.ITECH_LOAD_handle_get_params();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            network_dev[DevType.ANALYSER_SIGLENT].Cmd = "FREQ:START?";

            Socket_.send_socket(network_dev[DevType.ANALYSER_SIGLENT], network_dev[DevType.ANALYSER_SIGLENT].Cmd);
            network_dev[DevType.ANALYSER_SIGLENT].SendReceiveState = NetDev_SendState.SEND_BEGIN;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            network_dev[DevType.ANALYSER_SIGLENT].Cmd = "FREQ:STOP?";

            Socket_.send_socket(network_dev[DevType.ANALYSER_SIGLENT], network_dev[DevType.ANALYSER_SIGLENT].Cmd);
            network_dev[DevType.ANALYSER_SIGLENT].SendReceiveState = NetDev_SendState.SEND_BEGIN;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //System.Diagnostics.Debug.Print($"index: = {tabControl2.SelectedIndex}");
            switch (tabControl2.SelectedIndex)
            {
                case NetDev_Tab.MAIN_CONTROLLER:
                    break;
                case NetDev_Tab.HW_TESTER:
                    break;
                case NetDev_Tab.SIGLENT:
                    network_dev[DevType.ANALYSER_SIGLENT].State = NetDev_State.GET_PARAM_ALL;
                    network_dev[DevType.ANALYSER_SIGLENT].GetSetParamLeft = 3;
                    network_dev[DevType.ANALYSER_SIGLENT].GetSetParamCount = 3;
                    NetworkThreads.Sprectroscope_handle_get_params();
                    break;
                case NetDev_Tab.ITECH_LOAD:
                    break;
            }
        }

        private void dataGrid_Barcode2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.Print($"e.RowIndex: = {e.RowIndex} e.ColumnIndex: = {e.ColumnIndex}");

            network_dev[DevType.BARCODE_2].SubState = e.RowIndex + 1;//setinam state pagal paspausyta table btn

            NetworkThreads.Barcode2_handle_get_params();
            evse2_params.Text = "---";
        }

        private void dataGrid_Barcode1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGrid_Barcode3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket_.send_socket(network_dev[DevType.MAIN_CONTROLLER], "123:RL:MAIN:0");
            network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
        }

        private void dataGrid_main_board_click(object sender, DataGridViewCellEventArgs e)
        {

            var net_dev = Main.main.network_dev[DevType.MAIN_CONTROLLER];
            int set_state = 0;

            System.Diagnostics.Debug.Print($"e.RowIndex: = {e.RowIndex} e.ColumnIndex: = {e.ColumnIndex}");

            switch (e.ColumnIndex)
            {
                case 2:
                    set_state = 1;
                    break;

                case 3:
                    set_state = 0;
                    break;
            }

            switch (e.RowIndex)
            {
                case 0:
                    net_dev.Cmd = net_dev.CommandId + NetworkThreads.Main_board_rl_command[RL_Name.RL_MAIN] + set_state;
                    network_dev[DevType.MAIN_CONTROLLER].State = MainBoard_State.RL_SET;
                    network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;

                case 1:
                    net_dev.Cmd = net_dev.CommandId + NetworkThreads.Main_board_rl_command[RL_Name.RL_11] + set_state;
                    network_dev[DevType.MAIN_CONTROLLER].State = MainBoard_State.RL_SET;
                    network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;

                case 2:
                    net_dev.Cmd = net_dev.CommandId + NetworkThreads.Main_board_rl_command[RL_Name.RL_12] + set_state;
                    network_dev[DevType.MAIN_CONTROLLER].State = MainBoard_State.RL_SET;
                    network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;

                case 3:
                    net_dev.Cmd = net_dev.CommandId + NetworkThreads.Main_board_rl_command[RL_Name.RL_13] + set_state;
                    network_dev[DevType.MAIN_CONTROLLER].State = MainBoard_State.RL_SET;
                    network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;

                case 4:
                    net_dev.Cmd = net_dev.CommandId + NetworkThreads.Main_board_rl_command[RL_Name.RL_14] + set_state;
                    network_dev[DevType.MAIN_CONTROLLER].State = MainBoard_State.RL_SET;
                    network_dev[DevType.MAIN_CONTROLLER].SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;

            }

            NetworkThreads.MAIN_Ctrl_handle();

        }
    }
}
