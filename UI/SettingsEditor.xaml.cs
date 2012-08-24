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

        public void Load()
        {
            this.Load(LauncherSettings.Default);
        }

        public void Load(LauncherSettings s)
        {
            InitialMemory.Text = s.InitialMemory;
            MaxMemory.Text = s.MaxMemory;
            JavaPath.Text = s.JavaLocation;
            UseXincgc.IsChecked = s.UseXincgc;
            UseServer.IsChecked = s.UseServer;
            VMArgs.Text = s.VMArgs;
        }

        public void Save()
        {
            this.Save(LauncherSettings.Default);
        }

        public void Save(LauncherSettings s)
        {
            s.InitialMemory = InitialMemory.Text;
            s.MaxMemory = MaxMemory.Text;
            s.JavaLocation = JavaPath.Text;
            s.UseXincgc = (bool)UseXincgc.IsChecked;
            s.UseServer = (bool)UseServer.IsChecked;
            s.VMArgs = VMArgs.Text;
        }

        private void SettingsEditor_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.GlassBackground();
            this.Load();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Save();
            this.Hide();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
