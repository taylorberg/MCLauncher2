using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Net;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

using Microsoft.VisualBasic.Devices;

using tman0.Launcher.Interop;
using tman0.Launcher.Minecraft;
using tman0.Launcher.Utilities;

using TaskDialogInterop;

namespace tman0.Launcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region XAML Constructors
        public MainWindow()
        {
            LauncherInformationDownloader.OnManifestReceive += LauncherInformationDownloader_OnManifestReceive;
            InitializeComponent();
        }

        void LauncherInformationDownloader_OnManifestReceive(object sender, EventArgs e)
        {
            this.LoadingBox.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion
        #region Event Handlers
        private async void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            LauncherSettings.Load();

            WebClient w = new WebClient();
            StringReader r = new StringReader(await w.DownloadStringTaskAsync("https://dl.dropbox.com/u/37049399/MCLauncher/LauncherMessage.xaml"));
            Announcements.Document = (FlowDocument)XamlReader.Load(XmlReader.Create(r));

            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dirt.png"));
            b.Viewport = new Rect(0.1, 0.1, 0.07, 0.1);
            b.TileMode = TileMode.Tile;
            this.Background = b;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadingText.Content = "Please wait...";
            LoadingProgress.IsIndeterminate = true;
            
            ComputerInfo i = new ComputerInfo();
            SystemInfo.Content = "An error occured getting your system's RAM information. This is not a good sign! D:";
            SystemInfo.Content = String.Format("{0} MB ({1} GB) Free/{2} MB ({3} GB) Total", i.AvailablePhysicalMemory / 1024 / 1024, i.AvailablePhysicalMemory / 1024 / 1024 / 1024, i.TotalPhysicalMemory / 1024 / 1024, i.TotalPhysicalMemory / 1024 / 1024 / 1024);
            await this.GetLatestLauncherInformation();
            await this.ApplyLatestBackground();
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            String resname = @"Libraries\" + new AssemblyName(args.Name).Name + ".dll";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resname))
            {
                Byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return Assembly.Load(data);
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                w.Close();
            }

            TaskDialogOptions o = new TaskDialogOptions();
            Exception ex = (Exception)e.ExceptionObject;
            o.Title = "An application error has occurred.";
            o.Content = ex.GetType().FullName + ": " + ex.Message;
            o.ExpandedInfo = ex.ToString();
            o.MainIcon = VistaTaskDialogIcon.Error;
        }


        private void MainWindow_Closed_1(object sender, EventArgs e)
        {
            
        }
        #endregion

        private async void Login_Click_1(object sender, RoutedEventArgs e)
        {
            await this.LaunchSequence(Username.Text, Password.Password);
        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void Setup_Click_1(object sender, RoutedEventArgs e)
        {
            Globals.Windows.SettingsEditor.ShowDialog();
        }

        private void VChanger_Click_1(object sender, RoutedEventArgs e)
        {
            new SnapshotDownloader().ShowDialog();
        }
    }
}
