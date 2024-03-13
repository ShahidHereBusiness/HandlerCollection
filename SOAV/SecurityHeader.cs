using System;
using System.Globalization;
using System.Net.Sockets;

namespace SOAV
{
    public class SecurityHeader
    {
        /// <summary>
        /// Solution Developer:
        /// Access Specifier Key String
        /// </summary>
        public string AccessSpecifier { set; get; }
        /// <summary>
        /// Solution Developer:
        /// Watch Words Pass Key String
        /// </summary>
        public string WatchWords { set; get; }
        /// <summary>
        /// Solution Developer:
        /// Credentials are granted
        /// </summary>
        public Boolean IsConfigValid { set; get; } = false;
        /// <summary>
        /// Solution Developer:
        /// Logging File System Path
        /// </summary>
        private string Path { set; get; }
        private string[] accessUserArray;
        private string[] watchPassArray;
        private string[] expiryAlertArray;
        /// <summary>
        /// Solution Developer:
        /// Constructor Method Web.config Configuration:
        ///     Path
        ///     OwnerSecret
        ///     Secret
        ///     SecretExpiry
        /// </summary>
        public SecurityHeader(string path, string OwnersSecret, string Secrets, string SecretsExpiry)
        {
            string msg = string.Empty;
            Exception exp = new Exception("Empty");
            try
            {
                if (string.IsNullOrEmpty(path) ||
                    string.IsNullOrEmpty(OwnersSecret) ||
                    string.IsNullOrEmpty(Secrets) ||
                    string.IsNullOrEmpty(SecretsExpiry))
                    throw new Exception("Blank Parameters");
                //Configuration
                Path = $"{path}\\Configuration\\";
                this.accessUserArray = OwnersSecret?.ToString().Split('|');
                this.watchPassArray = Secrets?.ToString().Split('|');
                this.expiryAlertArray = SecretsExpiry?.ToString().Split('|');
                IsConfigValid = true;
            }
            catch (Exception ex)
            {
                exp = ex;
                msg = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    string logInput = $"SOAV Configuration Exception Details => Security Header TimeSpan: {DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Environment.NewLine} SOAV => SOAV.dll => Error Message:{msg}, Parameters: AccessSpecifier:{AccessSpecifier},WatchWord:{WatchWords}{Environment.NewLine}{string.Join("|", accessUserArray)}{Environment.NewLine}{string.Join("|", watchPassArray)}";
                    FileAppLog.FSLog(Path, "SecurityHeader.Authentication", logInput, true, exp);
                }
            }
        }
        /// <summary>
        /// Solution Developer:
        /// Validate Multiple AccessSpecifier & WatchWords
        /// </summary>
        /// <returns>bool</returns>
        public bool Authentication(out string errorMsg)
        {
            string msg = string.Empty;
            Exception exp = new Exception("Empty");
            errorMsg = string.Empty;
            try
            {
                if ((
                    accessUserArray?.Length < 1 ||
                    watchPassArray?.Length < 1 ||
                    expiryAlertArray?.Length < 1
                    ) ||
                    accessUserArray?.Length != watchPassArray?.Length ||
                    watchPassArray?.Length != expiryAlertArray?.Length
                    )
                    errorMsg = "Credentials are not configured.";
                else if (string.IsNullOrEmpty(AccessSpecifier) ||
                    string.IsNullOrEmpty(WatchWords))
                    errorMsg = "Credentials are required.";
                else if (Array.IndexOf(this.accessUserArray, AccessSpecifier) >= 0 && // Access Specifier Configured
                   Array.IndexOf(this.watchPassArray, WatchWords) >= 0) // Watch Word Configured
                {
                    // Validation Credentials
                    if (Array.IndexOf(this.accessUserArray, AccessSpecifier) == Array.IndexOf(this.watchPassArray, WatchWords))// AccessSpecifier & WatchWord are Paired
                    {
                        errorMsg = string.Empty;
                    }
                    else
                        errorMsg = "Credentials are mismatched.";
                }
                else
                {
                    errorMsg = "Credentials are not registered.";
                }
                #region Expiry Date
                if (string.IsNullOrEmpty(errorMsg))
                {
                    string expDate = this.expiryAlertArray[Array.IndexOf(this.accessUserArray, AccessSpecifier)];
                    DateTime dtExpiry = DateTime.ParseExact(expDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    DateTime dtGrace = dtExpiry.AddDays(7);// Add 7 days grace days in expiry

                    if (DateTime.Today > dtExpiry && dtExpiry <= dtGrace)
                        errorMsg = msg = $"Expired Credentials {dtExpiry.ToString("MM-dd-yyyy")}, Grace Days Ends {dtGrace.ToString("MM-dd-yyyy")}";
                    else if (DateTime.Today > dtGrace)
                        errorMsg = $"Credentials are expired, Expiry Dated:{dtExpiry} Graced Period:{dtGrace}";
                    else
                        return true;
                }
                #endregion                  
            }
            catch (Exception ex)
            {
                exp = ex;
                msg = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    string logInput = $"SOAV Authentication Exception Details => Security Header TimeSpan: {DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Environment.NewLine} SOAV => SOAV.dll => Error Message:{errorMsg}, Parameters: AccessSpecifier:{AccessSpecifier},WatchWord:{WatchWords}{Environment.NewLine}{string.Join("|", accessUserArray)}{Environment.NewLine}{string.Join("|", watchPassArray)}";
                    FileAppLog.FSLog(Path, "SecurityHeader.Authentication", logInput, true, exp);
                }
            }
            return false;
        }
        /// <summary>
        /// Solution Developer:
        /// Destructor Method
        /// </summary>
        ~SecurityHeader()
        {            
            GC.Collect();
        }
    }
}