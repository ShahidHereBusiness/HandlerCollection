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
        private readonly string evName = "EV_STATE";
        private string context { get; set; }
        [TestMethod]
        public void TestMethod()
        {            
            // Read First State
            context = EnvironmentState.EVFetch(evName, EnvironmentVariableTarget.Machine);
            // Conditional Stuff
            if (!string.IsNullOrEmpty(context))
                Console.WriteLine($"EV_STATE: {context}");
            // Create State
            context = $"Hello World to Environment Variable State is {DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}";
            Console.WriteLine($"Created EV_STATE:{EnvironmentState.EVCreate(context, evName, EnvironmentVariableTarget.Machine)}");
            // Update State
            context = $"Hello World to Environment Variable State Update is {DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}";
            Console.WriteLine($"Created EV_STATE:{EnvironmentState.EVCreate(context, evName, EnvironmentVariableTarget.Machine)}");
            // Read State
            Console.WriteLine($"Created EV_STATE:{EnvironmentState.EVFetch(evName, EnvironmentVariableTarget.Machine)}");
            // Create for Delete
            Console.WriteLine($"Created EV_STATE_DEL:{EnvironmentState.EVCreate(context, $"{evName}_DEL", EnvironmentVariableTarget.Machine)}");
            // Delete EV_STATE_DEL
            Console.WriteLine($"Deleted EV_STATE_DEL:{EnvironmentState.EVRemove($"{evName}_DEL", EnvironmentVariableTarget.Machine)}");
        }
    }
}
