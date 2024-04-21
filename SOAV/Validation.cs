using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    /// Attributes Validation and Formatting
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Solution Developer:
        /// Credentials Validation from System.Configuration.ConfigurationManager.AppSettings
        /// </summary>
        /// <param name="sh">AccessSpecifier and WatchWord min length 8
        /// OwnersSecret are required
        /// Secrets are required
        /// SecretsExpiry are required
        /// </param>
        /// <returns></returns>
        public static bool ErrorCredentials(string path, SecurityHeader sh)
        {
            try
            {
                if ((sh.AccessSpecifier != null && sh.WatchWords != null))
                {
                    if (sh.AccessSpecifier.Length > 7 && sh.WatchWords.Length > 7)
                    {
                        if (sh.IsConfigValid)
                        {
                            string msg = string.Empty;
                            if (sh.Authentication(out msg))
                                return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Length > 0)
                {
                    path += "\\Exception\\";
                    LogManager.FileSystemLog(path, "Validatation", "Exception", true, ex);
                    return true;
                }
            }
            return true;
        }
        /// <summary>
        /// Solution Developer:
        /// Email format
        /// </summary>
        /// <param name="mail">Email Address</param>
        /// <param name="encrypt">Is AES true or false</param>
        /// <returns></returns>
        public static bool ErrorEmail(string mail, bool encrypt)
        {
            if (string.IsNullOrEmpty(mail))
                return true;
            if (encrypt)
                mail = CoverAES.DecryptStringAES(mail);
            mail = mail.Trim().ToUpper();
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(mail, pattern))
                return true;
            return false;
        }
        /// <summary>
        /// Solution Developer:
        /// String with at length 3
        /// </summary>
        /// <param name="str">Not Null or Empty</param>
        /// <returns></returns>
        public static bool ErrorFormat(string str)
        {
            // Check if the string is not null or empty
            if (string.IsNullOrEmpty(str))
                return true;
            // Check if the string meets a minimum length 3 requirement
            if (str.Length < 3)
                return true;
            return false;
        }
        /// <summary>
        /// Solution Developer:
        /// Error when empty or null
        /// </summary>
        /// <param name="FileContent">Array length > 0</param>
        /// <param name="FileName">Not Null or Empty</param>
        /// <returns>true or false</returns>
        public static bool ErrorAttachment(Byte[] FileContent, string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                return true;
            if (FileContent == null)
                return true;
            if (FileContent.Length < 1)
                return true;
            return false;
        }
        /// <summary>
        /// Solution Developer:
        /// Make ISDN with last ten digits to 11, 12, 13 length
        /// </summary>
        /// <param name="validatedMobileNumber">Resultant Mobile Number</param>
        /// <param name="mobileNumber">Mobile Number</param>
        /// <param name="length">Length Required</param>        
        /// <returns>True (0) or False (1)</returns>
        public static bool MakeISDN(out string validatedMobileNumber, int length = 10, string mobileNumber = "3335930849")
        {
            // Verify Mobile Number is Numeric
            Double resultMobileNumber;
            bool castResult = Double.TryParse(mobileNumber, out resultMobileNumber);
            // Ask for Mimimum 10 digits length
            if (length < 10)
                length = 10;
            // Get Last 10 digits of Mobile Number
            validatedMobileNumber = mobileNumber.Substring(mobileNumber.Length - 10);

            if (!castResult || resultMobileNumber < 0) // Set Empty Mobile Number
            {
                validatedMobileNumber = mobileNumber;
                return false;
            }
            else if (length < 11)
                return true; // Set 10 digit Mobile Number
            else if (length == 11)
                validatedMobileNumber = $"0{validatedMobileNumber}"; // Set 11 digit Mobile Number
            else if (length == 12)
                validatedMobileNumber = $"92{validatedMobileNumber}"; // Set 12 digit Mobile Number
            else if (length == 13)
                validatedMobileNumber = $"+92{validatedMobileNumber}"; // Set 13 digit Mobile Number
            else
            {
                validatedMobileNumber = mobileNumber;// Input as Output Mobile Number
                return false;
            }
            return true;
        }
    }
}