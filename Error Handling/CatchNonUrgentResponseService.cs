using System;

namespace ErrorHandling
{
     class CatchNonUrgentResponseService : IErrorResponseService
    {
        public Exception E { get; set; }
        public CatchNonUrgentResponseService(Exception e)
        {
            E = e;
        }

        public void GetResponse()
        {

        }
    }
}