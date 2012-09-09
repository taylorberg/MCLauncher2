using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tman0.Launcher.UI;

namespace tman0.Launcher.Utilities
{
    class Globals
    {
        public static readonly string LauncherDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.mclauncher\";

        public static class Windows
        {
            public static MainWindow MainWindow;
            public static readonly SettingsEditor SettingsEditor = new UI.SettingsEditor();
        }
    }
}
