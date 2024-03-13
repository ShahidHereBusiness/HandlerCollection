
using System;
using System.Xml;

namespace SOAV.Include
{
    public static class ExceptionMapping
    {
        public static string NewLine { set; get; } = Environment.NewLine;
        public static string ErrorLineNo { set; get; }
        public static string ErrorMsg { set; get; }
        public static string ErrorLocation { set; get; }
        public static string ExType { set; get; }
        public static string ExURL { set; get; }
        public static string HostIP { set; get; }
        public static string HostAddress { set; get; }
        public static string RemoteAddress { set; get; }
    }
}