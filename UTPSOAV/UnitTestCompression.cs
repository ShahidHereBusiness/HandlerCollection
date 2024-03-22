using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestCompression
    {
        [TestMethod]
        public void TestMethodZipUnZip()
        {
            string str = "Hello World Compression";
            str = SOAV.Compression.Zip(str);
            Console.WriteLine($"Compressed String: {str}");
            Console.WriteLine($"UnCompressed String: {SOAV.Compression.UnZip(str)}");
        }
    }
}
