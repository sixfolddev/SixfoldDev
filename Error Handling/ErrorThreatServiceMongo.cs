using MongoDB.Driver;
using System;



namespace RoomAid.ErrorHandling
{
    /// <summary>
    /// Continuation of ErrorThreatService for MongoSpecificExceptions
    /// </summary>
    public partial class ErrorThreatService
    {
        public ErrorThreatService()
        { }

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
            return Level.Error;
        }
 
        /// <summary>
        /// GetThreatLevel of MongoCursorNotFoundException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoCursorNotFoundException exceptione)
        {
            return Level.Error;
        }
     

        /// <summary>
        /// GetThreatLevel of Generic MongoException
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(MongoException exceptione)
        {
            return Level.Warning;
        }

        
    }
}