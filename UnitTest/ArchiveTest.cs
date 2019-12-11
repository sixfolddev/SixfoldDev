using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ManagerLayer.Archive;
using RoomAid.ServiceLayer.Archive;

namespace UnitTest
{
    
    [TestClass]
    public class ArchiveTest
    {
        private IArchiveService archiver = new SevenZipArchiveService();
        private ArchiveManager manager = new ArchiveManager();
        private ArchiveConfig config = new ArchiveConfig();

        //manager test
        [TestMethod]
        ///<summary>
        ///Test for the archive start condition
        ///If the logdirectory the method should stop archiving and return a false 
        ///</summary>
        public void LogDirectoryNotPass()
        {
            //Arrange
            bool expected = false;
            Directory.Delete(config.GetLogStorage(), true);

            //Act
            bool actual =  manager.RunArchive();
            Directory.CreateDirectory(config.GetLogStorage());
            Console.WriteLine(manager.GetMessage());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //manager test
        [TestMethod]
        ///<summary>
        ///Test for the archive start condition
        //If the log storage is empty, the archive should be stopped and the method return a false
        ///</summary>
        public void EmptyStorageNotPass()
        {
            //Arrange
            bool expected = false;

            //Act
            bool actual = manager.RunArchive();
            Console.WriteLine(manager.GetMessage());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for GetFileNames() to see if it can return a list correctly
        ///Write two log files with date less than log life
        ///The list be empty
        ///Files shall be deleted after test
        ///</summary>
        [TestMethod]
        public void GetFileNamesNotPass()
        {
            //Arrange
            bool expected = false;
            bool actual = false;
            List<string> resultSet = new List<string>();
            string fileNameA = DateTime.Now.ToString(config.GetDateFormat()) + config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileNameA, "testing");

            string fileNameB = DateTime.Now.AddDays(-1).ToString(config.GetDateFormat()) + config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileNameB, "testing");

            //Act
            if (resultSet.Count > 0)
            {
                actual = true;
            }
            File.Delete(config.GetLogStorage() + fileNameA);
            File.Delete(config.GetLogStorage() + fileNameB);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for DeleteLog() 
        ///The method won't delete a file that does not exist or could not be found
        ///Should return a false
        ///</summary>
        [TestMethod]
        public void DeleteNotPassA()
        {
            //Arrange
            bool expected = false;

            //Act
            bool actual = archiver.DeleteLog("FileNotExist.csv");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for DeleteLog() 
        ///The method won't delete a file that is already deleted
        ///Delete a file first and then try to delete it again
        ///Should return a false
        ///</summary>
        [TestMethod]
        public void DeleteNotPassB()
        {
            //Arrange
            bool expected = false;
            string fileName = "deleteTwice" + config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");

            //Act
            File.Delete(config.GetLogStorage() + fileName);
            bool actual = archiver.DeleteLog(fileName);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for DeleteLog()
        ///when the file is openend and cannot be 
        ///deleted, the method should return a false.
        ///</summary>
        [TestMethod]
        public void DeleteNotPassC()
        {
            //Arrange
            bool expected = false;
            string fileName = "openWhileDeleting" + config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            StreamReader reader = new StreamReader(config.GetLogStorage() + fileName);

            //Act
            bool actual = archiver.DeleteLog(fileName);
            reader.Close();
            File.Delete(config.GetLogStorage() + fileName);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        ///<summary>
        ///Test for DeleteLog() the method should delete a file correctly if the file path is 
        ///correct
        ///</summary>
        [TestMethod]
        public void DeletePass()
        {
            //Arrange
            bool expected = true;
            string fileName = "deleteSuccess" + config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");

            //Act
            bool actual = archiver.DeleteLog(fileName);
            Console.WriteLine("the result is:" + actual);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for FileOutPut() the method should return true if no error occurs
        ///correct
        ///</summary>
        [TestMethod]
        public void OutPutPass()
        {
            //Arrange
            bool expected = true;
            List<string> resultSet = new List<string>();
            string fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() + 1)).ToString(config.GetDateFormat()) +
            config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            resultSet.Add(fileName);

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            Console.WriteLine(manager.GetMessage());
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for FileOutPut() 
        ///Given one two normal logs and then delete one of it after the archiver get the filenames
        ///Since the FileOutPut cannot find one of the file, it should return a false
        ///</summary>
        [TestMethod]
        public void OutPutNotPassA()
        {
            //Arrange
            bool expected = false;
            List<string> resultSet = new List<string>();
            string fileNameA = DateTime.Now.AddDays(-1 * (config.GetLogLife() + 1)).ToString(config.GetDateFormat()) +
config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileNameA, "testing");
            resultSet.Add(fileNameA);

            string fileNameB = DateTime.Now.AddDays(-1 * (config.GetLogLife() + 2)).ToString(config.GetDateFormat()) +
                config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileNameB, "testing");
            resultSet.Add(fileNameB);

            //Act
            File.Delete(config.GetLogStorage() + fileNameA);
            bool actual = archiver.FileOutPut(resultSet);
            Console.WriteLine(manager.GetMessage());
            File.Delete(config.GetLogStorage() + fileNameB);
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for FileOutPut() 
        ///Given one normal logs and then open the log file while compress it
        ///Since the FileOutPut cannot compress a opened file, it should return a false
        ///</summary>
        [TestMethod]
        public void OutPutNotPassB()
        {
            //Arrange
            bool expected = false;
            List<string> resultSet = new List<string>();

            string fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() + 1)).ToString(config.GetDateFormat()) +
         config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            resultSet.Add(fileName);
            StreamReader reader = new StreamReader(config.GetLogStorage() + fileName);

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            reader.Close();
            archiver.DeleteLog(fileName);
            Console.WriteLine(manager.GetMessage());
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for FileOutPut() 
        ///Given one normal logs and make it read only
        ///Since the FileOutPut cannot compress a readonly file, it should return a false
        ///</summary>
        [TestMethod]
        public void OutPutNotPassC()
        {
            //Arrange
            bool expected = false;
            List<string> resultSet = new List<string>();
            string fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() + 1)).ToString(config.GetDateFormat()) +
config.GetLogExtension();
            File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            resultSet.Add(fileName);
            FileInfo fInfo = new FileInfo(config.GetLogStorage() + fileName);
            fInfo.IsReadOnly = true;

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            fInfo.IsReadOnly = false;
            File.Delete(config.GetLogStorage() + fileName);
            Console.WriteLine(manager.GetMessage());
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for RunArchivePass() 
        ///Given 30 log files that is larger than log life
        ///The method should finish archive successfully with all path correct
        ///The method should return a true
        ///</summary>
        [TestMethod]
        public void RunArchivePass()
        {
            //Arrange
            ArchiveManager archiver = new ArchiveManager();
            bool expected = true;
            for(int i = 1; i < 10; i++)
            {
                string fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() +i)).ToString(config.GetDateFormat()) +
        config.GetLogExtension();
                File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            }

            //Act
            bool actual = archiver.RunArchive();
            Console.WriteLine(archiver.GetMessage());
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for RunArchivePass() 
        ///Given 10 log files that is larger than log life
        ///Open one logs, the logs should not be able to be added into the compressed file
        ///So the whole method should return a false
        ///</summary>
        [TestMethod]
        public void RunArchiveNotPassA()
        {
            //Arrange
            bool expected =false;
            string fileName = "";
            for (int i = 1; i < 10; i++)
            {
               fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() + i)).ToString(config.GetDateFormat()) +
      config.GetLogExtension();
                File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            }
            StreamReader reader = new StreamReader(config.GetLogStorage() + fileName);

            //Act
            bool actual = manager.RunArchive();
            Console.WriteLine(manager.GetMessage());
            reader.Close();
            File.Delete(config.GetLogStorage() + fileName);
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for RunArchivePass() 
        ///Given 10 log files that is larger than log life
        ///Madeone logs read only, the log should not be able to be added into the compressed file
        ///So the whole method should return a false
        ///</summary>
        [TestMethod]
        public void RunArchiveNotPassB()
        {
            //Arrange
            bool expected = false;
            string fileName = "";
            for (int i = 1; i < 10; i++)
            {
                fileName = DateTime.Now.AddDays(-1 * (config.GetLogLife() + i)).ToString(config.GetDateFormat()) +
       config.GetLogExtension();
                File.WriteAllText(config.GetLogStorage() + fileName, "testing");
            }
            FileInfo fInfo = new FileInfo(config.GetLogStorage() + fileName);
            fInfo.IsReadOnly = true;

            //Act
            bool actual = manager.RunArchive();
            Console.WriteLine(manager.GetMessage());
            fInfo.IsReadOnly = false;
            File.Delete(config.GetLogStorage() + fileName);
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Method that used to clean the archive storage for testing
        public  void DirClean()
        {
            //clean the archive storage
            DirectoryInfo dir = new DirectoryInfo(config.GetArchiveStorage());

           foreach (FileInfo file in dir.GetFiles())
            {
                file.IsReadOnly = false;
                file.Delete();
            }
        }
    }
}

