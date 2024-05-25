using System;
using System.IO;
using System.Xml.Serialization;

namespace SOA.XML.Request
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
        [XmlElement(ElementName = "MakeCall", Namespace = "http://tempuri.org/")]
        public MakeCall MakeCall { get; set; }

        public Body()
        {
            MakeCall = new MakeCall();
        }
    }

    public class MakeCall
    {
        [XmlElement(ElementName = "request")]
        public Request Request { get; set; }

        public MakeCall()
        {
            Request = new Request();
        }
    }

    public class Request
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Info")]
        public string Info { get; set; }
    }
}