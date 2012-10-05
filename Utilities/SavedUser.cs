using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tman0.Launcher.Utilities
{
    [Serializable]
    [XmlRoot("User")]
    public class SavedUser
    {
        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement(DataType="hexBinary")]
        public byte[] EncryptedPassword { get; set; }
    }
}
