using System;

namespace ErrorHandling
{
    public class FatalResponseService : IErrorResponseService, IEmailReady
    {
        public Exception E { get; set; }
        public FatalResponseService(Exception e) 
        {
            E = e; 
        }

        public void GetResponse()
        {
            
        }
    }
}