using System;
using Emailing;
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

        }

        public void GetResponse()
        {
            
        }
    }
}