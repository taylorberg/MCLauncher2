using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tman0.Launcher.Utilities
{
    class Globals
    {
        public static readonly string LauncherDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.mclauncher\";

        public static class Windows
        {
            public static tman0.Launcher.UI.MainWindow MainWindow;
            public static readonly tman0.Launcher.UI.SettingsEditor SettingsEditor = new UI.SettingsEditor();
        }
    }
}
