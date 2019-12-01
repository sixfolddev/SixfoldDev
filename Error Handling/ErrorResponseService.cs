using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErrorHandling
{
    public class ErrorResponseService : IErrorResponseService
    {
        public Exception E { get; set; }
        public ErrorResponseService(Exception e)
        { 
            E = e; 
        }

        public void GetResponse()
        {

        }
    }
}