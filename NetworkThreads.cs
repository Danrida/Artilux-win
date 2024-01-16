using Ion.Sdk.Idi;
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
using System.Windows.Forms.DataVisualization.Charting;
using static Ion.Sdk.Ici.Channel.BlackBox.Message;
using static Ion.Tools.Models.XmlDataExport.Graph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArtiluxEOL
{
    public partial class NetworkThreads : Component
    {

        UInt16 CMD_RETRANSMIT_L = 3;
        UInt16 CMD_RETRANSMIT_H = 10;

        UInt16 PING_TMR_CNT_VALUE = 20;

        public long unixTimeMilliseconds;
        long Main_Controller_TCP_Handler_Timer;
        long Main_Controller_TCP_Poll_Rate = 200;//(ms) How often should request be sent to the main controller
        int Main_Controller_TCP_Max_Poll_Count = 10;//Maximum ammount of attempts for a single command

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

        public string[] Evse_param_type = new string[] { "READY", "EGD?", "ESD:", "IGB?", "EGW?", "EGL?", "ESA:", "EGM?", "EGR?" };



        public NetworkThreads()
        {
            InitializeComponent();
        }

        SocketClient Socket_ = new SocketClient();

        public DevList dev_list;

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
                foreach (var dev in Main.main.network_dev)//einam per dev lista, kurie enable ieskom tinkle, jei toki radom bandom jungtis
                {

                    if (dev.Enable && !dev.Connected && a < 7) //tikrinam tik jei enable, nuo 7 jau nebe tinklo devaisai - skip.
                    {
                        ret = Socket_.start_socket(Main.main.network_dev[a], 0);

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
                                    Main.main.network_dev[a].ReceiveRunning = false;
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_CONNECTED;
                                    break;
                                case 5:
                                    Main.main.Barcode2.RunWorkerAsync();
                                    Main.main.network_dev[a].ReceiveRunning = false;
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_CONNECTED;
                                    break;
                                case 6:
                                    Main.main.Barcode3.RunWorkerAsync();
                                    Main.main.network_dev[a].ReceiveRunning = false;
                                    Socket_.receive_socket(Main.main.network_dev[a]);
                                    Main.main.network_dev[a].State = Evse_State.EVSE_CONNECTED;
                                    break;
                            }
                        }
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
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                     case NetDev_SendState.RECEIVE_WAIT:
                         break;
                    case NetDev_SendState.RECEIVE_OK:
                        net_dev.NewResp = true;
                        net_dev.ReceiveRunning = false;
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        Socket_.receive_socket(net_dev);
                        //System.Diagnostics.Debug.Print($"MAIN_newCMD:{net_dev.Resp}");
                        //MAIN_Ctrl_handle();
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }
                MAIN_Ctrl_handle();
                Thread.Sleep(100);
            }
            return result;
        }

        public void MAIN_Ctrl_handle()
        {

            var net_dev = Main.main.network_dev[DevType.MAIN_CONTROLLER];
            net_dev.State = MainBoard_State.IDLE;//Main controller idle (unless specified otherwise later in the code)
            string[] sepResps;//Stores the seperate responces from the main board
            string[] split;//Stores the split strings from the main board responces
            int respID = -1;//Stores responce ID
            int nmrcResp = -1;//Stores numeric responce

            if (net_dev.NewResp)//There is a new responce from the main board
            {
                net_dev.NewResp = false;//Reset new responce flag

                if (net_dev.Resp.StartsWith("ESTOP"))
                {
                    Main.E_Stop_Signal.STATE = 1;
                    System.Diagnostics.Debug.Print("STOP");
                }
                else
                {
                    sepResps = net_dev.Resp.Split('\n');//Split the response

                    for (int j = 0; j < (sepResps.Length - 1); j++)
                    {
                        //Commands without command id
                        if (sepResps[j].StartsWith("TEST_START:"))//Test start request
                        {
                            split = sepResps[j].Split(':');//Split the response
                            bool parseFail = false;
                            string posStr = split[1];
                            bool[] reqPos = new bool[3] { false, false, false };//Requested test positions

                            for (int i = 0; i < 3; i++)//Next 3 characters after ":" should be numbers 0 or 1
                            {
                                if (posStr[i] == '1')
                                {
                                    reqPos[i] = true;
                                }
                                else if (posStr[i] != '0')//Its either 1 or 0, otherwise bad command syntax
                                {
                                    parseFail = true;
                                    break;
                                }
                            }

                            if (!parseFail)
                            {
                                if (reqPos[0])
                                {
                                    Main.main.Requesting_Test_Start_I = true;
                                    System.Diagnostics.Debug.Print("Test start request I");
                                }
                                else if (reqPos[1])
                                {
                                    Main.main.Requesting_Test_Start_II = true;
                                    System.Diagnostics.Debug.Print("Test start request II");
                                }
                                else if (reqPos[2])
                                {
                                    Main.main.Requesting_Test_Start_III = true;
                                    System.Diagnostics.Debug.Print("Test start request III");
                                }
                            }
                        }
                        else if (sepResps[j].StartsWith("WORK_POS:?"))//Which work positions are operational?
                        {
                            //Responce: WORK_POS:101 - operational work positions are 1 and 3
                            //Responce: WORK_POS:010 - operational work position 2

                            string extraText = ":";

                            for (int i = 0; i < 3; i++)
                            {
                                if (Main.main.EVSE_Operational[i])
                                {
                                    extraText += "1";
                                }
                                else
                                {
                                    extraText += "0";
                                }
                            }

                            Main.Work_Pos_Signal.EXTRA = extraText;//Assign extra text at the end of the command
                            Main.Work_Pos_Signal.ONE_SHOT = true;//Send this signal one time
                            Main.Work_Pos_Signal.STATE = 1;//Signal needs to be sent

                        }
                        else//Commands with command id
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
                                        if (Main.Main_Board_Controls[i] is BinaryComponent)//Object type relay
                                        {
                                            BinaryComponent tmpRL = (BinaryComponent)Main.Main_Board_Controls[i];

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
                                        else if (Main.Main_Board_Controls[i] is NumericComponent)//Object type NumericComponent
                                        {
                                            NumericComponent tmpNmrc = (NumericComponent)Main.Main_Board_Controls[i];

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
                                        if (Main.Main_Board_Controls[i] is BinaryComponent)//Object type BinaryComponent
                                        {
                                            BinaryComponent tmpRL = (BinaryComponent)Main.Main_Board_Controls[i];

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
                                        else if (Main.Main_Board_Controls[i] is Signal)
                                        {
                                            Signal tmpSG = (Signal)Main.Main_Board_Controls[i];

                                            if (tmpSG.COM_ID == respID)//Found the sender
                                            {
                                                tmpSG.STATE = 1;//ON
                                                break;//Exit for loop
                                            }
                                        }
                                    }
                                }
                                else if (split[0] == "OFF")//State responce OFF (comes from a relay)
                                {
                                    for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                                    {
                                        if (Main.Main_Board_Controls[i] is BinaryComponent)//Object type relay
                                        {
                                            BinaryComponent tmpRL = (BinaryComponent)Main.Main_Board_Controls[i];

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
                                        else if (Main.Main_Board_Controls[i] is Signal)
                                        {
                                            Signal tmpSG = (Signal)Main.Main_Board_Controls[i];

                                            if (tmpSG.COM_ID == respID)//Found the sender
                                            {
                                                tmpSG.STATE = 0;//OFF
                                                break;//Exit for loop
                                            }
                                        }
                                    }
                                }
                                else if (int.TryParse(split[0], out nmrcResp))//State responce in mumeric (0/1/2/3...)
                                {
                                    for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Find who sent this command id
                                    {
                                        if (Main.Main_Board_Controls[i] is NumericComponent)//Object type numeric state device
                                        {
                                            NumericComponent tmpNmrc = (NumericComponent)Main.Main_Board_Controls[i];

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
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print($"Bad response format: {net_dev.Resp}");
                            }
                        }
                    }
                }

                if (Main.E_Stop_Signal.STATE == 0)
                {
                    if (Main.E_Stop_Previous)//Emergency stop reset
                    {
                        Main.main.Emergency_Stop_Reset();
                        Main.E_Stop_Previous = false;
                    }
                }
            }

            DateTimeOffset now = DateTimeOffset.UtcNow;
            unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            if ((unixTimeMilliseconds - Main_Controller_TCP_Handler_Timer) > Main_Controller_TCP_Poll_Rate)
            {
                if (Main.E_Stop_Signal.STATE == 1)
                {
                    if (!Main.E_Stop_Previous)//New emergency stop received
                    {
                        Main.main.Main_Board_Set_Command_ID(Main.E_Stop_Signal);
                        Main.main.Emergency_Stop_Procedure();
                        Main.E_Stop_Previous = true;
                    }

                    net_dev.State = MainBoard_State.BUSY;
                    net_dev.Cmd = Main.E_Stop_Signal.COM_ID + ":" + Main.E_Stop_Signal.NAME + "?";//Is it still emergency stop state?
                    Socket_.send_socket(net_dev, net_dev.Cmd);
                    net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                    System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");

                }
                else
                {
                    for (int i = 0; i < Main.Main_Board_Controls.Length; i++)//Go trough all main controller objects
                    {
                        if (Main.Main_Board_Controls[i] is BinaryComponent)//Object type relay
                        {
                            BinaryComponent tmpRL = (BinaryComponent)Main.Main_Board_Controls[i];

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
                                    break;//Exit loop, next command after Main_Controller_TCP_Poll_Rate
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
                                    break;//Exit loop, next command after Main_Controller_TCP_Poll_Rate
                                }
                                else
                                {
                                    tmpRL.STATE = -2;//Could not get "ON"/"OFF" confirmation
                                }
                            }
                        }
                        else if (Main.Main_Board_Controls[i] is NumericComponent)//Object type NumericStateDevice
                        {
                            NumericComponent tmpNmrc = (NumericComponent)Main.Main_Board_Controls[i];

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
                                    break;//Exit loop, next command after Main_Controller_TCP_Poll_Rate
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
                                    break;//Exit loop, next command after Main_Controller_TCP_Poll_Rate
                                }
                                else
                                {
                                    tmpNmrc.STATE = -101;//Could not get 0/1/2/3... confirmation
                                }
                            }
                        }
                        else if (Main.Main_Board_Controls[i] is Signal)//Object type Signal
                        {
                            Signal tmpSgnl = (Signal)Main.Main_Board_Controls[i];

                            if (tmpSgnl.STATE == 1)
                            {
                                Main.main.Main_Board_Set_Command_ID(tmpSgnl);
                                net_dev.State = MainBoard_State.BUSY;
                                net_dev.Cmd = tmpSgnl.COM_ID + ":" + tmpSgnl.NAME + tmpSgnl.EXTRA;
                                Socket_.send_socket(net_dev, net_dev.Cmd);
                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;

                                if (tmpSgnl != Main.Ping_Signal)//Do not show ping messages
                                {
                                    System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
                                }

                                if (tmpSgnl.ONE_SHOT)
                                {
                                    tmpSgnl.STATE = 0;
                                }
                                break;//Exit loop, next command after Main_Controller_TCP_Poll_Rate
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print($"Unknown object type was found");
                        }
                    }
                }

                Main_Controller_TCP_Handler_Timer = unixTimeMilliseconds;//Reset TCP poll timer
            }

        }

        public void Generate_Test_Pulse(string pulseLength)
        {
            var net_dev = Main.main.network_dev[DevType.MAIN_CONTROLLER];

            net_dev.State = MainBoard_State.BUSY;
            net_dev.Cmd = "123:OSCT:" + pulseLength;
            Socket_.send_socket(net_dev, net_dev.Cmd);
            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
            System.Diagnostics.Debug.Print($"Sending: {net_dev.Cmd}");
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
            //int ptr = 0;
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
                Load_Test_handler();
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
            //int param = 0;

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

        public void Load_Test_handler()
        {
            /*
             * -2: Reset 1/2
             * -1: Reset 2/2
             * 
             * Test sequence:
             * 
             * 0: Idle (not testing)
             * 1: Turn off ITECH load (test reset)
             * 2: Reset test (main controller)
             * 3: Set cable type 32A, set test position
             * 4: Set EV_MODE to B
             * 5: Check lock
             * 6: Set EV_MODE to C
             * 7: Set ITECH load to 0A
             * 8: Set ITECH mode to constant current
             * 9: Turn on EVSE relay, request data
             * 10: Check EVSE voltage (is relay on)
             * 11: Set LOAD / SOURCE
             * 12: LS enable
             * 13: Turn on ITECH load
             * 14: Delay @0A
             * 15: Get ITECH voltage
             * 16: EVSE data request
             * 17: Check EVSE data
             * 
             * 18: Set ITECH load to 5A
             * 19: Small delay @5A
             * 20: EVSE data request
             * 21: Check EVSE data
             * 22: Delay @5A
             * 23: EVSE data request
             * 24: Check EVSE data
             * 
             * 25: Set ITECH load to 16A
             * 26: Small delay @16A
             * 27: EVSE data request
             * 28: Check EVSE data
             * 29: Delay @16A
             * 30: EVSE data request
             * 31: Check EVSE data
             * 
             * 32: Set ITECH load to 32A
             * 33: Small delay @32A
             * 34: EVSE data request
             * 35: Check EVSE data
             * 36: Delay @32A
             * 37: EVSE data request
             * 38: Check EVSE data
             * 
             * 39: Set ITECH load to 34A
             * 40: EVSE data request
             * 41: Check EVSE data
             * 42: Turn off ITECH load
             * 
             * 43: Set ITECH load to 22A
             * 44: Set cable type 20A, set test position
             * 45: Set EV_MODE to A
             * 46: Set EV_MODE to B
             * 47: Check lock
             * 48: Set EV_MODE to C
             * 49: Turn on EVSE relay, request data
             * 50: Check EVSE voltage (is relay on)
             * 51: Turn on ITECH load
             * 52: EVSE data request
             * 53: Check EVSE data
             *
             */

            DateTimeOffset now = DateTimeOffset.UtcNow;
            unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            var net_dev = Main.main.network_dev[DevType.ITECH_LOAD];
            bool respOK = false;
            string[] split;
            float[] volts = new float[3];
            float[] amps = new float[3];
            string[] split_scnd;
            bool stepPass = true;

            var net_evse = Main.main.network_dev[DevType.BARCODE_1];

            if (Main.Load_Test_EVSE_ID == 1)
            {
                net_evse = Main.main.network_dev[DevType.BARCODE_2];
            }
            else if (Main.Load_Test_EVSE_ID == 2)
            {
                net_evse = Main.main.network_dev[DevType.BARCODE_3];
            }
            else
            {
                System.Diagnostics.Debug.Print("Can not perform load test, bad EVSE ID");
                Main.Load_Test_State = 0;//Reset test
            }

            if (Main.Load_Test_State > 0)
            {
                Main.Load_Test_Load_Possibly_ON = true;//It is possible the ITECH load is turned on right now (check when the load test is in Idle state)
            }

            /*
             
            ITECH load responce to MEAS:VOLT? = 0.000000,0.000000,0.000000 <-- measured value 6 decimal places (length > 7)
            ITECH load responce to CURR? = 0.00,0.00,0.00 <-- current setpoint 2 decimal places (length < 7)
             
             */

            /*
             =================================================================================
             =============================== EVSE possibly ON ================================
             ================================== Load beeper ==================================
             ============================== Load test reset -2 ===============================
             =============================== EVSE grazus reset ===============================
             ============ if (volts[0] < 200 || volts[1] < 200 || volts[2] < 200) ============
             =================================================================================
             */

            //"ESA:", "EGM?"   EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

            if (Main.Load_Test_Cancel)
            {
                Main.Load_Test_Failed = false;
                Main.Load_Test_State = -2;
                Main.Load_Test_One_Call = false;
                Main.Load_Test_Cmd_Attempts = 0;
                System.Diagnostics.Debug.Print("ITECH load test canceled");
            }

            Main.main.Update_Progress_Bar_Load_Test();

            if (!Main.Load_Test_Failed)
            {
                switch (Main.Load_Test_State)
                {
                    case -2://Reset 1/2
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.Main_Board_LS_EN(0);
                            Main.main.Main_Board_LOAD(0);
                            Main.main.Main_Board_SOURCE(0);
                            Main.main.CP_Selector_Set(0);
                            Main.main.PP_Selector_Set(0);
                            Main.main.TP_Selector_Set(0);
                            Main.Load_Test_Cancel = false;
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.LS_EN.STATE == 0 && Main.LOAD.STATE == 0 && Main.SOURCE.STATE == 0 && Main.CP_Selector.STATE == 0 && Main.PP_Selector.STATE == 0 && Main.TP_Selector.STATE == 0)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.LS_EN.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset LS_EN");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        else if (Main.LOAD.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset LOAD");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        else if (Main.SOURCE.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset SOURCE");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset EV_MODE");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        else if (Main.PP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset CABLE");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        else if (Main.TP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset TEST POSITION");
                            Main.Load_Test_State = 0;//Reset test
                        }
                        break;

                    case -1://Reset 2/2

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("0"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load turned off");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to turn off ITECH load (test reset)");
                                Main.Load_Test_State = 0;//Reset test
                            }
                        }
                        break;

                    default://0: Idle
                        Main.Load_Test_Cmd_Attempts = 0;
                        Main.Load_Test_One_Call = false;
                        Main.Load_Test_State = 0;
                        Main.Load_Test_Cancel = false;

                        //It is possible the ITECH load is turned on right now- turn off and check
                        if (net_dev.Connected && Main.Load_Test_Load_Possibly_ON && (unixTimeMilliseconds - Main.Load_Test_Timer) > 2000)
                        {
                            respOK = false;

                            if (net_dev.NewResp)
                            {
                                net_dev.NewResp = false;

                                if (net_dev.Resp.StartsWith("0"))
                                {
                                    System.Diagnostics.Debug.Print("ITECH load turned off (load test reset)");
                                    respOK = true;
                                    Main.Load_Test_Load_Possibly_ON = false;
                                    Main.Load_Test_One_Call = false;
                                    net_dev.Resp = "";
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                                }
                            }

                            if (respOK == false)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Timer = unixTimeMilliseconds;
                            }
                        }

                        break;

                    case 1://Turn off ITECH load (test reset)

                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            System.Diagnostics.Debug.Print("================================= Load test start =================================");
                            Main.Load_Test_One_Call = true;
                        }

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("0"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load turned off");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to turn off ITECH load (test reset)");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 2://Reset test (main controller)
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.Main_Board_LS_EN(0);
                            Main.main.Main_Board_LOAD(0);
                            Main.main.Main_Board_SOURCE(0);
                            Main.main.CP_Selector_Set(0);
                            Main.main.PP_Selector_Set(0);
                            Main.main.TP_Selector_Set(0);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.LS_EN.STATE == 0 && Main.LOAD.STATE == 0 && Main.SOURCE.STATE == 0 && Main.CP_Selector.STATE == 0 && Main.PP_Selector.STATE == 0 && Main.TP_Selector.STATE == 0)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.LS_EN.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset LS_EN");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.LOAD.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset LOAD");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.SOURCE.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset SOURCE");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.PP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset CABLE");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.TP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset TEST POSITION");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 3://Set cable type 32A, set test position
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.TP_Selector_Set(Main.Load_Test_EVSE_ID);
                            Main.main.PP_Selector_Set(3);//32A cable
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.TP_Selector.STATE == Main.Load_Test_EVSE_ID && Main.PP_Selector.STATE == 3)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.TP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set TEST POSITION");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.PP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set CABLE (32A)");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 4://Set EV_MODE to B
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.CP_Selector_Set(1);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.CP_Selector.STATE == 1)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 5://Check lock
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 3000)
                        {
                            System.Diagnostics.Debug.Print("Lock: pass");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 6://Set EV_MODE to C
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.CP_Selector_Set(2);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.CP_Selector.STATE == 2)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 7://Set ITECH load to 0A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 0 && amps[1] == 0 && amps[2] == 0)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 0A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 0A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 8://Set ITECH mode to constant current

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            if (net_dev.Resp.StartsWith("CC"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load function set ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "FUNC CC";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "FUNC?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load mode");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 9://Turn on EVSE relay, request data

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "ESA:1";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 10://Check EVSE voltage (is relay on)

                        if (net_evse.NewResp)
                        {
                            stepPass = true;
                            respOK = false;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            respOK = true;
                                            if (volts[0] < 200 || volts[1] < 200 || volts[2] < 200)
                                            {
                                                stepPass = false;
                                            }
                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass == true)
                                    {
                                        System.Diagnostics.Debug.Print("EVSE relay is ON");
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to turn on EVSE relay");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 11://Set LOAD / SOURCE
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.LS_Selector_Set((Main.Load_Test_EVSE_ID + 1));
                            Main.main.Main_Board_LOAD(1);
                            Main.main.Main_Board_SOURCE(1);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.LS_Selector.STATE == (Main.Load_Test_EVSE_ID + 1) && Main.LOAD.STATE == 1 && Main.SOURCE.STATE == 1)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.LS_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set LS SELECTOR");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.LOAD.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset LOAD");
                            Main.Load_Test_Failed = true;
                        }
                        else if (Main.SOURCE.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to reset SOURCE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 12://LS enable
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.Main_Board_LS_EN(1);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.LS_EN.STATE == 1)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.LS_EN.STATE < 0)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set LS_EN");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 13://Turn on ITECH load

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load turned on");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP ON";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to turn on ITECH load");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 14://Delay @0A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 2000)
                        {
                            System.Diagnostics.Debug.Print("Delay @0A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 15://Get ITECH voltage

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && split[0].Length > 7 && split[1].Length > 7 && split[2].Length > 7 && float.TryParse(split[0], out volts[0]) && float.TryParse(split[1], out volts[1]) && float.TryParse(split[2], out volts[2]))
                            {
                                Main.Load_Test_Measured_Voltage_Load = volts;
                                System.Diagnostics.Debug.Print("ITECH load voltage received: " + Main.Load_Test_Measured_Voltage_Load[0] + "V  " + Main.Load_Test_Measured_Voltage_Load[1] + "V  " + Main.Load_Test_Measured_Voltage_Load[2] + "V");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MEAS:VOLT?";//Measure voltage
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: ITECH load could not measure voltage");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 16://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 17://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 0A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    //5A

                    case 18://Set ITECH load to 5A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 5 && amps[1] == 5 && amps[2] == 5)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 5A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 5";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 5A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 19://Small delay @5A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 2000)
                        {
                            System.Diagnostics.Debug.Print("Small delay @5A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 20://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 21://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 5A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 22://Delay @5A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 5000)
                        {
                            System.Diagnostics.Debug.Print("Delay @5A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 23://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 24://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 5A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    //16A

                    case 25://Set ITECH load to 16A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 16 && amps[1] == 16 && amps[2] == 16)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 16A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 16";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 16A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 26://Small delay @16A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 2000)
                        {
                            System.Diagnostics.Debug.Print("Small delay @16A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 27://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 28://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 16A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 29://Delay @16A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 5000)
                        {
                            System.Diagnostics.Debug.Print("Delay @16A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 30://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 31://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 16A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    //32A

                    case 32://Set ITECH load to 32A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 32 && amps[1] == 32 && amps[2] == 32)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 32A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 32";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 32A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 33://Small delay @32A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 2000)
                        {
                            System.Diagnostics.Debug.Print("Small delay @32A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 34://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 35://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 32A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 36://Delay @32A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 5000)
                        {
                            System.Diagnostics.Debug.Print("Delay @32A");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 37://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 38://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "L= " + Main.Load_Test_Measured_Voltage_Load[i] + "V, U" + (i + 1) + "E= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V, Diff= " + Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Voltage_Load[i] - Main.Load_Test_Measured_Voltage_EVSE[i]) > Main.Load_Test_Max_Voltage_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                split_scnd = split[4].Split(':');

                                if (float.TryParse(split_scnd[1], out amps[0]))
                                {
                                    split_scnd = split[5].Split(':');
                                    if (float.TryParse(split_scnd[1], out amps[1]))
                                    {
                                        split_scnd = split[6].Split(':');
                                        if (float.TryParse(split_scnd[1], out amps[2]))
                                        {
                                            respOK = true;
                                            Main.Load_Test_Measured_Current_EVSE = amps;

                                            for (int i = 0; i < Main.Load_Test_Measured_Current_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("I" + (i + 1) + "L= " + Main.Load_Test_Measured_Current_Load[i] + "A, I" + (i + 1) + "E= " + Main.Load_Test_Measured_Current_EVSE[i] + "A, Diff= " + Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]));
                                                if (Math.Abs(Main.Load_Test_Measured_Current_Load[i] - Main.Load_Test_Measured_Current_EVSE[i]) > Main.Load_Test_Max_Current_Diff)
                                                {
                                                    stepPass = false;
                                                }
                                            }

                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Measurements difference (at 32A) between EVSE " + Main.Load_Test_EVSE_ID + " and ITECH load is too big, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    //34A cutoff

                    case 39://Set ITECH load to 34A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 34 && amps[1] == 34 && amps[2] == 34)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 34A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 34";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 34A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 40://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 41://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;
                                            respOK = true;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V");

                                                if (Main.Load_Test_Measured_Voltage_EVSE[i] > Main.Load_Test_EVSE_OFF_Voltage_Max)
                                                {
                                                    stepPass = false;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";

                                        //System.Diagnostics.Debug.Print("All good- reset");
                                        //Main.Load_Test_State = -2;
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("EVSE " + Main.Load_Test_EVSE_ID + " did not cut off at 34A, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 42://Turn off ITECH load

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("0"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load turned off");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to turn off ITECH load");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    //22A cutoff

                    case 43://Set ITECH load to 22A

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length >= 3 && float.TryParse(split[0], out amps[0]) && float.TryParse(split[1], out amps[1]) && float.TryParse(split[2], out amps[2]) && amps[0] == 22 && amps[1] == 22 && amps[2] == 22)
                            {
                                Main.Load_Test_Measured_Current_Load = amps;
                                System.Diagnostics.Debug.Print("ITECH load current set to 22A ok");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CURR 22";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CURR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set ITECH load current to 22A");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 44://Set cable type 20A, set test position
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.PP_Selector_Set(2);//20A cable
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.PP_Selector.STATE == 2)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.PP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set CABLE (20A)");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 45://Set EV_MODE to A
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.CP_Selector_Set(1);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.CP_Selector.STATE == 0)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 46://Set EV_MODE to B
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.CP_Selector_Set(1);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.CP_Selector.STATE == 1)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 47://Check lock
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.Load_Test_Timer = unixTimeMilliseconds;
                            Main.Load_Test_One_Call = true;
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timer) > 3000)
                        {
                            System.Diagnostics.Debug.Print("Lock: pass");
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        break;

                    case 48://Set EV_MODE to C
                        if (!Main.Load_Test_One_Call)//Call these commands only one time
                        {
                            Main.main.CP_Selector_Set(2);
                            Main.Load_Test_One_Call = true;
                        }

                        if (Main.CP_Selector.STATE == 2)
                        {
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                            Main.Load_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else if (Main.CP_Selector.STATE <= -100)
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to set EV_MODE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 49://Turn on EVSE relay, request data

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "ESA:1";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }
                        break;

                    case 50://Check EVSE voltage (is relay on)

                        if (net_evse.NewResp)
                        {
                            stepPass = true;
                            respOK = false;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            respOK = true;
                                            if (volts[0] < 200 || volts[1] < 200 || volts[2] < 200)
                                            {
                                                stepPass = false;
                                            }
                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass == true)
                                    {
                                        System.Diagnostics.Debug.Print("EVSE relay is ON");
                                        Main.Load_Test_State++;
                                        Main.Load_Test_One_Call = false;
                                        Main.Load_Test_Cmd_Attempts = 0;
                                        net_evse.Resp = "";
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to turn on EVSE relay");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                    case 51://Turn on ITECH load

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1"))
                            {
                                System.Diagnostics.Debug.Print("ITECH load turned on");
                                respOK = true;
                                Main.Load_Test_State++;
                                Main.Load_Test_One_Call = false;
                                Main.Load_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong ITECH load responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "INP ON";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "INP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Load_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: failed to turn off ITECH load");
                                Main.Load_Test_Failed = true;
                            }
                        }
                        break;

                    case 52://EVSE data request

                        if (Main.Load_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_evse.Resp = "";

                            net_evse.Cmd = "EGM?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_evse.Cmd}");
                            Socket_.send_socket(net_evse, net_evse.Cmd);

                            net_evse.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Load_Test_Cmd_Attempts++;

                            Main.Load_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Load_Test_State++;
                            Main.Load_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Load test failed on EVSE " + Main.Load_Test_EVSE_ID + ", reason: no responce from EVSE");
                            Main.Load_Test_Failed = true;
                        }

                        break;

                    case 53://Check EVSE data

                        if (net_evse.NewResp)
                        {
                            respOK = false;
                            stepPass = true;
                            net_evse.NewResp = false;
                            split = net_evse.Resp.Split(';');

                            //EGM:1;U1:26;U2:16;U3:15;I1:0;I2:0;I3:0;P:0;E:488;F:49;T:36;

                            if (split.Length == 12 && net_evse.Resp.StartsWith("EGM:1"))
                            {
                                split_scnd = split[1].Split(':');

                                if (float.TryParse(split_scnd[1], out volts[0]))
                                {
                                    split_scnd = split[2].Split(':');
                                    if (float.TryParse(split_scnd[1], out volts[1]))
                                    {
                                        split_scnd = split[3].Split(':');
                                        if (float.TryParse(split_scnd[1], out volts[2]))
                                        {
                                            Main.Load_Test_Measured_Voltage_EVSE = volts;
                                            respOK = true;

                                            for (int i = 0; i < Main.Load_Test_Measured_Voltage_EVSE.Length; i++)
                                            {
                                                System.Diagnostics.Debug.Print("U" + (i + 1) + "= " + Main.Load_Test_Measured_Voltage_EVSE[i] + "V");

                                                if (Main.Load_Test_Measured_Voltage_EVSE[i] > Main.Load_Test_EVSE_OFF_Voltage_Max)
                                                {
                                                    stepPass = false;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (respOK)
                                {
                                    if (stepPass)
                                    {
                                        System.Diagnostics.Debug.Print("All good- reset");
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("EVSE " + Main.Load_Test_EVSE_ID + " did not cut off at 22A, test failed");
                                        Main.Load_Test_Failed = true;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Could not parse EVSE responce: " + net_evse.Resp);
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong EVSE responce: " + net_evse.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Load_Test_Timeout_Timer) > Main.Load_Test_EVSE_Responce_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Trying again to get EVSE data");
                            Main.Load_Test_State--;
                            Main.Load_Test_One_Call = false;
                        }

                        break;

                }
            }
        }

        #endregion

        public float str_to_float(string str)
        {
            string[] split;
            split = str.Split('E');

            float param_float;
            //float.TryParse(split[0].Replace('.', ','), out param_float);
            float.TryParse(split[0], out param_float);
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
                System.Diagnostics.Debug.Print($"str: = {str}");
                float pwr_param_float;
                //float.TryParse(str.Replace('.', ','), out pwr_param_float);
                float.TryParse(str, out pwr_param_float);
                System.Diagnostics.Debug.Print($"f: = {pwr_param_float}");
                float float_tmp = (float)Math.Floor(pwr_param_float * 100f) / 100f;

                str = Convert.ToString(float_tmp);
                System.Diagnostics.Debug.Print($"p: = {str}");
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
            //int ptr = 0;

            var net_dev = Main.main.network_dev[DevType.ANALYSER_SIGLENT];

            var main_func = Main.main;

            while (!connection_closed)
            {
                switch (net_dev.SendReceiveState)
                {
                    case NetDev_SendState.IDLE:
                        break;
                    case NetDev_SendState.SEND_BEGIN:
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
                                //Spectroscope_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.SEND_FAIL:
                        net_dev.SendReceiveState = NetDev_SendState.IDLE;
                        System.Diagnostics.Debug.Print($"SEND_FAIL:{0}");
                        break;
                    case NetDev_SendState.RECEIVE_WAIT:
                        if(net_dev.CmdRetransmitCnt > CMD_RETRANSMIT_L)//negavom responso, persiunciam viska dar karta
                        {
                            net_dev.CmdRetransmitCnt = 0;
                            net_dev.GetSetParamLeft = net_dev.GetSetParamCount;
                            Main.main.dbg_print(DbgType.MAIN, "RETRANSMIT_CMD: Siglent" , Color.DarkRed);
                            Spectroscope_handle_get_params();
                        }

                        net_dev.CmdRetransmitCnt++;
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
                                    Spectroscope_handle_get_params();
                                }
                                break;
                            case NetDev_State.START_TEST:
                                if (net_dev.SubState == NetDev_Test.GET_CHART_DATA)
                                {
                                    net_dev.SubState = NetDev_Test.PROCESS_CHART_DATA;
                                    Spectroscope_handle_get_params();
                                }
                                break;
                            case NetDev_State.SET_PARAM:
                                net_dev.GetSetParamLeft--;
                                Spectroscope_handle_get_params();
                                break;
                        }
                        break;
                    case NetDev_SendState.RECEIVE_FAIL:
                        System.Diagnostics.Debug.Print($"RECEIVE_FAIL:{0}");
                        break;
                }

                Spectroscope_Test_handler();
                Thread.Sleep(100);
            }
            return result;
        }

        public void Spectroscope_handle_get_params()
        {
            var net_dev = Main.main.network_dev[DevType.ANALYSER_SIGLENT];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get.
            float start;
            float stop;

            DataGridViewRow Row;
            int Row_cnt = Main.main.dataGrid_Spectrum.Rows.Count;//get table row by test type

            switch (net_dev.State)
            {
                case NetDev_State.READY:
                    if (net_dev.NewResp)
                    {
                        net_dev.NewResp = false;
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

        public void Spectroscope_Test_handler()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            if (Main.Spectroscope_Test_Cancel)
            {
                Main.Spectroscope_Test_Failed = false;
                Main.Spectroscope_Test_State = -1;
                Main.Spectroscope_Test_One_Call = false;
                Main.Spectroscope_Test_Cmd_Attempts = 0;
                System.Diagnostics.Debug.Print("Spectroscope test canceled");
            }

            /*
             * Test sequence:
             * 
             * -1: Reset
             * 
             * 0: Idle
             * 
             * 1: Set trace mode to write
             * 2: Read marker readout
             * 3: Move marker to the peak
             * 4: Read marker readout (check if moved)
             * 5: Set start frequency
             * 6: Set stop frequency
             * 7: Set trace mode to max hold
             * 8: Set input attenuator
             * 9: Turn on preamp
             * 10: Set detection type to positive
             * 11: Set peak search type
             * 12: Set peak threshold
             * 13: Set peak excursion
             * 14: Disable buzzer
             * 15: Enable peak table
             * 
             * 16: Set retry count for the test
             * 17: Request spectroscope data
             * 18: Wait for spectroscope data
             * 19: Analyze data
             * 
             * 20: Switch to view mode
             * 21: Read marker readout
             * 22: Move marker to the peak
             * 23: Read marker readout (check if moved)
             * 24: Add first (main) peak
             * 25: Set retry count for moving the marker
             * 26: Move marker to the next peak
             * 27: Read marker readout (check if moved)
             * 28: Display peaks
             * 
             */

            //751

            if (!Main.Spectroscope_Test_Failed && (unixTimeMilliseconds - Main.Spectroscope_Test_Step_Timer) > Main.Spectroscope_Test_Step_Delay)
            {
                var net_dev = Main.main.network_dev[DevType.ANALYSER_SIGLENT];
                bool respOK = false;
                string[] split;
                float freqParse = 0;
                int cmdAttStart;
                float peakSampleNumber = 0;

                Main.main.Update_Progress_Bar_Spectroscope_Test();

                switch (Main.Spectroscope_Test_State)
                {
                    case -1://Reset

                        Main.Spectroscope_Test_Cancel = false;

                        for (int i = 0; i < Main.Spectroscope_Readings_Old.Length; i++)
                        {
                            Main.Spectroscope_Readings_Old[i] = 0;
                        }

                        Main.Spectroscope_Stable_Samples_Confirmed = 0;
                        Main.main.Spectroscope_Graph_Reset();

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("WRIT"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope trace mode set to Write");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "TRAC:MODE WRIT";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "TRAC:MODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test reset failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set trace mode on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }

                        break;

                    default://Idle

                        Main.Spectroscope_Test_Cmd_Attempts = 0;
                        Main.Spectroscope_Test_One_Call = false;
                        Main.Spectroscope_Test_State = 0;
                        Main.Spectroscope_Test_Cancel = false;

                        break;

                    case 1://Set trace mode to write

                        Main.main.Spectroscope_Graph_Reset();

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("WRIT"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope trace mode set to Write");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "TRAC:MODE WRIT";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "TRAC:MODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set trace mode on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 2://Read marker readout
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out Main.Spectroscope_Marker_Value_Previous))
                            {
                                System.Diagnostics.Debug.Print("Marker value red");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK1:X?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not read marker value");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 3://Move marker to the peak

                        if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_dev.Resp = "";

                            net_dev.Cmd = "CALC:MARK1:MAX";
                            System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                            Socket_.send_socket(net_dev, net_dev.Cmd);

                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Spectroscope_Test_Cmd_Attempts++;

                            Main.Spectroscope_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Spectroscope_Test_State++;
                            Main.Spectroscope_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not move marker to the peak");
                            Main.Spectroscope_Test_Failed = true;
                        }
                        break;

                    case 4://Read marker readout (check if moved)
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out Main.Spectroscope_Marker_Value_Current))
                            {

                                if (Main.Spectroscope_Marker_Value_Current != Main.Spectroscope_Marker_Value_Previous)//Check if readout changed- means marker was moved
                                {
                                    System.Diagnostics.Debug.Print("Marker moved");
                                    Main.Spectroscope_Marker_Value_Previous = Main.Spectroscope_Marker_Value_Current;
                                    respOK = true;
                                    Main.Spectroscope_Test_State++;
                                    Main.Spectroscope_Test_One_Call = false;
                                    Main.Spectroscope_Test_Cmd_Attempts = 0;
                                    net_dev.Resp = "";
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Marker has not moved, retry");
                                    Main.Spectroscope_Test_State--;
                                    Main.Spectroscope_Test_One_Call = false;
                                    net_dev.Resp = "";
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK1:X?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not read marker value");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 5://Set start frequency
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out freqParse) && freqParse == Main.Spectroscope_Frequency_Start)
                            {
                                System.Diagnostics.Debug.Print("Spectroscope start frequency set to " + Main.Spectroscope_Frequency_Start + "Hz");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "FREQ:START " + Main.Spectroscope_Frequency_Start;
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "FREQ:START?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set start frequency on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 6://Set stop frequency
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out freqParse) && freqParse == Main.Spectroscope_Frequency_Stop)
                            {
                                System.Diagnostics.Debug.Print("Spectroscope start frequency set to " + Main.Spectroscope_Frequency_Stop + "Hz");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "FREQ:STOP " + Main.Spectroscope_Frequency_Stop;
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "FREQ:STOP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set stop frequency on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 7://Set trace mode to max hold
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("MAXH"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope trace mode set to Max hold");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "TRAC:MODE MAXH";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "TRAC:MODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set trace mode on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 8://Set input attenuator
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1.500000000E+01"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope input attenuator set to 15dB");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "POW:ATT 15";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "POW:ATT?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set input attenuator on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 9://Turn on preamp
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope preamp enabled");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "POW:GAIN ON";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "POW:GAIN?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to enable preamp on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 10://Set detection type to positive
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("POS"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope detection type set to positive");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "DET:TRAC POS";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "DET:TRAC?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set detection type on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 11://Set peak search type
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("MAX"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope peak search type set to MAX");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK:PEAK:SEAR:MODE MAX";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CALC:MARK:PEAK:SEAR:MODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set peak search type on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 12://Set peak threshold
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("-7.000000000E+01"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope peak threshold set to -70 dBm");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK:PEAK:THR -70";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CALC:MARK:PEAK:THR?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set peak threshold on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 13://Set peak excursion
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1.500000000E+01"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope peak excursion set to 15 dB");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK:PEAK:EXC 15";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CALC:MARK:PEAK:EXC?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: failed to set peak excursion on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 14://Disable buzzer
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("0"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope buzzer disabled");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:LLIN:CONT:BEEP OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CALC:LLIN:CONT:BEEP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not disable buzzer on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 15://Enable peak table
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope peak table enabled");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK:PEAK:TABL ON";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "CALC:MARK:PEAK:TABL?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not enable peak table on the spectroscope");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    //Test start

                    case 16://Set retry count for the test

                        if (Main.Spectroscope_Test_Retry_Count >= Main.Max_TCP_Cmd_Attempts)//Set Main.Spectroscope_Test_Cmd_Attempts value so there is only Main.Spectroscope_Test_Retry_Count amount of retries left in it
                        {
                            cmdAttStart = 0;
                        }
                        else
                        {
                            cmdAttStart = Main.Max_TCP_Cmd_Attempts - Main.Spectroscope_Test_Retry_Count;
                        }

                        System.Diagnostics.Debug.Print("Spectroscope test retry count set");
                        respOK = true;
                        Main.Spectroscope_Test_State++;
                        Main.Spectroscope_Test_One_Call = false;
                        Main.Spectroscope_Test_Cmd_Attempts = cmdAttStart;
                        net_dev.Resp = "";

                        break;

                    case 17://Request spectroscope data

                        if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_dev.Resp = "";

                            net_dev.Cmd = "TRAC:DATA?";
                            System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                            Socket_.send_socket(net_dev, net_dev.Cmd);

                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Spectroscope_Test_Cmd_Attempts++;

                            Main.Spectroscope_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Spectroscope_Test_State++;
                            Main.Spectroscope_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not receive data from the spectroscope");
                            Main.Spectroscope_Test_Failed = true;
                        }
                        break;

                    case 18://Wait for spectroscope data

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;
                            split = net_dev.Resp.Split(',');

                            if (split.Length == Main.Spectroscope_Readings_Count)
                            {
                                respOK = true;

                                for (int i = 0; i < Main.Spectroscope_Readings_Count; i++)
                                {
                                    if (!float.TryParse(split[i], out Main.Spectroscope_Readings[i]))
                                    {
                                        respOK = false;
                                    }
                                }

                                if (respOK)
                                {
                                    System.Diagnostics.Debug.Print("Spectroscope data received");
                                    Main.Spectroscope_Test_State++;
                                    Main.Spectroscope_Test_One_Call = false;
                                    Main.Spectroscope_Test_Cmd_Attempts = 0;
                                    net_dev.Resp = "";
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Bad spectroscope responce");
                            }
                        }

                        if ((unixTimeMilliseconds - Main.Spectroscope_Test_Timeout_Timer) > Main.Spectroscope_Test_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Retry spectroscope test");
                            respOK = true;
                            Main.Spectroscope_Test_State--;
                            Main.Spectroscope_Test_One_Call = false;
                        }

                        break;

                    case 19://Analyze data
                        System.Diagnostics.Debug.Print("Analyze spectroscope data");
                        Main.main.Spectroscope_Analyze_Data();

                        if (Main.Spectroscope_Stable_Samples_Confirmed >= Main.Spectroscope_Stable_Samples_Ammount)
                        {
                            System.Diagnostics.Debug.Print("Spectroscope graph stablized");
                            respOK = true;
                            Main.Spectroscope_Test_State++;
                            Main.Spectroscope_Test_One_Call = false;
                            Main.Spectroscope_Test_Cmd_Attempts = 0;
                            net_dev.Resp = "";
                        }
                        else
                        {
                            //Thread.Sleep(Main.Spectroscope_Sample_Request_Delay);
                            Main.Spectroscope_Test_State -= 3;//Jump 3 steps back to test start
                        }

                        break;

                    //Test complete

                    case 20://Switch to view mode
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("VIEW"))
                            {
                                System.Diagnostics.Debug.Print("Spectroscope switched to view mode");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "TRAC:MODE VIEW";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "TRAC:MODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not switch spectroscope to view mode");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 21://Read marker readout
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out Main.Spectroscope_Marker_Value_Previous))
                            {
                                System.Diagnostics.Debug.Print("Marker value red");
                                respOK = true;
                                Main.Spectroscope_Test_State++;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK1:X?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not read marker value");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 22://Move marker to the peak

                        if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_dev.Resp = "";

                            net_dev.Cmd = "CALC:MARK1:MAX";
                            System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                            Socket_.send_socket(net_dev, net_dev.Cmd);

                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.Spectroscope_Test_Cmd_Attempts++;

                            Main.Spectroscope_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.Spectroscope_Test_State++;
                            Main.Spectroscope_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not move marker to the peak");
                            Main.Spectroscope_Test_Failed = true;
                        }
                        break;

                    case 23://Read marker readout (check if moved)
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out Main.Spectroscope_Marker_Value_Current))
                            {
                                if (Main.Spectroscope_Marker_Value_Current != Main.Spectroscope_Marker_Value_Previous)//Check if readout changed- means marker was moved
                                {
                                    System.Diagnostics.Debug.Print("Marker moved");
                                    Main.Spectroscope_Marker_Value_Previous = Main.Spectroscope_Marker_Value_Current;
                                    respOK = true;
                                    Main.Spectroscope_Test_State++;
                                    Main.Spectroscope_Test_One_Call = false;
                                    Main.Spectroscope_Test_Cmd_Attempts = 0;
                                    net_dev.Resp = "";
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("Marker has not moved, retry");
                                    Main.Spectroscope_Test_State--;
                                    Main.Spectroscope_Test_One_Call = false;
                                    net_dev.Resp = "";
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK1:X?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not read marker value");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 24://Add first (main) peak

                        peakSampleNumber = (Main.Spectroscope_Marker_Value_Current - Main.Spectroscope_Frequency_Start) / (Main.Spectroscope_Frequency_Stop - Main.Spectroscope_Frequency_Start) * Main.Spectroscope_Readings_Count;
                        Main.Spectroscope_Peaks = new SpectumPoint[1];
                        Main.Spectroscope_Peaks[0] = new SpectumPoint();
                        Main.Spectroscope_Peaks[0].SAMPLE = (int)Math.Round(peakSampleNumber);
                        Main.Spectroscope_Peaks[0].FREQUENCY = Main.Spectroscope_Marker_Value_Current;

                        Main.Spectroscope_Test_State++;
                        Main.Spectroscope_Test_One_Call = false;
                        Main.Spectroscope_Test_Cmd_Attempts = 0;
                        net_dev.Resp = "";

                        break;

                    case 25://Set retry count for moving the marker

                        Main.Spectroscope_Marker_Move_Attempts = 0;
                        respOK = true;
                        Main.Spectroscope_Test_State++;
                        Main.Spectroscope_Test_One_Call = false;
                        net_dev.Resp = "";
                        System.Diagnostics.Debug.Print("Spectroscope marker move retry count set");

                        break;

                    case 26://Move marker to the next peak

                        net_dev.Resp = "";

                        net_dev.Cmd = "CALC:MARK1:MAX:NEXT";
                        System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                        Socket_.send_socket(net_dev, net_dev.Cmd);

                        net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                        Main.Spectroscope_Marker_Move_Attempts++;
                        respOK = true;
                        Main.Spectroscope_Test_State++;
                        Main.Spectroscope_Test_One_Call = false;

                        break;

                    case 27://Read marker readout (check if moved)
                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (float.TryParse(net_dev.Resp, out Main.Spectroscope_Marker_Value_Current))
                            {
                                respOK = true;
                                Main.Spectroscope_Test_One_Call = false;
                                Main.Spectroscope_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";

                                if (Main.Spectroscope_Marker_Value_Current != Main.Spectroscope_Marker_Value_Previous)//Check if readout changed- means marker was moved
                                {
                                    System.Diagnostics.Debug.Print("Another peak found");

                                    SpectumPoint[] tmpArr = new SpectumPoint[(Main.Spectroscope_Peaks.Length + 1)];
                                    tmpArr[Main.Spectroscope_Peaks.Length] = new SpectumPoint();

                                    for (int i = 0; i < Main.Spectroscope_Peaks.Length; i++)
                                    {
                                        tmpArr[i] = Main.Spectroscope_Peaks[i];
                                    }

                                    peakSampleNumber = (Main.Spectroscope_Marker_Value_Current - Main.Spectroscope_Frequency_Start) / (Main.Spectroscope_Frequency_Stop - Main.Spectroscope_Frequency_Start) * Main.Spectroscope_Readings_Count;
                                    tmpArr[Main.Spectroscope_Peaks.Length].SAMPLE = (int)Math.Round(peakSampleNumber);
                                    tmpArr[Main.Spectroscope_Peaks.Length].FREQUENCY = Main.Spectroscope_Marker_Value_Current;
                                    Main.Spectroscope_Peaks = tmpArr;

                                    Main.Spectroscope_Marker_Move_Attempts = 0;
                                    Main.Spectroscope_Marker_Value_Previous = Main.Spectroscope_Marker_Value_Current;
                                    Main.Spectroscope_Test_State -= 2;//Jump to reseting the marker move attempts counter, continue serching for peaks
                                }
                                else
                                {
                                    if (Main.Spectroscope_Marker_Move_Attempts < Main.Spectroscope_Peak_Search_Retry_Count)
                                    {
                                        System.Diagnostics.Debug.Print("Marker has not moved, retry");
                                        Main.Spectroscope_Test_State--;//Send another marker move command
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.Print("Marker position is not changing, moving on");
                                        Main.Spectroscope_Test_State++;
                                    }
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong spectroscope responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.Spectroscope_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "CALC:MARK1:X?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.Spectroscope_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Spectroscope test failed on EVSE " + Main.Spectroscope_Test_EVSE_ID + ", reason: could not read marker value");
                                Main.Spectroscope_Test_Failed = true;
                            }
                        }
                        break;

                    case 28://Display peaks
                        System.Diagnostics.Debug.Print("Done");
                        Main.main.Spectroscope_Graph_Add_Peaks();

                        Main.Spectroscope_Test_State++;
                        Main.Spectroscope_Test_One_Call = false;
                        Main.Spectroscope_Test_Cmd_Attempts = 0;
                        net_dev.Resp = "";
                        break;
                }

                Main.Spectroscope_Test_Step_Timer = unixTimeMilliseconds;
            }
        }

        #endregion

        #region HVGEN_GW_INSTEK_HANDLER
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
            //int ptr = 0;

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
                    case NetDev_SendState.RECEIVE_WAIT:
                         break;
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

                GW_HV_Test_handler();
                Thread.Sleep(100);
            }
            return result;
        }

        public void GW_HV_handle_get_params()
        {
            var net_dev = Main.main.network_dev[DevType.GWINSTEK_HV_TESTER];
            int test_type = net_dev.TestType;
            int param_type_nr = net_dev.GetSetParamLeft; //parametro nr kuri norim set/get. Itech_hv_test_list[]

            //int param = 0;

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

        public void GW_HV_Test_handler()
        {

            /*
             * 192.168.15.12:12312
             *
             * ACW - AC Withstand mode
             * DCW - DC Withstand mode
             * IR - Insulation Resistance mode
             * GB - Ground Bond mode
             * CONT - Continuity mode
             *
             *
             * Test sequence:
             *
             *
             * 0: Idle
             * 
             * 1: Set test number to 0 and check
             * 2: Set test 000 ACW HI to 1mA and check
             * 3: Set test 000 ACW LO to 0mA and check
             * 4: Set test 000 ACW time to 3s and check
             * 5: Set test 000 ACW ramp time to 0.1s and check
             * 6: Set test 000 ACW arc function to OFF and check
             * 7: Set test 000 ACW arc current to 1mA and check
             * 8: Set test 000 ACW arc speed to NORMAL and check
             * 9: Set test 000 ACW frequency to 50Hz and check
             * 10: Set test 000 ACW wait time to 0s and check
             * 11: Set test 000 ACW ramp down time to 0s and check
             * 12: Set test 000 ACW ground mode to ON and check
             * 13: Set test 000 ACW max hold to OFF and check
             * 14: Set test 000 ACW pass hold to 0.5s and check
             * 15: Set test 000 ACW ref value to 0mA and check
             * 16: Set test 000 ACW initial voltage to 0% and check
             * 17: Enable test return and check
             * 
             * 18: Set retry count for the test
             * 19: Start 000 ACW test
             * 20: Wait for test 000 ACW to finish
             * 21: Get 000 ACW test results
             * 22: Stop 000 ACW test
             * 
             * 23: Set test number to 1 and check
             * 24: Set test 001 GB HI to 100mOhm and check
             * 25: Set test 001 GB Lo to 0mOhm and check
             * 26: Set test 001 GB Test time to 0.3s and check
             * 27: Set test 001 GB Frequency to 60Hz and check
             * 28: Set test 001 GB Contact to 0s and check
             * 29: Set test 001 GB Ground mode to OFF and check
             * 30: Set test 001 GB Max hold to OFF and check
             * 31: Set test 001 GB Pass hold to OFF and check
             * 32: Set test 001 GB reference value to 0mOhm and check
             * 33: Set test 001 GB zero check to OFF and check
             * 
             * 34: Set retry count for the test
             * 35: Start 001 GB Test
             * 36: Wait for test 001 GB to finish
             * 37: Get 001 GB test results
             * 38: Stop 001 GB test
             * 39: Test complete, reset
             *
             */

            DateTimeOffset now = DateTimeOffset.UtcNow;
            unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

            if (Main.HV_Test_Cancel)
            {
                Main.HV_Test_Failed = false;
                Main.HV_Test_State = 0;
                Main.HV_Test_One_Call = false;
                Main.HV_Test_Cmd_Attempts = 0;
                System.Diagnostics.Debug.Print("GW_HV test canceled");
            }

            if (!Main.HV_Test_Failed && (unixTimeMilliseconds - Main.HV_Test_Step_Timer) > Main.HV_Test_Step_Delay)
            {
                var net_dev = Main.main.network_dev[DevType.GWINSTEK_HV_TESTER];
                bool respOK = false;

                Main.main.Update_Progress_Bar_HV_Test();

                switch (Main.HV_Test_State)
                {
                    default://Idle

                        Main.HV_Test_Cmd_Attempts = 0;
                        Main.HV_Test_One_Call = false;
                        Main.HV_Test_State = 0;
                        Main.HV_Test_Cancel = false;

                        break;

                    //ACW setup

                    case 1://Set test number to 0 and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test number set to 0");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:STEP 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:STEP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set test number to 0 on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 2://Set test 000 ACW HI to 1mA and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1.000mA"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW HI set to 1mA");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:CHIS 1";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:CHIS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW HI on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 3://Set test 000 ACW LO to 0mA and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000uA"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW LO set to 0mA");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:CLOS 1";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:CLOS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW LO on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 4://Set test 000 ACW time to 3s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("003."))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW time set to 3s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:TTIM 3";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:TTIM?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW time on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 5://Set test 000 ACW ramp time to 0.1s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.1 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW ramp time set to 0.1s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:RTIM 0.1";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:RTIM?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW ramp time on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 6://Set test 000 ACW arc function to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("ARC OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW arc function set to OFF");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:ARCF OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:ARCF?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW arc function on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 7://Set test 000 ACW arc current to 1mA and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("1.000mA"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW arc current set to 1mA");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:ARCC 1";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:ARCC?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW arc current on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 8://Set test 000 ACW arc speed to NORMAL and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("NORMAL"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW arc speed set to NORMAL");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:ARCS NORMAL";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:ARCS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW arc speed on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 9://Set test 000 ACW frequency to 50Hz and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("50 Hz"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW frequency set to 50Hz");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:FREQ 50";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:FREQ?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW frequency on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 10://Set test 000 ACW wait time to 0s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.0 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW wait time set to 0s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:WAIT 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:WAIT?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW wait time on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 11://Set test 000 ACW ramp down time to 0s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.0 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW ramp down time set to 0s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:RAMP 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:RAMP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW ramp down time on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 12://Set test 000 ACW ground mode to ON and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("ON"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW ground mode set to ON");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:GROUNDMODE ON";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:GROUNDMODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW ground mode on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 13://Set test 000 ACW max hold to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW max hold set to OFF");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:MAXH OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:MAXH?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW max hold on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 14://Set test 000 ACW pass hold to 0.5s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.5 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW pass hold set to 0.5s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:PASS 0.5";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:PASS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW pass hold on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 15://Set test 000 ACW ref value to 0mA and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000uA"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW ref value set to 0mA");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:REF 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:REF?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW ref value on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 16://Set test 000 ACW initial voltage to 0% and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000%"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW initial voltage set to 0%");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:ACW:INIT 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:ACW:INIT?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 000 ACW initial voltage on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 17://Enable test return and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("ON"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test return enabled");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "TEST:RET?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to enable test return on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    //ACW test

                    case 18://Set retry count for the test

                        int cmdAttStart;

                        if (Main.HV_Test_Retry_Count >= Main.Max_TCP_Cmd_Attempts)//Set Main.HV_Test_Cmd_Attempts value so there is only Main.HV_Test_Retry_Count amount of retries left in it
                        {
                            cmdAttStart = 0;
                        }
                        else
                        {
                            cmdAttStart = Main.Max_TCP_Cmd_Attempts - Main.HV_Test_Retry_Count;
                        }

                        System.Diagnostics.Debug.Print("GW_HV test 000 ACW retry count set");
                        respOK = true;
                        Main.HV_Test_State++;
                        Main.HV_Test_One_Call = false;
                        Main.HV_Test_Cmd_Attempts = cmdAttStart;
                        net_dev.Resp = "";

                        break;

                    case 19://Start 000 ACW Test

                        if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_dev.Resp = "";

                            net_dev.Cmd = "FUNC:TEST ON";
                            System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                            Socket_.send_socket(net_dev, net_dev.Cmd);

                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.HV_Test_Cmd_Attempts++;

                            Main.HV_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.HV_Test_State++;
                            Main.HV_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to run 000 ACW test on GW_HV");
                            Main.HV_Test_Failed = true;
                        }
                        break;

                    case 20://Wait for test 000 ACW to finish

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OK"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW finished");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.HV_Test_Timeout_Timer) > Main.HV_Test_000_ACW_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Retry GW_HV test 000 ACW");
                            respOK = true;
                            Main.HV_Test_State--;
                            Main.HV_Test_One_Call = false;
                        }

                        break;

                    case 21://Get 000 ACW test results

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("ACW"))
                            {
                                if (net_dev.Resp.StartsWith("ACW,PASS"))
                                {
                                    System.Diagnostics.Debug.Print("GW_HV test 000 ACW PASSED");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("GW_HV test 000 ACW FAILED");
                                }

                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MEAS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to get 000 ACW test results on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 22://Stop 000 ACW test

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("TEST OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW stopped");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "FUNC:TEST OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "FUNC:TEST?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 000 ACW on EVSE " + Main.HV_Test_EVSE_ID + " could not be stopped");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    //GB setup

                    case 23://Set test number to 1 and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("001"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test number set to 1");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:STEP 1";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:STEP?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set test number to 1 on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 24://Set test 001 GB HI to 100mOhm and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("100.0m Ohm"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB HI set to 100mOhm");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:RHIS 100";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:RHIS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB HI on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 25://Set test 001 GB Lo to 0mOhm and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.0m Ohm"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB LO set to 0mOhm");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:RLOS 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:RLOS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB LO on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 26://Set test 001 GB Test time to 0.3s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.3 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB test time set to 0.3s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:TTIM 0.3";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:TTIM?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB test time on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 27://Set test 001 GB Frequency to 60Hz and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("60 Hz"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB frequency set to 60Hz");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:FREQ 60";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:FREQ?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB frequency on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 28://Set test 001 GB Contact to 0s and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.0 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB contact set to 0s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:CONT 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:CONT?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB contact on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 29://Set test 001 GB Ground mode to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB ground mode set to OFF");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:GROUNDMODE OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:GROUNDMODE?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB ground mode on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 30://Set test 001 GB Max hold to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB max hold set to OFF");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:MAXH OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:MAXH?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB max hold on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 31://Set test 001 GB Pass hold to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.5 s"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB pass hold set to 0.5s");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:PASS 0.5";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:PASS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB pass hold on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 32://Set test 001 GB reference value to 0mOhm and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("000.0m Ohm"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB reference value set to 0mOhm");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:REF 0";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:REF?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB reference value on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 33://Set test 001 GB zero check to OFF and check

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB zero check set to OFF");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MANU:GB:ZEROCHECK OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "MANU:GB:ZEROCHECK?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to set 001 GB zero check on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    //GB test

                    case 34://Set retry count for the test

                        if (Main.HV_Test_Retry_Count >= Main.Max_TCP_Cmd_Attempts)//Set Main.HV_Test_Cmd_Attempts value so there is only Main.HV_Test_Retry_Count amount of retries left in it
                        {
                            cmdAttStart = 0;
                        }
                        else
                        {
                            cmdAttStart = Main.Max_TCP_Cmd_Attempts - Main.HV_Test_Retry_Count;
                        }

                        System.Diagnostics.Debug.Print("GW_HV test 001 GB retry count set");
                        respOK = true;
                        Main.HV_Test_State++;
                        Main.HV_Test_One_Call = false;
                        Main.HV_Test_Cmd_Attempts = cmdAttStart;
                        net_dev.Resp = "";

                        break;

                    case 35://Start 001 GB Test

                        if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                        {
                            net_dev.Resp = "";

                            net_dev.Cmd = "FUNC:TEST ON";
                            System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                            Socket_.send_socket(net_dev, net_dev.Cmd);

                            net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                            Main.HV_Test_Cmd_Attempts++;

                            Main.HV_Test_Timeout_Timer = unixTimeMilliseconds;
                            respOK = true;
                            Main.HV_Test_State++;
                            Main.HV_Test_One_Call = false;
                        }
                        else
                        {
                            System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to run 001 GB test on GW_HV");
                            Main.HV_Test_Failed = true;
                        }
                        break;

                    case 36://Wait for test 001 GB to finish

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("OK"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB finished");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if ((unixTimeMilliseconds - Main.HV_Test_Timeout_Timer) > Main.HV_Test_001_GB_Timeout)
                        {
                            System.Diagnostics.Debug.Print("Retry GW_HV test 001 GB");
                            respOK = true;
                            Main.HV_Test_State--;
                            Main.HV_Test_One_Call = false;
                        }

                        break;

                    case 37://Get 001 GB test results

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("GB"))
                            {
                                if (net_dev.Resp.StartsWith("GB ,PASS"))
                                {
                                    System.Diagnostics.Debug.Print("GW_HV test 001 GB PASSED");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.Print("GW_HV test 001 GB FAILED");
                                }

                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "MEAS?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("HV test failed on EVSE " + Main.HV_Test_EVSE_ID + ", reason: failed to get 001 GB test results on GW_HV");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 38://Stop 001 GB test

                        respOK = false;

                        if (net_dev.NewResp)
                        {
                            net_dev.NewResp = false;

                            if (net_dev.Resp.StartsWith("TEST OFF"))
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB stopped");
                                respOK = true;
                                Main.HV_Test_State++;
                                Main.HV_Test_One_Call = false;
                                Main.HV_Test_Cmd_Attempts = 0;
                                net_dev.Resp = "";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("Wrong GW_HV responce: " + net_dev.Resp);
                            }
                        }

                        if (respOK == false)
                        {
                            if (Main.HV_Test_Cmd_Attempts < Main.Max_TCP_Cmd_Attempts)
                            {
                                net_dev.Resp = "";

                                net_dev.Cmd = "FUNC:TEST OFF";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.Cmd = "FUNC:TEST?";
                                System.Diagnostics.Debug.Print($"cmd: = {net_dev.Cmd}");
                                Socket_.send_socket(net_dev, net_dev.Cmd);

                                net_dev.SendReceiveState = NetDev_SendState.SEND_BEGIN;
                                Main.HV_Test_Cmd_Attempts++;
                            }
                            else
                            {
                                System.Diagnostics.Debug.Print("GW_HV test 001 GB on EVSE " + Main.HV_Test_EVSE_ID + " could not be stopped");
                                Main.HV_Test_Failed = true;
                            }
                        }
                        break;

                    case 39://Test complete, reset

                        respOK = false;

                        System.Diagnostics.Debug.Print("GW_HV test complete, reset");
                        Main.HV_Test_One_Call = false;
                        Main.HV_Test_Cmd_Attempts = 0;
                        Main.HV_Test_State = 0;

                        break;

                }

                Main.HV_Test_Step_Timer = unixTimeMilliseconds;
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
            //int ptr = 0;

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

                    case NetDev_SendState.RECEIVE_WAIT:
                         break;
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
            //int param = 0;
            //string temp = "";
            //int ptr = 0;
            //Int32 rssi_constant = -113;
            //DataGridViewRow Row;

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
                            /*switch (net_dev.Resp[2])        <--------    Kazkas out of range
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
                            }*/
                            
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
            //int param = 0;
            string temp = "";
            //int ptr = 0;
            Int32 rssi_constant = -113;
            //DataGridViewRow Row;

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
                        net_dev.Cmd = Evse_param_type[net_dev.SubState] + "1;";//test select komandos formavimas
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
