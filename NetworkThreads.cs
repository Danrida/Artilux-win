using MonitorsTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ion.Tools.Models.XmlDataExport.Graph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArtiluxEOL
{
    public partial class NetworkThreads : Component
    {

        public string[] Itech_hv_test_type = new string[] { "ACW", "IR", "CONT" };

        string[,] Itech_hv_test_list = new string[,]
        {
            {"VOLT", "CHIS", "CLOS","REF", "TTIM"},
            {"VOLT", "RHIS", "RLOS","REF", "TTIM"},
            {"-", "RHIS", "RLOS", "REF", "TTIM"}
        };

        public string[] Itech_load_param_get_type = new string[] { "MEAS", "FETC:POW:APP:TOT", "FUNC", "CURR", "INP" };

        string[,] Itech_load_list_load = new string[,]
        {
            {"STATE", "PHASE", "U", "I","P", "POWER"},
            {"STATE", "PHASE", "U", "I","P", "POWER"},
            {"STATE", "PHASE", "U", "I","P", "POWER"}
        };

        string[,] Itech_load_list_line = new string[,]
        {
            {"STATE", "PHASE", "U", "I","P", "POWER"},
            {"STATE", "PHASE", "U", "I","P", "POWER"},
            {"STATE", "PHASE", "U", "I","P", "POWER"}
        };
        public bool load_state = false;


        public string[] Siglent_param_type = new string[] { "START", "STOP", "ATTENU" };
        public string[] Siglent_cmd_type = new string[] { "FREQ:START", "FREQ:STOP", "POW:ATT" };

        public string[] Evse_param_type = new string[] { "READY", "IGB?", "EGW?", "EGL?", "ESR 1?", "ESR 0?", "EGM?", "EGR?"};

        public NetworkThreads()
        {
            InitializeComponent();
        }

        SocketClient Socket_ = new SocketClient();

        public NetworkThreads(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


        }

        public DevList devList;
        #region <ieskom tinklo devaisu>

        public void NetworkDevConn_DoWork(object sender, DoWorkEventArgs e)
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

        public void NetworkDevConn_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                Main.Main_main.show_msg("Network scan complete!", Color.SpringGreen);
            }        
    }

        public object Connect_network_periphery(BackgroundWorker bw, int arg)//backraound task
        {
            int result = 0;
            int a = 0;
            int ret = 1;
            System.Diagnostics.Debug.Print($"Connect_network_periphery:");
            try
            {
                foreach (var dev in Main.Main_main.network_dev)//einam per dev lista, kurie enable ieskom tinkle, jei toki radom bandom jungtis
                {

                    if (dev.Enable && a < 7) //tikrinam tik jei enable, nuo 5 jau nebe tinklo devaisai - skip.
                    {
                        // if (Socket_.socket_ping(network_dev[a], 0))//ar turim tinkle musu devaisa
                        //{
                        ret = Socket_.start_socket(Main.Main_main.network_dev[a], 0);

                        /*if (network_dev[a].Port_1 > 0)// jei devaisas turi antra porta ieskom, jei randam jungiames
                        {
                            //if (Socket_.socket_ping(network_dev[a], 1))//ar turim tinkle musu devaisa
                           // {
                                ret = Socket_.start_socket(network_dev[a], 1);
                            //}
                        }*/

                        if (ret == 0)
                        {
                            Main.Main_main.network_dev[a].Connected = true;
                            Main.Main_main.device_state_indication(a, Color.SpringGreen);//jei prisijungem indikuojam zaliai

                            switch (a)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Main.Main_main.HVgen.RunWorkerAsync();
                                    break;
                                case 2:
                                    Main.Main_main.Specroscope.RunWorkerAsync();
                                    break;
                                case 3:
                                    Main.Main_main.Load.RunWorkerAsync();
                                    break;
                                case 4:
                                    Main.Main_main.Barcode1.RunWorkerAsync();
                                    Socket_.receive_socket(Main.Main_main.network_dev[a]);
                                    break;
                                case 5:
                                    Main.Main_main.Barcode2.RunWorkerAsync();
                                    Socket_.receive_socket(Main.Main_main.network_dev[a]);
                                    break;
                                case 6:
                                    Main.Main_main.Barcode3.RunWorkerAsync();
                                    Socket_.receive_socket(Main.Main_main.network_dev[a]);
                                    break;
                            }
                        }

                        //}
                    }
                    a++;
                }
                Main.Main_main.update_all_device_ctrl_access();
            }
            catch (Exception err)
            {
                var x = err;
            }

            return result;
        }
        #endregion


        #region LOAD_ITECH_HANDLER
        public void Load_DoWork(object sender, DoWorkEventArgs e)
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

        public object Load_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;
            bool connection_closed = false;

            long timestamp_now = 0;

            int ptr = 0;

            var net_dev = Main.Main_main.network_dev[DevType.ITECH_LOAD];

            var main_func = Main.Main_main;

            while (!connection_closed)
            {
                //System.Diagnostics.Debug.Print($"HV_GEN_connected!!!:");

                //connection_closed = network_dev[DevType.GWINSTEK_HV_TESTER].client.Poll(1000, SelectMode.SelectRead);

                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.SEND_BEGIN:
                        //string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                        //Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                        break;
                    case NetDev_SendState.SEND_WAIT:
                        break;
                    case NetDev_SendState.SEND_OK:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        switch (net_dev.State)
                        {

                            case NetDev_State.SET_PARAM:
                                if (net_dev.GetSetParamLeft > 1)
                                {
                                    net_dev.GetSetParamLeft--;
                                    ITECH_LOAD_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                //network_dev[DevType.GWINSTEK_HV_TESTER].SubState++; 
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;


                                break;
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;
                                ITECH_LOAD_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                    case NetDev_SendState.RECEIVE_WAIT:
                        //timestamp_now = Socket_.UnixTimeNow();
                        //System.Diagnostics.Debug.Print($"RECEIVE_WAIT:{timestamp_now}");
                        break;
                    case NetDev_SendState.RECEIVE_OK:
                        net_dev.NewResp = true;
                        net_dev.ReceiveRunning = false;
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"ITECH_newCMD:{net_dev.Resp}");

                        switch (net_dev.State)
                        {
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.NewResp = false;
                                if (net_dev.GetSetParamLeft > 0)
                                {
                                    net_dev.GetSetParamLeft--;
                                    ITECH_LOAD_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                ITECH_LOAD_handle_get_params();
                                break;
                            case NetDev_State.SET_PARAM:
                                net_dev.GetSetParamLeft--;
                                ITECH_LOAD_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }

                /* if (network_dev[DevType.GWINSTEK_HV_TESTER].NewSendData)
                 {
                     string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                     Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                 }

                 if (network_dev[DevType.GWINSTEK_HV_TESTER].NewResp)
                 {
                     network_dev[DevType.GWINSTEK_HV_TESTER].NewResp = false;
                     System.Diagnostics.Debug.Print($"newCMD:{network_dev[DevType.GWINSTEK_HV_TESTER].Resp}");
                 }*/

                Thread.Sleep(100);
            }
            return result;
        }

        public void ITECH_LOAD_handle_get_params()
        {
            var net_dev = Main.Main_main.network_dev[DevType.ITECH_LOAD];
            int test_type = net_dev.TestType;

            var dev_struct = Main.Main_main.devList;

            int phase = 0;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            int param = 0;

            string[] split;
            string[,] load_param = new string[3, 19];
            int ptr = 0;
            int cells_count = Main.Main_main.dataGrid_Load_load.Rows[0].Cells.Count;
            int current_param_val;
            DataGridViewRow Row_load;
            DataGridViewRow Row_line;
            DataGridViewCellStyle style = new DataGridViewCellStyle();

            switch (net_dev.State)
            {
                case NetDev_State.READY:
                    if (net_dev.SubState == NetDev_Test.TEST_START)
                    {
                        net_dev.SubState = NetDev_Test.TEST_NONE;
                        net_dev.Cmd = "INP 0";// MANU:ACW:VOLT?
                        System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                        load_state = false;
                        style.BackColor = Color.Silver;
                        Main.Main_main.dataGrid_Load_load.Rows[0].Cells[cells_count - 2].Style = style;
                    }
                    break;

                case NetDev_State.START_TEST:
                    net_dev.Cmd = "INP 1";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    load_state = true;
                    style.BackColor = Color.SpringGreen;
                    Main.Main_main.dataGrid_Load_load.Rows[0].Cells[cells_count - 2].Style = style;
                    break;
                case NetDev_State.END_TEST:
                    //ITECH_LOAD_handle_get_params();

                    break;
                case NetDev_State.SELECT_TEST:
                    net_dev.Cmd = "MANU:STEP " + net_dev.TestType;//test select komandos formavimas
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.GET_PARAM_ALL:
                    Row_load = (DataGridViewRow)Main.Main_main.dataGrid_Load_load.Rows[phase];//get table row by test type
                    Row_line = (DataGridViewRow)Main.Main_main.dataGrid_Load_Line.Rows[phase];//get table row by test type

                    if (param_type_nr == 0)
                    {
                        split = net_dev.Resp.Split(',');//subs[0]=ip,subs[1]=port_0,subs[2]=port_1,
                        for (int i = 0; i < split.Length - 4; i++)
                        {
                            load_param[phase, ptr] = split[i];
                            if (ptr < 3)
                            {
                                Main.Main_main.dataGrid_Load_load.Invoke((MethodInvoker)(() => Row_load.Cells[ptr + 1].Value = format_float(load_param[phase, ptr])));//+1 nes, 0 lenteleje yra faze 
                                                                                                                                                                      //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }
                            if (ptr == 3)
                            {
                                Main.Main_main.dataGrid_Load_Line.Invoke((MethodInvoker)(() => Row_line.Cells[1].Value = load_param[phase, ptr]));
                                //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }

                            if (ptr == 5)
                            {
                                Main.Main_main.dataGrid_Load_Line.Invoke((MethodInvoker)(() => Row_line.Cells[2].Value = load_param[phase, ptr]));
                                //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }
                            ptr++;
                            if (ptr > 16 && phase != 2)//
                            {
                                ptr = 0;
                                phase++;
                                System.Diagnostics.Debug.Print($"phase: = {phase}");
                                Row_load = (DataGridViewRow)Main.Main_main.dataGrid_Load_load.Rows[phase];//get table row by test type
                                Row_line = (DataGridViewRow)Main.Main_main.dataGrid_Load_Line.Rows[phase];//get table row by test type
                            }
                            else
                            {
                                //ptr++;
                            }
                        }
                        net_dev.State = NetDev_State.READY;
                        return;
                    }

                    //System.Diagnostics.Debug.Print($"row_count: = {Row.Cells.Count} ptrt: = {param_type_nr + 4}");
                    if (Row_load.Cells.Count > param_type_nr + 3)
                    {
                        Main.Main_main.dataGrid_Load_load.Invoke((MethodInvoker)(() => Row_load.Cells[param_type_nr + 3].Value = format_float(net_dev.Resp)));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.Main_main.dataGrid_Load_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.Main_main.dataGrid_Load_load.AutoResizeColumns();


                    net_dev.Cmd = Itech_load_param_get_type[param_type_nr - 1] + "?";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.SET_PARAM:
                    switch (net_dev.GetSetParamLeft)
                    {
                        case 3://siunciam kazkokio parametro cmd-set, pvz: CURR 5
                            Socket_.send_socket(net_dev, net_dev.Cmd);
                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            break;
                        case 2://uzklausiam katik setinto parametro, pvz: CURR?
                            if (net_dev.Cmd.IndexOf('?') < 0)//ar paskutine komanda buvo set, nes visos get cmd gale yra ?
                            {
                                string str = net_dev.Cmd;
                                net_dev.Cmd = Regex.Replace(str, "[^A-Za-z:]", "");// ismetam skaicius Regex.Replace(dirty, "[^A-Za-z0-9 ]", "");
                                net_dev.Cmd += @"?";// prilipdom i gala

                                Socket_.send_socket(net_dev, net_dev.Cmd);
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print($"cmd_get_create_err:");
                            }
                            break;
                        case 1://gavom atsakyma, ir lyginam ar uzstate ka norejom
                            current_param_val = Convert.ToInt32(format_float(net_dev.Resp));
                            //System.Diagnostics.Debug.Print($"current_val_check: {current_param_val}");
                            if (current_param_val == dev_struct.DevLoad.load_current)//lyginam ka gavom ir kas turetu buti
                            {
                                //Main.Main_main.show_msg("Parametras pakeistas", Color.SpringGreen)
                            }
                            else
                            {
                                Main.Main_main.show_msg("KLAIDA, parametras nepakeistas", Color.LightCoral);
                            }
                            net_dev.State = NetDev_State.READY;
                            break;

                        default:
                            Main.Main_main.show_msg("KLAIDA, nustatant parametra", Color.LightCoral);
                            break;
                    }
                    break;

            }
        }
        #endregion

        public float str_to_float(string str)
        {
            string[] split;
            split = str.Split('E');

            float param_float;
            float.TryParse(split[0].Replace('.', ','), out param_float);
            float float_tmp = (float)Math.Floor(param_float * 100f) / 100f;
            System.Diagnostics.Debug.Print($"float_tmp: = {float_tmp}");
            return float_tmp;
        }

        public static string format_float(string value)
        {
            string str;
            int len = value.Length;

            int ptr = value.IndexOf(',');
            //System.Diagnostics.Debug.Print($"ptr: = {ptr}");
            str = value;

            if (char.IsNumber(value[0]) || char.IsNumber(value[1]))
            {
                if (ptr != -1)
                {
                    //System.Diagnostics.Debug.Print($"ptr: = {ptr} len: = {len}");
                    str = value.Remove(5, len - ptr).Trim();


                }
                //System.Diagnostics.Debug.Print($"str: = {str}");
                float pwr_param_float;
                float.TryParse(str.Replace('.', ','), out pwr_param_float);
                //System.Diagnostics.Debug.Print($"f: = {pwr_param_float}");
                float float_tmp = (float)Math.Floor(pwr_param_float * 100f) / 100f;

                str = Convert.ToString(float_tmp);
                return str;
            }



            return value;
        }

        public static bool check_is_numbers(string stringValue)
        {
            var pattern = @"^-?[0-9]+(?:\.[0-9]+)?$";
            var regex = new Regex(pattern);

            return regex.IsMatch(stringValue);
        }

        public void Specroscope_DoWork(object sender, DoWorkEventArgs e)
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

        public object Specroscope_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;
            int len = 0;
            bool connection_closed = false;

            long timestamp_now = 0;

            int ptr = 0;

            var net_dev = Main.Main_main.network_dev[DevType.ANALYSER_SIGLENT];

            var main_func = Main.Main_main;

            while (!connection_closed)
            {
                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.SEND_BEGIN:
                        //string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                        //Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                        break;
                    case NetDev_SendState.SEND_WAIT:
                        break;
                    case NetDev_SendState.SEND_OK:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        switch (net_dev.State)
                        {

                            case NetDev_State.SET_PARAM:
                                if (net_dev.GetSetParamLeft > 1)
                                {
                                    net_dev.GetSetParamLeft--;
                                    Sprectroscope_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                //network_dev[DevType.GWINSTEK_HV_TESTER].SubState++; 
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;


                                break;
                            case NetDev_State.GET_PARAM_ALL:
                                //net_dev.SendReceiveState = NetDev_SendState.IDLE;
                                //Sprectroscope_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                    case NetDev_SendState.RECEIVE_WAIT:
                        //timestamp_now = Socket_.UnixTimeNow();
                        //System.Diagnostics.Debug.Print($"RECEIVE_WAIT:{timestamp_now}");
                        break;
                    case NetDev_SendState.RECEIVE_OK:
                        net_dev.NewResp = true;
                        net_dev.ReceiveRunning = false;
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;

                        //System.Diagnostics.Debug.Print($"newCMD:{net_dev.Resp}");

                        if (net_dev.Resp != null)
                        {
                            len = net_dev.Resp.Length;
                            if (len < 1024)
                            {
                                System.Diagnostics.Debug.Print($"SIGLENT_newCMD:{net_dev.Resp}");
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print($"newCMD_len:{len}");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print($"SIGLENT_newCMD: NULL");
                        }



                        switch (net_dev.State)
                        {
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.NewResp = false;
                                if (net_dev.GetSetParamLeft > 0)
                                {
                                    net_dev.GetSetParamLeft--;
                                    Sprectroscope_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                if (net_dev.SubState == NetDev_Test.GET_CHART_DATA)
                                {
                                    net_dev.SubState = NetDev_Test.PROCESS_CHART_DATA;
                                    Sprectroscope_handle_get_params();
                                }
                                break;
                            case NetDev_State.SET_PARAM:
                                net_dev.GetSetParamLeft--;
                                Sprectroscope_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }

                Thread.Sleep(100);
            }
            return result;
        }

        public void Sprectroscope_handle_get_params()
        {
            var net_dev = Main.Main_main.network_dev[DevType.ANALYSER_SIGLENT];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]
            float start;
            float stop;

            int param = 0;

            DataGridViewRow Row;
            int Row_cnt = Main.Main_main.dataGrid_Spectrum.Rows.Count;//get table row by test type

            switch (net_dev.State)
            {
                case NetDev_State.READY:

                    break;

                case NetDev_State.START_TEST:
                    switch (net_dev.SubState)
                    {

                        case NetDev_Test.GET_CHART_DATA:
                            net_dev.Cmd = "TRAC:DATA?";
                            Socket_.send_socket(net_dev, net_dev.Cmd);
                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            //net_dev.SubState = NetDev_State.READY;
                            break;
                        case NetDev_Test.PROCESS_CHART_DATA:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                Main.Main_main.update_chart();
                            }
                            break;
                        case NetDev_Test.GET_RESULT:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                net_dev.SubState = NetDev_Test.TEST_NONE;
                                net_dev.State = NetDev_State.END_TEST;
                                System.Diagnostics.Debug.Print($"GET_RESULT: = {0}");
                                ITECH_HV_handle_get_params();
                            }
                            else
                            {
                                net_dev.Cmd = "MEAS?";//test select komandos formavimas
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                            }

                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                    }
                    break;
                case NetDev_State.END_TEST:
                    Main.Main_main.gwinstek_handle_test_result();

                    break;
                case NetDev_State.SELECT_TEST:

                    break;
                case NetDev_State.GET_PARAM_ALL:

                    Row = (DataGridViewRow)Main.Main_main.dataGrid_Spectrum.Rows[param_type_nr];//get table row by test type


                    //System.Diagnostics.Debug.Print($"row_count: = {Row.Cells.Count} ptrt: = {param_type_nr + 4}");
                    if (Row_cnt - 1 > param_type_nr)
                    {
                        net_dev.device_param[param_type_nr] = net_dev.Resp;
                        Main.Main_main.dataGrid_Spectrum.Invoke((MethodInvoker)(() => Row.Cells[1].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.Main_main.dataGrid_Spectrum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.Main_main.dataGrid_Spectrum.AutoResizeColumns();




                    if (param_type_nr == 0)
                    {
                        start = str_to_float(net_dev.device_param[Siglent_param.START_X]);
                        stop = str_to_float(net_dev.device_param[Siglent_param.STOP_X]);

                        net_dev.device_param[Siglent_param.X_UNIT] = Main.Main_main.get_siglent_unit(net_dev.device_param[Siglent_param.STOP_X]);
                        net_dev.device_param[Siglent_param.START_X] = Convert.ToString(start);
                        net_dev.device_param[Siglent_param.STOP_X] = Convert.ToString(stop);

                        System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.STOP_X]}");
                        System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.START_X]}");
                        System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.ATTENUATION]}");
                        net_dev.SubState = NetDev_Test.TEST_NONE;
                        return;
                    }

                    net_dev.Cmd = Siglent_cmd_type[param_type_nr - 1] + "?";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.SET_PARAM:

                    break;

            }
        }

        #region HVGEN_GW_Instek
        public void HVgen_DoWork(object sender, DoWorkEventArgs e)
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

        public object HVgen_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;
            bool connection_closed = false;

            long timestamp_now = 0;

            int ptr = 0;

            var net_dev = Main.Main_main.network_dev[DevType.GWINSTEK_HV_TESTER];

            var main_func = Main.Main_main;

            while (!connection_closed)
            {
                //System.Diagnostics.Debug.Print($"HV_GEN_connected!!!:");

                //connection_closed = network_dev[DevType.GWINSTEK_HV_TESTER].client.Poll(1000, SelectMode.SelectRead);

                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.SEND_BEGIN:
                        //string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                        //Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                        break;
                    case NetDev_SendState.SEND_WAIT:
                        break;
                    case NetDev_SendState.SEND_OK:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        switch (net_dev.State)
                        {
                            case NetDev_State.SELECT_TEST:
                                net_dev.State = NetDev_State.GET_PARAM_ALL;
                                ITECH_HV_handle_get_params();
                                break;
                            case NetDev_State.SET_PARAM:
                                if (net_dev.GetSetParamLeft > 1)
                                {
                                    //network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamLeft--;
                                    ITECH_HV_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                //network_dev[DevType.GWINSTEK_HV_TESTER].SubState++; 
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;

                                if (net_dev.SubState < NetDev_Test.TEST_START)
                                {
                                    if (net_dev.Cmd.IndexOf('?') < 0)//ar paskutine komanda buvo set, nes visos get cmd gale yra ?
                                    {
                                        string str = net_dev.Cmd;
                                        net_dev.Cmd = Regex.Replace(str, "[^A-Za-z:]", "");// ismetam skaicius Regex.Replace(dirty, "[^A-Za-z0-9 ]", "");
                                        net_dev.Cmd += @"?";// prilipdom i gala

                                        if (net_dev.SubState == NetDev_Test.TEST_RET_ON)
                                        {
                                            net_dev.Cmd = "TEST:RET?";
                                            System.Diagnostics.Debug.Print($"make_get_cmd:{net_dev.Cmd}");
                                        }
                                        else
                                        {
                                            System.Diagnostics.Debug.Print($"make_get_cmd:{net_dev.Cmd}");
                                        }
                                        Socket_.send_socket(net_dev, net_dev.Cmd);
                                    }
                                }
                                break;
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;
                                ITECH_HV_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                    /* case NetDev_SendState.RECEIVE_WAIT:
                         timestamp_now = Socket_.UnixTimeNow();
                         System.Diagnostics.Debug.Print($"RECEIVE_WAIT:{timestamp_now}");
                         break;*/
                    case NetDev_SendState.RECEIVE_OK:
                        net_dev.NewResp = true;
                        net_dev.ReceiveRunning = false;
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"GWINST_newCMD:{net_dev.Resp}");

                        switch (net_dev.State)
                        {
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.NewResp = false;
                                if (net_dev.GetSetParamLeft > 0)
                                {
                                    net_dev.GetSetParamLeft--;
                                    ITECH_HV_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                ITECH_HV_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }

                /* if (network_dev[DevType.GWINSTEK_HV_TESTER].NewSendData)
                 {
                     string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                     Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                 }

                 if (network_dev[DevType.GWINSTEK_HV_TESTER].NewResp)
                 {
                     network_dev[DevType.GWINSTEK_HV_TESTER].NewResp = false;
                     System.Diagnostics.Debug.Print($"newCMD:{network_dev[DevType.GWINSTEK_HV_TESTER].Resp}");
                 }*/

                Thread.Sleep(100);
            }
            return result;
        }

        public void ITECH_HV_handle_get_params()
        {
            var net_dev = Main.Main_main.network_dev[DevType.GWINSTEK_HV_TESTER];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            int param = 0;

            DataGridViewRow Row;

            switch (net_dev.State)
            {
                case NetDev_State.READY:

                    break;

                case NetDev_State.START_TEST:
                    switch (net_dev.SubState)
                    {
                        case NetDev_Test.TEST_SELECT:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;

                                int numVal0 = Int32.Parse(net_dev.Resp);
                                int numVal1 = Int32.Parse(net_dev.TestParam);

                                if (numVal0 == numVal1)//lyginam stringus, ar uzsetinom parametra
                                {
                                    //System.Diagnostics.Debug.Print($"cmd_OK: = {network_dev[DevType.GWINSTEK_HV_TESTER].Cmd}");
                                    net_dev.SubState++;
                                    ITECH_HV_handle_get_params();
                                }
                                else
                                {
                                    string str0 = net_dev.Resp.Remove(0, 2);
                                    string str1 = net_dev.TestParam.Remove(0, 2);

                                    //int numVal0 = Int32.Parse(str0);
                                    //int numVal1 = Int32.Parse(str1);

                                    System.Diagnostics.Debug.Print($"cmd_NOT_EQUAL: = {numVal0} TestParam:{numVal1}");
                                }
                            }
                            else
                            {
                                net_dev.TestParam = "00" + net_dev.TestType.ToString();//sita turim gauti kaip responsa
                                net_dev.Cmd = "MANU:STEP " + net_dev.TestType;//test select komandos formavimas
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            }
                            break;
                        case NetDev_Test.TEST_RET_ON:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                string str = net_dev.Resp.Replace("\r\n", string.Empty);

                                if (String.Equals(net_dev.TestParam, str))//lyginam stringus, ar uzsetinom parametra
                                {
                                    //System.Diagnostics.Debug.Print($"cmd_OK: = {network_dev[DevType.GWINSTEK_HV_TESTER].Cmd}");
                                    net_dev.SubState++;
                                    ITECH_HV_handle_get_params();
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print($"cmd_NOT_EQUAL: = {net_dev.Cmd}");
                                }
                            }
                            else
                            {
                                net_dev.TestParam = "ON";//sita turim gauti kaip responsa
                                net_dev.Cmd = "TEST:RET ON";//Return ok pasibaigus testui
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;

                            }
                            break;
                        case NetDev_Test.TEST_START:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                string str = net_dev.Resp.Replace("\r\n", string.Empty);

                                if (String.Equals(net_dev.TestParam, str))//lyginam stringus, ar uzsetinom parametra
                                {
                                    System.Diagnostics.Debug.Print($"TEST_END: = {0}");
                                    net_dev.SubState++;
                                    ITECH_HV_handle_get_params();
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print($"cmd_NOT_EQUAL:TestParam {net_dev.TestParam} resp: {net_dev.Resp}");
                                }
                            }
                            else
                            {
                                net_dev.TestParam = "OK";//sita turim gauti kaip responsa
                                net_dev.Cmd = "FUNC:TEST ON";//test select komandos formavimas
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            }
                            break;
                        case NetDev_Test.GET_RESULT:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                net_dev.SubState = NetDev_Test.TEST_NONE;
                                net_dev.State = NetDev_State.END_TEST;
                                System.Diagnostics.Debug.Print($"GET_RESULT: = {0}");
                                ITECH_HV_handle_get_params();
                            }
                            else
                            {
                                net_dev.Cmd = "MEAS?";//test select komandos formavimas
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                            }

                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                    }
                    break;
                case NetDev_State.END_TEST:
                    Main.Main_main.gwinstek_handle_test_result();

                    break;
                case NetDev_State.SELECT_TEST:
                    net_dev.Cmd = "MANU:STEP " + net_dev.TestType;//test select komandos formavimas
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.GET_PARAM_ALL:
                    Row = (DataGridViewRow)Main.Main_main.dataGrid_HV_test.Rows[test_type];//get table row by test type


                    //System.Diagnostics.Debug.Print($"row_count: = {Row.Cells.Count} ptrt: = {param_type_nr + 4}");
                    if (Row.Cells.Count > param_type_nr + 4)
                    {
                        Main.Main_main.dataGrid_HV_test.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.Main_main.dataGrid_HV_test.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.Main_main.dataGrid_HV_test.AutoResizeColumns();

                    if (param_type_nr == 0)
                    {
                        net_dev.SubState = NetDev_Test.TEST_NONE;
                        return;
                    }

                    if (Itech_hv_test_list[test_type, (param_type_nr - 1)] == "-") // Jei param liste radom '-' jeiskia nera tokio paramtero, SKIP
                    {
                        if (param_type_nr > 1)// jei ne paskutinis tada tik skip 
                        {
                            param_type_nr--;
                        }
                        else // paskutinis tada einam lauk is GET_STATE
                        {
                            net_dev.State = NetDev_State.READY;
                            return;
                        }
                    }

                    net_dev.Cmd = "MANU:" + Itech_hv_test_type[test_type] + ":" + Itech_hv_test_list[test_type, (param_type_nr - 1)] + "?";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.SET_PARAM:



                    break;

            }

        }


        #endregion

        internal void Barcode2_DoWork(object sender, DoWorkEventArgs e)
        {

            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = Barcode2_Socket_Thread(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }


        public object Barcode2_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;
            bool connection_closed = false;

            long timestamp_now = 0;

            int ptr = 0;

            var net_dev = Main.Main_main.network_dev[DevType.BARCODE_2];

            var main_func = Main.Main_main;

            while (!connection_closed)
            {
                //System.Diagnostics.Debug.Print($"HV_GEN_connected!!!:");

                //connection_closed = network_dev[DevType.GWINSTEK_HV_TESTER].client.Poll(1000, SelectMode.SelectRead);

                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.SEND_BEGIN:
                        //string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                        //Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                        break;
                    case NetDev_SendState.SEND_WAIT:
                        break;
                    case NetDev_SendState.SEND_OK:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                    /* case NetDev_SendState.RECEIVE_WAIT:
                         timestamp_now = Socket_.UnixTimeNow();
                         System.Diagnostics.Debug.Print($"RECEIVE_WAIT:{timestamp_now}");
                         break;*/
                    case NetDev_SendState.RECEIVE_OK:
                        net_dev.NewResp = true;
                        net_dev.ReceiveRunning = false;
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"BAR1_newCMD:{net_dev.Resp}");
                        

                        Barcode2_handle_get_params();
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }

                Thread.Sleep(100);
            }
            return result;
        }

       public void Barcode2_handle_get_params()
        {
            var net_dev = Main.Main_main.network_dev[DevType.BARCODE_2];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            string[] split_raw;//kapojam pagal ;
            string[] split;
            int param = 0;
            string temp = "";
            int ptr = 0;
            DataGridViewRow Row;

            
            

            Row = (DataGridViewRow)Main.Main_main.dataGrid_Barcode2.Rows[net_dev.State];//get table row by test type

            DevList dev_list = Main.Main_main.devList;

            switch (net_dev.State)
            {
                case Evse_State.READY:

                
                    break;

                case Evse_State.BARCODE:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(';');//IGB:3156546487687;0D0A
                        devList.DevEvse[1].barcode = split[1];
                        System.Diagnostics.Debug.Print($"evse_2_bar:{devList.DevEvse[1].barcode}");

                        Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                        net_dev.State = Evse_State.READY;
                    }
                    else
                    {        
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.WIFI_SIGNAL:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        

                        Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                        net_dev.State = Evse_State.READY;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.LTE_SIGNAL:
                    if (net_dev.NewResp)
                    {
                        /*0 - 113dBm or less
                        1 - 111dBm
                        2...30 - 109dBm... -53dBm
                        31 - 51dBm or greater
                        99 Not known or not detectable
                        100 - 116dBm or less
                        101 - 115dBm
                        102...190 - 114dBm...-26dBm
                        191 - 25dBm or greater
                        199 Not known or not detectable*/

                        net_dev.NewResp = false;
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].lte_imei = split[1];
                        System.Diagnostics.Debug.Print($"lte_imei:{Main.Main_main.devList.DevEvse[1].lte_imei}");

                        split = split_raw[1].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].lte_imsi = split[1];
                        System.Diagnostics.Debug.Print($"lte_imsi:{Main.Main_main.devList.DevEvse[1].lte_imsi}");
                        split = split_raw[2].Split(':');//IGB:3156546487687;0D0A
                        //////////RSSI
                        temp = Regex.Replace(split[2], "[^,0-9]", "");
                        split = temp.Split(',');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].lte_rssi = Convert.ToUInt32(split[0]);
                        System.Diagnostics.Debug.Print($"lte_rssi:{Main.Main_main.devList.DevEvse[1].lte_rssi}");

                        //Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        //Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        //Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                        net_dev.State = Evse_State.READY;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.RELAY_ON:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        string str0 = net_dev.Resp.Remove(0, 2);

                        Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.RELAY_OFF:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        string str0 = net_dev.Resp.Remove(0, 2);

                        Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.GET_METER:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        //////////VOLTAGE
                        split = split_raw[0].Split(':');//IGB:3156546487687;0D0A
                        System.Diagnostics.Debug.Print($"split:{split[1]}");
                        Main.Main_main.devList.DevEvse[1].voltage[0] = Convert.ToUInt32(split[2]);
                        split = split_raw[1].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].voltage[1] = Convert.ToUInt32(split[1]);
                        split = split_raw[2].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].voltage[2] = Convert.ToUInt32(split[1]);
                        //////////CURRENT
                        split = split_raw[3].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].current[0] = Convert.ToUInt32(split[1]);
                        split = split_raw[4].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].current[1] = Convert.ToUInt32(split[1]);
                        split = split_raw[5].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].current[2] = Convert.ToUInt32(split[1]);
                        //////POWER
                        split = split_raw[6].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].power = Convert.ToUInt32(split[1]);
                        /////ENERGY
                        split = split_raw[7].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].energy = Convert.ToUInt32(split[1]);
                        //////FREQ
                        split = split_raw[8].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].frequency = Convert.ToUInt32(split[1]);
                        /////TEMP
                        split = split_raw[9].Split(':');//IGB:3156546487687;0D0A
                        Main.Main_main.devList.DevEvse[1].temperature = Convert.ToUInt32(split[1]);

                        System.Diagnostics.Debug.Print($"U1:{Main.Main_main.devList.DevEvse[1].voltage[0]} U2:{Main.Main_main.devList.DevEvse[1].voltage[1]} U3:{Main.Main_main.devList.DevEvse[1].voltage[2]} " +
                            $"I1:{Main.Main_main.devList.DevEvse[1].current[0]} I2:{Main.Main_main.devList.DevEvse[1].current[0]} I3:{Main.Main_main.devList.DevEvse[1].current[0]} P:{Main.Main_main.devList.DevEvse[1].power} " +
                            $"E:{Main.Main_main.devList.DevEvse[1].energy} F:{Main.Main_main.devList.DevEvse[1].frequency} T:{Main.Main_main.devList.DevEvse[1].temperature}");

                        //Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));

                        //Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        //Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                        
                        Main.Main_main.evse2_params.Invoke((MethodInvoker)(() => Main.Main_main.evse2_params.Text = "Voltage: " + dev_list.DevEvse[1].voltage[0] + "V " + dev_list.DevEvse[1].voltage[1] + "V " + dev_list.DevEvse[1].voltage[2] + "V\r\n" +
                            "Current: " + dev_list.DevEvse[1].current[0] + "A " + dev_list.DevEvse[1].current[1] + "A " + dev_list.DevEvse[1].current[2] + "A\r\n" +
                            "Power: " + dev_list.DevEvse[1].power + "W\r\n" +
                            "Frequency: " + dev_list.DevEvse[1].frequency + "Hz\r\n" +
                            "Temperature: " + dev_list.DevEvse[1].temperature + "C"));
                        Main.Main_main.groupBoxBarcode2.Invoke((MethodInvoker)(() => Main.Main_main.groupBoxBarcode2.Controls.Add(Main.Main_main.evse2_params)));
                       
                        
                        net_dev.State = Evse_State.READY;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_State.GET_RFID:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(';');//IGB:3156546487687;0D0A
                        devList.DevEvse[1].barcode = split[1];
                        System.Diagnostics.Debug.Print($"evse_2_bar:{devList.DevEvse[1].barcode}");

                        Main.Main_main.dataGrid_Barcode2.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                        Main.Main_main.dataGrid_Barcode2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                        Main.Main_main.dataGrid_Barcode2.AutoResizeColumns();
                        net_dev.State = Evse_State.READY;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.State];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;

            }
            Socket_.receive_socket(net_dev);
        }


        internal void Barcode1_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void Barcode3_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
