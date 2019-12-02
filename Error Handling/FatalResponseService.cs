using System;
using Emailing;
using MimeKit;

namespace ErrorHandling
{
    public class FatalResponseService : IErrorResponseService, IEmailReady 
    {
        public Exception E { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ToUsername { get; set; }
        public string ToEmail { get; set; }
        public FatalResponseService(Exception e)
        {
            E = e;
            ToUsername = "SysAdmin";
            ToEmail = "sixfolddev@gmail.com";
            Subject = "The System requires your Immediate Attention";
            Body = $"System Admin:\n An exception of type {e.GetType()} has occurred and requires your immediate attention. Please check logs for the day.";

        }

        public void GetResponse()
        {
            var Emailer = new EmailService();
            Emailer.EmailSender(Body, Subject, ToUsername, ToEmail);
        }
    }
}