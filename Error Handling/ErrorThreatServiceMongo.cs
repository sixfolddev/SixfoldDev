using MongoDB.Driver;
using System;



namespace RoomAid.ErrorHandling
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
        /// GetThreatLevel of MongoCursorNotFoundException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(MongoCursorNotFoundException exceptione)
        {
            return Level.Error;
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