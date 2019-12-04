using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Archive;

namespace RoomAid.Archiving.Tests
{
    [TestClass]
    public class ArchiveTest
    {
        [TestMethod]
        ///<summary>
        ///Test for IsSpaceEnough()
        ///Given a large required space, should return false for insufficient space
        ///</summary>
        public void SpaceCheckNotPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;

            //Act
            bool actual = archiver.IsSpaceEnough(new System.IO.DriveInfo("D"), 9999999999999999999);
            Console.WriteLine(archiver.GetMessage());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        ///<summary>
        ///Test for log archiveable
        ///Given a new log file which the date is less than the log life
        ///should return false since a log file that is old enough should pass
        ///</summary>
        public void IsNotOldPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;

            //Act
            File.WriteAllText(@"D:\LogStorage\20191119.csv", "testing");
            bool actual = archiver.Archiveable("20191119.csv");
            archiver.DeleteLog("20191119.csv");
            Console.WriteLine("the result is:" + actual);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for log archiveable
        ///Given a old log file which the date is larger than the log life
        ///should return true since a log file is old enough to be archived
        ///</summary>
        [TestMethod]
        public void IsOldPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;

            //Act
            File.WriteAllText(@"D:\LogStorage\20190919.csv", "testing");
            bool actual = archiver.Archiveable("20190919.csv");
            archiver.DeleteLog("20190919.csv");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for log archiveable
        ///Given a file that has non datetime name
        ///Since the Archiveable method shall check the format, it should return false
        ///</summary>
        [TestMethod]
        public void ArchiveableNotPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;

            //Act
            bool actual = archiver.Archiveable("BadName.csv");

            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for GetFileNames() to see if it can return a list correctly
        ///Write two log files that are older than the log life
        ///The list should has a size of 2 
        ///Files shall be deleted after test
        ///</summary>
        [TestMethod]
        public void GetFileNamesPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            bool actual = false;

            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\20190826.csv", "testing");
            System.IO.File.WriteAllText(@"D:\LogStorage\20190824.csv", "testing");
            List<string> resultSet = archiver.GetFileNames();
            if (resultSet.Count == 2)
            {
                actual = true;
            }
            archiver.DeleteLog("20190826.csv");
            archiver.DeleteLog("20190824.csv");

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
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            bool actual = false;

            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\201901203.csv", "testing");
            System.IO.File.WriteAllText(@"D:\LogStorage\201901204.csv", "testing");
            List<string> resultSet = archiver.GetFileNames();
            if (resultSet.Count > 0)
            {
                actual = true;
            }
            archiver.DeleteLog("201901203.csv");
            archiver.DeleteLog("201901204.csv");
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
            ArchiveService archiver = new ArchiveService();
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
            ArchiveService archiver = new ArchiveService();
            bool expected = false;

            //Act
            File.WriteAllText(@"D:\LogStorage\20190801.csv", "testing");
            archiver.DeleteLog("20190801.csv");
            bool actual = archiver.DeleteLog("20190801.csv");

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
            ArchiveService archiver = new ArchiveService();
            bool expected = false;

            //Act
            File.WriteAllText(@"D:\LogStorage\20190701.csv", "testing");
            StreamReader reader = new StreamReader(@"D:\LogStorage\20190701.csv");
            bool actual = archiver.DeleteLog("20190701.csv");
            reader.Close();
            archiver.DeleteLog("20190701.csv");

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
            ArchiveService archiver = new ArchiveService();
            bool expected = true;

            //Act
            File.WriteAllText(@"D:\LogStorage\20190825.csv", "testing");
            bool actual = archiver.DeleteLog("20190825.csv");
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
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            File.WriteAllText(@"D:\LogStorage\20190815.csv", "testing");
            var resultSet = archiver.GetFileNames();

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            Console.WriteLine(archiver.GetMessage());

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
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            File.WriteAllText(@"D:\LogStorage\20190815.csv", "testing");
            File.WriteAllText(@"D:\LogStorage\20190819.csv", "testing");
            var resultSet = archiver.GetFileNames();
            archiver.DeleteLog("20190819.csv");

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            Console.WriteLine(archiver.GetMessage());
            archiver.DeleteLog("20190815.csv");
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
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            File.WriteAllText(@"D:\LogStorage\20190815.csv", "testing");
            var resultSet = archiver.GetFileNames();
            StreamReader reader = new StreamReader(@"D:\LogStorage\20190815.csv");

            //Act
            bool actual = archiver.FileOutPut(resultSet);
            reader.Close();
            archiver.DeleteLog("20190815.csv");
            Console.WriteLine(archiver.GetMessage());
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
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            string filePath = @"D:\LogStorage\2019060";
            for (int i = 1; i < 10; i++)
            {
                string fileName = filePath + i + ".csv";
                File.WriteAllText(fileName, "testing");
            }

            filePath = @"D:\LogStorage\2019061";
            for (int i = 1; i < 10; i++)
            {
                string fileName = filePath + i + ".csv";
                File.WriteAllText(fileName, "testing");
            }

            filePath = @"D:\LogStorage\2019062";
            for (int i = 1; i < 10; i++)
            {
                string fileName = filePath + i + ".csv";
                File.WriteAllText(fileName, "testing");
            }
            File.WriteAllText("20190630.csv", "testing");
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
        ///Open one of these logs, the log should not be able to be added into the compressed file
        ///So the whole method should return a false
        ///</summary>
        [TestMethod]
        public void RunArchiveNotPassA()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            string filePath = @"D:\LogStorage\2019060";
            for (int i = 1; i < 10; i++)
            {
                string fileName = filePath + i + ".csv";
                File.WriteAllText(fileName, "testing");
            }
            StreamReader reader = new StreamReader(@"D:\LogStorage\20190609.csv");
            //Act
            bool actual = archiver.RunArchive();
            Console.WriteLine(archiver.GetMessage());
            reader.Close();
            archiver.DeleteLog("20190609.csv");
            DirClean();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Method that used to clean the archive storage for testing
        public static void DirClean()
        {
            //clean the archive storage
            DirectoryInfo dir = new DirectoryInfo(@"D:\ArchiveStorage\");

            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
        }
    }
}

