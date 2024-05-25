using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UTPSOAV
{
    /// <summary>
    /// Summary description for UnitTestSerialisation
    /// </summary>
    [TestClass]
    public class UnitTestSerialisation
    {        
        [TestMethod]
        public void TestMethodREST()
        {
            #region Create an instance


            SOA.XML.Request.Request e = new SOA.XML.Request.Request
            {
                Code = "0",
                Info = "Success"
            };

            SOA.XML.Response.MakeCallResult r = new SOA.XML.Response.MakeCallResult
            {
                Error = false,
                Data = new SOA.XML.Response.Data
                {
                    Code = "1",
                    Message = "Send"
                },
                Request = new SOA.XML.Response.Request
                {
                    Code = "1",
                    Info = "Send"
                }
            };
            
            #endregion
            string msg = JsonConvert.SerializeObject(e);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            // Make Call
            SOAV.Component.HttpClientSOA.ServiceName = "Demo REST Service";
            SOAV.Component.HttpClientSOA.ServiceUri = "http://localhost:8080/RestService/Host/MakeCall";
            (Object rsl, string str) = SOAV.Component.HttpClientSOA.ConnectServiceREST<SOA.XML.Request.Request, SOA.XML.Response.MakeCallResult>(e, r).GetAwaiter().GetResult();
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");
            Console.WriteLine(JsonConvert.SerializeObject(rsl));
        }
        [TestMethod]
        public void TestMethodXML()
        {
            #region Create an instance
            SOA.XML.Request.Envelope e = new SOA.XML.Request.Envelope
            {
                Body = new SOA.XML.Request.Body
                {
                    MakeCall = new SOA.XML.Request.MakeCall
                    {
                        Request = new SOA.XML.Request.Request
                        {
                            Code = "0",
                            Info = "Success"
                        }
                    }
                }
            };
            SOA.XML.Response.Envelope r = new SOA.XML.Response.Envelope
            {
                Body = new SOA.XML.Response.Body
                {
                    MakeCallResponse = new SOA.XML.Response.MakeCallResponse
                    {
                        MakeCallResult = new SOA.XML.Response.MakeCallResult
                        {
                            Error = false,
                            Data = new SOA.XML.Response.Data
                            {
                                Code = "1",
                                Message = "Send"
                            },
                            Request = new SOA.XML.Response.Request
                            {
                                Code = "1",
                                Info = "Send"
                            }
                        }
                    }
                }
            };
            #endregion
            Dictionary<string, string> Tags = new Dictionary<string, string>
                { {"xsi","http://www.w3.org/2001/XMLSchema-instance"},
                  {"xsd","http://www.w3.org/2001/XMLSchema"},
                { "soap","http://schemas.xmlsoap.org/soap/envelope/"}
                };
            string msg = SOAV.XMLObjectSOA.SerializeObjectToXml(e, Tags);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            SOAV.Component.HttpClientSOA.ServiceName = "Demo Web Service";
            SOAV.Component.HttpClientSOA.ServiceUri = "http://localhost:8080/Webservice/WebService.asmx";
            // Make Call
            (Object rsl, string str) = SOAV.Component.HttpClientSOA.ConnectServiceXML<SOA.XML.Request.Envelope, SOA.XML.Response.Envelope>(e, r, Tags).GetAwaiter().GetResult();
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");
            Console.WriteLine(SOAV.XMLObjectSOA.SerializeObjectToXml(rsl, Tags));
        }
    }
}