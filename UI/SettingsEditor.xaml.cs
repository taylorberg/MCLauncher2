using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tman0.Launcher.Interop;
using tman0.Launcher.Utilities;
using tman0.Launcher.Properties;

namespace tman0.Launcher.UI
{
    /// <summary>
    /// Interaction logic for SettingsEditor.xaml
    /// </summary>
    public partial class SettingsEditor : Window
    {
        public SettingsEditor()
        {
            InitializeComponent();
        }

        private void SettingsEditor_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.GlassBackground();
        }
    }
}
