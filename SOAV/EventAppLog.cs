using System;
using System.Diagnostics;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    ///  Application and Services Logging in Windows Event Viewer App
    /// </summary>
    public class EventAppLog : IDisposable
    {
        System.Diagnostics.EventLog eventLog = null; // EventLog Object
        /// <summary>
        /// Setter Getter Method for Name
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// Setter Getter Method for ID
        /// </summary>
        public int ProcessNameId { get; set; }
        /// <summary>
        /// Setter Getter Method for Code
        /// </summary>
        public short ProcessNameCode { get; set; }
        /// <summary>
        /// Setter Getter for Stage
        /// </summary>
        public string ProcessStage { get; set; }
        /// <summary>
        /// Solution Developer:
        /// Constructor Method
        /// Windows -> Event Viewer -> Application and Services Logs
        /// </summary>
        public EventAppLog(string source = "SOAevents", string logName = "SOAlogEvents")
        {
            this.ProcessNameId = 5175;// Default Value Assignment
            this.ProcessNameCode = 101;// Default Value Assignment
            this.ProcessName = string.Empty;// Default Value Assignment
            this.ProcessStage = string.Empty;// Default Value Assignment
            eventLog = new System.Diagnostics.EventLog();// New Object Instantiation
            if (!System.Diagnostics.EventLog.SourceExists(source))// Find by Source
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    source,// Source
                    logName// Log Name
                    );
            }            
        }
        /// <summary>
        /// Solution Developer:
        /// Mark Event In Windows Event Viewer
        /// </summary>
        /// <param name="msg">Event Log Description</param>
        /// <returns>True/False</returns>
        public bool MarkEventLog(string msg = "Event Viewer Message", string source = "SOAevents")
        {
            bool flag = false;
            if (!System.Diagnostics.EventLog.SourceExists(source)) return flag;    // Find by Source        
            string timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                eventLog.Source = source;
                // Write a new entry to the source.
                eventLog.WriteEntry($"{this.ProcessStage}: {this.ProcessName}:: {msg}-{timeStr}", EventLogEntryType.Information, this.ProcessNameId, this.ProcessNameCode,null);
                flag = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 0)
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Solution Developer:
        /// Read Event Log Entries by Log Name
        /// </summary>
        /// <returns>True/False</returns>
        public bool ReadEventLog(ref EventLogEntryCollection msg, bool show = true, string logName = "SOAlogEvents")
        {
            bool flag = false;
            if (!System.Diagnostics.EventLog.Exists(logName)) return flag;    // Find by Source
            try
            {
                eventLog.Log = logName;// Log Name
                if (show)
                    foreach (EventLogEntry entry in eventLog.Entries)
                    {
                        Console.WriteLine(entry.Message);
                    }
                else
                {
                    msg = eventLog.Entries;
                }
                flag = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 0)
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Solution Developer:
        /// Delete Event Source & Log Name Entries
        /// </summary>
        /// <returns>True/False</returns>
        public bool DeleteEventLog(string source = "SOAevents", string logName = "SOAlogEvents")
        {
            bool flag = false;
            try
            {
                // Delete Event Source if exists.
                if (System.Diagnostics.EventLog.SourceExists(source))
                {
                    System.Diagnostics.EventLog.DeleteEventSource(source);
                }

                // Delete the Log Name if exists
                if (System.Diagnostics.EventLog.Exists(logName))
                {
                    // Delete Source
                    System.Diagnostics.EventLog.Delete(logName);
                }
                flag = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 0)
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Solution Developer:
        /// Dispose Objects here
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            //Dispose();
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        /// <summary>
        /// Solution Developer:
        /// Destructor Method
        /// </summary>
        ~EventAppLog()
        {
            this.eventLog = null;
            GC.Collect();
        }
    }
}