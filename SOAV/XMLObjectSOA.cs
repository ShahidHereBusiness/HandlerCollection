using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SOAV
{
    public static class XMLObjectSOA
    {
        /// <summary>
        /// Object to XML formatted String
        /// </summary>
        /// <typeparam name="T">Source Object Type</typeparam>
        /// <param name="obj">Objecr</param>
        /// <returns>string</returns>
        public static string SerializeObjectToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return Regex.Replace(
                    Regex.Replace(writer.ToString(), @"\t|\n|\r", string.Empty),
                    @"(<=>).*(=<)", string.Empty);
            }
        }
        /// <summary>
        /// Object with Tag xmlns Information to XML formatted String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObjectToXml<T>(T obj, Dictionary<string, string> Tags)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            // Read and tag to namespace          
            if (Tags.Count > 0)
            {
                foreach (var item in Tags)
                    emptyNamespaces.Add(item.Key, item.Value);
            }
            var serializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, emptyNamespaces);
                return stream.ToString().Replace("_x003A_", ":");
            }
        }
        /// <summary>
        /// SOAP string to Object
        /// </summary>
        /// <typeparam name="T">Target Object Type</typeparam>
        /// <param name="xmlString">string</param>
        /// <returns>Object</returns>
        public static T DeserializeXmlToObject<T>(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
