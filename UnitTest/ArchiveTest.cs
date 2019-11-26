using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Service;

namespace UnitTest
{
    [TestClass]
    public class ArchiveTest
    {
       
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
    
    }
}

