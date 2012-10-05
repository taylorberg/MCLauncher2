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
        public static byte[] Encrypt(string PlainText)
        {
            return ProtectedData.Protect(UTF8Encoding.UTF8.GetBytes(PlainText), new byte[] { 200, 201, 39, 203, 21, 32, 34 }, DataProtectionScope.CurrentUser);
        }
        public static string Decrypt(byte[] EncryptedData)
        {
            return UTF8Encoding.UTF8.GetString(ProtectedData.Unprotect(EncryptedData, new byte[] { 200, 201, 39, 203, 21, 32, 34 }, DataProtectionScope.CurrentUser));
        }
    }
}
