using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestAuth2.Tools
{
    public static class AesTool
    {
        private const string secrectKey = "TestAuth2";

        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Base64 字符串</returns>
        public static string Encrypt(string source)
        {
            byte[] secso = Encoding.UTF8.GetBytes(secrectKey);
            byte[] sec = new byte[16];
            int len = sec.Length;
            if (secso.Length < sec.Length)
            {
                len = secso.Length;
            }
            Array.Copy(secso, sec, len);
            byte[] ened;
            using (Aes aes = Aes.Create())
            {
                aes.IV = sec;
                aes.Key = sec;
                aes.Mode = CipherMode.CBC;
                var enter = aes.CreateEncryptor(sec, sec);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, enter, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(cryptoStream))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(source);
                        }
                        ened = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(ened);
        }

        public static string Decrypt(string source)
        {
            string deresult = "";
            byte[] secso = Encoding.UTF8.GetBytes(secrectKey);
            byte[] sec = new byte[16];
            byte[] ened = Convert.FromBase64String(source);
            int len = sec.Length;
            if (secso.Length < sec.Length)
            {
                len = secso.Length;
            }
            Array.Copy(secso, sec, len);
            using (Aes aes = Aes.Create())
            {
                aes.IV = sec;
                aes.Key = sec;
                aes.Mode = CipherMode.CBC;
                var deter = aes.CreateDecryptor(sec, sec);
                using (MemoryStream ms = new MemoryStream(ened))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, deter, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                        {
                            //Write all data to the stream.
                            deresult = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return deresult;
        }
    }
}
