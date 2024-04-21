using System;
using System.Security;

//using System.Collections.Generic;
//using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.Web;

namespace SOAV
{
    public class CoverRSAsme
    {
        public static string PublicKey { get; set; }
        public static string PrivateKey { get; set; }
        /// <summary>
        /// Solution Developer:
        /// RSA Encryption and Decryption
        /// </summary>
        public CoverRSAsme() { }
        public static string EncryptString(string data, string publicKey)
        {
            try
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
            catch
            {
                return string.Empty;
            }
        }
        public static string DecryptString(string data, string privateKey)
        {
            try
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
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Genearte Pair of Public & Private Keys
        /// </summary>
        /// <param name="keySize">Integer</param>
        /// <returns>PublicKey, PrivateKey Static Variables</returns>
        public static bool GenerateKeys(int keySize = 2048)
        {
            try
            {
                // Generate RSA key pair
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
                {
                    // Export the public key
                    PublicKey = rsa.ToXmlString(false);
                    // Export the private key
                    PrivateKey = rsa.ToXmlString(true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        ~CoverRSAsme()
        {
            GC.Collect();
        }
    }
}