using System;
using System.Configuration;


namespace RoomAid.ErrorHandling
{
    public class WarningResponseService : IErrorResponseService
    {
      

        public AnalyzedError Err { get; }
        public WarningResponseService(AnalyzedError err)
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
            if(Err.E is UnauthorizedAccessException)
            {
                Err.Message = "You do not have permissions for this resource";
            }
            else
            {
                Err.Message = "Something unexpected occurred. Please try again.";
            }
        }
        
    }
}