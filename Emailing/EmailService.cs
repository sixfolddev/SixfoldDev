using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace RoomAid.ServiceLayer.Emailing
{
    /// <summary>
    /// This class is about sending and securing emails sent from our roomaidnotifications@gmail.com
    /// 
    /// to actually send emails, make sure to remove the system.net and everything inside of it from web.config
    /// </summary>
    public class EmailService
    {

        //These are client id and secrets of the googleapi project, used for token based authn
       
        private readonly string _email = "roomaidnotifications@gmail.com";

        //blank constructor
        public EmailService()
        { }

        //async method that creates message and then sends the email
        public void EmailSender(string body, string subject, string nameTo, string emailTo)
        {
            
            MimeMessage Message = BuildMessage(body, subject, new MailboxAddress(nameTo, emailTo));
            EmailSend(Message);
        }

        /// <summary>
        /// Sends an email after gathering correct credentials to send gmail safely
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private void EmailSend(MimeMessage message)
        {
        
            //using gmail.com to connect securely and send the email message passed into the method
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                var Password = Environment.GetEnvironmentVariable("emailPassword", EnvironmentVariableTarget.User);
                client.Authenticate(_email, Password);
                client.Send(message);
                client.Disconnect(true); 
            }
        }
        /// <summary>
        /// uses passed in parameters in order to build email messages for sending
        /// 
        /// </summary>
        /// <param name="body"> Message.body = body</param>
        /// <param name="subject">Message.subject = subject</param>
        /// <param name="to">Message.to = to</param>
        /// <returns></returns>
        private MimeMessage BuildMessage(string body, string subject, MailboxAddress to)
        {
            MimeMessage Message = new MimeMessage();
            
            
            Message.From.Add(new MailboxAddress("RoomAidNotifications@DoNotRespond", _email));
            Message.To.Add(to);
            Message.Subject = subject;
            var BuildBody = new BodyBuilder
            {
                TextBody = body
            };
            Message.Body = BuildBody.ToMessageBody();

            return Message;
        }

        
      
    }
}