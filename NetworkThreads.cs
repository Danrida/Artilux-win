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

        UInt16 CMD_RETRANSMIT_L = 3;
        UInt16 CMD_RETRANSMIT_H = 10;

        UInt16 PING_TMR_CNT_VALUE = 20;

        long unixTimeMilliseconds;
        long Main_Controller_TCP_Handler_Timer;
        long Main_Controller_TCP_Poll_Rate = 500;//(ms) How often should request be sent to the main controller
        int Main_Controller_TCP_Max_Poll_Count = 10;//Maximum ammount of attempts for a single command

        public string[] Itech_hv_test_type = new string[] { "ACW", "IR", "CONT" };

        public string[] Main_board_rl_command = new string[] {":RL:MAIN:", ":RL:11:", ":RL:12:", ":RL:13:", ":RL:14:", ":LS:", ":LS_EN:", ":LOAD:", ":SOURCE:", ":PP_SEL:", ":CP_SEL:", ":DIODE_SH:", ":PE_OP:", ":CP_SH:"};

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

        public string[] Evse_param_type = new string[] { "READY", "EGD?", "ESD:", "IGB?", "EGW?", "EGL?", "ESR 1?", "ESR 0?", "EGM?", "EGR?"};

        public NetworkThreads()
        {
            InitializeComponent();
        }

        SocketClient Socket_ = new SocketClient();

        public DevList dev_list;

        public NetworkThreads(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            //dev_list = Main.main.devList;

            
        }

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
                Main.main.show_msg("Network scan complete!", Color.SpringGreen);
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
                //Socket_.socket_ping(Main.main.network_dev[a], 0);//ar turim tinkle musu devaisa

                foreach (var dev in Main.main.network_dev)//einam per dev lista, kurie enable ieskom tinkle, jei toki radom bandom jungtis
                {

                    if (dev.Enable && !dev.Connected && a < 7) //tikrinam tik jei enable, nuo 7 jau nebe tinklo devaisai - skip.
                    {
                        /*if (Socket_.socket_ping(Main.main.network_dev[a], 0))//ar turim tinkle musu devaisa
                        {
                            System.Diagnostics.Debug.Print($"Ping_ok: {Main.main.network_dev[a].Name}");
                        }*/
                        
                        ret = Socket_.start_socket(Main.main.network_dev[a], 0);

                        /*if (network_dev[a].Port_1 > 0)// jei devaisas turi antra porta ieskom, jei randam jungiames
                        {
                            //if (Socket_.socket_ping(network_dev[a], 1))//ar turim tinkle musu devaisa
                           // {
                                ret = Socket_.start_socket(network_dev[a], 1);
                            //}
                        }*/

                        if (ret == 0)
                        {
                            Main.main.network_dev[a].Connected = true;
                            Main.main.device_state_indication(a, Color.SpringGreen);//jei prisijungem indikuojam zaliai

                            switch (a)
                            {
                                case 0:
                                    Main.main.MainControllerTCP.RunWorkerAsync();
                                    break;
                                case 1:
                                    Main.main.HVgen.RunWorkerAsync();
                                    break;
                                case 2:
                                    Main.main.Specroscope.RunWorkerAsync();
                                    break;
                                case 3:
                                    Main.main.Load.RunWorkerAsync();
                                    break;
                                case 4:
                                    Main.main.Barcode1.RunWorkerAsync();
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_NOT_CONNECTED;
                                    break;
                                case 5:
                                    Main.main.Barcode2.RunWorkerAsync();
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_NOT_CONNECTED;
                                    break;
                                case 6:
                                    Main.main.Barcode3.RunWorkerAsync();
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_NOT_CONNECTED;
                                    break;
                            }
                        }

                        //}
                    }
                    a++;
                }
                Main.main.update_all_device_ctrl_access();
            }
            catch (Exception err)
            {
                var x = err;
            }

            return result;
        }
        #endregion


        #region MAIN_CONTROLLER
        public void MainControllerTCP_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;
            int arg = 0;
            // Start the time-consuming operation.
            e.Result = Main_Ctrl_Socket_Thread(bw, arg);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        public object Main_Ctrl_Socket_Thread(BackgroundWorker bw, int arg)
        {
            int result = 0;
            bool connection_closed = false;
            long timestamp_now = 0;
            int ptr = 0;

            var net_dev = Main.main.network_dev[DevType.MAIN_CONTROLLER];
            var main_func = Main.main;

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
                        net_dev.SendReceiveState = NetDev_SendState.RECEIVE_WAIT;

                        if (net_dev.CommandId < 254)
                        {
                            net_dev.CommandId++;
                        }
                        else
                        {
                            net_dev.CommandId = 0;
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
                        //System.Diagnostics.Debug.Print($"MAIN_newCMD:{net_dev.Resp}");
                        //MAIN_Ctrl_handle();
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }
                MAIN_Ctrl_handle();
                // System.Diagnostics.Debug.Print($"MAIN");
                Thread.Sleep(100);
            }
            return result;
        }

        public void MAIN_Ctrl_handle()
        {

            //System.Diagnostics.Debug.Print($"RL11: {Main.RL11.STATE}");

            var net_dev = Main.main.network_dev[DevType.MAIN_CONTROLLER];
            net_dev.State = MainBoard_State.IDLE;//Main controller idle (unless specified otherwise later in the code)
            string[] sepResps;//Stores the seperate responces from the main board
            string[] split;//Stores the split strings from the main board responces
            int respID = -1;//Stores responce ID
            int nmrcResp = -1;//Stores numeric responce

            Main.main.Update_data_grid();
            Main.main.Update_controls();


            if (net_dev.NewResp)//There is a new responce from the main board
            {

                net_dev.NewResp = false;//Reset new responce flag
                sepResps = net_dev.Resp.Split('\n');//Split the response

                for (int j = 0; j < (sepResps.Length - 1); j++)
                {

                    System.Diagnostics.Debug.Print($"Received: {sepResps[j]}");
                    split = sepResps[j].Split(':');//Split the response

                    if (split.Length > 1)//Check responce format
                    {
                        split[1] = split[1].Split('.')[0];//Remove the ending of the responce (everything after and including ".")

                        if (!int.TryParse(split[1], out respID))//Get responce id
                        {
                            System.Diagnostics.Debug.Print($"Could not obtain responce id from responce: {net_dev.Resp}");
                        }

                        if (split[0] == "OK")//Responce OK (comes from any command)
                        {
                            for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                            {
                                if (Main.Main_Board_Controls[i] is Relay)//Object type relay
                                {
                                    Relay tmpRL = (Relay)Main.Main_Board_Controls[i];

                                    if (tmpRL.COM_ID == respID)//Found the sender
                                    {
                                        if (tmpRL.STATE == 10)//If the sender was waiting for an OK responce- move to next state
                                        {
                                            tmpRL.STATE = 11;//Now waiting for ON/OFF state confirmation
                                            tmpRL.ATTEMPTS = 0;//Reset the tracker for number of times a command was sent repeatedly
                                            Main.main.Main_Board_Set_Command_ID(tmpRL);//Assign an ID for this command
                                        }
                                        break;//Exit for loop
                                    }
                                }
                                else if (Main.Main_Board_Controls[i] is NumericStateDevice)//Object type relay
                                {
                                    NumericStateDevice tmpNmrc = (NumericStateDevice)Main.Main_Board_Controls[i];

                                    if (tmpNmrc.COM_ID == respID)//Found the sender
                                    {
                                        if (tmpNmrc.STATE == -10)//If the sender was waiting for an OK responce- move to next state
                                        {
                                            tmpNmrc.STATE = -11;//Now waiting for 0/1/2/3... state confirmation
                                            tmpNmrc.ATTEMPTS = 0;//Reset the tracker for number of times a command was sent repeatedly
                                            Main.main.Main_Board_Set_Command_ID(tmpNmrc);//Assign an ID for this command
                                        }
                                        break;//Exit for loop
                                    }
                                }
                            }
                        }
                        else if (split[0] == "ON")//State responce ON (comes from a relay)
                        {
                            for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                            {
                                if (Main.Main_Board_Controls[i] is Relay)//Object type relay
                                {
                                    Relay tmpRL = (Relay)Main.Main_Board_Controls[i];

                                    if (tmpRL.COM_ID == respID)//Found the sender
                                    {
                                        if (tmpRL.STATE == 11)//If the sender was waiting for an state responce- confirm state
                                        {
                                            tmpRL.STATE = 1;//State is ON
                                            tmpRL.ATTEMPTS = 0;//Reset the tracker for number of times a command was sent repeatedly
                                        }
                                        break;//Exit for loop
                                    }
                                }
                            }
                        }
                        else if (split[0] == "OFF")//State responce OFF (comes from a relay)
                        {
                            for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                            {
                                if (Main.Main_Board_Controls[i] is Relay)//Object type relay
                                {
                                    Relay tmpRL = (Relay)Main.Main_Board_Controls[i];

                                    if (tmpRL.COM_ID == respID)//Found the sender
                                    {
                                        if (tmpRL.STATE == 11)//If the sender was waiting for an state responce- confirm state
                                        {
                                            tmpRL.STATE = 0;//State is OFF
                                            tmpRL.ATTEMPTS = 0;//Reset the tracker for number of times a command was sent repeatedly
                                        }
                                        break;//Exit for loop
                                    }
                                }
                            }
                        }
                        else if (int.TryParse(split[0], out nmrcResp))//State responce in mumeric (0/1/2/3...)
                        {
                            for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                            {
                                if (Main.Main_Board_Controls[i] is NumericStateDevice)//Object type numeric state device
                                {
                                    NumericStateDevice tmpNmrc = (NumericStateDevice)Main.Main_Board_Controls[i];

                                    if (tmpNmrc.COM_ID == respID)//Found the sender
                                    {
                                        if (tmpNmrc.STATE == -11)//If the sender was waiting for an state responce- confirm state
                                        {
                                            tmpNmrc.STATE = nmrcResp;//State is equal to responce
                                            tmpNmrc.ATTEMPTS = 0;//Reset the tracker for number of times a command was sent repeatedly
                                        }
                                        break;//Exit for loop
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print($"Unhandled responce from main controller: {net_dev.Resp}");
                        }

                        Main.main.Update_data_grid();

                    }
                    else
                    {
                        System.Diagnostics.Debug.Print($"Bad response format: {net_dev.Resp}");
                    }

                }

            }

            DateTimeOffset now = DateTimeOffset.UtcNow;
            unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            if ((unixTimeMilliseconds - Main_Controller_TCP_Handler_Timer) > Main_Controller_TCP_Poll_Rate)
            {
                for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Go trough all main controller objects
                {
                    if (Main.Main_Board_Controls[i] is Relay)//Object type relay
                    {
                        Relay tmpRL = (Relay)Main.Main_Board_Controls[i];

                        if (tmpRL.STATE == 10)//Found an object in a waiting state
                        {
                            if (tmpRL.ATTEMPTS < Main_Controller_TCP_Max_Poll_Count)//Check for timeout
                            {
                                tmpRL.ATTEMPTS++;//Next attempt
                                net_dev.State = MainBoard_State.BUSY;//Main controller is now busy (TCP communication)
                                net_dev.Cmd = tmpRL.COM_ID + ":" + tmpRL.NAME + ":" + tmpRL.SET;//Generate TCP command (000:RLXX:1/0)
                                Socket_.send_socket(net_dev, net_dev.Cmd);//#sendit
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;//Main controller in send state
                                System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
                            }
                            else
                            {
                                tmpRL.STATE = -1;//Could not get "OK" response
                            }
                        }
                        else if (tmpRL.STATE == 11)//Found an object in a waiting state
                        {
                            if (tmpRL.ATTEMPTS < Main_Controller_TCP_Max_Poll_Count)//Check for timeout
                            {
                                tmpRL.ATTEMPTS++;//Next attempt
                                net_dev.State = MainBoard_State.BUSY;//Main controller is now busy (TCP communication)
                                net_dev.Cmd = tmpRL.COM_ID + ":" + tmpRL.NAME + ":?";//Generate TCP command (000:RLXX:?)
                                Socket_.send_socket(net_dev, net_dev.Cmd);//#sendit
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;//Main controller in send state
                                System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
                            }
                            else
                            {
                                tmpRL.STATE = -2;//Could not get "ON"/"OFF" confirmation
                            }
                        }
                    }
                    else if (Main.Main_Board_Controls[i] is NumericStateDevice)//Object type NumericStateDevice
                    {
                        NumericStateDevice tmpNmrc = (NumericStateDevice)Main.Main_Board_Controls[i];

                        if (tmpNmrc.STATE == -10)//Found an object in a waiting state
                        {
                            if (tmpNmrc.ATTEMPTS < Main_Controller_TCP_Max_Poll_Count)//Check for timeout
                            {
                                tmpNmrc.ATTEMPTS++;//Next attempt
                                net_dev.State = MainBoard_State.BUSY;//Main controller is now busy (TCP communication)
                                net_dev.Cmd = tmpNmrc.COM_ID + ":" + tmpNmrc.NAME + ":" + tmpNmrc.SET;//Generate TCP command (000:XXXX:0/1/2/3...)
                                Socket_.send_socket(net_dev, net_dev.Cmd);//#sendit
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;//Main controller in send state
                                System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
                            }
                            else
                            {
                                tmpNmrc.STATE = -100;//Could not get "OK" response
                            }
                        }
                        else if (tmpNmrc.STATE == -11)//Found an object in a waiting state
                        {
                            if (tmpNmrc.ATTEMPTS < Main_Controller_TCP_Max_Poll_Count)//Check for timeout
                            {
                                tmpNmrc.ATTEMPTS++;//Next attempt
                                net_dev.State = MainBoard_State.BUSY;//Main controller is now busy (TCP communication)
                                net_dev.Cmd = tmpNmrc.COM_ID + ":" + tmpNmrc.NAME + ":?";//Generate TCP command (000:XXXX:?)
                                Socket_.send_socket(net_dev, net_dev.Cmd);//#sendit
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;//Main controller in send state
                                System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
                            }
                            else
                            {
                                tmpNmrc.STATE = -101;//Could not get 0/1/2/3... confirmation
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.Print($"Unknown object type was found");
                    }
                }

                Main_Controller_TCP_Handler_Timer = unixTimeMilliseconds;//Reset TCP poll timer
            }

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

            var net_dev = Main.main.network_dev[DevType.ITECH_LOAD];

            var main_func = Main.main;

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
            var net_dev = Main.main.network_dev[DevType.ITECH_LOAD];
            int test_type = net_dev.TestType;

            var dev_struct = Main.main.devList;

            int phase = 0;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            int param = 0;

            string[] split;
            string[,] load_param = new string[3, 19];
            int ptr = 0;
            int cells_count = Main.main.dataGrid_Load_load.Rows[0].Cells.Count;
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
                        Main.main.dataGrid_Load_load.Rows[0].Cells[cells_count - 2].Style = style;
                    }
                    break;

                case NetDev_State.START_TEST:
                    net_dev.Cmd = "INP 1";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    load_state = true;
                    style.BackColor = Color.SpringGreen;
                    Main.main.dataGrid_Load_load.Rows[0].Cells[cells_count - 2].Style = style;
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
                    Row_load = (DataGridViewRow)Main.main.dataGrid_Load_load.Rows[phase];//get table row by test type
                    Row_line = (DataGridViewRow)Main.main.dataGrid_Load_Line.Rows[phase];//get table row by test type

                    if (param_type_nr == 0)
                    {
                        split = net_dev.Resp.Split(',');//subs[0]=ip,subs[1]=port_0,subs[2]=port_1,
                        for (int i = 0; i < split.Length - 4; i++)
                        {
                            load_param[phase, ptr] = split[i];
                            if (ptr < 3)
                            {
                                Main.main.dataGrid_Load_load.Invoke((MethodInvoker)(() => Row_load.Cells[ptr + 1].Value = format_float(load_param[phase, ptr])));//+1 nes, 0 lenteleje yra faze 
                                                                                                                                                                      //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }
                            if (ptr == 3)
                            {
                                Main.main.dataGrid_Load_Line.Invoke((MethodInvoker)(() => Row_line.Cells[1].Value = load_param[phase, ptr]));
                                //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }

                            if (ptr == 5)
                            {
                                Main.main.dataGrid_Load_Line.Invoke((MethodInvoker)(() => Row_line.Cells[2].Value = load_param[phase, ptr]));
                                //Row_load.Cells[param_type_nr + 3].Value = load_param[phase, ptr];
                            }
                            ptr++;
                            if (ptr > 16 && phase != 2)//
                            {
                                ptr = 0;
                                phase++;
                                System.Diagnostics.Debug.Print($"phase: = {phase}");
                                Row_load = (DataGridViewRow)Main.main.dataGrid_Load_load.Rows[phase];//get table row by test type
                                Row_line = (DataGridViewRow)Main.main.dataGrid_Load_Line.Rows[phase];//get table row by test type
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
                        Main.main.dataGrid_Load_load.Invoke((MethodInvoker)(() => Row_load.Cells[param_type_nr + 3].Value = format_float(net_dev.Resp)));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.main.dataGrid_Load_load.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.main.dataGrid_Load_load.AutoResizeColumns();


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
                                //Main.main.show_msg("Parametras pakeistas", Color.SpringGreen)
                            }
                            else
                            {
                                Main.main.show_msg("KLAIDA, parametras nepakeistas", Color.LightCoral);
                            }
                            net_dev.State = NetDev_State.READY;
                            break;

                        default:
                            Main.main.show_msg("KLAIDA, nustatant parametra", Color.LightCoral);
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


        #region SPECTROSCOPE
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

            var net_dev = Main.main.network_dev[DevType.ANALYSER_SIGLENT];

            var main_func = Main.main;

            while (!connection_closed)
            {
                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.IDLE:
                        if(net_dev.State == NetDev_State.READY && net_dev.Connected)
                        {
                            if (net_dev.NewResp)
                            {
                                Sprectroscope_handle_get_params();
                            }
                            else
                            {
                                if (net_dev.PingPktTmrCnt > PING_TMR_CNT_VALUE)
                                {
                                    net_dev.PingPktTmrCnt = 0;
                                    net_dev.Cmd = "SYST:TIME?";//ping cmd, uzklausiam laiko
                                    Socket_.send_socket(net_dev, net_dev.Cmd);
                                }
                            }

                            net_dev.PingPktTmrCnt++;
                        }
                        break;
                    case NetDev_SendState.SEND_BEGIN:
                        //string data = network_dev[DevType.GWINSTEK_HV_TESTER].Cmd;
                        //Socket_.send_socket(network_dev[DevType.GWINSTEK_HV_TESTER], data);
                        break;
                    case NetDev_SendState.SEND_WAIT:
                        break;
                    case NetDev_SendState.SEND_OK:
                        
                        switch (net_dev.State)
                        {

                            case NetDev_State.SET_PARAM:
                                
                                break;
                            case NetDev_State.START_TEST:
                                //network_dev[DevType.GWINSTEK_HV_TESTER].SubState++; 
                                net_dev.SendReceiveState = NetDev_SendState.IDLE;


                                break;
                            case NetDev_State.GET_PARAM_ALL:
                                net_dev.SendReceiveState = NetDev_SendState.RECEIVE_WAIT;
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
                        if(net_dev.CmdRetransmitCnt > CMD_RETRANSMIT_L)//negavom responso, persiunciam viska dar karta
                        {
                            net_dev.CmdRetransmitCnt = 0;
                            net_dev.GetSetParamLeft = net_dev.GetSetParamCount;
                            Main.main.dbg_print(DbgType.MAIN, "RETRANSMIT_CMD: Siglent" , Color.DarkRed);
                            Sprectroscope_handle_get_params();
                        }

                        net_dev.CmdRetransmitCnt++;
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
                                //System.Diagnostics.Debug.Print($"SIGLENT_newCMD:{net_dev.Resp}");
                                Main.main.dbg_print(DbgType.SPECTRO, "SIGLENT_newCMD:" + net_dev.Resp, Color.Orange);
                            }
                            else
                            {
                                //System.Diagnostics.Debug.Print($"newCMD_len:{len}");
                                Main.main.dbg_print(DbgType.SPECTRO, "SIGLENT_newCMD_len:" + len, Color.Orange);
                            }
                        }
                        else
                        {
                            //len = net_dev.Resp.Length;
                            //System.Diagnostics.Debug.Print($"SIGLENT_newCMD: NULL");
                            net_dev.Connected = false;
                            Main.main.dbg_print(DbgType.SPECTRO, "SIGLENT_newCMD: NULL", Color.Orange);
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
            var net_dev = Main.main.network_dev[DevType.ANALYSER_SIGLENT];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get.
            float start;
            float stop;

            int param = 0;

            DataGridViewRow Row;
            int Row_cnt = Main.main.dataGrid_Spectrum.Rows.Count;//get table row by test type

            switch (net_dev.State)
            {
                case NetDev_State.READY:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        //Main.main.dbg_print(DbgType.MAIN, "ping_CMD: Siglent", Color.DarkRed);
                        //Debug.Print($"ping_CMD:{net_dev.Resp}");
                        Main.main.dbg_print(DbgType.PING, "ping:" + net_dev.Resp, Color.Blue);
                    }
                    
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
                                net_dev.PingPktTmrCnt = 0;
                                net_dev.State = NetDev_State.READY;
                                Main.main.update_chart();
                            }
                            break;
                        case NetDev_Test.GET_RESULT:
                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;
                                net_dev.SubState = NetDev_Test.TEST_NONE;
                                net_dev.State = NetDev_State.END_TEST;
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
                    //Main.main.gwinstek_handle_test_result();

                    break;
                case NetDev_State.SELECT_TEST:

                    break;
                case NetDev_State.GET_PARAM_ALL:

                    Row = (DataGridViewRow)Main.main.dataGrid_Spectrum.Rows[param_type_nr];//get table row nr.by test type  

                    //System.Diagnostics.Debug.Print($"row_count: = {Row.Cells.Count} ptrt: = {param_type_nr + 4}");
                    if (Row_cnt - 1 > param_type_nr)
                    {
                        net_dev.device_param[param_type_nr] = net_dev.Resp;
                        Main.main.dataGrid_Spectrum.Invoke((MethodInvoker)(() => Row.Cells[1].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.main.dataGrid_Spectrum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.main.dataGrid_Spectrum.AutoResizeColumns();

                    if (param_type_nr == 0)//gavom paskutini parametra
                    {
                        start = str_to_float(net_dev.device_param[Siglent_param.START_X]);
                        stop = str_to_float(net_dev.device_param[Siglent_param.STOP_X]);

                        net_dev.device_param[Siglent_param.X_UNIT] = Main.main.get_siglent_unit(net_dev.device_param[Siglent_param.STOP_X]);
                        net_dev.device_param[Siglent_param.START_X] = Convert.ToString(start);
                        net_dev.device_param[Siglent_param.STOP_X] = Convert.ToString(stop);

                        //System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.STOP_X]}");
                        //System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.START_X]}");
                        //System.Diagnostics.Debug.Print($"sig_param:{net_dev.device_param[Siglent_param.ATTENUATION]}");
                        Main.main.dbg_print(DbgType.SPECTRO, "START_X:" + net_dev.device_param[Siglent_param.START_X], Color.Orange);
                        Main.main.dbg_print(DbgType.SPECTRO, "STOP_X:" + net_dev.device_param[Siglent_param.STOP_X], Color.Orange);
                        Main.main.dbg_print(DbgType.SPECTRO, "ATTENUATION:" + net_dev.device_param[Siglent_param.ATTENUATION], Color.Orange);
                        net_dev.PingPktTmrCnt = 0;
                        net_dev.State = NetDev_State.READY;
                        net_dev.SubState = NetDev_Test.TEST_NONE;
                        return;
                    }

                    net_dev.Cmd = Siglent_cmd_type[param_type_nr - 1] + "?";// MANU:ACW:VOLT?
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Main.main.dbg_print(DbgType.SPECTRO, "cmd:" + net_dev.Cmd, Color.Orange);
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.SET_PARAM:

                    break;

            }
        }
        #endregion

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

            var net_dev = Main.main.network_dev[DevType.GWINSTEK_HV_TESTER];

            var main_func = Main.main;

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
                                GW_HV_handle_get_params();
                                break;
                            case NetDev_State.SET_PARAM:
                                if (net_dev.GetSetParamLeft > 1)
                                {
                                    //network_dev[DevType.GWINSTEK_HV_TESTER].GetSetParamLeft--;
                                    GW_HV_handle_get_params();
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
                                GW_HV_handle_get_params();
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
                                    GW_HV_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                GW_HV_handle_get_params();
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

        public void GW_HV_handle_get_params()
        {
            var net_dev = Main.main.network_dev[DevType.GWINSTEK_HV_TESTER];
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
                                    GW_HV_handle_get_params();
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
                                    GW_HV_handle_get_params();
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
                                    GW_HV_handle_get_params();
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
                                GW_HV_handle_get_params();
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
                    Main.main.gwinstek_handle_test_result();

                    break;
                case NetDev_State.SELECT_TEST:
                    net_dev.Cmd = "MANU:STEP " + net_dev.TestType;//test select komandos formavimas
                    System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    break;
                case NetDev_State.GET_PARAM_ALL:
                    Row = (DataGridViewRow)Main.main.dataGrid_HV_test.Rows[test_type];//get table row by test type

                    //System.Diagnostics.Debug.Print($"row_count: = {Row.Cells.Count} ptrt: = {param_type_nr + 4}");
                    if (Row.Cells.Count > param_type_nr + 4)//testo parametra kisam i lentele
                    {
                        Main.main.dataGrid_HV_test.Invoke((MethodInvoker)(() => Row.Cells[param_type_nr + 4].Value = net_dev.Resp));
                        //Row.Cells[param_type_nr + 4].Value = network_dev[DevType.GWINSTEK_HV_TESTER].Resp;//push param value to table
                    }
                    Main.main.dataGrid_HV_test.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//formatas auto, pagal data_size
                    Main.main.dataGrid_HV_test.AutoResizeColumns();

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

        #region EVSE_2 control
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

            var net_dev = Main.main.network_dev[DevType.BARCODE_2];

            var main_func = Main.main;

            while (!connection_closed)
            {
                //System.Diagnostics.Debug.Print($"HV_GEN_connected!!!:");

                //connection_closed = network_dev[DevType.GWINSTEK_HV_TESTER].client.Poll(1000, SelectMode.SelectRead);

                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.IDLE:
                        if (net_dev.State == Evse_State.EVSE_NOT_CONNECTED)
                        {
                            if (net_dev.NewResp)
                            {
                                Barcode2_handle_get_params();
                            }
                            else
                            {
                                if (net_dev.PingPktTmrCnt > PING_TMR_CNT_VALUE)
                                {
                                    net_dev.PingPktTmrCnt = 0;
                                    net_dev.Cmd = "IGP?";//test select komandos formavimas
                                    Socket_.send_socket(net_dev, net_dev.Cmd);
                                }
                            }

                            net_dev.PingPktTmrCnt++;
                        }
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
                        //System.Diagnostics.Debug.Print($"BAR2_newCMD:{net_dev.Resp}");
                        Main.main.dbg_print(DbgType.EVSE, "BAR2_newCMD:" + net_dev.Resp, Color.Orange);
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
            var net_dev = Main.main.network_dev[DevType.BARCODE_2];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            string[] split_raw;//kapojam pagal ;
            string[] split;
            int param = 0;
            string temp = "";
            int ptr = 0;
            Int32 rssi_constant = -113;
            DataGridViewRow Row;

            net_dev.PingPktTmrCnt = 0;//resetinam ping tmr

            DevList dev_list = Main.main.devList;
            Test_struc[] test = Main.main.Test;
            //Row = (DataGridViewRow)Main.main.dataGrid_Barcode2.Rows[net_dev.State];//get table row by test type

            switch (net_dev.State)
            {
                
                case Evse_State.EVSE_CONNECTED:
                    if (net_dev.NewResp)
                    {
                        if (net_dev.SubState == Evse_Sub_State.NONE)
                        {
                            switch (net_dev.Resp[2])
                            {
                                case 'D':
                                    split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                                    split = split_raw[0].Split(':');
                                    //dev_list.DevEvse[1].rfid_length = Convert.ToUInt32(split[1]);
                                    Main.main.dbg_print(DbgType.MAIN, "Rfid_len:" + split[1], Color.DarkGray);
                                    break;
                                case 'E':
                                    split_raw = net_dev.Resp.Split(';');//IGE:2;0D0A
                                    split = split_raw[0].Split(':');
                                    dev_list.DevEvse[1].serial_no = split[1];
                                    Main.main.dbg_print(DbgType.MAIN, "EVSE_NR:" + split[1], Color.DarkViolet);
                                    Main.main.lbl_evse2.BackColor = Color.SpringGreen;
                                    break;
                                case 'T':
                                    //Debug.Print($"ping_CMD:{net_dev.Resp}");
                                    Main.main.dbg_print(DbgType.PING, "ping:" + net_dev.Resp, Color.Blue);
                                    break;
                            }
                            
                        }
                    }
                    Evse2_conected_sub_state();

                    break;
                case Evse_State.EVSE_WAIT_CONNECT:
                    if (net_dev.NewResp)
                    {
                        switch (net_dev.Resp[2])
                        {
                            case 'W':
                                split_raw = net_dev.Resp.Split(';');//IGW:52;0D0A
                                split = split_raw[0].Split(':');
                                Main.main.dbg_print(DbgType.EVSE, "EVSE_WAIT:" + split[1], Color.DarkGray);
                                break;

                            case 'E':
                                split_raw = net_dev.Resp.Split(';');//IGE:2;0D0A
                                split = split_raw[0].Split(':');
                                dev_list.DevEvse[1].serial_no = split[1];
                                Main.main.dbg_print(DbgType.EVSE, "EVSE_NR:" + split[1], Color.DarkViolet);
                                net_dev.State = Evse_State.EVSE_CONNECTED;
                                test[1].evse_state = Evse_State.EVSE_CONNECTED;
                                Main.main.lbl_evse2.BackColor = Color.SpringGreen;
                                break;
                            case 'T':
                                //Debug.Print($"ping_CMD:{net_dev.Resp}");
                                Main.main.dbg_print(DbgType.PING, "ping:" + net_dev.Resp, Color.Blue);
                                break;
                        }
                    }
                    break;

                case Evse_State.EVSE_NOT_CONNECTED:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        switch (net_dev.Resp[2])
                        {
                            case 'B':
                                split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                                split = split_raw[0].Split(':');
                                dev_list.DevEvse[1].barcode = split[1];
                                Main.main.dbg_print(DbgType.EVSE, "Barcode2:" + dev_list.DevEvse[1].barcode, Color.DarkGray);
                                net_dev.State = Evse_State.EVSE_WAIT_CONNECT;
                                break;
                            case 'W':
                                split_raw = net_dev.Resp.Split(';');//IGW:52;0D0A
                                split = split_raw[0].Split(':');
                                Main.main.dbg_print(DbgType.EVSE, "WAIT_BARCOD:" + split[1], Color.DarkGray);
                                break;
                            case 'T':
                                  //Debug.Print($"ping_CMD:{net_dev.Resp}");
                                Main.main.dbg_print(DbgType.PING, "ping:"+ net_dev.Resp, Color.Blue);
                                break;
                        }
                    }
                    break;

                case Evse_Sub_State.BARCODE:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(':');//IGB:3156546487687;0D0A
                        dev_list.DevEvse[1].barcode = split[1];
                        System.Diagnostics.Debug.Print($"evse_2_bar:{dev_list.DevEvse[1].barcode}");

                        net_dev.State = Evse_State.EVSE_CONNECTED;
                    }
                    else
                    {        
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;

                
                

            }
            Socket_.receive_socket(net_dev);
        }

        public void Evse2_conected_sub_state()
        {
            var net_dev = Main.main.network_dev[DevType.BARCODE_2];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            string[] split_raw;//kapojam pagal ;
            string[] split;
            int param = 0;
            string temp = "";
            int ptr = 0;
            Int32 rssi_constant = -113;
            DataGridViewRow Row;

            DevList dev_list = Main.main.devList;


            //Row = (DataGridViewRow)Main.main.dataGrid_Barcode2.Rows[net_dev.State];//get table row by test type

            switch (net_dev.SubState)
            {
                case Evse_Sub_State.GET_DATE:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(':');
                        //dev_list.DevEvse[1].lte_imei = split[1];

                        bool cmd_ok = false;

                        if (split[0][2] == 'D')
                        {
                            System.Diagnostics.Debug.Print($"split[0]:{split[0]} split[1]:{split[1]}");
                            cmd_ok = Convert.ToBoolean(Convert.ToInt32(split[1]));
                            System.Diagnostics.Debug.Print($"lte_cmd:{cmd_ok}");
                        }
                        else
                        {
                            //System.Diagnostics.Debug.Print($"wrong_cmd:{split_raw[0]}");
                            Main.main.evse2_params.Invoke((MethodInvoker)(() => Main.main.evse2_params.Text =
                                "BAD_CMD: " + split_raw[0] + "\r\n"));
                        }

                        if (cmd_ok)
                        {
                            //System.Diagnostics.Debug.Print($"TimeStamp:{split[1]}");
                            Main.main.evse2_params.Invoke((MethodInvoker)(() => Main.main.evse2_params.Text =
                                "EVSE_TIMESTAMP: " + split[1] + "\r\n"));
                            net_dev.SubState = Evse_Sub_State.NONE;
                        }
                        }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }

                    break;
                case Evse_Sub_State.SET_DATE:
                    if (net_dev.NewResp)
                    {
                        
                    }
                    else
                    {
                        long timestamp_now = Socket_.UnixTimeNow();
                        net_dev.Cmd = Evse_param_type[net_dev.SubState] + Convert.ToString(timestamp_now) + ';' ;//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }

                    break;

                case Evse_Sub_State.WIFI_SIGNAL:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;

                        net_dev.State = Evse_State.EVSE_CONNECTED;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;

                #region LTE_DATA
                case Evse_Sub_State.LTE_SIGNAL:
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
                        split = split_raw[0].Split(':');
                        //dev_list.DevEvse[1].lte_imei = split[1];

                        bool lte_ok = false;

                        if (split[0][2] == 'L')
                        {
                            System.Diagnostics.Debug.Print($"split[0]:{split[0]} split[1]:{split[1]}");
                            lte_ok = Convert.ToBoolean(Convert.ToInt32(split[1]));
                            System.Diagnostics.Debug.Print($"lte_cmd:{lte_ok}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print($"not_lte_cmd:{split_raw[0]}");
                        }

                        if (lte_ok)
                        {
                            split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                            split = split_raw[1].Split(':');
                            dev_list.DevEvse[1].lte_imei = split[1];
                            System.Diagnostics.Debug.Print($"lte_imei:{dev_list.DevEvse[1].lte_imei}");

                            split = split_raw[2].Split(':');
                            dev_list.DevEvse[1].lte_imsi = split[1];
                            System.Diagnostics.Debug.Print($"lte_imsi:{dev_list.DevEvse[1].lte_imsi}");
                            split = split_raw[3].Split(':');
                            //////////RSSI
                            temp = Regex.Replace(split[2], "[^,0-9]", "");
                            split = temp.Split(',');
                            dev_list.DevEvse[1].lte_rssi_raw = Convert.ToInt16(split[0]);
                            System.Diagnostics.Debug.Print($"lte_rssi:{dev_list.DevEvse[1].lte_rssi_raw}");

                            dev_list.DevEvse[1].lte_rssi = (rssi_constant + (dev_list.DevEvse[1].lte_rssi_raw * 2));// +CSQ value to dBm

                            Main.main.evse2_params.Invoke((MethodInvoker)(() => Main.main.evse2_params.Text =
                                "LTE_IMEI: " + dev_list.DevEvse[1].lte_imei + "\r\n" +
                                "LTE_IMSI: " + dev_list.DevEvse[1].lte_imsi + "\r\n" +
                                "LTE_RSSI: " + dev_list.DevEvse[1].lte_rssi + "dBm\r\n"));

                            net_dev.SubState = Evse_Sub_State.NONE;
                        }

                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                #endregion
                case Evse_Sub_State.RELAY_ON:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        string str0 = net_dev.Resp.Remove(0, 2);

                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                case Evse_Sub_State.RELAY_OFF:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        string str0 = net_dev.Resp.Remove(0, 2);

                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                #region  GET_METER_DATA
                case Evse_Sub_State.GET_METER:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        bool cmd_ok = false;

                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(':');

                        if (split[0][2] == 'M')
                        {
                            System.Diagnostics.Debug.Print($"split[0]:{split[0]} split[1]:{split[1]}");
                            cmd_ok = Convert.ToBoolean(Convert.ToInt32(split[1]));
                            System.Diagnostics.Debug.Print($"cmd_ok:{cmd_ok}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print($"not_lte_cmd:{split_raw[0]}");
                        }


                        if (cmd_ok)
                        {
                            //////////VOLTAGE
                            //System.Diagnostics.Debug.Print($"split:{split[1]}");
                            split = split_raw[1].Split(':');
                            dev_list.DevEvse[1].voltage[0] = Convert.ToUInt32(split[1]);
                            split = split_raw[2].Split(':');
                            dev_list.DevEvse[1].voltage[1] = Convert.ToUInt32(split[1]);
                            split = split_raw[3].Split(':');
                            dev_list.DevEvse[1].voltage[2] = Convert.ToUInt32(split[1]);
                            //////////CURRENT
                            split = split_raw[4].Split(':');
                            dev_list.DevEvse[1].current[0] = Convert.ToUInt32(split[1]);
                            split = split_raw[5].Split(':');
                            dev_list.DevEvse[1].current[1] = Convert.ToUInt32(split[1]);
                            split = split_raw[6].Split(':');
                            dev_list.DevEvse[1].current[2] = Convert.ToUInt32(split[1]);
                            //////POWER
                            split = split_raw[7].Split(':');
                            dev_list.DevEvse[1].power = Convert.ToUInt32(split[1]);
                            /////ENERGY
                            split = split_raw[8].Split(':');
                            dev_list.DevEvse[1].energy = Convert.ToUInt32(split[1]);
                            //////FREQ
                            split = split_raw[9].Split(':');
                            dev_list.DevEvse[1].frequency = Convert.ToUInt32(split[1]);
                            /////TEMP
                            split = split_raw[10].Split(':');
                            dev_list.DevEvse[1].temperature = Convert.ToUInt32(split[1]);

                            Main.main.evse2_params.Invoke((MethodInvoker)(() => Main.main.evse2_params.Text = "Voltage: " + dev_list.DevEvse[1].voltage[0] + "V " + dev_list.DevEvse[1].voltage[1] + "V " + dev_list.DevEvse[1].voltage[2] + "V\r\n" +
                                "Current: " + dev_list.DevEvse[1].current[0] + "A " + dev_list.DevEvse[1].current[1] + "A " + dev_list.DevEvse[1].current[2] + "A\r\n" +
                                "Power: " + dev_list.DevEvse[1].power + "W\r\n" +
                                "Frequency: " + dev_list.DevEvse[1].frequency + "Hz\r\n" +
                                "Temperature: " + dev_list.DevEvse[1].temperature + "C"));

                            net_dev.SubState = Evse_Sub_State.NONE;
                        }

                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
                #endregion
                case Evse_Sub_State.GET_RFID:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
                        split_raw = net_dev.Resp.Split(';');//IGB:3156546487687;0D0A
                        split = split_raw[0].Split(':');
                        dev_list.DevEvse[1].rfid = split[1];
                        //System.Diagnostics.Debug.Print($"rfid:{devList.DevEvse[1].barcode}");

                        Main.main.evse2_params.Invoke((MethodInvoker)(() => Main.main.evse2_params.Text =
                            "RFID: " + dev_list.DevEvse[1].rfid + "\r\n"));

                        net_dev.SubState = Evse_Sub_State.NONE;
                    }
                    else
                    {
                        net_dev.Cmd = Evse_param_type[net_dev.SubState];//test select komandos formavimas
                        Socket_.send_socket(net_dev, net_dev.Cmd);
                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    }
                    break;
            }
        }
        #endregion

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
