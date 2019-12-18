using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AirportBroadcast.Equipment.test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.lstMsg.ItemsSource = Items;
        }

        bool isRun = false;


        ObservableCollection<string> Items = new ObservableCollection<string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isRun)
            {
                return;
            }
            isRun = true;
            CommandHelp.StartRead();
            ActiveMQHelp.StartSend();
            //将获取到的数据展现到界面
            //Task.Factory.StartNew(() =>
            //{
            //    while (isRun)
            //    {
            //        System.Threading.Thread.Sleep(1);
            //        var commands = CommandHelp.GetCommands();
            //        if (commands == null)
            //        {
            //            continue;
            //        }

            //        commands.ToList().ForEach(command =>
            //        {
            //            this.Dispatcher.Invoke(() =>
            //            {
            //                Items.Insert(0, command.Message);
            //            });
            //        });

            //        if (Items.Count > 100)
            //        {
            //            this.Dispatcher.Invoke(() =>
            //            {
            //                while (Items.Count > 100)
            //                {
            //                    Items.RemoveAt(Items.Count - 1);
            //                }
            //            });
            //        }
            //    }
            //});
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            isRun = false;
            ActiveMQHelp.StopSend();
            CommandHelp.StopRead();
            base.OnClosed(e);
        }
    }
}
