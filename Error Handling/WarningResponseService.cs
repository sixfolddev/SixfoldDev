using System;

namespace ErrorHandling
{
    class WarningResponseService : IErrorResponseService
    {
        public Exception E { get; set; }
        public WarningResponseService(Exception e)
        {
            E = e;
        }

        public void GetResponse()
        {

        }
    }
}