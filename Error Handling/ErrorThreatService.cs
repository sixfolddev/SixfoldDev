using System;
using System.Web;
using System.Data.SqlClient;

namespace RoomAid.ErrorHandling 
{
    /// <summary>
    /// Service that takes different exceptions and determines the threat based on the type and severity
    /// Returns a value Level, which is an enumerated List within the namespace RoomAidErrorHandling
    /// </summary>
    public partial class ErrorThreatService
    {
        public Level GetThreatLevel(Exception exceptione)
        {
            return Level.Warning;
        }
        /// <summary>
        /// GetThreatLevel of SqlException based off of .Class property, which contains severity
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(SqlException exceptione)
        {
            if (exceptione.Class <= 10)
            {
                return Level.Info;
            }
            else if (exceptione.Class <= 16)
            {
                return Level.Warning;
            }
            else if (exceptione.Class <= 19)
            {
                return Level.Error;
            }
            else
            {
                return Level.Fatal;
            }
        }
        /// <summary>
        /// GetThreatLevel of UnauthorizedAccessException 
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(UnauthorizedAccessException exceptione)
        {
            return Level.Warning;  

        }
        /// <summary>
        /// GetThreatLevel of HttpUnhandledException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(HttpUnhandledException exceptione)
        {
            return Level.Warning;
        }
        
    }
}