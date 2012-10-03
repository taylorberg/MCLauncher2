using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace tman0.Launcher.Utilities
{
    class Encryption
    {
        private readonly static string Key = "wElLiCoUlDcOmEuPwItHaBeTtErKeYtHaNtHiSbUtIdK";
        private readonly static byte[] IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static string Encrypt(string PlainText)
        {
            throw new NotImplementedException();
        }
        public static string Decrypt(string EncryptedString)
        {
            throw new NotImplementedException();
        }
    }
}
