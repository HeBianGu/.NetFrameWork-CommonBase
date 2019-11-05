using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.Socket
{
    public class Sockets
    {
        /// <summary>
        /// 接收缓冲区大小8k
        /// </summary>
        public byte[] RecBuffer = new byte[8 * 1024];

        /// <summary>
        /// 发送缓冲区大小8k
        /// </summary>
        public byte[] SendBuffer = new byte[8 * 1024];

        /// <summary>
        /// 异步接收后包的大小
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// 当前IP地址,端口号
        /// </summary>
        public IPEndPoint Ip { get; set; }
        /// <summary>
        /// 客户端主通信程序
        /// </summary>
        public TcpClient Client { get; set; }
        /// <summary>
        /// 承载客户端Socket的网络流
        /// </summary>
        public NetworkStream nStream { get; set; }

        /// <summary>
        /// 发生异常时不为null.
        /// </summary>
        public Exception ex { get; set; }

        /// <summary>
        /// 新客户端标识.如果推送器发现此标识为true,那么认为是客户端上线
        /// 仅服务端有效
        /// </summary>
        public bool NewClientFlag { get; set; }

        /// <summary>
        /// 客户端退出标识.如果服务端发现此标识为true,那么认为客户端下线
        /// 客户端接收此标识时,认为客户端异常.
        /// </summary>
        public bool ClientDispose { get; set; }

        /// <summary>
        /// 空参构造
        /// </summary>
        public Sockets() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">ip节点</param>
        /// <param name="client">TCPClient客户端</param>
        /// <param name="ns">NetworkStream </param>
        public Sockets(IPEndPoint ip, System.Net.Sockets.TcpClient client, NetworkStream ns)
        {
            this.Ip = ip;
            this.Client = client;
            this.nStream = ns;
        }
    }
}