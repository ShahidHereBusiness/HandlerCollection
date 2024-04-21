using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SOAV
{
    public static class EnvironmentState
    {
        public static string PublicKey { get; set; } = "<RSAKeyValue><Modulus>1OxXlILr7dKQK/9zPAuvnbI89TZjeDVv5kLsISuHhSBnHe3zgeHmMZWkgah2y1cbH8XzUgvBv8E/Z9UiPOhd6pjyllez8/G+YE4wT0U8NgfUGAIl0lpiV2rj/MVx2TrjnPJXU4vkDulaem6eF+Jl0+HGC0qOuVykEkSt64hR9PrB0/GSLdADESdKke0fltRobYQMEbm55U5/BdgH754r10fk1UA5UjnKSNAYS5id7NnT55YD+C8AWyLZOfn1vmwmQILt3oPhP55kbmTpjJ6/S03FpmWwh0A8Wl5yGvxIPWLdZmGBe3mKrEIYTkSuP2z08DynYl4W0gTtrtF4XuDwMQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string PrivateKey { get; set; } = "<RSAKeyValue><Modulus>1OxXlILr7dKQK/9zPAuvnbI89TZjeDVv5kLsISuHhSBnHe3zgeHmMZWkgah2y1cbH8XzUgvBv8E/Z9UiPOhd6pjyllez8/G+YE4wT0U8NgfUGAIl0lpiV2rj/MVx2TrjnPJXU4vkDulaem6eF+Jl0+HGC0qOuVykEkSt64hR9PrB0/GSLdADESdKke0fltRobYQMEbm55U5/BdgH754r10fk1UA5UjnKSNAYS5id7NnT55YD+C8AWyLZOfn1vmwmQILt3oPhP55kbmTpjJ6/S03FpmWwh0A8Wl5yGvxIPWLdZmGBe3mKrEIYTkSuP2z08DynYl4W0gTtrtF4XuDwMQ==</Modulus><Exponent>AQAB</Exponent><P>58QlmWs3HK49/bFGQnLp97nwqxUZ4p8BduIcF2F+WWWJAoKp0sMlMOYCkdHnmurk2vrMjOiJNQ2xdW7p/jSYrUtJnrxo+nlfyAIAdW4wR/Bm6G85CGXskENaaLE4VMYns6kbGg3KEE9DfVUUGS6kxsGU8NbMfFWU5AcqwBUoXds=</P><Q>6y/Pt4v3qnwNxrtRf10gab3dYErQBijZrEVSBcPNlISvsH1PbSL2T96BHkyEtSanHINIy6hTFMDJVlO+cPbWb42sxVDxUi24RrzwjE3Rg4ejwVN1Iaj8E4vglcOtLJOoHs8jisFOHqmITo1BqxI7gf6YG3LGucU1rGzubXCEVeM=</Q><DP>uaXUuF+5da/c2Cn4LaH+6AB5V5E/etgDREc6WUAvBUzfwrMHdVv7nn+f705ER0OArifXUceyoFynmXmO1aEr6yQzhINHU9uFbaCs3WO2KOnYhLcS6Dc8lxJciR/sD3vqsW7z1prVENttJCfSQZmrQ8osk/57Ld0ftshG5jYMb6E=</DP><DQ>6gf+vBgVENo3Xz3BYGsMOQ0NQKfRj3+Q88NbVo88gID0zzwzEFgNIPc6Jytfl1/qRQ5DHx3V5r3c3AGjbYfDZOhnN0ZPjUYfrIyRFhzN0wPjqRpiYWdlyBGWH5HDPquETXw0Uvv+v7ZWBEy+Pvx6qOXu+MljBTNAMN+I9vBTT58=</DQ><InverseQ>fYjXVJnv+SNgcyvYzBLlFyeqHMBwVTNBP9mvgyUfJK8ry8RiardSsczY/Aa+UhS4i/OAsQGzUE9g5r0gpoyTt9UEcjVUl6oY6/IDEL7auFTkMpJdgQIwNMKxLF2RzUdkWjzM1j1zrrkQlka5BhASs9/tbpzvsX1KRAikawjO564=</InverseQ><D>xTfQe0e0/gi973LiKqjsVlXErqBdC/f7GL29949ZLE7yFD/V/+Tcker4f/wpZHjfL9PrZ4BqSpHVh/hbiSz16JkYgXbkwLyWyJ9DkfkgJy+jHUXIAIolcrEYUYYUyNAKCdJSXplBeSpd2u9g0eCqztHd3ZGGl1yewCkLKxGOUCjPIXqGcPJxCIknPKdQgBOa1LZq+Ef0eqHiYyWaCyKYJ16emtztpF59W+RlzgmuZ8Y7tSEwIjjzy17RckZ6LuXfcyEyeiFYeNpcvXHPM2RdV75eM9NG42rHaYFB2oJYYGxPgHmcCrL2/EhDyul1pYho6O9bimBIOo790ajn8z22DQ==</D></RSAKeyValue>";
       /// <summary>
       /// Create Environment Variable in Windows System Properties
       /// </summary>
       /// <param name="surrogate">Your string</param>
       /// <param name="evName">Environment Vairable Name</param>
       /// <param name="location">Variable Location</param>
       /// <param name="AES">false:RSA Encryption</param>
       /// <returns>true/false</returns>
        public static bool EVCreate(string surrogate, string evName, EnvironmentVariableTarget location, bool AES = false)
        {
            try
            {
                // Encrypt
                if (AES)
                    surrogate = SOAV.CoverAES.EncryptStringAES(surrogate);
                else
                    surrogate = SOAV.CoverRSAsme.EncryptString(surrogate, PublicKey);

                // Set an environment variable
                Environment.SetEnvironmentVariable(evName, surrogate, location);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Length > 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Get Environment Variable from Windows System Properties
        /// </summary>
        /// <param name="surrogate">Your string</param>
        /// <param name="evName">Environment Vairable Name</param>
        /// <param name="location">Variable Location</param>
        /// <param name="AES">false:RSA Encryption</param>
        /// <returns>true/false</returns>
        public static string EVFetch(string evName, EnvironmentVariableTarget location, bool AES = false)
        {
            string surrogate = string.Empty;
            try
            {
                // Get the value of an environment variable
                surrogate = Environment.GetEnvironmentVariable(evName, location) ?? "";
                if (string.IsNullOrEmpty(surrogate))
                    return null;
                // Decrypt
                if (AES)
                    surrogate = SOAV.CoverAES.DecryptStringAES(surrogate);
                else
                    surrogate = SOAV.CoverRSAsme.DecryptString(surrogate, PrivateKey);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Length > 0)
                    return string.Empty;
            }
            return surrogate;
        }
        /// <summary>
        /// Unset Environment Variable from Windows System Properties
        /// </summary>
        /// <param name="evName">Environment Vairable Name</param>
        /// <param name="location">Variable Location</param>
        /// <returns>true/false</returns>
        public static bool EVRemove(string evName, EnvironmentVariableTarget location)
        {
            try
            {
                // Remove an environment variable
                Environment.SetEnvironmentVariable(evName, null, location);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Length > 0)
                    return false;
            }
            return true;
        }
    }
}
