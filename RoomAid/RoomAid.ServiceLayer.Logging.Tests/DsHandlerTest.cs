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
            //Arrange
            var dsHandler = new DataStoreHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "DataStoreHandlerTest.cs",
                "WriteLog_NewFileCreationAndWrite_Pass()", LogLevels.Levels.None, "Tester", "1", "Testing...");
            var expected = true;
            var actual = false;
            //Act
            if(dsHandler.WriteLog(msg))
            {
                actual = true;
            }
            //Assert
            Assert.IsTrue(expected == actual);
        }
        [TestMethod]
        public void DeleteLog_DeleteExistingLog_Pass()
        {
            //Arrange
            var dsHandler = new DataStoreHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "DataStoreHandlerTest.cs",
                "DeleteLog_DeleteExistingLog_Pass()", LogLevels.Levels.None, "Tester", "1", "Proof of first file write");
            LogMessage msg2 = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "DataStoreHandlerTest.cs",
                "DeleteLog_DeleteExistingLog_Pass()", LogLevels.Levels.None, "Tester", "1", "Proof of second file write");
            dsHandler.WriteLog(msg);
            dsHandler.WriteLog(msg2);
            var expected = true;
            var actual = false;
            //Act
            if(dsHandler.DeleteLog(msg2))
            {
                actual = true;
            }
            //Assert
            Assert.IsTrue(expected == actual);
        }
    }
}
