using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

using tman0.Launcher.Interop;
using tman0.Launcher.Utilities;
using System.IO;
using TaskDialogInterop;

namespace tman0.Launcher.UI
{
    /// <summary>
    /// Interaction logic for SnapshotDownloader.xaml
    /// </summary>
    public partial class SnapshotDownloader : Window
    {
        public SnapshotDownloader()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.GlassBackground();
            this.RefreshAssetList();
        }

        private ObservableCollection<Release> _Releases = new ObservableCollection<Release>();
        public ObservableCollection<Release> Releases { get { return _Releases; } }

        public void RefreshAssetList()
        {
            string SecretKey = null;
            string PublicKey = null;
            AmazonS3Client Client = new AmazonS3Client(PublicKey, SecretKey);

            ListObjectsRequest Request = new ListObjectsRequest
            {
                BucketName = "assets.minecraft.net"
            };

            ListObjectsResponse Result;

            do
            {
                Result = Client.ListObjects(Request);
                foreach (S3Object o in Result.S3Objects)
                {
                    string IsSnapshot = "Release";
                    if(!o.Key.Contains("minecraft.jar")) continue;
                    if (Regex.IsMatch(o.Key, "[0-9][0-9]w[0-9][0-9]")) IsSnapshot = "Snapshot";
                    else if (o.Key.Contains("pre")) IsSnapshot = "Pre-release";
                    
                    _Releases.Add(new Release {  Version = o.Key.Split('/')[0],
                                                Size = (o.Size / 1024).ToString() + "KB", 
                                                Uploaded = o.LastModified, 
                                                Type = IsSnapshot, 
                                                Key = o.Key} );
                }
            }
            while (Result.IsTruncated);
            Client.Dispose();
            Result.Dispose();
        }

        private void DownloadClick(object sender, RoutedEventArgs e)
        {
            TaskDialogOptions o = new TaskDialogOptions
            {
                ShowMarqueeProgressBar = true,
                MainInstruction = "Press OK to download selected release... (MCLauncher will freeze! Do not close!)",
                MainIcon = VistaTaskDialogIcon.Information,
                EnableCallbackTimer = true,
                CustomButtons = new [] { "Cancel", "OK" }
            };
            string SecretKey = null;
            string PublicKey = null;
            Release Selected = (Release)JarList.SelectedItem;

            AmazonS3Client Client = new AmazonS3Client(PublicKey, SecretKey);

            GetObjectRequest Request = new GetObjectRequest
            {
                BucketName = "assets.minecraft.net",
                Key = Selected.Key
            };

            GetObjectResponse Result;
            TaskDialogResult tdr = TaskDialog.Show(o);
            if (tdr.CustomButtonResult == 0) return;
            Result = Client.GetObject(Request);
            File.Copy(Globals.LauncherDataPath + "/Minecraft/bin/minecraft.jar", Globals.LauncherDataPath + "/Minecraft/OldMinecraft.jar");
            Result.WriteResponseStreamToFile(Globals.LauncherDataPath + "/Minecraft/bin/minecraft.jar");
        }

        private void RestoreOldClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Globals.LauncherDataPath + "/Minecraft/OldMinecraft.jar")) return;
            File.Delete(Globals.LauncherDataPath + "/Minecraft/bin/minecraft.jar");
            File.Copy(Globals.LauncherDataPath + "/Minecraft/OldMinecraft.jar", Globals.LauncherDataPath + "/Minecraft/bin/minecraft.jar");
        }

        private void ForceUpdateClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CachedMinecraftTimestamp = 0;
            Properties.Settings.Default.CachedLWJGLTimestamp = 0;
            Properties.Settings.Default.CachedBackgroundTimestamp = 0;
        }
    }
}
