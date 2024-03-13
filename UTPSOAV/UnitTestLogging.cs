using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static SOAV.FileAppLog;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestLogging
    {
        /// <summary>
        /// File System Logging
        /// </summary>
        [TestMethod]
        public void TestMethod1FSLog()
        {
            int resultant;
            //HandleRemoteAddress = "::1";
            // Missed Fodler Reference atleats one postfix "\\"
            resultant = FSLog("UTP", "HelloWorld", "M|N|O|P");
            Console.WriteLine($"Resultant:{resultant}");// Return Overview
            // Request Logging
            resultant = FSLog("UTP\\", "HelloWorld", "M|N|O|P");
            Console.WriteLine($"Resultant:{resultant}");// Return Overview
            // Response Logging
            resultant = FSLog("D:\\LOGS\\UTP\\", "HelloWorld", "M|N|O|P|R");
            Console.WriteLine($"Resultant:{resultant}");// Return Overview            
        }
        /// <summary>
        /// Windows Application Logging
        /// </summary>
        [TestMethod]
        public void TestMethod2ApplicationEventLog()
        {
            Boolean flag;
            RemoteAddress = "::1";
            // Request Logging
            flag = EVAppLog("Request|M|N|O|P");
            Console.WriteLine($"Flag:{flag}");// Return Overview
            // Response Logging
            flag = EVAppLog("Response|M|N|O|P|R");
            Console.WriteLine($"Flag:{flag}");// Return Overview
        }
    }
}