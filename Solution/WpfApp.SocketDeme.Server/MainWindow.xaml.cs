using BDC.General.Analysis;
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

namespace WpfApp.SocketDeme.Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private CHTcpServer tcpServer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CHTcpServer.pushSockets = new PushSockets(DataReceive);
                tcpServer = new CHTcpServer();

                string ip = this.txt_ip.Text;

                tcpServer.InitSocket(System.Net.IPAddress.Parse(ip), 3333);
                tcpServer.Start();

                PrintInfo("服务启动成功!");

            }
            catch (Exception ex)
            {
                PrintInfo("服务启动错误!" + ex.Message);
            }
        }

        /// <summary>
        /// TCP数据接收
        /// </summary>
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
                    PrintInfo("【开始接收数据接……");
                    byte[] buffer = new byte[sockets.Offset];
                    Array.Copy(sockets.RecBuffer, buffer, sockets.Offset);
                    string str = Encoding.Default.GetString(buffer);



                    try
                    {
                        var item = AnalysisService.Instance.GetDataWithHL7(str, out Exception ex);

                        if (item == null)
                        {
                            PrintInfo(ex, str);
                        }
                        else
                        {
                            PrintInfo("【" + item?.PID?.Name + "】数据接收成功");
                            //dataCaches.Add(new DataCache() { Data = item, Err = str });

                            //string callback = str.Replace("ORU^R01", "ACK^R01");

                            //sockets.SendBuffer = Encoding.UTF8.GetBytes(callback);

                          

                            var ss = str.Split('\r')[0].Replace("ORU^R01", "ACK^R01");

                            ss += Environment.NewLine + "MSA|AA|1|Message accepted|||0";

                            tcpServer.SendToClient(sockets.Ip, ss);

                            //            < SB > MSH | ^~\&||||| 20120508094823 || ACK ^ R01 | 1 | P | 2.3.1 |||| 0 || ASCII |||< CR >
                            //MSA | AA | 1 | Message accepted ||| 0 |< CR >
                            //      < EB >< CR >
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintInfo(ex);
                    }
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
        void PrintInfo(Exception ex, string message = null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.txt_message.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message + Environment.NewLine;

                this.txt_message.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + ex.Message + Environment.NewLine;

            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tcpServer.SendToAll(this.txt_text.Text);

            PrintInfo("发送数据:" + this.txt_text.Text);
        }
    }
}
