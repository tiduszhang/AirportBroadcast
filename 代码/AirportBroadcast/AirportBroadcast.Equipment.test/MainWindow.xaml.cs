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
            TAClientHelp.StartRecivce();

            Task.Factory.StartNew(() =>
            {
                isRun = true;
                do
                {
                    System.Threading.Thread.Sleep(1);
                    var messages = TAClientHelp.GetMessages();

                    if (messages != null && messages.Length > 0)
                    {
                        messages.ToList().ForEach(message =>
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                //Items.Add(message);
                                Items.Insert(0, message);
                                //if (Items.Count > 100)
                                //{
                                //    //Items.RemoveAt(0);
                                //    Items.RemoveAt(Items.Count - 1);
                                //}
                            }, System.Windows.Threading.DispatcherPriority.Loaded);
                        });

                    }
                    else if (Items.Count > 100)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            while (Items.Count > 100)
                            { 
                                Items.RemoveAt(Items.Count - 1);
                            }
                        }, System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                } while (isRun);
            });
        }


        protected override void OnClosed(EventArgs e)
        {
            isRun = false;
            TAClientHelp.StopRecivce();
            base.OnClosed(e);
        }
    }
}
