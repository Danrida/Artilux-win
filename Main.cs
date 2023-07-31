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

namespace ArtiluxEOL
{
    public partial class Main : Form
    {
        static RegistryKey Config_reg;
        static RegistryKey Workplaces_reg;
        static RegistryKey Periphery_reg;

        public bool init_done = false;
        //public static Main main_win;

        const int MAIN_CONTROLLER = 0;
        const int ITECH_HV_TESTER = 1;
        const int ITECH_LOAD = 2;
        const int ANALYSER_SIGLENT = 3;




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

        UInt64[] SavedWorkplaces;

        ComboBox[] cBoxWplace = new ComboBox[4];
        TextBox[] TextBox_dev_info = new TextBox[8];
        CheckBox[] CheckBox_dev_info = new CheckBox[8];

        public List<MonitorTest> mtlist = new List<MonitorTest>();
        List<string> ml = new List<string>();//monitor list

        public List<SocketDevList> network_dev = new List<SocketDevList>();
        public Main()
        {
            Config_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Configs");
            Workplaces_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Workplaces");
            Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Devices");

            SavedWorkplaces = new UInt64[5];
            //workplace
            cBoxWplace[0] = new ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 40), Size = new Size(200,37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200};
            cBoxWplace[1] = new ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 100), Size = new Size(200, 37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200 };
            cBoxWplace[2] = new ComboBox { Enabled = false, Font = new Font("Microsoft Sans Serif", 14), Location = new Point(x: 69, y: 165), Size = new Size(200, 37), DropDownStyle = ComboBoxStyle.DropDownList, DropDownHeight = 106, DropDownWidth = 200 };

            

            InitializeComponent();

            this.groupBox1.Controls.Add(cBoxWplace[0]);
            this.groupBox1.Controls.Add(cBoxWplace[1]);
            this.groupBox1.Controls.Add(cBoxWplace[2]);

            if (Workplaces_reg != null)
            {
                regReadWplace();
            }

            ip_texbox_show();

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



            //NetworkDevConn.WorkerSupportsCancellation = true;
            NetworkDevConn.DoWork += new System.ComponentModel.DoWorkEventHandler(NetworkDevConn_DoWork);
            NetworkDevConn.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(NetworkDevConn_RunWorkerCompleted);
            NetworkDevConn.RunWorkerAsync();
            NetworkDevConn.WorkerSupportsCancellation = true;
            MainControllerTCP.DoWork += new System.ComponentModel.DoWorkEventHandler(MainControllerTCP_DoWork);
            //MainControllerTCP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(MainControllerTCP_RunWorkerCompleted);
            //MainControllerTCP.RunWorkerAsync();
            //NetworkDevConn.WorkerSupportsCancellation = true;
            MainControllerMODBUS.DoWork += new System.ComponentModel.DoWorkEventHandler(MainControllerMODBUS_DoWork);
            //MainControllerMODBUS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(MainControllerMODBUS_RunWorkerCompleted);
            //MainControllerMODBUS.RunWorkerAsync();

            HVgen.DoWork += new System.ComponentModel.DoWorkEventHandler(HVgen_DoWork);
            Specroscope.DoWork += new System.ComponentModel.DoWorkEventHandler(Specroscope_DoWork);
            Load.DoWork += new System.ComponentModel.DoWorkEventHandler(Load_DoWork);



        }

        

        private void ShowCheckedCheckboxes(object sender, EventArgs e)
        {
            if (init_done)
            {
                bool state;
                for (int a = 0; a < CheckBox_dev_info.Length; a++)
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

        public void ip_texbox_show() {//dev ip ivedino laukeliai ir devaisu enable checbox



            int cbox_y_location = 30;
            Font textbox_font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            SocketDevList dev;
            dev = new SocketDevList { Name = "Main_controller", TestMsg = "TEST?", client = null, Ip = "192.168.11.85", Port_0 = 5566, Port_1 = 5567, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "HV_Test", TestMsg = "SYSTEM:TIME?", client = null, Ip = "192.168.11.150", Port_0 = 12312, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Siglent", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 50252, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Load", TestMsg = "TEST?", client = null, Ip = "192.168.11.150", Port_0 = 11311, Port_1 = 0, State = 0, Enable = true, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Rfid", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Power", TestMsg = "", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Evse", TestMsg = "BB;", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);
            dev = new SocketDevList { Name = "Oscil", TestMsg = "---", client = null, Ip = "", Port_0 = 0, Port_1 = 0, State = 0, Enable = false, Connected = false };
            network_dev.Add(dev);


            regReadPeriphery();


            for (int x = 0; x < network_dev.Count; x++)
            {
                TextBox_dev_info[x] = new TextBox { Enabled = true, Font = textbox_font, Location = new Point(x: 330, y: 25), Size = new Size(220, 24) };
                CheckBox_dev_info[x] = new CheckBox {Checked = true, Location = new Point(x: 225, y: cbox_y_location) };
                CheckBox_dev_info[x].CheckedChanged += new EventHandler(ShowCheckedCheckboxes);
                //CheckBox_dev_info[x] = new CheckBox {Size = new Size(24, 24), Padding = new Padding(0, 20, 0, 0) };

                CheckBox_dev_info[x].Checked = network_dev[x].Enable;//setinam kurie enable
                this.panel1.Controls.Add(CheckBox_dev_info[x]);
                
                if (CheckBox_dev_info[x].Checked)
                {
                    device_state_indication(x, Color.LightCoral);//pazymim raudonai, jei rasim pakeisim i zalia 
                }

                if (network_dev[x].Port_1 == 0)
                {
                   
                    TextBox_dev_info[x].Text = network_dev[x].Ip + ':' + network_dev[x].Port_0;//ip port setings
                }
                else
                {
                    TextBox_dev_info[x].Text = network_dev[x].Ip + ':' + network_dev[x].Port_0 + ':' + network_dev[x].Port_1;//ip port setings
                }
                

                

                cbox_y_location += 64;
            }

            this.groupBox_Valdiklis.Controls.Add(TextBox_dev_info[0]);
            this.groupBox_HVgen.Controls.Add(TextBox_dev_info[1]);
            this.groupBox_Spectr.Controls.Add(TextBox_dev_info[2]);
            this.groupBox_Load.Controls.Add(TextBox_dev_info[3]);
            this.groupBox_Metrel_USB.Controls.Add(TextBox_dev_info[6]);
            this.groupBox_Osc_USB.Controls.Add(TextBox_dev_info[7]);

            init_done = true;
        }

        #region <ieskom tinklo devaisu>
        private void NetworkDevConn_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                //string msg = String.Format("Result = {0}", e.Result);
                //MessageBox.Show(msg);
                show_msg("Network scan complete!", Color.SpringGreen);
            }
        }

        private void NetworkDevConn_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = Connect_network_periphery(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
            

        }

        private object Connect_network_periphery(BackgroundWorker bw, int arg)//backraound task
        {   
            int result = 0;
            int a = 0;
            int ret = 1;
            System.Diagnostics.Debug.Print($"Connect_network_periphery:");
            try
            {
                foreach (var dev in network_dev)//einam per dev lista, kurie enable ieskom tinkle, jei toki radom bandom jungtis
                {

                    if (dev.Enable && a < 4) //tikrinam tik jei enable, nuo 4 jau nebe tinklo devaisai - skip.
                    {
                        if (Socket_.socket_ping(network_dev[a], 0))//ar turim tinkle musu devaisa
                        {
                            ret = Socket_.start_socket(network_dev[a], 0);
                            network_dev[a].Connected = true;

                            if (network_dev[a].Port_1 > 0)// jei devaisas turi antra porta ieskom, jei randam jungiames
                            {
                                if (Socket_.socket_ping(network_dev[a], 1))//ar turim tinkle musu devaisa
                                {
                                    ret = Socket_.start_socket(network_dev[a], 1);
                                    network_dev[a].Connected = true;
                                    device_state_indication(a, Color.SpringGreen);//jei prisijungem indikuojam zaliai
                                }
                            }

                            if (a > 0)
                            {
                                device_state_indication(a, Color.SpringGreen);//jei prisijungem indikuojam zaliai
                                if (a == 1)
                                {
                                    HVgen.RunWorkerAsync();
                                }
                            }
                        }
                    }
                    
                    a++;
                }
            }
            catch (Exception err)
            {
                var x = err;
            }

            return result;
        }

        private void Load_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = Load_Socket_Thread(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private object Load_Socket_Thread(BackgroundWorker bw, int arg)
        {
            throw new NotImplementedException();
        }

        private void Specroscope_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = Specroscope_Socket_Thread(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private object Specroscope_Socket_Thread(BackgroundWorker bw, int arg)
        {
            throw new NotImplementedException();
        }

        private void HVgen_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = HVgen_Socket_Thread(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private object HVgen_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;

            while (true)
            {
                System.Diagnostics.Debug.Print($"HV_GEN_connected!!!:");
                Thread.Sleep(1000);
            }

            
            
            return result;
        }

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
                    lbl_rfid.BackColor = color;
                    break;
                case 5:
                    lbl_power.BackColor = color;
                    break;
                case 6:
                    lbl_evse.BackColor = color;
                    break;
                case 7:
                    lbl_osc.BackColor = color;
                    break;
            }
        }

        #endregion

        #region <MainControllerSocket>
        private void MainControllerMODBUS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainControllerMODBUS_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainControllerTCP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainControllerTCP_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

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
                dbg_print("");
                dbg_print("-DISCONNECTED " + (portsFoundBefore - PCportai.Length) + "-\n");
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
                                dbg_print("  close port " + SerPorts[a].port.PortName.ToString());
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
                dbg_print("");
                dbg_print("-CONNECTED " + (PCportai.Length - portsFoundBefore) + "-");
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
                                        dbg_print(msg);
                                    //}
                                    
                                }
                                if (!priskirtas_sekmingai) // jai niekam nepavyko priskirti bet portas grazino ID, informuojam apie tai
                                {
                                    dbg_print("  neatpazintas " + SerPorts[nr].port.PortName + " ID ... skip");
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
                        dbg_print("Exception!   :" + ex.StackTrace);
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
                        dbg_print("! ERROR port " + SerPorts[nr].port.PortName + " lost");
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
                            dbg_print("! Port " + SerPorts[a].port.PortName + " timeout");
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
                catch (Exception) { dbg_print(d + " ...FAIL !"); return false; }

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
                    dbg_print(get);
                    //System.Diagnostics.Debug.Print($"METREL:: = {get}");
                    if (get.Length > 2)
                    {
                        id = get.Substring(2, (get.Length - 2)).ToUpper().Trim();
                        dbg_print(d + " ...OK, " + " ID=\"" + id + "\"");
                        SerPorts[nr].id = id;
                        found_ser_nr = usb_dev_ptr;
                        return true;
                    }
                    else
                    {
                        
                        if (usb_dev_ptr == (all_dev_count-1))
                        {
                            dbg_print(d + " ...TIMEOUT !");
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
                            dbg_print(d + " ...FIND NEXT !");
                        }
                    }
                    usb_dev_ptr++;
                }
                return false;
            }
            else
            {
                dbg_print(d + " ...jau aktyvus! kas per...UAZDAROM!");
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

                            dbg_print("getting answer.." + bytesCount.ToString());
                            dbg_print(line);
                        }
                        catch (Exception ex)
                        {
                            dbg_print("! ERROR reading port " + SerPorts[SerPorts.Count - 1].port.PortName);
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
                dbg_print("! EROOR ! port " + SerPorts[pnr].port.PortName + " disconnected");
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
            get_all_monitors();

            try
            {
                int i = 0;
                int py = 10;

                int w = 0;
                int h = 0;
                foreach (string ids in ml)//pridedam reikiamus atributus ir kiekvinam monitoriuje atverciam TESTAVIMO langa
                {
                    string labelName = "label" + i;

                    Label label = new Label { Name = labelName, AutoSize=true, Text = ids, Location = new Point(x: 5, y: py) };
                    ProgressBar progress = new ProgressBar();
                    progress.Name = Name = "progres" + i;
                    progress.Style = ProgressBarStyle.Continuous;
                    progress.Location = new Point(x: 150, y: py);
                    panelTestResult.Controls.Add(label);
                    panelTestResult.Controls.Add(progress);
                    py = py + 35;

                    w = Screen.AllScreens[i].WorkingArea.Width;
                    h = Screen.AllScreens[i].WorkingArea.Height;

                    MonitorTest mt = new MonitorTest { Id = i, MonitorIds = ids, Width=w, Height=h, WorkPlaceNr = 0, testList = new List<TestList>() };
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
                DialogResult dialog = MessageBox.Show(err.Message, err.InnerException.Message,  MessageBoxButtons.OK);
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
                    lbl_text.Text = lbl_text.Text +" " + x.Substring(0, 4);
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
                Workplaces_reg.SetValue("Workplace" + id, cBoxWplace[a].SelectedItem.ToString());
                //System.Diagnostics.Debug.Print($"WPLACE {id}" + $": = {SavedWorkplaces[a]}");
            }

            Workplaces_reg.Close();
        }

        private void regReadWplace()
        {
            int wp_count = 0;
            int id = 0;

            Workplaces_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Workplaces");

            if (Workplaces_reg != null)
            {
                WorkplacesCount = UInt16.Parse(Workplaces_reg.GetValue("NumWorkplaces").ToString());
                //int height = int.Parse(key.GetValue("Height").ToString());
                System.Diagnostics.Debug.Print($"REGISTRY: = {WorkplacesCount}");

                for(int a=0; a< WorkplacesCount; a++)
                {
                    id = a + 1;
                    SavedWorkplaces[a] = UInt64.Parse(Workplaces_reg.GetValue("Workplace"+id).ToString());
                    System.Diagnostics.Debug.Print($"WPLACE: = {SavedWorkplaces[a]}");
                    
                }

                Workplaces_reg.Close();
            }
            else
            {
                Workplaces_reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Artilux\Workplaces");
                
                wp_count = mtlist.Count;
                System.Diagnostics.Debug.Print($"wp_count: = {wp_count}");
                Workplaces_reg.SetValue("NumWorkplaces", wp_count);

                 foreach (MonitorTest mtx in mtlist)
                 {
                    id = mtx.Id + 1;
                    Workplaces_reg.SetValue("Workplace" + id, mtx.MonitorIds);
                 }

                System.Diagnostics.Debug.Print($"REG_not_found: = {"create"}");
                Workplaces_reg.Close();
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

            for (a = 0; a < WorkplacesCount-1; a++)
            {

                monitor[a] = UInt64.Parse(cBoxWplace[a].SelectedItem.ToString());

                if (WorkplacesCount > 2)
                {
                    for (b = a+1; b < WorkplacesCount; b++)
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
                    if (monitor[a] == monitor[a+1])
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
                    Periphery_reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Artilux\Periphery\Phe_" + a);

                    //network_dev[a].Name = UInt64.Parse(Periphery_reg.GetValue("Name").ToString());
                    network_dev[a].Name = (Periphery_reg.GetValue("Name").ToString());
                    network_dev[a].Ip = (Periphery_reg.GetValue("Ip").ToString());
                    network_dev[a].Port_0 = (int)(Periphery_reg.GetValue("Port0"));
                    network_dev[a].Port_1 = (int)(Periphery_reg.GetValue("Port1"));
                    network_dev[a].Enable = Convert.ToBoolean((Periphery_reg.GetValue("Enable"))); 
                    Workplaces_reg.Close();
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
                    Periphery_reg.SetValue("Enable", Convert.ToInt32(dev.Enable) );
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

            System.Diagnostics.Debug.Print($"UPDATE: = {phe_nr} ip: = {network_dev[phe_nr].Ip} port_0: = {network_dev[phe_nr].Port_0} port_1: = {network_dev[phe_nr].Port_1} en: = {network_dev[phe_nr].Enable}" );
        }

        public void get_all_monitors()
        {
            if (cbAdmin.Checked)
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

                    System.Diagnostics.Debug.Print($"MONITOR: = {id}");
                    id = id.Substring(0, 16);
                    //Console.WriteLine("Active : {0}", oObject["SerialNumberID"].ToString());
                    ml.Add(id);
                    cBoxWplace[0].Items.Add(id);
                    cBoxWplace[1].Items.Add(id);
                    cBoxWplace[2].Items.Add(id);

                    cBoxWplace[a].Enabled = true;
                    a++;
                }
                cBoxWplace[0].Text = SavedWorkplaces[0].ToString();
                cBoxWplace[1].Text = SavedWorkplaces[1].ToString();
                cBoxWplace[2].Text = SavedWorkplaces[2].ToString();
                //System.Diagnostics.Debug.Print($"MONITOR: = {ml}");
            }
            else
            {
                foreach (var screen in Screen.AllScreens)
                {
                    ml.Add(screen.WorkingArea.ToString());
                }
            }

            panelTestResult.Controls.Clear();
        }

        public void dbg_print(string s)
        {
            list_debug.Items.Add(s);
            // nutrinam visus senesnius nei XXX irasu
            while (list_debug.Items.Count > 80)
            {
                list_debug.Items.RemoveAt(0);
            }
            int nr = (list_debug.Items.Count - 1);
            list_debug.Items[nr].Focused = true;
            list_debug.Items[nr].Selected = true;
            if (nr > 0)
            {
                list_debug.Items[nr - 1].Focused = false;
                list_debug.Items[nr - 1].Selected = false;
            }
            list_debug.Update();
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
            
            Socket_.start_socket(network_dev[ITECH_HV_TESTER], 0);
            //Socket_.socket_ping(network_dev[ITECH_HV_TESTER]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Socket_.send_socket(network_dev[ITECH_HV_TESTER]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Socket_.close_socket(network_dev[ITECH_HV_TESTER]);
        }

        private void cbAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void save_ip_Click(object sender, EventArgs e)
        {

            string str;
            string[] subs;
            //string[] ip;
            string ip;
            bool update = false;
            int port;

            for (int i = 0; i < network_dev.Count; i++) {

                str = TextBox_dev_info[i].Text.ToString();
                subs = str.Split(':');


                if ((subs.Length > 1)|| i > 5)// turim turet nora viena porta)
                {
                    ip = Convert.ToString(subs[0]);

                    if (!String.Equals(network_dev[i].Ip, ip))
                    {
                        network_dev[i].Ip = ip;
                        update = true;
                    }


                    port = Convert.ToInt16(subs[1]);

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

        public void show_msg(string msg, Color _color)
        {
            Popup_msg popup = new Popup_msg("", msg, _color, 2);
            popup.Show();
        }

        private void btn_popup_Click(object sender, EventArgs e)
        {
            Popup_msg popup = new Popup_msg("TEST", "asdsd", Color.SpringGreen, 2);
            popup.Show();
        }
    }
}
