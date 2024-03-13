using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOAV;
using System;
using System.Diagnostics;
using System.Windows;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestEventLog
    {
        /// <summary>
        /// Event Viewer Application and Services Logging
        /// </summary>
        [TestMethod]
        public void TestMethod3AppAndServicesEventLog()
        {
            using (EventAppLog asl = new EventAppLog())
            {
                Boolean flag;

                flag = asl.MarkEventLog();
                Console.WriteLine($"Flag:{flag}");// Return Overview

                // Fully Qualified Method Calling
                asl.ProcessName = "Test Method";
                asl.ProcessNameCode = 101;
                asl.ProcessNameId = 5175;
                asl.ProcessStage = "Request";

                flag = asl.MarkEventLog("M|N|O|P");
                Console.WriteLine($"Flag:{flag}");// Return Overview
                asl.ProcessStage = "Response";
                flag = asl.MarkEventLog("M|N|O|P|R");
                Console.WriteLine($"Flag:{flag}");// Return Overview
            }
        }
        /// <summary>
        /// Read Service and Application Event Viewer Entries
        /// </summary>
        [TestMethod]
        public void TestMethod4AppAndServicesRead()
        {
            using (EventAppLog asl = new EventAppLog())
            {
                Boolean flag;

                EventLogEntryCollection msg = null;
                // Read SOALogEvents
                flag = asl.ReadEventLog(ref msg, false);
                if (flag)
                {
                    foreach (EventLogEntry entry in msg)
                        Console.WriteLine($"Read Success:{entry.Message}");// Return Overview
                }
                // Read Application Logs
                flag = asl.ReadEventLog(ref msg, false, "Application");
                if (flag)
                {
                    foreach (EventLogEntry entry in msg)
                        if (entry.InstanceId == 5175)
                            Console.WriteLine($"Read Success:{entry.Message}, Event ID:{entry.InstanceId}, Category:{entry.Category}");// Return Overview   
                }
            }
        }
        /// <summary>
        /// Delete All Service and Application Event Viewer Entries
        /// </summary>
        [TestMethod]
        public void TestMethod5AppAndServicesDelete()
        {
            using (EventAppLog asl = new EventAppLog())
            {
                Boolean flag;
                // Get User Consent
                MessageBoxResult rsl = MessageBox.Show("Delete All Confirm?", "Delete All", MessageBoxButton.YesNoCancel);
                if (rsl == MessageBoxResult.Yes)// Confirm Delete Action
                {
                    flag = asl.DeleteEventLog();
                    Console.WriteLine($"Deleted:{flag}");// Return Overview   
                }
            }
        }
    }
}