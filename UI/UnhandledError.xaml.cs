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

using tman0.Launcher.Utilities;
using tman0.Launcher.Interop;

namespace tman0.Launcher.UI
{
    /// <summary>
    /// Interaction logic for UnhandledError.xaml
    /// </summary>
    public partial class UnhandledError : Window
    {
        public UnhandledError(Exception e)
        {

            InitializeComponent();
            InnerException = e;
            ExceptionType.Content = e.GetType().FullName;
            ExceptionMessage.Content = e.Message;
            StackTrace.Text = e.StackTrace.ToString();
        }

        Exception InnerException;

        private void UnhandledError_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.GlassBackground();
        }
    }
}
