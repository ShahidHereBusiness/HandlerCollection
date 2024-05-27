using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Xml.Serialization;
using Indorse = SOAV.Validation;

namespace SOA.XML.MakeResponse
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body")]
        public Body Body { get; set; }

        public Envelope()
        {
            Body = new Body();
        }
    }

    public class Body
    {
        [XmlElement(ElementName = "MakeCallResponse", Namespace = "http://tempuri.org/")]
        public MakeCallResponse MakeCallResponse { get; set; }

        public Body()
        {
            MakeCallResponse = new MakeCallResponse();
        }
    }

    public class MakeCallResponse
    {
        [XmlElement(ElementName = "MakeCallResult")]
        public MakeCallResult MakeCallResult { get; set; }

        public MakeCallResponse()
        {
            MakeCallResult = new MakeCallResult();
        }
    }

    public class MakeCallResult
    {
        [XmlElement(ElementName = "error")]
        public bool Error { get; set; }

        [XmlElement(ElementName = "data")]
        public Data Data { get; set; }

        [XmlElement(ElementName = "request")]
        public Request Request { get; set; }

        public MakeCallResult()
        {
            Data = new Data();
            Request = new Request();
        }
    }

    public class Data
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Message")]
        public string Message { get; set; }
    }

    public class Request
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Info")]
        public string Info { get; set; }
        private bool Status { get; set; }//Localize
        /// <summary>
        /// Return True if Response is desirable
        /// </summary>
        /// <returns></returns>
        public bool GetStatus()//Localize
        {
            if (Indorse.ErrorType(Info) || Indorse.ErrorType(Code))
                Status = false;
            else
                Status = true;
            return Status;
        }
    }
}