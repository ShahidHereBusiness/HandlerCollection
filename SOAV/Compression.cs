using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            byte[] buffer = Encoding.UTF8.GetBytes(str);

            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    deflateStream.Write(buffer, 0, buffer.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        /// <summary>
        /// Solution Developer:
        /// String  Datatype Uncompression
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnZip(string str)
        {
            byte[] compressedData = Convert.FromBase64String(str);
            using (MemoryStream ms = new MemoryStream(compressedData))
            {
                using (DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedMs = new MemoryStream())
                    {
                        deflateStream.CopyTo(decompressedMs);
                        byte[] decompressedBytes = decompressedMs.ToArray();
                        return Encoding.UTF8.GetString(decompressedBytes);
                    }
                }
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
        public static bool SaveByteArrayToFile(byte[] fileContent, string filePath = "\\fileName", string extension = ".xlsx")
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
    }
}