using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Registration;

namespace RoomAid.Registration.Test
{
    [TestClass]
    public class RegistrationTest
    {
        //Get the instance of registration service
        private RegistrationService testRS = new RegistrationService();

        //The success condition for Name Check
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

        //The faile condtion for Name check
        //With an input that is out of range, the method should return false
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

        //The faile condtion for Name check
        //With an input that is white space, the method should return false
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

        //The faile condtion for Name check
        //With an input that is empty, the method should return false
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

        //The faile condtion for Name check
        //With an input that is null, the method should return false
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

        //The success condition for Name Check
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

        //The fail condtion forpassword check
        //With an input that contains '<' and '>', the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassA()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy1997020>501<36615");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that is less than 12, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassB()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy19970205");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains repetitive numbers, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassC()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy1999967205");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains repetitive letters, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassD()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy1bbbbb67205");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains sequential numbers, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassE()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy123456albertdu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains sequential letters, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassF()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djyabcdelbertdu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains sequential numners in reversed order, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassG()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy54321albertdu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that contains sequential letters in reversed order, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassH()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djyzyxwu33267albertdu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion forpassword check
        //With an input that contains sequential and repetitive contents, the method should return false
        [TestMethod]
        public void PasswordCheckNOTPassI()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordCheck("Djy45678bbbbbbbalbertdu");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for password check
        //With an input that caontains or the same as user's email/username, the method should return false
        [TestMethod]
        public void PasswordUserNameCheckNOTPass()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.PasswordUserNameCheck("bbcdalbertdu233@gmail.com", "albertdu233@gmail.com" );
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The success condtion for Email check
        [TestMethod]
        public void EmailCheckPass()
        {
            //Arrange
            bool expected = true;

            //Act
            Iresult checkResult = testRS.EmailCheck("albertdu233@gmail.com");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        //The fail condtion for Email check
        //with bad format of email input, should return false
        [TestMethod]
        public void EmailCheckNotPass()
        {
            //Arrange
            bool expected = false;

            //Act
            Iresult checkResult = testRS.EmailCheck("dhajdhadh@kjshdjakhg@mail.com");
            bool actual = checkResult.isSuccess;
            Console.WriteLine(checkResult.message);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
