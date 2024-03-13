using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    /// Compression of String and Content File System writing
    /// </summary>
    public class Compression
    {
        /// <summary>
        /// Solution Developer:
        /// String Datatype Compression
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        /// <summary>
        /// Solution Developer:
        /// String  Datatype Uncompression
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Unzip(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        /// <summary>
        /// Solution Developer:
        /// Byte array to Write File
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="filePath"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool SaveByteArrayToFile(byte[] fileContent, string filePath="\\fileName", string extension= ".xlsx")
        {
            if (fileContent.Length > 0 && !string.IsNullOrEmpty(filePath))
            {
                string fileName = filePath + $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{new Random().Next()}{extension}";
                File.WriteAllBytes(fileName, fileContent);
            }
            else
                return false;
            return true;
        }
        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

    }
}