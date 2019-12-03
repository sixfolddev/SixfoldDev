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
    /// </summary>
    public class EmailService
    {

        //These are client id and secrets of the googleapi project, used for token based authn
        private string _clientID = "740461239177-531qol5vrlgltl6mqcd5q7ebl70ftdqr.apps.googleusercontent.com";
        private string _clientSecret = "_AWrAdZ267-QJDFbZQWxE3T-";
        private string _email = "roomaidnotifications@gmail.com";

        //blank constructor
        public EmailService()
        { }

        //async method that creates message and then sends the email
        public async void EmailSenderAsync(string body, string subject, string nameTo, string emailTo)
        {
            MimeMessage Message = BuildMessage(body, subject, new MailboxAddress(nameTo, emailTo));
            await EmailSendAsync(Message);


        }

        /// <summary>
        /// Sends an email after gathering correct credentials to send gmail safely
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task EmailSendAsync(MimeMessage message)
        {
            var secrets = new ClientSecrets
            {
                ClientId = _clientID,
                ClientSecret = _clientSecret
            };
       //waits for credentials bassed on the secrets given, and gets a refresh token if too much time has passed 
        var credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, new[] { GmailService.Scope.MailGoogleCom }, _email, CancellationToken.None);
            if(credentials.Token.IsExpired(SystemClock.Default))
            {
                await credentials.RefreshTokenAsync(CancellationToken.None);
            }
            //using gmail.com to connect securely and send the email message passed into the method
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    var auth = new SaslMechanismOAuth2(credentials.UserId, credentials.Token.AccessToken);
                    client.Authenticate(auth);

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                catch (Exception)
                {
                    throw;
                }
             
                
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