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
using System.Windows.Shapes;

using tman0.Launcher.Interop;

namespace tman0.Launcher.UI
{
    /// <summary>
    /// Interaction logic for DebugLog.xaml
    /// </summary>
    public partial class DebugLog : Window
    {
        public DebugLog()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.GlassBackground();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        public void WriteLine(string text)
        {
            Dispatcher.Invoke(() =>
            {
                Log.Text += text + Environment.NewLine;
                if ((bool)AutoScroll.IsChecked) Scroller.ScrollToEnd();
            });

        }
    }
}
