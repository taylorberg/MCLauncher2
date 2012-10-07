using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tman0.Launcher.Utilities
{
    public class LauncherSettings
    {
        public static LauncherSettings Default {get; set;}
        
        public static void Load()
        {
            if (!File.Exists(Globals.LauncherDataPath + @"\Settings.xml")) Save();
            XmlSerializer s = new XmlSerializer(typeof(LauncherSettings));
            FileStream f = File.OpenRead(Globals.LauncherDataPath + @"\Settings.xml");
            Default = (LauncherSettings)s.Deserialize(f);
            f.Close();
            if (Default == null)
            {
                f.Close();
                Default = new LauncherSettings();
                Save();
                return;
            }
        }

        public static void Save()
        {
            if (!Directory.Exists(Globals.LauncherDataPath)) Directory.CreateDirectory(Globals.LauncherDataPath);
            XmlSerializer s = new XmlSerializer(typeof(LauncherSettings));
            File.Delete(Globals.LauncherDataPath + @"\Settings.xml");
            FileStream f = File.OpenWrite(Globals.LauncherDataPath + @"\Settings.xml");
            s.Serialize(f, Default);
            f.Close();
        }

        public string JavaLocation = @"C:\Program Files\Java\jre7\bin\java.exe";
        public string MaxMemory = "1G";
        public string InitialMemory = "512M";
        public bool UseXincgc = true;
        public bool UseServer = false;
        public string VMArgs = "";
        public List<SavedUser> SecuredLoginInfo = new List<SavedUser>();
    }
}
