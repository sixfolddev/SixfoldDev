using System;

namespace RoomAid.ErrorHandling
{
    class WarningResponseService : IErrorResponseService
    {
        public AnalyzedError Err { get; }
        public WarningResponseService(AnalyzedError err)
        {
            Err = err;
        }

        public AnalyzedError GetResponse()
        {



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
                Err.Message = "Something unexpected has occurred";
            }
        }
    }
}