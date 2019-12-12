namespace RoomAid.ServiceLayer.Logging
{
    public interface ILogFormatter
    {
        string FormatLog(LogMessage logMessage);
    }
}
