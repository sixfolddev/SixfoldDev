using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomAid.ServiceLayer.Emailing;

namespace EmailingTest
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void EmailSendAsyncTest()
        {
            bool Answer = false;
            string Body = "body";
            string Subject = "subject";
            var User = "User";
            var Email = "itsthat0n3guy@gmail.com";

            var Service = new EmailService();
            Service.EmailSenderAsync(Body, Subject, User, Email); 



        }
    }
}
