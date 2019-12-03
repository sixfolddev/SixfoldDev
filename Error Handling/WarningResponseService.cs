using System;

namespace RoomAid.ErrorHandling
{
    class WarningResponseService : IErrorResponseService
    {
        private readonly Exception _e;
        public WarningResponseService(Exception e)
        {
            _e = e;
        }

        public void GetResponse()
        {

        }
    }
}