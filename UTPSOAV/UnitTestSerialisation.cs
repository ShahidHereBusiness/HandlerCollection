using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Outbound = SOA.XML.MakeRequest;
using Inbound = SOA.XML.MakeResponse;
using Component = SOAV.Component.HttpClientSOA;
using Serial = SOAV.XMLObjectSOA;

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
            Outbound.Request e = new Outbound.Request
            {
                Code = "0",
                Info = "Success"
            };

            Inbound.MakeCallResult r = new Inbound.MakeCallResult
            {
                Error = false,
                Data = new Inbound.Data
                {
                    Code = "1",
                    Message = "Send"
                },
                Request = new Inbound.Request
                {
                    Code = "1",
                    Info = "Send"
                }
            };
            #endregion
            string msg = JsonConvert.SerializeObject(e);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            // Make Call
            Component.ServiceName = "Demo REST Service";
            Component.ServiceUri = "http://localhost:8080/RestService/Host/MakeCall";
            (Object rsl, string str) = Component.ConnectServiceREST<Outbound.Request, Inbound.MakeCallResult>(e, r).GetAwaiter().GetResult();
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");
            r = (Inbound.MakeCallResult)rsl;
            if (r.Request.GetStatus())//Localize
                Console.WriteLine(JsonConvert.SerializeObject(r));
            else
                Console.WriteLine("Response Invalidated");
        }
        [TestMethod]
        public void TestMethodXML()
        {
            #region Create an instance
            Outbound.Envelope e = new Outbound.Envelope
            {
                Body = new Outbound.Body
                {
                    MakeCall = new Outbound.MakeCall
                    {
                        Request = new Outbound.Request
                        {
                            Code = "0",
                            Info = "Success"
                        }
                    }
                }
            };
            Inbound.Envelope r = new Inbound.Envelope
            {
                Body = new Inbound.Body
                {
                    MakeCallResponse = new Inbound.MakeCallResponse
                    {
                        MakeCallResult = new Inbound.MakeCallResult
                        {
                            Error = false,
                            Data = new Inbound.Data
                            {
                                Code = "1",
                                Message = "Send"
                            },
                            Request = new Inbound.Request
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
            string msg = Serial.SerializeObjectToXml(e, Tags);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            Component.ServiceName = "Demo Web Service";
            Component.ServiceUri = "http://localhost:8080/Webservice/WebService/";
            // Make Call
            (Object rsl, string str) = Component.ConnectServiceXML<Outbound.Envelope, Inbound.Envelope>(e, r, Tags).GetAwaiter().GetResult();
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");
            
            if (r.Body.MakeCallResponse.MakeCallResult.Request.GetStatus())//Localized
                Console.WriteLine(Serial.SerializeObjectToXml(rsl, Tags));
            else
                Console.WriteLine("Response Invalidated");
        }
    }
}