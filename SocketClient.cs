using Ion.Tools.Models.XmlDataExport;
using MonitorsTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ion.Sdk.Ici.Channel.BlackBox.Message;

namespace ArtiluxEOL
{
    public partial class SocketClient : Component
    {
        //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

   

        public SocketClient()
        {
            InitializeComponent();
        }

        public SocketClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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

        public class AssyncSocketClient
        {
            public static ManualResetEvent connnectCompleted = new ManualResetEvent(false);
            public static ManualResetEvent sendCompleted = new ManualResetEvent(false);
            public static ManualResetEvent receiveCompleted = new ManualResetEvent(false);
            private static string response = String.Empty;
            //private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            static IAsyncResult asyncResult;


            public static bool ping(SocketDevList dev, int port)
            {
                TcpClient client;
                
                try
                {
                    switch (port)
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
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"No_Ping:{0}");
                    client = null;
                    return false;
                }

            }
            public static int StartClient(SocketDevList dev, int port)
            {
                try
                {
                    dev.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
                    //IPAddress ip = iPHost.AddressList[0];
                    //string ip_adr = "192.168.11.150";
                    //var ip_a = ConvertFromIpAddressToInteger(ip_adr);
                    Console.WriteLine($"dev.Ip:{dev.Ip.ToString()}");
                   
                    IPAddress ipAddress = IPAddress.Parse(dev.Ip);
                    //IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 12312);
                    IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_0);// 

                    switch (port)
                    {
                        case 0:
                            dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            connnectCompleted.WaitOne(2000, true);
                            break;

                        case 1:
                            remoteEndPoint = new IPEndPoint(ipAddress, dev.Port_1);// 

                            dev.client.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectionCallback), dev.client);
                            connnectCompleted.WaitOne(2000, true);
                            break;
                    }

                    //Socket state = (Socket)asyncResult.AsyncState;
                    Console.WriteLine($"SOCKET_OPENED:{0}");
                    return 0;
                }
                catch (Exception e)
                {
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
                    Console.WriteLine($"Socket connection:{client.RemoteEndPoint.ToString()}");
                    connnectCompleted.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERR_CON_callback:{0}");
                    Console.WriteLine(e.ToString());
                }
            }

            public static void Send(SocketDevList dev, string data)
            {
                try
                {
                    Console.WriteLine($"Send_port:{dev.Port_0}");
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    dev.client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), dev.client);
                }
                catch (Exception e)
                {  
                    Console.WriteLine($"ERR_SEND:{0}");
                    Console.WriteLine(e.ToString());
                }
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket client = (Socket)ar.AsyncState;
                    int byteSent = client.EndSend(ar);
                    Console.WriteLine($"Sent:{byteSent}");
                    sendCompleted.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public static void Receive(SocketDevList dev)
            {
                try
                {
                    ObjectState state = new ObjectState();
                    state.socket = dev.client;
                    dev.client.BeginReceive(state.Buffer, 0, ObjectState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public static void send_receive(SocketDevList dev)
            {
                Send(dev, "SYSTEM:TIME?\r\n");
                sendCompleted.WaitOne();


                Receive(dev);
                //receiveCompleted.WaitOne();
                Console.WriteLine($"Resp:{response}");
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
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
                    state.sb.Append(Encoding.ASCII.GetString(state.Buffer,0, byteRead));
                    client.BeginReceive(state.Buffer, 0, ObjectState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                }
                    if (byteRead == 2)
                    {
                        Console.WriteLine($"state.sb:{state.sb.Length}");
                        if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                            Console.WriteLine($"Resp:{response}");
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


        public bool socket_ping(SocketDevList dev, int port)
        {
            return AssyncSocketClient.ping(dev, port);
        }
        public int start_socket(SocketDevList dev, int port)
        {
            return AssyncSocketClient.StartClient(dev, port);
        }

        public void send_socket(SocketDevList dev)
        {
            AssyncSocketClient.send_receive(dev);


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
