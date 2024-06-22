using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static SOAV.LogManager;
using SOAV;

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
        /// <summary>
        /// File System IDisposable Implementation Logging
        /// </summary>
        [TestMethod]
        public void TestMethod3FSLog()
        {
            string timeLog;

            #region Shared TID
            using (ILogManager log = new ILogManager())
            {
                // Missed Fodler Reference atleast one postfix "\\"
                timeLog = log.FileSystemLog("UTP", "HelloWorld", "M|N|O|P", out string response);
                Console.WriteLine($"Resultant:{response}, TID:{timeLog}");// Return Overview
                // Request Logging
                timeLog = log.FileSystemLog("UTP\\", "HelloWorld", "M|N|O|P", out response);
                Console.WriteLine($"Resultant:{response}, TID:{timeLog}");// Return Overview
                // Response Logging
                timeLog = log.FileSystemLog("D:\\DEVLOGS\\UTP\\", "HelloWorld", "M|N|O|P|R", out response, true);
                Console.WriteLine($"Resultant:{response}, TID:{timeLog}");// Return Overview
            };
            #endregion

            #region New TID
            using (ILogManager log = new ILogManager())
            {
                // Exception Logging                
                timeLog = log.FileSystemLog("D:\\DEVLOGS\\UTP\\", "HelloWorld", $"{timeLog}|M|N|O|P|R", out string response, true, new Exception("Time Trace"));
                Console.WriteLine($"Resultant:{response}, TID:{timeLog}");// Return Overview 
            };
            #endregion
        }
        /// <summary>
        /// Windows Application IDisposable Implementation Logging
        /// </summary>
        [TestMethod]
        public void TestMethod4ApplicationEventLog()
        {
            string timeLog;

            using (ILogManager log = new ILogManager())
            {
                // Request Logging
                timeLog = log.EventViewerAppLog("M|N|O|P", out bool flag);
                Console.WriteLine($"Flag:{flag}, TID:{timeLog}");// Return Overview
                // Response Logging
                timeLog = log.EventViewerAppLog("M|N|O|P|R", out flag, true);
                Console.WriteLine($"Flag:{flag}, TID:{timeLog}");// Return Overview
            };
        }
    }
}