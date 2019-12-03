using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer;

namespace RoomAid.Logging.Tests
{
    [TestClass]
    public class DsHandlerTest
    {
        [TestMethod]
        public void WriteLog_NewFileCreationAndWrite_Pass()
        {
            var dsHandler = new DataStoreHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "DataStoreHandlerTest.cs",
                "WriteLog_NewFileCreationAndWrite_Pass()", LogLevels.Levels.None, "Tester", "1", "Testing...");
            var expected = true;
            var actual = false;
            if(dsHandler.WriteLog(msg))
            {
                actual = true;
            }
            Assert.IsTrue(expected == actual);
        }
    }
}
