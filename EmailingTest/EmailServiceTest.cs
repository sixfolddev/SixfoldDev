using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Emailing;
using System;

namespace EmailingTest
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void EmailServiceConstructorTest()
        {
            var Service = new EmailService();
            Assert.IsInstanceOfType(Service, typeof(EmailService));
        }

        [TestMethod]
        public void EmailSendTest()
        {
            
            string Body = "body";
            string Subject = "subject";
            var User = "User";
            var Email = "itsthat0n3guy@gmail.com";

            var Service = new EmailService();
            Service.EmailSender(Body, Subject, User, Email);
            

        }
    }
}
