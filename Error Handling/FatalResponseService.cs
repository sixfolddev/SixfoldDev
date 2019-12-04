using RoomAid.ServiceLayer.Emailing;

namespace RoomAid.ErrorHandling
{
    /// <summary>
    /// Service generated when a Fatal Error is handled, sends an email to admin immediately
    /// </summary>
    public class FatalResponseService :  EmailReady, IErrorResponseService
    {
        private readonly AnalyzedError Err;
        /// <summary>
        /// Uses base constructor in order to fill the actual parameter fields
        /// base class helps to ensure all email fields have been filled out 
        /// </summary>
        /// <param name="e"></param>
        public FatalResponseService(AnalyzedError err) : base($"System Admin:\n An exception of type {err.E.GetType()} " +
            $"has occurred and requires your immediate attention. " +
            $"Please check logs for the day.",
            "The System requires your Immediate Attention",
            "SysAdmin",
            "sixfolddev@gmail.com")
        {
            Err = err;
        }
        /// <summary>
        /// method provided by IErrorResponse, actually responds to the fatal error
        /// </summary>
        public AnalyzedError GetResponse()
        {
            var Emailer = new EmailService();
            Emailer.EmailSender(_body, _subject, _toUsername, _toEmail);
            MessageMaker();
            return Err;
        }


        private void MessageMaker()
        {
            Err.Message = "A fatal error has occurred and system admin has been contacted. Please try again at another time";
        }
    }
}