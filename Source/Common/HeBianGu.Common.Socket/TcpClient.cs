using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Common.Socket
{
    public class CHTcpClient : SocketObject
    {
        bool IsClose = false;

        /// <summary>
        /// 当前管理对象
        /// </summary>
        Sockets sk;

        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient client;

        /// <summary>
        /// 当前连接服务端地址
        /// </summary>
        IPAddress Ipaddress;

        /// <summary>
        /// 当前连接服务端端口号
        /// </summary>
        int Port;

        /// <summary>
        /// 服务端IP+端口
        /// </summary>
        IPEndPoint ip;

        /// <summary>
        /// 发送与接收使用的流
        /// </summary>
        NetworkStream nStream;



        /// <summary>
        /// 初始化Socket
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public override void InitSocket(string ipaddress, int port)
        {
            Ipaddress = IPAddress.Parse(ipaddress);
            Port = port;
            ip = new IPEndPoint(Ipaddress, Port);
            client = new TcpClient();
        }

        public static PushSockets pushSockets;
        public void SendData(string SendData)
        {
            try
            {

                if (client == null || !client.Connected)
                {
                    Sockets sks = new Sockets();
                    sks.ex = new Exception("客户端无连接..");
                    sks.ClientDispose = true;

                    pushSockets.Invoke(sks);//推送至UI 
                }
                if (client.Connected) //如果连接则发送
                {
                    if (nStream == null)
                    {
                        nStream = client.GetStream();
                    }
                    byte[] buffer = Encoding.UTF8.GetBytes(SendData);
                    nStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception skex)
            {
                Sockets sks = new Sockets();
                sks.ex = skex;
                sks.ClientDispose = true;
                pushSockets.Invoke(sks);//推送至UI
            }
        }
        /// <summary>
        /// 初始化Socket
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public override void InitSocket(IPAddress ipaddress, int port)
        {
            Ipaddress = ipaddress;
            Port = port;
            ip = new IPEndPoint(Ipaddress, Port);
            client = new TcpClient();
        }
        private void Connect()
        {
            client.Connect(ip);
            nStream = new NetworkStream(client.Client, true);
            sk = new Sockets(ip, client, nStream);
            sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
        }
        private void EndReader(IAsyncResult ir)
        {

            Sockets s = ir.AsyncState as Sockets;
            try
            {
                if (s != null)
                {

                    if (IsClose && client == null)
                    {
                        sk.nStream.Close();
                        sk.nStream.Dispose();
                        return;
                    }
                    s.Offset = s.nStream.EndRead(ir);
                    pushSockets.Invoke(s);//推送至UI
                    sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
                }
            }
            catch (Exception skex)
            {
                Sockets sks = s;
                sks.ex = skex;
                sks.ClientDispose = true;
                pushSockets.Invoke(sks);//推送至UI

            }

        }
        /// <summary>
        /// 重写Start方法,其实就是连接服务端
        /// </summary>
        public override void Start()
        {
            Connect();
        }
        public override void Stop()
        {
            Sockets sks = new Sockets();
            if (client != null)
            {
                client.Client.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                client.Close();
                IsClose = true;
                client = null;
            }
            else
            {
                sks.ex = new Exception("客户端没有初始化.!");
            }
            pushSockets.Invoke(sks);//推送至UI
        }

    }
}
