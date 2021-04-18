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
    /// <summary>
    /// TCPServer类 服务端程序
    /// </summary>
    public class CHTcpServer : SocketObject
    {
        private bool IsStop = false;
        object obj = new object();
        public static PushSockets pushSockets;

        /// <summary>
        /// 信号量
        /// </summary>
        private Semaphore semap = new Semaphore(5, 5000);

        /// <summary>
        /// 客户端列表集合
        /// </summary>
        public List<Sockets> ClientList = new List<Sockets>();

        /// <summary>
        /// 服务端实例对象
        /// </summary>
        public TcpListener Listener;

        /// <summary>
        /// 当前的ip地址
        /// </summary>
        private IPAddress IpAddress;

        /// <summary>
        /// 初始化消息
        /// </summary>
        private string InitMsg = "JiYF笨小孩TCP服务端";

        /// <summary>
        /// 监听的端口
        /// </summary>
        private int Port;

        /// <summary>
        /// 当前ip和端口节点对象
        /// </summary>
        private IPEndPoint Ip;


        /// <summary>
        /// 初始化服务器对象
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        public override void InitSocket(IPAddress ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.Listener = new TcpListener(IpAddress, Port);
        }

        /// <summary>
        /// 初始化服务器对象
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public override void InitSocket(string ipAddress, int port)
        {
            this.IpAddress = IPAddress.Parse(ipAddress);
            this.Port = port;
            this.Ip = new IPEndPoint(IpAddress, Port);
            this.Listener = new TcpListener(IpAddress, Port);
        }

        /// <summary>
        /// 服务端启动监听，处理链接
        /// </summary>
        public override void Start()
        {
            try
            {
                Listener.Start();
                Thread Accth = new Thread(new ThreadStart(
                    delegate
                    {
                        while (true)
                        {
                            if (IsStop != false)
                            {
                                break;
                            }
                            this.GetAcceptTcpClient();
                            Thread.Sleep(1);
                        }
                    }
                    ));
                Accth.Start();
            }
            catch (SocketException skex)
            {
                Sockets sks = new Sockets();
                sks.ex = skex;
                pushSockets.Invoke(sks);
            }
        }

        /// <summary>
        /// 获取处理新的链接请求
        /// </summary>
        private void GetAcceptTcpClient()
        {
            try
            {
                if (Listener.Pending())
                {
                    semap.WaitOne();
                    //接收到挂起的客户端请求链接
                    System.Net.Sockets.TcpClient tcpClient = Listener.AcceptTcpClient();

                    //维护处理客户端队列
                    System.Net.Sockets.Socket socket = tcpClient.Client;
                    NetworkStream stream = new NetworkStream(socket, true);
                    Sockets sks = new Sockets(tcpClient.Client.RemoteEndPoint as IPEndPoint, tcpClient, stream);
                    sks.NewClientFlag = true;

                    //推送新的客户端连接信息
                    pushSockets.Invoke(sks);

                    //客户端异步接收数据
                    sks.nStream.BeginRead(sks.RecBuffer, 0, sks.RecBuffer.Length, new AsyncCallback(EndReader), sks);

                    //加入客户端队列
                    this.AddClientList(sks);

                    //链接成功后主动向客户端发送一条消息
                    if (stream.CanWrite)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(this.InitMsg);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    semap.Release();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 异步接收发送的的信息
        /// </summary>
        /// <param name="ir"></param>
        private void EndReader(IAsyncResult ir)
        {
            Sockets sks = ir.AsyncState as Sockets;
            if (sks != null && Listener != null)
            {
                try
                {
                    if (sks.NewClientFlag || sks.Offset != 0)
                    {
                        sks.NewClientFlag = false;
                        sks.Offset = sks.nStream.EndRead(ir);
                        //推送到UI
                        pushSockets.Invoke(sks);
                        sks.nStream.BeginRead(sks.RecBuffer, 0, sks.RecBuffer.Length, new AsyncCallback(EndReader), sks);
                    }
                }
                catch (Exception skex)
                {
                    lock (obj)
                    {
                        ClientList.Remove(sks);
                        Sockets sk = sks;
                        //标记客户端退出程序
                        sk.ClientDispose = true;
                        sk.ex = skex;
                        //推送至UI
                        pushSockets.Invoke(sks);
                    }
                }
            }
        }

        /// <summary>
        /// 客户端加入队列
        /// </summary>
        /// <param name="sk"></param>
        private void AddClientList(Sockets sk)
        {
            lock (obj)
            {
                Sockets sockets = ClientList.Find(o => { return o.Ip == sk.Ip; });
                if (sockets == null)
                {
                    ClientList.Add(sk);
                }
                else
                {
                    ClientList.Remove(sockets);
                    ClientList.Add(sk);
                }
            }
        }

        /// <summary>
        /// 服务端停止监听
        /// </summary>
        public override void Stop()
        {
            if (Listener != null)
            {
                Listener.Stop();
                Listener = null;
                IsStop = true;
                pushSockets = null;
            }
        }

        /// <summary>
        /// 向所有在线客户端发送消息
        /// </summary>
        /// <param name="SendData">消息内容</param>
        public void SendToAll(string SendData)
        {
            for (int i = 0; i < ClientList.Count; i++)
            {
                SendToClient(ClientList[i].Ip, SendData);
            }
        }

        /// <summary>
        /// 向单独的一个客户端发送消息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="SendData"></param>
        public void SendToClient(IPEndPoint ip, string SendData)
        {
            try
            {
                Sockets sks = ClientList.Find(o => { return o.Ip == ip; });
                if (sks == null || !sks.Client.Connected)
                {
                    Sockets ks = new Sockets();
                    //标识客户端下线
                    sks.ClientDispose = true;
                    sks.ex = new Exception("客户端没有连接");
                    pushSockets.Invoke(sks);
                }
                if (sks.Client.Connected)
                {
                    //获取当前流进行写入.
                    NetworkStream nStream = sks.nStream;
                    if (nStream.CanWrite)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(SendData);
                        nStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        //避免流被关闭,重新从对象中获取流
                        nStream = sks.Client.GetStream();
                        if (nStream.CanWrite)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes(SendData);
                            nStream.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            //如果还是无法写入,那么认为客户端中断连接.
                            ClientList.Remove(sks);
                            Sockets ks = new Sockets();
                            sks.ClientDispose = true;//如果出现异常,标识客户端下线
                            sks.ex = new Exception("客户端无连接");
                            pushSockets.Invoke(sks);//推送至UI

                        }
                    }
                }
            }
            catch (Exception skex)
            {
                Sockets sks = new Sockets();
                sks.ClientDispose = true;//如果出现异常,标识客户端退出
                sks.ex = skex;
                pushSockets.Invoke(sks);//推送至UI
            }
        }
    }
}
