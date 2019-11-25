namespace RoomAid.ServiceLayer
{
    internal class DataStoreHandler : ILogHandler
    {
        public bool WriteLog(LogMessage logMessage)
        {
            return false;
        }

        public bool DeleteLog(LogMessage logMessage)
        {
            return false;
        }
    }
}
