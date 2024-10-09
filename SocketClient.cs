using Ion.Tools.Models.XmlDataExport;
using iText.Layout.Element;
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
        public static bool start_receive = false;

        public SocketClient()
        {
            InitializeComponent();
        }

        public long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
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
            static IAsyncResult asyncResult;
            private static SocketAsyncEventArgs socketReceiveArgs;

            private static SocketDevList net_dev;

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

            public static int StartClient(SocketDevList dev, int port)
            {
                bool connected = false;
                try
                {
                    dev.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
                    Console.WriteLine($"dev.Ip:{dev.Ip.ToString()}");
                    IPAddress ipAddress = IPAddress.Parse(dev.Ip);
                    IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_0);// 

                    bool success = false;

                    switch (port)
                    {
                        case 0:
                            var result = dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            success = result.AsyncWaitHandle.WaitOne(1000, true);
                            connected = result.IsCompleted;
                            break;

                        case 1:
                            remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_1);// 
                            var resul = dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            success = resul.AsyncWaitHandle.WaitOne(1000, true);
                            connected = resul.IsCompleted;
                            break;
                    }

                    if (!connected)
                    {
                        return 3;
                    }

                    if (success)
                    {
                        Main.main.dbg_print(DbgType.NETWORK, "SOCKET_OPENED", Color.MediumSeaGreen);
                        Console.WriteLine($"SOCKET_OPENED:{0}");
                        Console.WriteLine("Dev= " + dev);
                        return 0;
                    }
                    else
                    {
                        dev.client.Close();
                        Main.main.dbg_print(DbgType.NETWORK, "SOCKET_TIMEOUT", Color.Gold);
                        Console.WriteLine($"SOCKET_TIMEOUT:{0}");
                        return 1;
                    }

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
                    Socket client = (Socket)ar.AsyncState;
                    client.EndConnect(ar);
                    Main.main.dbg_print(DbgType.NETWORK, "Socket connection:" + client.RemoteEndPoint.ToString(), Color.DimGray);
                    Console.WriteLine($"Socket connection:{client.RemoteEndPoint.ToString()}");
                    connnectCompleted.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERR_CON_callback:{0}");
                }
            }

            public static void Send(SocketDevList dev, string data)
            {
                net_dev = dev;

                try
                {
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
                    socketReceiveArgs = new SocketAsyncEventArgs();
                    socketReceiveArgs.SetBuffer(buffer, 0, buffer.Length);
                    socketReceiveArgs.Completed += SocketReceiveArgs_Completed;
                    socketReceiveArgs.UserToken = net_dev.Port_0;

                    var bytes = dev.client.ReceiveAsync(socketReceiveArgs);

                    net_dev.ReceiveRunning = true;
                }
                catch (Exception e)
                {
                    Main.main.dbg_print(DbgType.NETWORK, "RECEIVE_FAIL", Color.LightCoral);
                    Console.WriteLine(e.ToString());
                    net_dev.SendReceiveState = NetDev_SendState.RECEIVE_FAIL;
                    net_dev.ReceiveRunning = false;
                }
            }

            private static void SocketReceiveArgs_Completed(object sender, SocketAsyncEventArgs e)
            {
                int line_end = 0;
                try
                {
                    int len = e.BytesTransferred;
                    response = Encoding.UTF8.GetString(e.Buffer, 0, len);
                    Main.main.dbg_print(DbgType.NETWORK, "RX:" + len + " <- " + net_dev.Name, Color.DimGray);
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
                    }
                }

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
                    System.Diagnostics.Debug.Print("RX data no \\n char");
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

                if (!dev.ReceiveRunning)
                {
                    var result = ReceiveAsync(dev);
                }
            }

            public static void start_receive(SocketDevList dev)
            {
                if (!dev.ReceiveRunning)// receiv nepaleistas, tai paleidziam
                {
                    var result = ReceiveAsync(dev);
                }
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
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public int start_socket(SocketDevList dev, int port)
        {
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
    }
}