using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tman0.Launcher.Utilities
{
    [Serializable]
    
    public class Release
    {
        public string Version {get; set;}
        public string Size {get; set;}
        public string Uploaded {get; set;}
        public string Type {get; set;}
    }
}
