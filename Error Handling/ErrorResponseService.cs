using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomAid.ErrorHandling
{
    public class ErrorResponseService : IErrorResponseService
    {
        private readonly Exception _e;
        public ErrorResponseService(Exception e)
        { 
            _e = e; 
        }

        public void GetResponse()
        {

        }
    }
}