using Ion.Tools.Models.XmlDataExport;
using MonitorsTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ion.Sdk.Ici.Channel.BlackBox.Message;
using static Ion.Sdk.Idi.Value.Constraint;

namespace ArtiluxEOL
{
    public partial class SocketClient : Form
    {
        //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static bool start_receive = false;

        //public static Main mainForm;
        //Main main_tst = new Main();


        public SocketClient()
        {
            InitializeComponent();
        }



        public SocketClient(Form callingForm)
        {
            //container.Add(this);

            //mainForm = callingForm as Main;
            InitializeComponent();

        }



        public long UnixTimeNow()
        {

            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static uint ConvertFromIpAddressToInteger(string ipAddress)
        {
            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }



            return BitConverter.ToUInt32(bytes, 0);
        }

        public class ObjectState
        {
            public const int BufferSize = 256;
            public Socket socket = null;
            public byte[] Buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();

        }

        public static class AssyncSocketClient
        {
            public static ManualResetEvent connnectCompleted = new ManualResetEvent(false);
            public static ManualResetEvent sendCompleted = new ManualResetEvent(false);
            public static ManualResetEvent receiveCompleted = new ManualResetEvent(false);
            private static string response = String.Empty;
            //private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            static IAsyncResult asyncResult;
            private static SocketAsyncEventArgs socketReceiveArgs;

            private static SocketDevList net_dev;



            public static void tt()
            {
                //mainForm.dbg_print("s");

            }

            public static bool ping(SocketDevList dev, int port)
            {
                TcpClient client;


                try
                {
                    /*switch (port)
                    {
                        case 0:
                            Console.WriteLine($"Port0:{dev.Port_0}");
                            client = new TcpClient(dev.Ip, dev.Port_0);
                            client.Close();
                            break;

                        case 1:
                            Console.WriteLine($"Port1:{dev.Port_1}");
                            client = new TcpClient(dev.Ip, dev.Port_1);
                            client.Close();
                            break;
                    }*/
                    //isPortAvailable();
                    var isPortOpen = IsPortOpen(dev.Ip, dev.Port_0);
                    Console.WriteLine(isPortOpen ? "{0} : open" : "{0} : close", port);


                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"No_Ping:{0}");
                    client = null;
                    return false;
                }

            }

            private static bool IsPortOpen(string ipAddress, int port)
            {
                var tcpClient = new TcpClient();
                try
                {
                    var result = tcpClient.BeginConnect(ipAddress, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                    tcpClient.EndConnect(result);
                    tcpClient.Close();
                    return success;
                }
                catch
                {
                    return false;
                }
            }

            public static int GetAvailablePort(string ip, int p)
            {
                IPAddress ipAddress = IPAddress.Parse(ip);

                TcpListener l = new TcpListener(ipAddress, 0);
                l.Start();
                int port = ((IPEndPoint)l.LocalEndpoint).Port;
                l.Stop();
                Console.WriteLine($"Available port found: {port}");
                return port;
            }

            static bool isPortAvailable()
            {
                var availablePorts = new List<int>();
                var properties = IPGlobalProperties.GetIPGlobalProperties();

                // Active connections
                /*var connections = properties.GetActiveTcpConnections();
                availablePorts.AddRange(connections);

                // Active tcp listners
                var endPointsTcp = properties.GetActiveTcpListeners();
                availablePorts.AddRange(endPointsTcp);

                // Active udp listeners
                var endPointsUdp = properties.GetActiveUdpListeners();
                availablePorts.AddRange(endPointsUdp);

                foreach (int p in availablePorts)
                {
                    if (p == myPort) return false;
                }*/

                IPEndPoint[] endPoints = properties.GetActiveTcpListeners();
                foreach (IPEndPoint e in endPoints)
                {

                    int port = 0;
                    //tmpClnt.player_ip = e.Address.ToString();
                    port = e.Port;
                    //tmpClnt.computer_name = Dns.GetHostEntry(e.Address).HostName;
                    //res.Add(tmpClnt);
                    System.Diagnostics.Debug.Print($"port: {port}");

                }
                return true;
            }

            public static int StartClient(SocketDevList dev, int port)
            {
                bool connected = false;
                try
                {
                    dev.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
                    //IPAddress ip = iPHost.AddressList[0];
                    //string ip_adr = "192.168.11.150";
                    //var ip_a = ConvertFromIpAddressToInteger(ip_adr);


                    Console.WriteLine($"dev.Ip:{dev.Ip.ToString()}");
                    //mainForm.dbg_print("dev.Ip:");

                    IPAddress ipAddress = IPAddress.Parse(dev.Ip);
                    //IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 12312);
                    IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_0);// 

                    bool success = false;

                    switch (port)
                    {
                        case 0:
                            var result = dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            //connnectCompleted.WaitOne(2000, true);
                            success = result.AsyncWaitHandle.WaitOne(1000, true);
                            connected = result.IsCompleted;
                            break;

                        case 1:
                            remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_1);// 
                            var resul = dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            //connnectCompleted.WaitOne(2000, true);
                            success = resul.AsyncWaitHandle.WaitOne(1000, true);
                            connected = resul.IsCompleted;
                            break;
                    }

                    //Console.WriteLine($"CONNECTED:{connected}");

                    if (!connected)
                    {
                        return 3;
                    }

                    if (success)
                    {
                        //socket.EndConnect(result);
                        Main.main.dbg_print(DbgType.NETWORK, "SOCKET_OPENED", Color.MediumSeaGreen);
                        Console.WriteLine($"SOCKET_OPENED:{0}");
                        return 0;
                    }
                    else
                    {
                        dev.client.Close();
                        Main.main.dbg_print(DbgType.NETWORK, "SOCKET_TIMEOUT", Color.Gold);
                        Console.WriteLine($"SOCKET_TIMEOUT:{0}");
                        return 1;
                    }

                    //Socket state = (Socket)asyncResult.AsyncState;

                    return 1;
                }
                catch (Exception e)
                {
                    Main.main.dbg_print(DbgType.NETWORK, "ERR_CON", Color.LightCoral);
                    Console.WriteLine($"ERR_CON:{e.ToString()}");
                    return 1;
                }
            }

            private static void ConnectionCallback(IAsyncResult ar)
            {
                try
                {

                    //Console.WriteLine($"END_CONN:{ar.AsyncState}");
                    Socket client = (Socket)ar.AsyncState;
                    client.EndConnect(ar);
                    Main.main.dbg_print(DbgType.NETWORK, "Socket connection:" + client.RemoteEndPoint.ToString(), Color.DimGray);
                    Console.WriteLine($"Socket connection:{client.RemoteEndPoint.ToString()}");
                    connnectCompleted.Set();
                }
                catch (Exception e)
                {
                    //Main.main.dbg_print(DbgType.NETWORK, "ERR_CON_callback", Color.LightCoral);
                    Console.WriteLine($"ERR_CON_callback:{0}");
                    //Console.WriteLine(e.ToString());
                }
            }

            public static void Send(SocketDevList dev, string data)
            {
                net_dev = dev;

                try
                {
                    //Main.main.dbg_print(DbgType.NETWORK, "Send_to:" + dev.Name, Color.DimGray);
                    //Console.WriteLine($"Send_to:{dev.Name}");
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    dev.client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), dev.client);
                }
                catch (Exception e)
                {
                    Main.main.dbg_print(DbgType.NETWORK, "ERR_SEND", Color.LightCoral);
                    Console.WriteLine(e.ToString());
                }
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket client = (Socket)ar.AsyncState;
                    int byteSent = client.EndSend(ar);
                    Main.main.dbg_print(DbgType.NETWORK, "TX:" + byteSent + " -> " + net_dev.Name, Color.DimGray);
                    //Console.WriteLine($"Sent:{byteSent}");
                    if (net_dev.State == NetDev_State.GET_PARAM_ALL && net_dev.GetSetParamLeft == net_dev.GetSetParamCount) //pirma komanda be ack 
                    {
                        Console.WriteLine($"ACK-RX");
                        net_dev.SendReceiveState = NetDev_SendState.RECEIVE_OK;
                    }
                    else
                    {
                        if (net_dev.SendReceiveState == NetDev_SendState.RECEIVE_OK)
                        {
                            //Console.WriteLine($"SKIP_SEND_OK_CALB");
                            Main.main.dbg_print(DbgType.NETWORK, "SKIP_SEND_OK_CALB", Color.Violet);
                            sendCompleted.Set();
                            return;
                        }
                        //Console.WriteLine($"ACK-TX");
                        if (net_dev.State == NetDev_State.GET_PARAM_ALL)
                        {
                            net_dev.SendReceiveState = NetDev_SendState.RECEIVE_WAIT;
                        }
                        else
                        {
                            net_dev.SendReceiveState = NetDev_SendState.SEND_OK;
                        }
                    }

                    sendCompleted.Set();

                }
                catch (Exception e)
                {
                    Main.main.dbg_print(DbgType.NETWORK, "SEND_FAILED", Color.LightCoral);
                    //Console.WriteLine($"SEND_FAILED!");
                    Console.WriteLine(e.ToString());
                }
            }

            public static async Task ReceiveAsync(SocketDevList dev)
            {


                net_dev = dev;
                try
                {
                    byte[] buffer = new byte[1024];
                    ObjectState state = new ObjectState();
                    state.socket = dev.client;
                    //dev.client.BeginReceive(state.Buffer, 0, ObjectState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                    socketReceiveArgs = new SocketAsyncEventArgs();
                    socketReceiveArgs.SetBuffer(buffer, 0, buffer.Length);
                    socketReceiveArgs.Completed += SocketReceiveArgs_Completed;
                    socketReceiveArgs.UserToken = net_dev.Port_0;

                    var bytes = dev.client.ReceiveAsync(socketReceiveArgs);

                    net_dev.ReceiveRunning = true;


                    //string response = Encoding.UTF8.GetString(socketReceiveArgs.Buffer, 0, 256);
                    //Console.WriteLine($"Transferred:{socketReceiveArgs.BytesTransferred}");
                    //Console.WriteLine($"response:{response}");
                }
                catch (Exception e)
                {
                    Main.main.dbg_print(DbgType.NETWORK, "RECEIVE_FAIL", Color.LightCoral);
                    Console.WriteLine(e.ToString());
                    net_dev.SendReceiveState = NetDev_SendState.RECEIVE_FAIL;
                }
            }

            private static void SocketReceiveArgs_Completed(object sender, SocketAsyncEventArgs e)
            {
                int line_end = 0;
                try
                {
                    //throw new NotImplementedException();
                    int len = e.BytesTransferred;
                    response = Encoding.UTF8.GetString(e.Buffer, 0, len);


                    //Console.WriteLine($"Transferred:{e.BytesTransferred} resp_end0:{Convert.ToInt16(response[len - 1])} resp_end1:{Convert.ToInt16(response[len-2])}" );
                    Main.main.dbg_print(DbgType.NETWORK, "RX:" + len + " <- " + net_dev.Name, Color.DimGray);
                    //Console.WriteLine($"Received:{e.BytesTransferred} IP: {e.UserToken}");
                    //Console.WriteLine($"response:{response}");

                    line_end = Convert.ToInt16(response[len - 1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RX_FAULT:{net_dev.Name}");
                }

                


                foreach (var dev in Main.main.network_dev)//ieskom per visus porto, tam devaisui pakisim rx data
                {
                    if (dev.Port_0 == Convert.ToInt16(e.UserToken))
                    {
                        net_dev = dev;
                        //Console.WriteLine($"Found port:{dev.Port_0} line-end:{line_end}");
                    }
                }

                //Main.main.network_dev[0].Resp

                if (line_end == 10)//ar turim gale enter
                {
                    if (net_dev.RespPktCount == 0)//jei turim visus duomenis pirmu siuntimu tai neapendinam
                    {
                        net_dev.Resp = response;
                    }
                    else
                    {
                        net_dev.Resp += @response;
                    }
                    net_dev.NewResp = true;
                    net_dev.RespPktCount = 0;
                    net_dev.ReceiveRunning = false;
                    net_dev.SendReceiveState = NetDev_SendState.RECEIVE_OK;
                }
                else
                {
                    if (net_dev.RespPktCount == 0)//jei turim visus duomenis pirmu siuntimu tai neapendinam
                    {
                        net_dev.Resp = response;
                    }
                    else
                    {
                        net_dev.Resp += @response;
                    }
                    net_dev.RespPktCount++;
                    _ = ReceiveAsync(net_dev);
                }
            }

            public static void send_receive(SocketDevList dev, string cmd)
            {
                dev.NewResp = false;
                //Console.WriteLine($"cmd.Length:{cmd.Length} state:{dev.State}");
                if (cmd.Length > 2)
                {
                    dev.SendReceiveState = NetDev_SendState.SEND_WAIT;
                    dev.NewSendData = false;
                    Send(dev, cmd + "\r\n");
                    //sendCompleted.WaitOne();
                }


                if (!dev.ReceiveRunning)// receiv nepaleistas, tai paleidziam
                {
                    var result = ReceiveAsync(dev);
                }

                //receiveCompleted.WaitOne();
                //Console.WriteLine($"Resp:{response}");

                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }

            public static void start_receive(SocketDevList dev)
            {
                dev.NewResp = false;

                if (!dev.ReceiveRunning)// receiv nepaleistas, tai paleidziam
                {
                    var result = ReceiveAsync(dev);
                }
            }



            public static void socket_close(SocketDevList dev)
            {

                dev.client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }

            private static void ReceiveCallBack(IAsyncResult ar)
            {
                try
                {
                    ObjectState state = (ObjectState)ar.AsyncState;
                    var client = state.socket;
                    int byteRead = client.EndReceive(ar);
                    Console.WriteLine($"read_byte:{byteRead}");
                    if (byteRead > 0)
                    {
                        state.sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, byteRead));
                        client.BeginReceive(state.Buffer, 0, ObjectState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                    }
                    if (byteRead < 3)
                    {
                        Console.WriteLine($"state.sb:{state.sb.Length}");
                        if (state.sb.Length > 1)
                        {
                            response = state.sb.ToString();
                            Console.WriteLine($"Resp_calb:{response}");
                        }
                        receiveCompleted.Set();
                        //client.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }


        public bool socket_ping(SocketDevList dev, int port)
        {
            return AssyncSocketClient.ping(dev, port);
        }
        public int start_socket(SocketDevList dev, int port)
        {
            //mainForm.dbg_print("sss");
            //main_tst.dbg_print("kljsakidjfi");

            return AssyncSocketClient.StartClient(dev, port);
        }

        public void send_socket(SocketDevList dev, string cmd)
        {
            AssyncSocketClient.send_receive(dev, cmd);


        }

        public void receive_socket(SocketDevList dev)
        {
            AssyncSocketClient.start_receive(dev);


        }

        public void close_socket(SocketDevList dev)
        {
            AssyncSocketClient.socket_close(dev);


        }

        /* public async void Socket_send(byte[] messageBytes)
         {
             while (true)
             {

                     NetworkStream serverStream = clientSocket.GetStream();
                     string data_cmd = "SYSTEM:TIME?";
                     byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data_cmd + "$");
                     serverStream.Write(outStream, 0, outStream.Length);
                     serverStream.Flush();
                     byte[] inStream = new byte[1025];
                     serverStream.
                     serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                     string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                     Console.WriteLine($"Status: {returndata}");

                     // Send message.
                     var message = "Hi friends";
                     messageBytes = Encoding.UTF8.GetBytes(message);
                     _ = await socket.SendToAsync(messageBytes, SocketFlags.None);
                     Console.WriteLine($"Socket client sent message: \"{message}\"");

                     // Receive ack.
                     var buffer = new byte[1_024];
                     var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                     var response = Encoding.UTF8.GetString(buffer, 0, received);
                     if (response == "<|ACK|>")
                     {
                         Console.WriteLine(
                             $"Socket client received acknowledgment: \"{response}\"");
                         break;
                     }


             }
             socket.Shutdown(SocketShutdown.Both);
         }*/

        public async void Socket_open()
        {
            try
            {
                /*System.Diagnostics.Debug.Print($"Client Started");
                clientSocket.Connect("192.168.11.150", 12312);*/
                IPAddress ipAddress = IPAddress.Parse("192.168.11.150");
                IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 12312);
                //socket.ConnectAsync(remoteEndPoint);
                // await socket.ConnectAsync(remoteEndPoint);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
