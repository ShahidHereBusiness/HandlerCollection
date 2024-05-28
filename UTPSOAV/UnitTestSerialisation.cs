using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Outbound = SOA.XML.MakeRequest;
using Inbound = SOA.XML.MakeResponse;
using Component = SOAV.Component.HttpClientSOA;
using Serial = SOAV.XMLObjectSOA;
using System.Windows.Controls;
using Indorse = SOAV.Validation;

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
            Outbound.Request reqObj = new Outbound.Request
            {
                Code = "0",
                Info = "Success"
            };

            Inbound.MakeCallResult respObj = new Inbound.MakeCallResult { };

            #endregion
            string msg = JsonConvert.SerializeObject(reqObj);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            // Make Call
            Component.ServiceName = "Demo REST Service";
            Component.ServiceUri = "http://localhost:8080/RestService/Host/MakeCall";
            (Object rsl, string str) = Component.ConnectServiceREST<Outbound.Request, Inbound.MakeCallResult>(reqObj, respObj).GetAwaiter().GetResult();
            respObj = (Inbound.MakeCallResult)rsl;
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");

            if (Indorse.ErrorType(respObj))
                Console.WriteLine("Service Unavaiable");
            else if (!respObj.Error)
                Console.WriteLine("Request Successful");
            else if (respObj.Data.GetStatus())//Localize
                Console.WriteLine(JsonConvert.SerializeObject(respObj));
            else
                Console.WriteLine("Response Invalidated");
        }
        [TestMethod]
        public void TestMethodXML()
        {
            #region Create an instance
            Outbound.Envelope reqObj = new Outbound.Envelope
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
            Inbound.Envelope respObj = new Inbound.Envelope { };

            #endregion
            Dictionary<string, string> Tags = new Dictionary<string, string>
                { {"xsi","http://www.w3.org/2001/XMLSchema-instance"},
                  {"xsd","http://www.w3.org/2001/XMLSchema"},
                { "soap","http://schemas.xmlsoap.org/soap/envelope/"}
                };
            string msg = Serial.SerializeObjectToXml(reqObj, Tags);
            Console.WriteLine($"Request Sent:{Environment.NewLine}{msg}{Environment.NewLine}");
            Component.ServiceName = "Demo Web Service";
            Component.ServiceUri = "http://localhost:8080/Webservice/WebService/";
            // Make Call
            (Object rsl, string str) = Component.ConnectServiceXML<Outbound.Envelope, Inbound.Envelope>(reqObj, respObj, Tags).GetAwaiter().GetResult();
            respObj = (Inbound.Envelope)rsl;
            Console.WriteLine($"Response Received:{str}{Environment.NewLine}");

            if(Indorse.ErrorType(respObj))
                Console.WriteLine("Service Unavaiable");
            else if (!respObj.Body.MakeCallResponse.MakeCallResult.Error)
                Console.WriteLine("Request Successful");
            else if (respObj.Body.MakeCallResponse.MakeCallResult.Data.GetStatus())//Localized
                Console.WriteLine(Serial.SerializeObjectToXml(respObj, Tags));
            else
                Console.WriteLine("Response Invalidated");
        }
    }
}