using BDC.General.Communicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.SocketDemo.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.txt_text.Text = @"MSH|^~\&|||||20120508094822||ORU^R01|1|P|2.3.1||||0||ASCII||| 
PID|1|1001||| Mike ||19851001095133|M|||||||||||||||||||||||
OBR|1|12345678|10|^|Y|20120405193926|20120405193914|2012040519391 4|1001^10001^1|||||20120405193914|serum|||||||||||||||||||||||||||||||||
OBX|1|NM|2|TBil|100|umol/L|-|N|||F||100|20120405194245|||0|R.Exp|Instr|M1| 1102:2501
OBX|2|NM|5|ALT|98.2|umol/L|-|N|||F||98.2|20120405194403|||0|R.Exp|Instr|M 1| 1102:2501
OBX|3|NM|6|AST|26.4|umol/L|-|N|||F||26.4|||||R.Exp|Instr|M1|1102:2501";
        }

        private CHTcpClient tcpServer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CHTcpClient.pushSockets = new PushSockets(DataReceive);
                tcpServer = new CHTcpClient();
                //tcpServer.InitSocket(System.Net.IPAddress.Parse("127.0.0.1"), 3333);

                string ip = this.txt_ip.Text;

                tcpServer.InitSocket(System.Net.IPAddress.Parse(ip), 3333);

                tcpServer.Start();

                PrintInfo("连接服务成功!");

            }
            catch (Exception ex)
            {
                PrintInfo("连接服务错误!" + ex.Message);
            }
        }

        private void DataReceive(Sockets sockets)
        {
            if (sockets.ex != null)
            {
                if (sockets.ClientDispose)
                {
                    PrintInfo(string.Format("client:{0} offline!", sockets.Ip));
                }
            }
            else
            {
                if (sockets.NewClientFlag)
                {
                    PrintInfo(string.Format("new client：{0} connect succeed", sockets.Ip));
                }
                else if (sockets.Offset == 0)
                {
                    PrintInfo(string.Format("client:{0} offline!", sockets.Ip));
                }
                else
                {
                    byte[] buffer = new byte[sockets.Offset];
                    Array.Copy(sockets.RecBuffer, buffer, sockets.Offset);
                    string str = Encoding.Default.GetString(buffer);

                    PrintInfo(str);
                }
            }
        }

        void PrintInfo(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.txt_message.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message + Environment.NewLine;
            });

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            tcpServer.SendData(this.txt_text.Text);

            PrintInfo("发送数据:" + this.txt_text.Text);
        }
    }
}
