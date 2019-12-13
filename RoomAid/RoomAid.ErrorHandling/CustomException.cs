using RoomAid.ServiceLayer.Logging;
using System;

namespace RoomAid.ErrorHandling
{/// <summary>
/// used as a base class for the addition of any new needed exceptions, allowing for easy assignment of new threat levels
/// </summary>
    public class CustomException : Exception
    {
        private readonly LogLevels.Levels _level;

        public CustomException(): base()
        {
            _level = LogLevels.Levels.Warning;
        }

        public CustomException(string message) : base(message)
        {
            _level = LogLevels.Levels.Warning;
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            _level = LogLevels.Levels.Warning;
        }

        public LogLevels.Levels GetLevel()
        {
            return _level;
        }

       



    }
}