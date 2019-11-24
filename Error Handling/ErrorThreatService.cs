﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Error_Handling
{
    /// <summary>
    /// Service that takes different exceptions and determines the threat based on the type and severity
    /// Returns a value Level, which is an enumerated List within the namespace Error_Handling
    /// </summary>
    public partial class ErrorThreatService
    {
        public ErrorThreatService()
        { }
        /// <summary>
        /// Get ThreatLevel of Generic Exception
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(Exception exceptione)
        {
            return Level.Error;
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
                return Level.Error;
            }
            else if (exceptione.Class <= 19)
            {
                return Level.Warning;
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
            return Level.Error;  

        }
        /// <summary>
        /// GetThreatLevel of HttpUnhandledException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(HttpUnhandledException exceptione)
        {
            return Level.Error;
        }
        
    }
}