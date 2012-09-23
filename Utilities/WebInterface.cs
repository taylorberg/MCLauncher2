using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

using tman0.Launcher.Properties;
using tman0.Launcher.UI;

using Ionic.Zip;


namespace tman0.Launcher.Utilities
{
    static class BackgroundDownloader
    {
        public static async Task ApplyLatestBackground(this MainWindow window)
        {
            long CurrentTimestamp = (long) Settings.Default["CachedBackgroundTimestamp"];
            window.LoadingProgress.IsIndeterminate = false;
            if (!Directory.Exists(Globals.LauncherDataPath)) Directory.CreateDirectory(Globals.LauncherDataPath);
                if (LauncherInformation.Current.BackgroundTimestamp <= (long)Settings.Default["CachedBackgroundTimestamp"])
                {
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("file://" + Globals.LauncherDataPath + @"\Background.png"));
                    window.Background = brush;
                }
                else
                {
                    WebClient w = new WebClient();
                    await w.DownloadFileTaskAsync(new Uri(LauncherInformation.Current.BackgroundLocation), Globals.LauncherDataPath + @"\Background.png");
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("file://" + Globals.LauncherDataPath + @"\Background.png"));
                    window.Background = brush;
                    window.LoadingBox.Visibility = Visibility.Collapsed;
                    Settings.Default["CachedBackgroundTimestamp"] = LauncherInformation.Current.BackgroundTimestamp;
                    Settings.Default.Save();
                    w.Dispose();
                }
        }
    }
   
    static class MinecraftDownloader
    {
        public static async Task BeginDownloadMinecraft(this MainWindow window)
        {
            window.LoadingBox.Visibility = Visibility.Visible;
            window.LoadingText.Content = "Updating Minecraft...";
            window.LoadingProgress.IsIndeterminate = true;
            if (!Directory.Exists(Globals.LauncherDataPath + @"\Minecraft")) Directory.CreateDirectory(Globals.LauncherDataPath + @"\Minecraft");
            if (!Directory.Exists(Globals.LauncherDataPath + @"\Minecraft\bin")) Directory.CreateDirectory(Globals.LauncherDataPath + @"\Minecraft\bin");
            if (!Directory.Exists(Globals.LauncherDataPath + @"\Minecraft\bin\natives")) Directory.CreateDirectory(Globals.LauncherDataPath + @"\Minecraft\bin\natives");
            WebClient c = new WebClient();
            try
            {
                
                if (LauncherInformation.Current.LWJGLTimestamp > (long)Settings.Default["CachedLWJGLTimestamp"])
                {
                    window.LoadingText.Content = "Downloading updated LWJGL...";
                    byte[] d = await c.DownloadDataTaskAsync(LauncherInformation.Current.LWJGLLocation);
                    
                    ZipFile f = ZipFile.Read(new MemoryStream(d));
                    f.ExtractAll(Globals.LauncherDataPath + @"\Minecraft\bin", ExtractExistingFileAction.OverwriteSilently);

                    Settings.Default["CachedBackgroundTimestamp"] = LauncherInformation.Current.LWJGLTimestamp;
                    Settings.Default.Save();
                }
                if(LauncherInformation.Current.MinecraftTimestamp > (long)Settings.Default["CachedMinecraftTimestamp"])
                {
                    MessageBoxResult r = MessageBox.Show("Do you want to update Minecraft? (Latest version: " + LauncherInformation.Current.MinecraftVersion + ")", "MCLauncher", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (r == MessageBoxResult.Yes)
                    {
                        window.LoadingText.Content = "Downloading Minecraft " + LauncherInformation.Current.MinecraftVersion + "...";
                        byte[] mc = await c.DownloadDataTaskAsync(LauncherInformation.Current.MinecraftLocation);

                        File.WriteAllBytes(Globals.LauncherDataPath + @"\Minecraft\bin\minecraft.jar", mc);
                        Settings.Default["CachedMinecraftTimestamp"] = LauncherInformation.Current.MinecraftTimestamp;
                        Settings.Default.Save();
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Failed to update Minecraft. Is your internet down? Exception information: " + ex.ToString(), "An error has occured.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                c.Dispose();
            }
            
        }
    }
}
