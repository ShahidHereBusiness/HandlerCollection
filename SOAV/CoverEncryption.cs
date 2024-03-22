using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SOAV
{
    public static class CoverEncryption
    {
        public static string SecurityKey { get; set; } = "655902ec3366086c23791e638824d89d662f62632ce4e3d1e8c5380888ee0989e6099f21e10793489722e5eee9152c30657913695dc2929491108e5cc33feef9";
        /// <summary>
        /// MD5 
        ///      Basic
        ///             Encryption
        /// </summary>
        /// <param name="watchWord">String to Encrypt</param>
        /// <param name="securityStamp">True:Enhance Checksum Encryption</param>
        /// <returns></returns>
        public static string CoverMD5(string watchWord, bool securityStamp = false)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(watchWord ?? "");
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("x2"));
                if (securityStamp)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    char[] chars = $"{DateTime.Now.ToString("MMssyyyyHHddmmfffffff")}{sb.ToString()}{rand.Next()}".ToCharArray();
                    int n = chars.Length;
                    while (n > 1)
                    {
                        n--;
                        int k = rand.Next(n + 1);
                        char temp = chars[k];
                        chars[k] = chars[n];
                        chars[n] = temp;
                    }
                    return new string(chars);
                }
                else
                    return sb.ToString();
            }
        }
        /// <summary>
        /// Encrypt        
        ///          Text
        ///                 Based on Provided MD5 Key
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CoverEncryptCryptoKey(string str)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(str);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;
            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray);
        }
        /// <summary>        
        ///Decrypt
        ///         Text
        ///               Based on Provided MD5 Key
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CoverDecryptCryptoKey(string str)
        {
            byte[] toEncryptArray = Convert.FromBase64String(str);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            TripleDESCryptoServiceProvider objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;
            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}