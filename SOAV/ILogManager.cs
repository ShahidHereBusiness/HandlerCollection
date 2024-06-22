using System;
using System.Diagnostics;
using System.IO;
using static SOAV.Include.ExceptionMapping;
using static SOAV.Include.Enumeration;
using System.Web;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    /// IDisposable Interface Implementation upon
    /// File System Logging and Backed by Application Event Viewer App
    /// </summary>
    public class ILogManager : IDisposable
    {
        private static readonly string RemoteAddress = Validation.RationalizePath(HttpContext.Current?.Request.ServerVariables["REMOTE_ADDR"] ?? "App");
        /// <summary>
        /// Traceability Identification
        /// </summary>
        private string TID { get; set; }
        public ILogManager()
        {
            TID = $"{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}{new Random(Guid.NewGuid().GetHashCode()).Next()}";
        }
        /// <summary>
        /// Solution Developer:
        /// File System application logging
        /// </summary>
        /// <param name="path">Local Drive:\\Folder</param>
        /// <param name="MethodName">Method of Log trace</param>
        /// <param name="logMsg">Pipe delimated or Newtonjson Param Message</param>
        /// <param name="ResponseMsg">Resultant Message Response</param>
        /// <param name="IsResponse">False if Request or Receipt</param>
        /// <param name="exp">Exception Details Logging</param>
        /// <returns>-6:Input not recognised ,0:Success, -3:Error in Logging, 1: Unexpected Failure</returns>
        public string FileSystemLog(string path, string MethodName, string logMsg, out string ResponseMsg, bool IsResponse = false, Exception exp = null)
        {
            ResponseMsg=string.Empty;
            try
            {
                if (string.IsNullOrEmpty(path))
                    ResponseMsg = ResponseEnum.FormatError.ToString();
                path += $"{DateTime.Now.ToString("yyyy")}\\{DateTime.Now.ToString("MM")}\\{DateTime.Now.ToString("dd")}\\";
                if (!path.Substring(path.Length - 1).Contains("\\"))
                    ResponseMsg = ResponseEnum.FormatError.ToString();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string path2 = $"{path}{DateTime.Now:yyyyMMddHH}_{RemoteAddress}.txt";
                StreamWriter streamWriter = (File.Exists(path2) ? File.AppendText(path2) : File.CreateText(path2));
                string expMsg = (exp != null) ? logMsg = $"{logMsg}{NewLine}{ExceptionDetails(exp)}" : string.Empty;
                streamWriter.WriteLine($"{MethodName},{((IsResponse) ? "Response" : "Receipt")}|{logMsg}");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                EventViewerAppLog($"{MethodName}{NewLine}{logMsg}{NewLine}{ex.Message}", out bool response, true, "Application", 5175, 101);
                if (!response)
                    ResponseMsg = ResponseEnum.FileSystemLogFailure.ToString();
                ResponseMsg = ResponseEnum.UnexpectedFailure.ToString();
            }
            finally
            {
                ResponseMsg = string.IsNullOrEmpty(ResponseMsg) ? ResponseEnum.Success.ToString() : ResponseMsg;
            }
            return TID;
        }
        /// <summary>
        /// Solution Developer:
        /// Event Viewer Windows Log in Application
        /// </summary>
        /// <param name="logMsg">Row wise Param Msg</param>
        /// <param name="ResponseMsg">Resultant true or false Response</param>
        /// <param name="IsResponse">True or False Response</param>
        /// <param name="source">Handle Event Source</param>
        /// <param name="eventId">Event Identification Number</param>
        /// <param name="category">Category Number</param>
        /// <returns>true/false</returns>
        public string EventViewerAppLog(string logMsg, out bool ResponseMsg, bool IsResponse = false, string source = "Application", int eventId = 5175, short category = 101)
        {
            try
            {
                if (!EventLog.SourceExists(source)) ResponseMsg = false;// Verify Event Registered
                using (EventLog eventLog = new EventLog(source))//Log Name
                {
                    logMsg = $"{((IsResponse) ? "Response" : "Receipt")}|{logMsg}";
                    eventLog.Source = source;//Source
                    eventLog.WriteEntry(logMsg, EventLogEntryType.Information, eventId, category);
                }
                ResponseMsg = true;
            }
            catch
            {
                ResponseMsg = false;
            }
            return TID;
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
        /// <summary>
        /// Your Dispositions
        /// </summary>
        public void Dispose()
        {
            return;
        }
    }
}