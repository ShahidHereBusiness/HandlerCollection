using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTPSOAV
{
    /// <summary>
    /// Summary description for UnitTestCoverAES
    /// </summary>
    [TestClass]
    public class UnitTestCoverAES
    {
        public UnitTestCoverAES()
        {
            //
            // TODO: Add constructor logic here
            //
            SOAV.CoverAES.IVString = "(*^%$#@7Hjgtv^%$";
            SOAV.CoverAES.KeyString = "BGVGHjU&^%4Ft(8764UhKJnJlO*^%$#@";
        }
        [TestMethod]
        public void TestMethodEncrypt()
        {

            Console.WriteLine(SOAV.CoverAES.EncryptStringAES("Hi"));
        }
        [TestMethod]
        public void TestMethodDencrypt()
        {               
            Console.WriteLine(SOAV.CoverAES.DecryptStringAES("oKdqWnAt+8gDj0sHSDuwhrFaOi4TXlwakmr35Kc7JmmDHni2ytblzAjijqRjpgwwMkgvesfuLehMatQ1lX75FtHtzfoqUAHJGTmMvRDc7Sig0f2g4gyKvSvrbRWDebqkAoACbAjbtYUBq4d+7ZQy2vdc6fXZsdAAdBc+2rA/sy1sLB3fJ75UdJwtr0/e41RdkzvVZ1K9FqbTBaEmcGCY0g6HM4lmWw4+pYaeMaYsPZpcoV1N0qqzGrNI/gKK/7M0mdQCEIKTnJwI0siClfcjqOn7UGE1KiUG0l4xkG70ucsva0qjSbU9vCzCxJomGtaT25V+d3hNgJ4O+evcA8bB43lqJaDLIs33THxpyjBf3NRiFYWbzusHyhc5bs9Pk/ZKp6ApS7EZ0BPwBY9s2YvPEGFsd+BF3ROXIjUlM1693jbMDckAMLesICBR7DJLqMzuJ757LZ56qP2Z7ipeBOR+SGazV2Mj15ylGP39GDCOwdfkUt/SoyG1r8aeDH2tdaVr2UUkdI0ajWSJOWdDmBYsyNpfzToxcjOJFM2jcFpRk+3L9LbXX07iAOugKVGjD0ZYYCuAn2KtjCghw9B4WSB6livIkjVsmFIQ5iimd7RCZKZrcCwq7CWUM+cLL8wzA3Dk0fp9CzDaWxUu6TCQwO5g4omv4tpSR0EFGthPj9s2aEF3w73CRB8YUMtL8OE+bjita4RQguY4nBwP5xmNGC5UkJMkbwtgab9zUzChq5SQI0UsIZWwL1AdcOHo+PbHh0l3evq6IvJOGfs/s0sEbjlbGLIhwiWKcPe0AzrJrBoboIFWL2LsykPqj6BJuspCfd6H9z1lbr2EAlxLK4F1XKTzc9Icg13DbhozC3ibzCl1jjnVZWlTfyTrPaQe/XdKmmWrcLZac0GnpLxvlULs3FuHKVTw4aSzsA5KDxH5xr4JNzNSzysV9CWxyLNwj2A/2KMmYZRcNv+Jeu4/kmt/dQfM3OBT74b/YKYZaheU+aUXIvk7SnCb2B09Vy0RtlMxB/QvFKYTTxfUOQMUzoDUu+Cazq5BGVUS8nn/BKLATSsIfb3L3fELMV6rkO5lG7lq50gz1SegULOHuki1bp9ke1iL9vNXKrufu8Hbs655mrYghRcpM4Or1ucvnV/yBZtkSDQiZV0nyIGvaivuGE/So76qpU5aVDDVAeB9lkh8ZkdJouY/tDhpm4MnSBKk7eAhrOk3tDuMyeZSEo4Puhute+gtiRa4wQNj/aRfGshYOSgAzqOtC2V85UDpoYHuOK2s+sBlx92B8X9p4gS9DHhNvqGvv7ewcCoZh1ecuNOMo8MGuAirljyl/51XzGJ5ffkwnHQE3i6nr+Zlals+I/6mSsmMzMKV/vjq+mZROkgm52tJoXheULT1l2MsCUKmAisKoUKev5yw87Ymm/RkZfG+ERpw9jH1/czIZ9kPcf8mw0P0LAO0GHVaNu8+0HzA0AfzPf05AsiwM2+aDNqFt1IvJTq4e0L9K4Wzq4t/uOxN2lFDCaKUC0crfmFRq5KlD0zxbWstnfoSgufCN7VBTtVHk7r90qugJ5y24F5DauXR+5QZqHIwOfhRYyuI77e/K7sI68aYYbkMbutm+fwPrF+JzZOMCROzbBG4M8IrUXetGHaRlMyrlB9E5kSeqq3V5CMDGa2iZEPvBa0f9BcIXzIQtWrLLl2qIQWyWRqiJqU9TgEq0R8K7kXkPt92J6UdDAAhfyBkmMbXHUiOmMfN5oho4y36ELhjO3pPnH5Z7wSmyCJGdGQ="));
        }
    }
}
