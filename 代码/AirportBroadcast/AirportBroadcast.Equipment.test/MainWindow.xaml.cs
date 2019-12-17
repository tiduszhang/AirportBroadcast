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
            CommandReader.StartRead();

            Task.Factory.StartNew(() =>
            {
                while (isRun)
                {
                    System.Threading.Thread.Sleep(1);
                    var commands = CommandReader.GetCommands();
                    if (commands == null)
                    {
                        continue;
                    }

                    commands.ToList().ForEach(command =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            Items.Insert(0, command.Message);
                        }, System.Windows.Threading.DispatcherPriority.Loaded);
                    });

                    if (Items.Count > 100)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            while (Items.Count > 100)
                            {
                                Items.RemoveAt(Items.Count - 1);
                            }
                        }, System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                }
            });
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            isRun = false;
            CommandReader.StopRead();
            base.OnClosed(e);
        }
    }
}
