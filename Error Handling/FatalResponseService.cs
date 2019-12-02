using System;
using RoomAid.ServiceLayer.Emailing;

namespace RoomAid.ErrorHandling
{
    /// <summary>
    /// Service generated when a Fatal Error is handled, sends an email to admin immediately
    /// </summary>
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
        /// <summary>
        /// method provided by IErrorResponse, actually responds to the fatal error
        /// </summary>
        public void GetResponse()
        {
            var Emailer = new EmailService();
            Emailer.EmailSenderAsync(Body, Subject, ToUsername, ToEmail);
        }
    }
}