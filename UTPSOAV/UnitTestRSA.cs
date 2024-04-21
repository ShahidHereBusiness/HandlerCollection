using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOAV;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestRSA
    {        
        [TestMethod]
        public void TestMethod()
        {
            string str = "Hello World RSA";
            Console.WriteLine($"Gerate Keys:");
            CoverRSAsme.GenerateKeys();

            Console.WriteLine($"Public Key:{CoverRSAsme.PublicKey}");
            Console.WriteLine($"Private Key:{CoverRSAsme.PrivateKey}");

            str = CoverRSAsme.EncryptString(str, CoverRSAsme.PublicKey);
            Console.WriteLine($"Encrypted String:{str}");

            str=CoverRSAsme.DecryptString(str, CoverRSAsme.PrivateKey);
            Console.WriteLine($"Decrypted String:{str}");
        }
    }
}
