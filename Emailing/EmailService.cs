using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace Emailing
{
    /// <summary>
    /// This class is about sending and securing emails sent from our roomaidnotifications@gmail.com
    /// </summary>
    public class EmailService
    {

        //These are client id and secrets of the googleapi project, used for token based authn
        public string clientID = "740461239177-531qol5vrlgltl6mqcd5q7ebl70ftdqr.apps.googleusercontent.com";
        public string clientSecret = "_AWrAdZ267-QJDFbZQWxE3T-";
        public string email = "roomaidnotifications@gmail.com";

        public EmailService()
        { }

        public void EmailSender(string body, string subject, string nameTo, string emailTo)
        {
            MimeMessage Message = BuildMessage(body, subject, new MailboxAddress(nameTo, emailTo));
            Task.Run(() => EmailSendAsync(Message));


        }


        private async Task EmailSendAsync(MimeMessage message)
        {
            var secrets = new ClientSecrets
            {
                ClientId = clientID,
                ClientSecret = clientSecret
            };
       //waits for credentials bassed on the secrets given, and gets a refresh token if too much time has passed 
        var credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, new[] { GmailService.Scope.MailGoogleCom }, email, CancellationToken.None);
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
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private MimeMessage BuildMessage(string body, string subject, MailboxAddress to)
        {
            MimeMessage Message = new MimeMessage();
            
            
            Message.From.Add(new MailboxAddress("RoomAidNotifications@DoNotRespond", "roomaidnotifications@gmail.com"));
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