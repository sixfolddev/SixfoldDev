using System;
using System.Web;
using System.Data.SqlClient;
using RoomAid.ServiceLayer.Logging;

namespace RoomAid.ErrorHandling 
{
    /// <summary>
    /// Service that takes different exceptions and determines the threat based on the type and severity
    /// Returns a value Level, which is an enumerated List within the namespace RoomAid.ServiceLayer.Logging
    /// </summary>
    public partial class ErrorThreatService
    {
        public LogLevels.Levels GetThreatLevel(Exception exceptione)
        {
            return LogLevels.Levels.Warning;
        }
        /// <summary>
        /// test for throwing of timeoutException
        /// </summary>
        /// <param name="exceptoine"></param>
        /// <returns></returns>
        public LogLevels.Levels GetThreatLevel(TimeoutException exceptoine)
        {
            return LogLevels.Levels.Error;
        }

        /// <summary>
        /// GetThreatLevel of SqlException based off of .Class property, which contains severity
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>LogLevels.Levels</returns>
        public LogLevels.Levels GetThreatLevel(SqlException exceptione)
        {
            if (exceptione.Class <= 10)
            {
                return LogLevels.Levels.Info;
            }
            else if (exceptione.Class <= 16)
            {
                return LogLevels.Levels.Warning;
            }
            else if (exceptione.Class <= 19)
            {
                return LogLevels.Levels.Error;
            }
            else
            {
                return LogLevels.Levels.Fatal;
            }
        }
        /// <summary>
        /// GetThreatLevel of UnauthorizedAccessException 
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>LogLevels.Levels</returns>
        public LogLevels.Levels GetThreatLevel(UnauthorizedAccessException exceptione)
        {
            return LogLevels.Levels.Warning;  

        }
        /// <summary>
        /// GetThreatLevel of HttpUnhandledException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>LogLevels.Levels</returns>
        public LogLevels.Levels GetThreatLevel(HttpUnhandledException exceptione)
        {
            return LogLevels.Levels.Warning;
        }
        

        public LogLevels.Levels GetThreatLevel(CustomException exceptione)
        {
            return exceptione.GetLevel();
        }
    }
}   