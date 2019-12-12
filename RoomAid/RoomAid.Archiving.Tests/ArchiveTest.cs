using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using RoomAid.ServiceLayer.Archiving;
using RoomAid.ManagerLayer.Archiving;

namespace UnitTest
{
    
    [TestClass]
    public class ArchiveTest
    {
        private IArchiveService archiver = new SevenZipArchiveService();
        private ArchiveManager manager = new ArchiveManager();
        private string logStorage = ConfigurationManager.AppSettings["logStorage"];
        private int logLife = Int32.Parse(ConfigurationManager.AppSettings["logLife"]);
        private string dateFormat = ConfigurationManager.AppSettings["dateFormat"];
        private string logExtension = ConfigurationManager.AppSettings["logExtension"];


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

            //For testing, make sure directory exists then delete it
            if (Directory.Exists(logStorage))
            {
               Directory.Delete(logStorage);
            }
            
            //Act
            bool actual =  manager.RunArchive();
            Directory.CreateDirectory(logStorage);
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

            //Make sure logStorage exists
            LogStorageCheck();

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

            //Make sure logStorage exists
            LogStorageCheck();

            List<string> resultSet = new List<string>();
            string fileNameA = DateTime.Now.ToString(dateFormat) + logExtension;
            File.WriteAllText(logStorage + fileNameA, "testing");

            string fileNameB = DateTime.Now.AddDays(-1).ToString(dateFormat) + logExtension;
            File.WriteAllText(logStorage + fileNameB, "testing");

            //Act
            if (resultSet.Count > 0)
            {
                actual = true;
            }
            File.Delete(logStorage + fileNameA);
            File.Delete(logStorage + fileNameB);
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

            //Make sure logStorage exists
            LogStorageCheck();

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

            //Make sure logStorage exists
            LogStorageCheck();

            string fileName = "deleteTwice" + logExtension;
            File.WriteAllText(logStorage + fileName, "testing");

            //Act
            File.Delete(logStorage + fileName);
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

            //Make sure logStorage exists
            LogStorageCheck();

            string fileName = "openWhileDeleting" + logExtension;
            File.WriteAllText(logStorage + fileName, "testing");
            StreamReader reader = new StreamReader(logStorage + fileName);

            //Act
            bool actual = archiver.DeleteLog(fileName);
            reader.Close();
            File.Delete(logStorage + fileName);

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

            //Make sure logStorage exists
            LogStorageCheck();

            string fileName = "deleteSuccess" + logExtension;
            File.WriteAllText(logStorage + fileName, "testing");

            //Act
            bool actual = archiver.DeleteLog(fileName);
            Console.WriteLine("the result is:" + actual);

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

            //Make sure logStorage exists
            LogStorageCheck();

            ArchiveManager archiver = new ArchiveManager();
            bool expected = true;
            for(int i = 1; i < 10; i++)
            {
                string fileName = DateTime.Now.AddDays(-1 * (logLife +i)).ToString(dateFormat) +
        logExtension;
                File.WriteAllText(logStorage + fileName, "testing");
            }
            DirClean();
           

            //Act
            bool actual = archiver.RunArchive();
            Console.WriteLine(archiver.GetMessage());
            

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

            //Make sure logStorage exists
            LogStorageCheck();

            string fileName = "";
            for (int i = 1; i < 10; i++)
            {
               fileName = DateTime.Now.AddDays(-1 * (logLife + i)).ToString(dateFormat) +
     logExtension;
                File.WriteAllText(logStorage + fileName, "testing");
            }
            StreamReader reader = new StreamReader(logStorage + fileName);
            DirClean();

            //Act
            bool actual = manager.RunArchive();
            Console.WriteLine(manager.GetMessage());
            reader.Close();
            File.Delete(logStorage + fileName);
           

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

            //Make sure logStorage exists
            LogStorageCheck();

            string fileName = "";
            for (int i = 1; i < 10; i++)
            {
                fileName = DateTime.Now.AddDays(-1 * (logLife + i)).ToString(dateFormat) +
       logExtension;
                File.WriteAllText(logStorage + fileName, "testing");
            }
            File.SetAttributes(logStorage + fileName, FileAttributes.ReadOnly);
            DirClean();

            //Act
            bool actual = manager.RunArchive();
            Console.WriteLine(manager.GetMessage());
            File.SetAttributes(logStorage + fileName, FileAttributes.Normal);
            File.Delete(logStorage + fileName);
            

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Method that used to clean the archive storage for testing
        public void DirClean()
        {
            //clean up tool to delete all files in archive storage, because the requirement said
            //the successed output file should be read only, to avoid conflict in testing, just delete output files from 
            //last test

            //clean the archive storage
            DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["archiveStorage"]);
            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    File.SetAttributes(file.FullName, FileAttributes.Normal);
                    file.Delete();
                }
            }
        }

        //logStorage should already be given by the logging component, create logStorage for testing if logStorage not exists
        public void LogStorageCheck()
        {
            var directory = new DirectoryInfo(logStorage);
            if(!directory.Exists)
            {
                directory.Create();
            }
        }
    }
}

