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
        public static LauncherSettings Default {get; private set;}
        
        public static void Load()
        {
            if (!File.Exists(Globals.LauncherDataPath + @"\Settings.xml")) Save();
            XmlSerializer s = new XmlSerializer(typeof(LauncherSettings));
            Default = (LauncherSettings)s.Deserialize(File.OpenRead(Globals.LauncherDataPath + @"\Settings.xml"));
        }

        public static void Save()
        {
            XmlSerializer s = new XmlSerializer(typeof(LauncherSettings));
            File.Delete(Globals.LauncherDataPath + @"\Settings.xml");
            s.Serialize(File.OpenWrite(Globals.LauncherDataPath + @"\Settings.xml"), Default);
        }

        public string JavaLocation = @"C:\Program Files\Java\jre7\bin\java.exe";
        public string MaxMemory = "8G";
        public string InitialMemory = "4G";
        public bool UseXincgc = true;
        public bool UseServer = false;
        public string VMArgs = "";
    }
}
