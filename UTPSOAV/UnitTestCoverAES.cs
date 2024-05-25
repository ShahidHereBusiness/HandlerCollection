using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTPSOAV
{
    /// <summary>
    /// Summary description for UnitTestCoverAES
    /// </summary>
    [TestClass]
    public class UnitTestCoverAES
    {
        public UnitTestCoverAES()
        {
            //
            // TODO: Add constructor logic here
            //
            SOAV.CoverAES.IVString = "(*^%$#@7Hjgtv^%$";
            SOAV.CoverAES.KeyString = "BGVGHjU&^%4Ft(8764UhKJnJlO*^%$#@";
        }
        [TestMethod]
        public void TestMethodEncrypt()
        {

            Console.WriteLine(SOAV.CoverAES.EncryptStringAES("Hello World"));
        }
        [TestMethod]
        public void TestMethodDencrypt()
        {               
            Console.WriteLine(SOAV.CoverAES.DecryptStringAES("jIfgCdZfLYihCBUx4ESIVQ=="));
        }
    }
}
