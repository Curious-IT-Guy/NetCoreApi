using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;

namespace NetCoreApi.Helpers
{
    public static class Password
    {
        private static IConfiguration _configuration;

        private static string Key(string value) => "3bn7665h4jb745jhb745ljh6b7lh45b" + value + "58uvfd8gup3u4n2345nr97wy97fp3r";

        public static string Encrypt(string email, string password)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key(email), new byte[] 
                { 
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 
                });
                
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }

                password = Convert.ToBase64String(ms.ToArray());
            }

            return password;
        }

    }
}