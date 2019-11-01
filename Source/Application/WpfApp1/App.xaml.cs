using HeBianGu.Common.TestData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                Persion persion = new Persion();
                Debug.WriteLine(persion.Card);
            }

            if (e.Args != null)
            {
                foreach (var item in e.Args)
                {
                    MessageBox.Show(item);
                }
            }

            base.OnStartup(e);
        }

    }
}
