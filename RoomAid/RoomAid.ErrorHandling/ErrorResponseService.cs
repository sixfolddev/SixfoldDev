using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomAid.ErrorHandling
{
    public class ErrorResponseService : IErrorResponseService
    {
        public AnalyzedError Err { get; }
        public ErrorResponseService(AnalyzedError err)
        {
            Err = err;
        }

        public AnalyzedError GetResponse()
        {
            MessageMaker();
            return Err;
        }

        private void MessageMaker()
        {
            Err.Message = "An Error has occurred and your request could not be fulfilled at this time. Please try again";
        }
    }
}