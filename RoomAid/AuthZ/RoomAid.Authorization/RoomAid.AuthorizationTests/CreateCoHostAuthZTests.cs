using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.Authorization;

namespace RoomAid.AuthorizationTests
{
    [TestClass]
    public class CreateCoHostAuthZTests
    {
        AuthZFactory factory;
        AuthZAttribute attributes;
        bool[] adminAuthZ;
        int householdID;
        string displayName;

        [TestInitialize]
        public void Setup()
        {
            //arrange
            factory = new AuthZFactory();
            adminAuthZ = new bool[2] { true, true };
            householdID = 1234;
            displayName = "ExampleName";
            attributes = factory.CreateCoHostAuthZ(displayName, householdID, adminAuthZ);
        }

        [TestMethod]
        public void DisplayNameTest_Pass()
        {
            //Act
            var expected = attributes.DisplayName;

            //Assert
            Assert.AreEqual(displayName, expected);
        }

        [TestMethod]
        public void HouseholdIDTest_Pass()
        {
            //Act
            var expected = attributes.HouseholdID;
            //Assert
            Assert.AreEqual((int)1234, expected);
        }

        [TestMethod]
        public void EnabledAuthZTest_Pass()
        {
            //Act
            var expected = attributes.EnabledAuthZ;
            //Assert
            Assert.AreEqual(true, expected);
        }

        [TestMethod]
        public void AdminAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[2] { true, true };
            //Act
            var authZ = attributes.AdminAuthZ;
            //Assert
            Assert.AreEqual(expected[0], authZ[0]);
            Assert.AreEqual(expected[1], authZ[1]);
        }

        [TestMethod]
        public void AccountAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[3] { true, true, true };
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
            bool expected = true;
            //Act
            var actual = attributes.SearchHouseholdAuthZ;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageAuthZTest_Pass()
        {
            //Arrange
            bool[] expected = new bool[5] { false, true, true, true, true };
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
            bool[] expected = new bool[4] { true, true, false, true };
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
            bool[] expected = new bool[4] { false, true, false, false };
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
            bool[] expected = new bool[5] { true, false, false, false, true };
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
            bool[] expected = new bool[4] { true, true, true, true };
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
            bool[] expected = new bool[4] { true, true, true, true };
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
            bool[] expected = new bool[4] { true, true, true, true };
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
