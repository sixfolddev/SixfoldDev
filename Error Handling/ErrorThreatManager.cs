using System;


namespace ErrorHandling
{
    public static class ErrorThreatManager
    {
        /// <summary>
        /// Calls the Overloaded GetThreatLevel Method of ErrorThreatService
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public static Level GetThreatLevel(Exception exceptione)
        {
            return ErrorThreatService.GetThreatLevel(exceptione);
        }
    }
}