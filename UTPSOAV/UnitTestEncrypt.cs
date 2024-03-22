using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestEncrypt
    {        
        [TestMethod]
        public void TestMethodMD5()
        {
            string str = "Hello world MD5";
            Console.WriteLine($"MD5:{SOAV.CoverEncryption.CoverMD5(str)}");
            Console.WriteLine($"MD5 Unique:{SOAV.CoverEncryption.CoverMD5(str, true)}");
            Console.WriteLine($"MD5 Checksum:{SOAV.CoverEncryption.CoverMD5(str)}");
            Console.WriteLine($"MD5 Unique:{SOAV.CoverEncryption.CoverMD5(str, true)}");
        }
        [TestMethod]
        public void TestMethodCryptoKey()
        {       
            string str = "Hello World Crypto Keyed";// String to Encrypt

            // Deafult Security Key 128bits
            str = SOAV.CoverEncryption.CoverEncryptCryptoKey(str);
            Console.WriteLine($"Encrypt:{str}");
            str = SOAV.CoverEncryption.CoverDecryptCryptoKey(str);
            Console.WriteLine($"Decrypt:{str}");

            Console.WriteLine("New Security Key");
            SOAV.CoverEncryption.SecurityKey = "kdjnfu&90)ih&*^eHV0UHT$@hjvj)-><:N*(^&CFR#@";
            str = SOAV.CoverEncryption.CoverEncryptCryptoKey(str);
            Console.WriteLine($"Encrypt:{str}");
            str = SOAV.CoverEncryption.CoverDecryptCryptoKey(str);
            Console.WriteLine($"Decrypt:{str}");
        }
    }
}
