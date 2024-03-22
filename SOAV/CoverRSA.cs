using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.Web;

namespace SOAV
{
    public class CoverRSAsme
    {
        /// <summary>
        /// Solution Developer:
        /// RSA Encryption and Decryption
        /// </summary>
        public CoverRSAsme() { }
        public static string EncryptString(string data, string publicKey)
        {
            data = Compression.Zip(data);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToString());
                byte[] encryptedData = rsa.Encrypt(dataBytes, false);
                return Convert.ToBase64String(encryptedData);
            }
        }
        public static string DecryptString(string data, string privateKey)
        {
            byte[] encryptedData = Convert.FromBase64String(data);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                string decryptedString = Encoding.UTF8.GetString(decryptedData);
                decryptedString = Compression.UnZip(decryptedString);
                return decryptedString;
            }
        }
        ~CoverRSAsme()
        {
            GC.Collect();
        }
    }
}