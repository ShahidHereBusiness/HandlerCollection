using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOAV;
using System;
using static SOAV.LogManager;

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
            string TimeLog = $"{DateTime.Now:yyyyMMddHHmmssfffffff}{new Random(Guid.NewGuid().GetHashCode()).Next()}";            
            // Missed Fodler Reference atleats one postfix "\\"
            resultant = FileSystemLog("UTP", "HelloWorld", "M|N|O|P");
            Console.WriteLine($"Resultant:{resultant}");// Return Overview
            // Request Logging
            resultant = FileSystemLog("UTP\\", "HelloWorld", "M|N|O|P");
            Console.WriteLine($"Resultant:{resultant}");// Return Overview
            // Response Logging
            resultant = FileSystemLog("D:\\DEVLOGS\\UTP\\", "HelloWorld", "M|N|O|P|R", true);
            Console.WriteLine($"Resultant:{resultant}");// Return Overview
            // Exception Logging
            resultant = FileSystemLog("D:\\DEVLOGS\\UTP\\", "HelloWorld", $"{TimeLog}|M|N|O|P|R", true, new Exception("Time Trace"));
            Console.WriteLine($"Resultant:{resultant}");// Return Overview 
        }
        /// <summary>
        /// Windows Application Logging
        /// </summary>
        [TestMethod]
        public void TestMethod2ApplicationEventLog()
        {
            Boolean flag;            
            // Request Logging
            flag = EventViewerAppLog("M|N|O|P");
            Console.WriteLine($"Flag:{flag}");// Return Overview
            // Response Logging
            flag = EventViewerAppLog("M|N|O|P|R", true);
            Console.WriteLine($"Flag:{flag}");// Return Overview
        }
    }
}