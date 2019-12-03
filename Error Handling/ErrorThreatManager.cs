using System;


namespace RoomAid.ErrorHandling
{
    
    public static class ErrorThreatManager
    {
        /// <summary>
        /// Calls the Overloaded GetThreatLevel Method of ErrorThreatService
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static AnalyzedError GetThreatLevel(Exception exceptione)
        {
             AnalyzedError Analysis = new AnalyzedError(exceptione);
             Analysis.Lev = ErrorThreatService.GetThreatLevel(exceptione);
             return Analysis;
        }
    }
}