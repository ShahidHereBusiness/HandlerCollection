using SOAV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestISDN
    {
        /// <summary>
        /// Mobile Number or ISDN Validations
        /// </summary>
        [TestMethod]
        public void TestMethodISDN()
        {            
            string prePaidMSISDN = "3319421643";

            bool flag = false;
            flag = Validation.MakeISDN(out prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [10-Digits]");

            prePaidMSISDN = "331942164t";
            flag = Validation.MakeISDN(out prePaidMSISDN, 9, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [9-Digits]");

            prePaidMSISDN = "33194216t3";
            flag = Validation.MakeISDN(out prePaidMSISDN, 10, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [10-Digits]");

            prePaidMSISDN = "3319421t43";
            flag = Validation.MakeISDN(out prePaidMSISDN, 11, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [11-Digits]");

            prePaidMSISDN = "331942t643";
            flag = Validation.MakeISDN(out prePaidMSISDN, 12, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [12-Digits]");

            prePaidMSISDN = "33194t1643";
            flag = Validation.MakeISDN(out prePaidMSISDN, 13, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [13-Digits]");

            prePaidMSISDN = "33194t1643";
            flag = Validation.MakeISDN(out prePaidMSISDN, 130, prePaidMSISDN);
            Console.WriteLine($"{prePaidMSISDN} {flag} MSISDN [13-Digits]");

            string postPaidMSISDN = "923331959863";

            flag = Validation.MakeISDN(out postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [10-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 9, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [9-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 10, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [10-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 11, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [11-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 12, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [12-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 13, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [13-Digits]");
            postPaidMSISDN = "923331959863";
            flag = Validation.MakeISDN(out postPaidMSISDN, 130, postPaidMSISDN);
            Console.WriteLine($"{postPaidMSISDN} {flag} MSISDN [130-Digits]");
        }
    }
}