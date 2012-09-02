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
                    
                    _Releases.Add(new Release {  Version = o.Key.Split('/')[0],
                                                Size = (o.Size / 1024).ToString() + "KB", 
                                                Uploaded = o.LastModified, 
                                                Type = IsSnapshot } );
                }
            }
            while (Result.IsTruncated);
        }
    }
}
