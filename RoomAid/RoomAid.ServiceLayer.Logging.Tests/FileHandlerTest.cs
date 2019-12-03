using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer;

namespace RoomAid.Logging.Tests
{
    [TestClass]
    public class FileHandlerTest
    {
        [TestMethod]
        public void WriteLog_NewFileIsCreatedAndWrites_Pass()
        {
            //Arrange
            var fileHandler = new FileHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "FileHandlerTest.cs",
                "WriteLog_NewFileIsCreatedAndWrites_Pass()", LogLevels.Levels.None, "Tester", "1", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(@"C:\SixfoldLogFiles", fileName);
            var expected = true;
            var actual = false;

            //Act
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            if (fileHandler.WriteLog(msg))
            {
                if (File.Exists(path))
                {
                    actual = true;
                }
            }

            //Assert
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void WriteLog_LogEntryAppends_Pass()
        {
            //Arrange
            var fileHandler = new FileHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "FileHandlerTest.cs",
                "WriteLog_LogEntryAppends_Pass()", LogLevels.Levels.None, "Tester", "2", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(@"C:\SixfoldLogFiles", fileName);
            var formatter = new SingleLineFormatter();
            var expected = true;
            var actual = false;

            //Act
            if (fileHandler.WriteLog(msg))
            {
                string[] entries = File.ReadAllLines(path);
                string message = formatter.FormatLog(msg);
                var lastEntry = entries.Length - 1;
                if (entries[lastEntry].Equals(message))
                {
                    actual = true;
                }
            }

            //Assert
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void DeleteLog_LogEntryFoundAndDeleted_Pass()
        {
            //Arrange
            var fileHandler = new FileHandler();
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, "FileHandlerTest.cs",
                "LogEntryFoundAndDeleted_Pass()", LogLevels.Levels.None, "Tester", "3", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(@"C:\SixfoldLogFiles", fileName);
            var formatter = new SingleLineFormatter();
            var expected = true;
            var actual = false;

            //Act
            if (fileHandler.WriteLog(msg))
            {
                string[] entries = File.ReadAllLines(path);
                string message = formatter.FormatLog(msg);
                var lastEntry = entries.Length - 1;
                if (entries[lastEntry].Equals(message))
                {
                    fileHandler.DeleteLog(msg);
                }
                entries = File.ReadAllLines(path);
                lastEntry = entries.Length - 1;
                if (!entries[lastEntry].Equals(message))
                {
                    actual = true;
                }
            }

            //Assert
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void DeleteLog_LogFileUnaltered_Pass()
        {
            //Arrange


            //Act
            //pass in a log message that DNE in file; file should have all logs intact

            //Assert
            Assert.Fail();
        }
    }   
}
