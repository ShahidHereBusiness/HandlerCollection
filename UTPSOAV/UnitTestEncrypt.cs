using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestEncrypt
    {
        [TestMethod]
        public void TestMethodEncrypt()
        {
            Object m = MD5.Create("MyPassWord");
            //HandleEncryption e = new HandleEncryption();
            Console.Write("Success Object");
        }
    }
}
