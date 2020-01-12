using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Registration;

namespace RoomAid.Registration.Test
{
    [TestClass]
    public class RegistrationTest
    {
        private RegistrationService testRS = new RegistrationService();
        [TestMethod]
        public void NameCheckPass()
        {
            //Arrange
            bool expected = true;

            //Act
            Iresult checkResult = testRS.NameCheck("AlbertDu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void NameCheckNotPassA()
        {
            //Arrange
            bool expected = false;
            int checkRange = Int32.Parse(ConfigurationManager.AppSettings["nameLength"]);
            string input = "a";
            for(int i = 0; i < checkRange + 1; i++)
            {
                input = input + "a";
            }
            //Act
            Iresult checkResult = testRS.NameCheck(input);
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void NameCheckNotPassB()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.NameCheck(" ");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void NameCheckNotPassC()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.NameCheck("");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void NameCheckNotPassD()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.NameCheck(null);
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void PasswordCheckPass()
        {
            //Arrange
            bool expected = true;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy19970205014436615");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
