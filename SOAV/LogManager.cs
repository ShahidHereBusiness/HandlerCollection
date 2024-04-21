using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using static SOAV.Include.ExceptionMapping;
using static SOAV.Include.Enumeration;
using System.Reflection;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    /// File System Logging and Backed by Application Event Viewer App
    /// </summary>
    public static class LogManager
    {
        public static string RemoteAddress { get; set; }
        /// <summary>
        /// Solution Developer:
        /// File System application logging
        /// </summary>
        /// <param name="path">Local Drive:\\Folder</param>
        /// <param name="MethodName">Method of Log trace</param>
        /// <param name="logMsg">Pipe delimated or Newtonjson Param Message</param>
        /// <param name="IsResponse">False if Request or Receipt</param>
        /// <param name="exp">Exception Details Logging</param>
        /// <returns>-6:Input not recognised ,0:Success, -3:Error in Logging, 1: Unexpected Failure</returns>
        public static int FileSystemLog(string path, string MethodName, string logMsg, bool IsResponse = false, Exception exp = null)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return (int)ResponseEnum.FormatError;
                path += DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                if (!path.Substring(path.Length - 1).Contains("\\"))
                    return (int)ResponseEnum.FormatError;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!Validation.ErrorFormat(RemoteAddress))
                    RemoteAddress = RemoteAddress.Replace(":", "_");
                string path2 = path + DateTime.Now.ToString("yyyyMMddHH") + "_" + RemoteAddress + ".txt";
                StreamWriter streamWriter = (File.Exists(path2) ? File.AppendText(path2) : File.CreateText(path2));
                string expMsg = (exp != null) ? logMsg = $"{logMsg}{NewLine}{ExceptionDetails(exp)}" : string.Empty;
                streamWriter.WriteLine($"{MethodName},{((IsResponse) ? "Response" : "Receipt")}|{logMsg}");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                if (!EventViewerAppLog(MethodName + "\r\n" + logMsg + "\r\n" + ex.Message, true, "Application", 5175, 101))
                    return (int)ResponseEnum.FileSystemLogFailure;
                return (int)ResponseEnum.UnexpectedFailure;
            }
            return (int)ResponseEnum.Success;
        }
        /// <summary>
        /// Solution Developer:
        /// Event Viewer Windows Log in Application
        /// </summary>
        /// <param name="logMsg">Row wise Param Msg</param>
        /// <param name="source">Handle Event Source</param>
        /// <param name="eventId">Event Identification Number</param>
        /// <param name="category">Category Number</param>
        /// <returns>true/false</returns>
        public static bool EventViewerAppLog(string logMsg, bool IsResponse = false, string source = "Application", int eventId = 5175, short category = 101)
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(source)) return false;// Verify Event Registered
                using (System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog(source))//Log Name
                {
                    logMsg = $"{((IsResponse) ? "Response" : "Receipt")}|{logMsg}";
                    eventLog.Source = source;//Source
                    eventLog.WriteEntry(logMsg, EventLogEntryType.Information, eventId, category);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Solution Developer:
        /// Exception Handled Details Logging
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Exception string message</returns>
        private static string ExceptionDetails(Exception ex)
        {
            ErrorLineNo = ex?.StackTrace?.Substring(ex.StackTrace.Length - 7, 7) ?? "";
            ErrorMsg = ex?.GetType().Name.ToString() ?? "";
            ExType = ex?.GetType().ToString() ?? "";
            ExURL = RemoteAddress ?? "";
            ErrorLocation = ex?.Message.ToString() ?? "";

            string error = "-----------Exception Details Start-----------";
            error += $"Log Written Date:{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{NewLine}Error Line No :{ErrorLineNo}{NewLine}Error Message:{ErrorMsg}{NewLine}Exception Type:{ExType}{NewLine}Error Location :{ErrorLocation}{NewLine}Error Page Url:{ExURL}{NewLine}User Host IP:{HostAddress}{NewLine}";
            error += "-----------Exception Details End-----------";
            return error;
        }
    }
}