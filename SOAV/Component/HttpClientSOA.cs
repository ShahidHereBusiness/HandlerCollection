using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SOAV.Include.Enumeration;
using Newtonsoft.Json;
using System.Xml.Serialization;
using SOAV.Include;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;

namespace SOAV.Component
{
    public static class HttpClientSOA
    {
        public static string ServiceName { get; set; }
        public static string ServiceUri { get; set; }
        public static Double TimeOut { get; set; }
        public static DateTime StartTime { get; set; }
        public static string LogID { get; set; }
        public static string LogPath { get; set; }
        public static Dictionary<string, string> HEADERS { get; set; }
        public static bool SkipCertificate { get; set; }
        public static string BASIC_AUTH { get; set; }
        public static bool SPMContinue { get; set; }
        public static string WSSAccessSpecifier { get; set; }
        public static string WSSWatchWord { get; set; }
        public static SecurityProtocolType TLS { get; set; }
        public static async Task<Object> ConnectServiceREST<C, R>(C requestObj, R responseObj)
        {
            #region Validation & Initialization
            //if (string.IsNullOrEmpty(ServiceName))
            //    return ErrorFormat("ServiceName Required");
            //if (string.IsNullOrEmpty(ServiceUri))
            //    return ErrorFormat("ServiceUri Required");
            //if (requestObj == null)
            //    return ErrorFormat("Request Creation Required");
            if (StartTime == null)
                StartTime = DateTime.Now;
            if (string.IsNullOrEmpty(LogID))
                LogID = $"{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}{new Random(Guid.NewGuid().GetHashCode()).Next()}";
            if (string.IsNullOrEmpty(LogPath))
                LogPath = "\\SOALog";
            #endregion

            string jsonResponse = string.Empty;
            string logResponse = string.Empty;
            string contentType = "application/json";
            try
            {
                #region Make Receipt Diminish
                logResponse = $"{ServiceName} REST SOA Receipt => Start Time: {StartTime}, LogID: {LogID}, Parameters: {JsonConvert.SerializeObject(requestObj)}";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceREST", logResponse);
                #endregion

                using (HttpClient httpClient = new HttpClient())
                {
                    if (TimeOut > 0)
                        httpClient.Timeout = TimeSpan.FromSeconds(TimeOut);
                    httpClient.BaseAddress = new Uri(ServiceUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    // Read all Header Pair
                    if (HEADERS.Count > 0)
                    {
                        foreach (var item in HEADERS)
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                    // Basic Authentication
                    if (!string.IsNullOrEmpty(BASIC_AUTH))
                    {
                        string authHeaders = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{BASIC_AUTH}"));//UserName:Password
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaders);
                    }
                    // Headers Content Type
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    #region SPM
                    // Set for Large Data Sending
                    if (SPMContinue)
                        ServicePointManager.Expect100Continue = true;
                    if (TLS > 0)
                        ServicePointManager.SecurityProtocol = TLS;//Tls13
                    // Skip SSL/HTTPS Certificate Validation
                    if (SkipCertificate)
                        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;// Supress SSL Certificate
                    #endregion
                    // Serialize Object to REST
                    string contentData = JsonConvert.SerializeObject(requestObj);
                    // Content Type
                    StringContent contents = new StringContent(contentData, Encoding.UTF8, contentType);
                    // Send a POST await request
                    HttpResponseMessage msg = await httpClient.PostAsync(ServiceUri, contents).ConfigureAwait(false);
                    // Read POST Web Response                    

                    if (msg.IsSuccessStatusCode)
                    {
                        jsonResponse = await msg.Content.ReadAsStringAsync();
                        if (jsonResponse.Length > 0)
                            responseObj = JsonConvert.DeserializeObject<R>(jsonResponse);
                        else
                            throw new Exception(((int)ResponseEnum.UnhandledServing).ToString());
                    }
                    else
                        throw new Exception(msg.ToString());

                    return responseObj;
                }
            }
            catch (Exception ex)
            {
                #region Make Exception Diminish
                logResponse = $"{ServiceName} VAS SOA Exception => Start Time: {StartTime}, LogID: {LogID}, TimeSpan:{(DateTime.Now - StartTime).ToString()}, Parameters: {JsonConvert.SerializeObject(responseObj)}";
                ExceptionMapping.ErrorLineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7) ?? "";
                ExceptionMapping.ErrorMsg = ex.GetType().Name.ToString() ?? "";
                ExceptionMapping.ExType = ex.GetType().ToString() ?? "";
                ExceptionMapping.ExURL = "::1" ?? "";
                ExceptionMapping.ErrorLocation = ex.Message.ToString() ?? "";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceREST", logResponse, true, ex);
                #endregion
            }
            finally
            {
                #region Make Response Diminish
                logResponse = $"{ServiceName} REST SOA Response => Start Time: {StartTime}, LogID: {LogID}, TimeSpan:{(DateTime.Now - StartTime).ToString()}, Parameters: {jsonResponse}";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceREST", logResponse, true);
                #endregion
            }
            return null;
        }
        public static async Task<Object> ConnectServiceSOAP<C, R>(C requestObj, R responseObj)
        {
            #region Validation & Initialization
            //if (string.IsNullOrEmpty(ServiceName))
            //    return ErrorFormat("ServiceName Required");
            //if (string.IsNullOrEmpty(ServiceUri))
            //    return ErrorFormat("ServiceUri Required");
            //if (requestObj == null)
            //    return ErrorFormat("Request Creation Required");
            if (StartTime == null)
                StartTime = DateTime.Now;
            if (string.IsNullOrEmpty(LogID))
                LogID = $"{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}{new Random(Guid.NewGuid().GetHashCode()).Next()}";
            if (string.IsNullOrEmpty(LogPath))
                LogPath = "\\SOALog";
            #endregion

            string jsonResponse = string.Empty;
            string logResponse = string.Empty;
            try
            {
                #region Make Receipt Diminish
                logResponse = $"{ServiceName} SOAP SOA Receipt => Start Time: {StartTime}, LogID: {LogID}, Parameters: {SerializeObjectToXml(requestObj)}";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceSOAP", logResponse);
                #endregion

                using (HttpClient httpClient = new HttpClient())
                {
                    if (TimeOut > 0)
                        httpClient.Timeout = TimeSpan.FromSeconds(TimeOut);
                    httpClient.BaseAddress = new Uri(ServiceUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    string contentType = "text/xml";
                    // Set the content type
                    httpClient.DefaultRequestHeaders.Add("ContentType", contentType);
                    // Serialize Object to SOAP
                    string contentData = SerializeObjectToXml(requestObj);
                    HttpContent content = new StringContent(contentData, Encoding.UTF8, contentType);
                    // Create a new HttpRequestMessage
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ServiceUri);
                    // Add WS-Security header
                    if (!string.IsNullOrEmpty(WSSAccessSpecifier) && !string.IsNullOrEmpty(WSSWatchWord))
                        request.Headers.Add("X-WSSE", $"UsernameToken Username=\"{WSSAccessSpecifier}\", Password=\"{WSSWatchWord}\"");
                    // Read all Header Pair
                    if (HEADERS.Count > 0)
                    {
                        foreach (var item in HEADERS)
                            request.Headers.Add(item.Key, item.Value);
                    }
                    // Set the content
                    request.Content = content;
                    #region SPM
                    // Set for Large Data Sending
                    if (SPMContinue)
                        ServicePointManager.Expect100Continue = true;
                    if (TLS > 0)
                        ServicePointManager.SecurityProtocol = TLS;//Tls13
                    // Skip SSL/HTTPS Certificate Validation
                    if (SkipCertificate)
                        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;// Supress SSL Certificate
                    #endregion
                    // Send a POST await request
                    HttpResponseMessage msg = await httpClient.SendAsync(request).ConfigureAwait(false);
                    // Read POST Web Response                    
                    if (msg.IsSuccessStatusCode)
                    {
                        jsonResponse = await msg.Content.ReadAsStringAsync();
                        if (jsonResponse.Length > 0)
                            responseObj = DeserializeXmlToObject<R>(await msg.Content.ReadAsStringAsync());
                        else
                            throw new Exception(((int)ResponseEnum.UnhandledServing).ToString());
                    }
                    else
                        throw new Exception(msg.ToString());

                    return responseObj;
                }
            }
            catch (Exception ex)
            {
                #region Make Exception Diminish
                logResponse = $"{ServiceName} SOAP SOA Exception => Start Time: {StartTime}, LogID: {LogID}, TimeSpan:{(DateTime.Now - StartTime).ToString()}, Parameters: {SerializeObjectToXml(responseObj)}";
                ExceptionMapping.ErrorLineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7) ?? "";
                ExceptionMapping.ErrorMsg = ex.GetType().Name.ToString() ?? "";
                ExceptionMapping.ExType = ex.GetType().ToString() ?? "";
                ExceptionMapping.ExURL = "::1" ?? "";
                ExceptionMapping.ErrorLocation = ex.Message.ToString() ?? "";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceSOAP", logResponse, true, ex);
                #endregion
            }
            finally
            {
                #region Make Response Diminish
                logResponse = $"{ServiceName} SOAP SOA Response => Start Time: {StartTime}, LogID: {LogID}, TimeSpan:{(DateTime.Now - StartTime).ToString()}, Parameters: {jsonResponse}";
                LogManager.FileSystemLog(LogPath, "HttpClientSOA:ConnectServiceSOAP", logResponse, true);
                #endregion
            }
            return null;
        }
        //private static async Task<Object> RESTWSSConnectService<T>(T requestData)
        //{
        //    #region Validation
        //    if (string.IsNullOrEmpty(ServiceName))
        //        return ErrorFormat("ServiceName Required");
        //    if (string.IsNullOrEmpty(ServiceUri))
        //        return ErrorFormat("ServiceUri Required");
        //    if (requestData == null)
        //        return ErrorFormat("RequestData Required");
        //    #endregion

        //    string jsonResponse = string.Empty;
        //    string logResponse = string.Empty;
        //    Object response = new SubscriptionResponse
        //    {
        //        error = true,
        //        data = new ErrorData
        //        {
        //            code = ((int)ResponseEnum.UnexpectedFailure).ToString(),
        //            message = "Opps something is broken!"
        //        }
        //    };
        //    try
        //    {
        //        #region Receipt Diminish
        //        logResponse = $"{ServiceName} REST SOA Receipt => Start Time: {StartTime}, LogID: {LogID}, Parameters: {JsonConvert.SerializeObject(requestData)}";
        //        FileAppLog.FSLog(LogPath, "HttpClientSOA:ConnectRESTService", logResponse);
        //        #endregion

        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            if (TimeOut > 0)
        //                httpClient.Timeout = TimeSpan.FromSeconds(TimeOut);
        //            httpClient.BaseAddress = new Uri(ServiceUri);
        //            httpClient.DefaultRequestHeaders.Accept.Clear();
        //            // Set the content type
        //            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
        //            // Serialize Object to REST
        //            string contentData = JsonConvert.SerializeObject(requestData);
        //            HttpContent content = new StringContent(contentData, Encoding.UTF8, "application/json");
        //            // Create a new HttpRequestMessage
        //            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ServiceUri);
        //            // Add WS-Security header
        //            if (!string.IsNullOrEmpty(WSSAccessSpecifier) && !string.IsNullOrEmpty(WSSWatchWord))
        //                request.Headers.Add("X-WSSE", $"UsernameToken Username=\"{WSSAccessSpecifier}\", Password=\"{WSSWatchWord}\"");
        //            // Read all Header Pair
        //            if (HEADERS.Count > 0)
        //            {
        //                foreach (var item in HEADERS)
        //                    request.Headers.Add(item.Key, item.Value);
        //            }
        //            // Set the content
        //            request.Content = content;
        //            #region SPM
        //            // Set for Large Data Sending
        //            if (SPMContinue)
        //                ServicePointManager.Expect100Continue = true;
        //            if (TLS != null)
        //                ServicePointManager.SecurityProtocol = TLS;//Tls13
        //            // Skip SSL/HTTPS Certificate Validation
        //            if (SkipCertificate)
        //                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;// Supress SSL Certificate
        //            #endregion
        //            // Send a POST await request
        //            HttpResponseMessage msg = await httpClient.SendAsync(request).ConfigureAwait(false);
        //            // Read POST Web Response                    
        //            if (msg.IsSuccessStatusCode)
        //            {
        //                jsonResponse = await msg.Content.ReadAsStringAsync();
        //                if (jsonResponse.Contains("error\":true"))
        //                {
        //                    response = JsonConvert.DeserializeObject<SubscriptionResponse>(await msg.Content.ReadAsStringAsync());
        //                }
        //                else if (jsonResponse.Contains("error\":false"))
        //                {
        //                    response = new SubscriptionResponse { error = false, data = null };
        //                }
        //                else //Suppress External's SOA to Object
        //                {
        //                    response = JsonConvert.DeserializeObject<Object>(await msg.Content.ReadAsStringAsync());
        //                }
        //            }
        //            else
        //            {
        //                response = new SubscriptionResponse
        //                {
        //                    error = true,
        //                    data = new ErrorData
        //                    {
        //                        code = ((int)msg.StatusCode).ToString(),
        //                        message = msg.ReasonPhrase  ((int)ResponseEnum.UnhandledServing).ToString()
        //                    }
        //                };
        //            }

        //            #region Make Diminish
        //            logResponse = $"{ServiceName} REST SOA Results => Start Time: {StartTime}, LogID: {LogID}, Parameters: {JsonConvert.SerializeObject(response)}";
        //            FileAppLog.FSLog(LogPath, "HttpClientSOA:ConnectRESTService", logResponse, true);
        //            #endregion
        //            return response;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Make Diminish
        //        logResponse = $"{ServiceName} VAS SOA Exception => Start Time: {StartTime}, LogID: {LogID}";
        //        FileAppLog.FSLog(LogPath, "HttpClientSOA:ConnectRESTService", logResponse, true, ex);
        //        #endregion
        //        return new SubscriptionResponse
        //        {
        //            error = true,
        //            data = new ErrorData
        //            {
        //                code = ((int)ResponseEnum.UnhandledServing).ToString(),
        //                message = (ex.ToString().Length > 0)  ?"We are facing disconnect at the moment!!" : "Something is missing!"
        //            }
        //        };
        //    }
        //    finally
        //    {
        //        #region Make Diminish
        //        logResponse = $"{ServiceName} REST SOA Finally Results => Start Time: {StartTime}, TimeSpan:{(DateTime.Now - StartTime).ToString()}, LogID: {LogID}, Parameters: {jsonResponse}";
        //        FileAppLog.FSLog(LogPath, "HttpClientSOA:ConnectRESTService", logResponse, true);
        //        #endregion
        //    }
        //}
        /// <summary>
        /// Object to SOAP string
        /// </summary>
        /// <typeparam name="T">Source Object Type</typeparam>
        /// <param name="obj">Objecr</param>
        /// <returns>string</returns>
        private static string SerializeObjectToXml<T>(T obj)
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
        /// SOAP string to Object
        /// </summary>
        /// <typeparam name="T">Target Object Type</typeparam>
        /// <param name="xmlString">string</param>
        /// <returns>Object</returns>
        private static T DeserializeXmlToObject<T>(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}