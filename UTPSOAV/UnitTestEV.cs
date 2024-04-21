using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOAV;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestEV
    {        
        [TestMethod]
        public void TestMethod()
        {
            string evName = "EV_STATE";
            // Read
            string surrogate = EnvironmentState.EVFetch(evName, EnvironmentVariableTarget.Machine);

            if (!string.IsNullOrEmpty(surrogate))
                Console.WriteLine($"EV_STATE: {surrogate}");
            // Create
            surrogate = $"Hello World to Environment Variable State is {DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}";
            Console.WriteLine($"Created EV_STATE:{EnvironmentState.EVCreate(surrogate, evName, EnvironmentVariableTarget.Machine)}");
            // Create for Delete
            Console.WriteLine($"Created EV_STATE_DEL:{EnvironmentState.EVCreate(surrogate, $"{evName}_DEL", EnvironmentVariableTarget.Machine)}");
            // Delete EV_STATE_DEL
            Console.WriteLine($"Deleted EV_STATE_DEL:{EnvironmentState.EVRemove($"{evName}_DEL", EnvironmentVariableTarget.Machine)}");
        }
    }
}
