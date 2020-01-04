using System.Configuration;


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
            Err.Message = "An Error has occurred. Please try again";
        }
    }
}