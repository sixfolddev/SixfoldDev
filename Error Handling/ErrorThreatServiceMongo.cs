using MongoDB.Driver;
using System;



namespace ErrorHandling
{
    /// <summary>
    /// Continuation of ErrorThreatService for MongoSpecificExceptions
    /// </summary>
    public static partial class ErrorThreatService
    {
        /// <summary>
        /// GetThreatLevel of mongoauthenticationexception
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoAuthenticationException exceptione)
        {
            return Level.Fatal;
        }
        /// <summary>
        /// GetThreatLevel of MongoConnectionException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoConnectionException exceptione)
        {
            return Level.Error;
        }
        /// <summary>
        /// GetThreatLevel of MongoConfigurationException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoConfigurationException exceptione)
        {
            return Level.Warning;
        }
        /// <summary>
        /// GetThreatLevel of MongoCursorNotFoundException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoCursorNotFoundException exceptione)
        {
            return Level.Error;
        }
        /// <summary>
        /// GetThreatLevel of MongoInternalException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoInternalException exceptione)
        {
            return Level.Warning;
        }

        /// <summary>
        /// GetThreatLevel of Generic MongoException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoException exceptione)
        {
            return Level.Warning;
        }

        
    }
}