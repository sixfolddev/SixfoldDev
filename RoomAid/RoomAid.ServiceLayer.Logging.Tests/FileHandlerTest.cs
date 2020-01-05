using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Logging;

namespace RoomAid.Logging.Tests
{
    [TestClass]
    public class FileHandlerTest
    {
        // Log Storage directory
        private string logStorage = ConfigurationManager.AppSettings["logStorage"];
        private FileHandler fileHandler = new FileHandler();
        private string className = "FileHandlerTest.cs";
        private ILogFormatter formatter = new SingleLineFormatter();

        [TestMethod]
        public void WriteLog_NewFileIsCreatedAndWrites_Pass()
        {
            //Arrange
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, className,
                "WriteLog_NewFileIsCreatedAndWrites_Pass()", LogLevels.Levels.None, "Tester", "1", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(logStorage, fileName);
            var expected = true;
            var actual = false;

            //Act
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            if (fileHandler.WriteLog(msg))
            {
                if (File.Exists(path)) // New file created
                {
                    string[] entries = File.ReadAllLines(path);
                    string message = formatter.FormatLog(msg);
                    if (entries[0].Equals(message)) // Log is properly written
                    {
                        actual = true;
                    }
                }
            }

            //Assert
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void WriteLog_LogEntryAppends_Pass()
        {
            //Arrange
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, className,
                "WriteLog_LogEntryAppends_Pass()", LogLevels.Levels.None, "Tester", "2", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(logStorage, fileName);
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
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, className,
                "LogEntryFoundAndDeleted_Pass()", LogLevels.Levels.None, "Tester", "3", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(logStorage, fileName);
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
            LogMessage msg = new LogMessage(Guid.NewGuid(), DateTime.UtcNow, className,
                "LogFileUnaltered_Pass()", LogLevels.Levels.None, "Tester", "4", "Testing...");
            string fileName = fileHandler.MakeFileNameByDate(msg);
            string path = Path.Combine(logStorage, fileName);
            var expected = true;
            var actual = false;

            //Act
            string[] entries = File.ReadAllLines(path);
            string message = formatter.FormatLog(msg);
            if(!entries.Any(message.Equals))
            {
                fileHandler.DeleteLog(msg);
                entries = File.ReadAllLines(path);
                var lastEntry = entries.Length - 1;
                if (!entries[lastEntry].Equals(message))
                {
                    actual = true;
                }
            }

            //Assert
            Assert.IsTrue(expected == actual);
        }
    }   
}
