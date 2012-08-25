using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using tman0.Launcher.UI;

namespace tman0.Launcher.Utilities
{
    [Serializable]
    public class LauncherInformation
    {
        public static LauncherInformation Current { get; set; }
        public string Message { get; set; }
        public long LauncherTimestamp { get; set; }
        public string LauncherLocation { get; set; }
        public long LWJGLTimestamp { get; set; }
        public string LWJGLLocation { get; set; }
        public string LWJGLVersion { get; set; }
        public long BackgroundTimestamp { get; set; }
        public string BackgroundLocation { get; set; }
        public long MinecraftTimestamp { get; set; }
        public string MinecraftVersion { get; set; }
        public string MinecraftLocation { get; set; }
    }

    static class LauncherInformationDownloader
    {
        public static async Task GetLatestLauncherInformation(this MainWindow window)
        {
            LauncherInformationDownloader.window = window;
            window.LoadingBox.Visibility = Visibility.Visible;
            window.LoadingText.Content = "Downloading update info.";
            WebClient w = new WebClient();
            string a = await w.DownloadStringTaskAsync(new Uri("https://dl.dropbox.com/u/37049399/MCLauncher/LauncherInformation.xml"));
            StringReader r = new StringReader(a);
            XmlSerializer s = new XmlSerializer(typeof(LauncherInformation));
            LauncherInformation i;
            i = (LauncherInformation)s.Deserialize(r);
            r.Close();
            window.LoadingBox.Visibility = Visibility.Collapsed;
            w.Dispose();
            OnManifestReceive(i, new EventArgs());
            LauncherInformation.Current = i;
        }


        private static MainWindow window;
        public static event EventHandler OnManifestReceive;
    }
}
