using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Error_Handling
{
    /// <summary>
    /// Continuation of ErrorThreatService for MongoSpecificExceptions
    /// </summary>
    public partial class ErrorThreatService
    {
        /// <summary>
        /// GetThreatLevel of mongoauthenticationexception
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoAuthenticationException exceptione)
        {
            return Level.Fatal;
        }
        /// <summary>
        /// GetThreatLevel of MongoConnectionException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoConnectionException exceptione)
        {
            return Level.Warning;
        }
        /// <summary>
        /// GetThreatLevel of MongoConfigurationException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoConfigurationException exceptione)
        {
            return Level.Fatal;
        }
        /// <summary>
        /// GetThreatLevel of MongoCursorNotFoundException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoCursorNotFoundException exceptione)
        {
            return Level.Warning;
        }
        /// <summary>
        /// GetThreatLevel of MongoInternalException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoInternalException exceptione)
        {
            return Level.Warning;
        }

        /// <summary>
        /// GetThreatLevel of Generic MongoException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoException exceptione)
        {
            return Level.Error;
        }

        
    }
}