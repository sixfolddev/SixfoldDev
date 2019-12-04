using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.Authorization;

namespace RoomAid.AuthorizationTests
{
    [TestClass]
    public class CreateAnonAuthZTests
    {
        AuthZFactory factory;
        AuthZAttribute attributes;

        [TestInitialize]
        public void Setup()
        {
            //arrange
            factory = new AuthZFactory();
            attributes = factory.CreateAnonAuthZ();
        }

        [TestMethod]
        public void DisplayNameTest_Pass()
        { 
            //Act
            var displayName = attributes.DisplayName;

            //Assert
            Assert.AreEqual(null, displayName);
        }

        [TestMethod]
        public void HouseholdIDTest_Pass()
        {
            //Act
            var id = attributes.HouseholdID;
            //Assert
            Assert.AreEqual(0,id);
        }

        [TestMethod]
        public void EnabledAuthZTest_Pass()
        {
            //Act
            var authZ = attributes.EnabledAuthZ;
            //Assert
            Assert.AreEqual(true, authZ);
        }

        [TestMethod]
        public void AdminAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[2] { false, false };
            //Act
            var authZ = attributes.AdminAuthZ;
            //Assert
            Assert.AreEqual(expected[0],authZ[0]);
            Assert.AreEqual(expected[1], authZ[1]);
        }

        [TestMethod]
        public void AccountAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[3] { false, false, false };
            //Act
            var actual = attributes.AccountAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        public void SearchHouseholdAuthZTest_Pass()
        {
            //Arrange
            bool expected = false;
            //Act
            var actual = attributes.SearchHouseholdAuthZ;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[5] { false, false, false, false, false };
            //Act
            var actual = attributes.MessageAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
        }

        [TestMethod]
        public void InviteAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[4] { false, false, false, false};
            //Act
            var actual = attributes.InviteAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void HouseholdAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[4] { false, false, false, false };
            //Act
            var actual = attributes.HouseholdAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void TenantAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[5] { false, false, false, false, false };
            //Act
            var actual = attributes.TenantAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
        }

        [TestMethod]
        public void ExpenseAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[4] { false, false, false, false };
            //Act
            var actual = attributes.ExpenseAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void TaskAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[4] { false, false, false, false };
            //Act
            var actual = attributes.TaskAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void SRequestAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[4] { false, false, false, false };
            //Act
            var actual = attributes.SRequestAuthZ;
            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
    }
}

