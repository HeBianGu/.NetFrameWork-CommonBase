using HeBianGu.Common.Registry;
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

namespace WpfApp1
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

        RightMouseRegisterService class1 = new RightMouseRegisterService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            class1.RegisterFile("测试项目");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            class1.UnRegister("测试项目",SystemFileType.File);
        }
    }
}
