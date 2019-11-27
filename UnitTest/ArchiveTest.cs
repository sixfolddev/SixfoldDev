using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Service;

namespace UnitTest
{
    [TestClass]
    public class ArchiveTest
    {
        [TestMethod]
        ///<summary>
        ///Test for IsSpaceEnough(), should return false for insufficient space
        ///</summary>
        public void SpaceCheckNotPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            //Act
            bool actual = archiver.IsSpaceEnough(new System.IO.DriveInfo("D"),9999999999999999999);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        ///<summary>
        ///Test for log archiveable, a log file that is old enough should pass
        ///</summary>
        public void IsNotOldPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            //Act
            bool actual = archiver.Archiveable("20191119.csv");
            Console.WriteLine("the result is:"+actual);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for log archiveable, a log file that is not old enough should not pass
        ///</summary>
        [TestMethod]
        public void IsOldPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            //Act
            bool actual = archiver.Archiveable("20190919.csv");
            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for GetFileNames() to see if it can return a list correctly
        ///</summary>
        [TestMethod]
        public void ListNotEmptyPassA()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            bool actual = false;
            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\20190826.csv", "testing");
            System.IO.File.WriteAllText(@"D:\LogStorage\20190824.csv", "testing");
            List<string> resultSet = archiver.GetFileNames();
            if (resultSet.Count > 0)
            {
                actual =true;
            }
            archiver.DeleteLog("20190826.csv");
            archiver.DeleteLog("20190824.csv");
            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for GetFileNames() to see if it can return a list correctly
        ///An empty storage should return an empty list
        ///</summary>
        [TestMethod]
        public void ListNotEmptyPassB()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            bool actual = false;
            //Act
            List<string> resultSet = archiver.GetFileNames();
            if (resultSet.Count > 0)
            {
                actual = true;
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }

        ///<summary>
        ///Test for DeleteLog() the method won't delete a file that does not exist
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
        ///Test for DeleteLog() the method won't delete a file twice
        ///</summary>
        [TestMethod]
        public void DeleteNotPassB()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\20190801.csv", "testing");
            archiver.DeleteLog("20190801.csv");
            bool actual = archiver.DeleteLog("20190801.csv");
            //Assert
            Assert.AreEqual(expected, actual);
        }
        ///<summary>
        ///Test for DeleteLog(), when the file is openend and cannot be 
        ///deleted, the method should return a false.
        ///</summary>
        [TestMethod]
        public void DeleteNotPassC()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\20190701.csv", "testing");
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
        public void DeletePassA()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            //Act
            System.IO.File.WriteAllText(@"D:\LogStorage\20190825.csv", "testing");
            bool actual = archiver.DeleteLog("20190825.csv");
            Console.WriteLine("the result is:" + actual);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutPutPass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            System.IO.File.WriteAllText(@"D:\LogStorage\20190815.csv", "testing");
            var resultSet = archiver.GetFileNames();
            //Act
            bool actual = archiver.FileOutPut(resultSet);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OutPutNotPassA()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = false;
            System.IO.File.WriteAllText(@"D:\LogStorage\20190815.csv", "testing");
            System.IO.File.WriteAllText(@"D:\LogStorage\20190819.csv", "testing");
            var resultSet = archiver.GetFileNames();
            archiver.DeleteLog("20190819.csv");

            //Act
            bool actual = archiver.FileOutPut(resultSet);

            //clean the archive storage
            DirectoryInfo dir = new DirectoryInfo(@"D:\ArchiveStorage\");

            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RunArchivePass()
        {
            //Arrange
            ArchiveService archiver = new ArchiveService();
            bool expected = true;
            string filePath = @"D:\LogStorage\201906";
            for(int i = 0; i < 30; i++)
            {
                string fileName = filePath + i + ".csv";
                System.IO.File.WriteAllText(fileName, "testing");
            }
            //Act
            bool actual = archiver.RunArchive();
            //clean the archive storage
            DirectoryInfo dir = new DirectoryInfo(@"D:\ArchiveStorage\");

            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

