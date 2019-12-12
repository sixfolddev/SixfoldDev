using System;

namespace RoomAid.ErrorHandling
{

    public class ErrorThreatManager
    {
        private readonly Exception _e;
        public ErrorThreatManager(Exception e)
        {
            _e = e;
        }

        /// <summary>
        /// Calls the Overloaded GetThreatLevel Method of ErrorThreatService
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public AnalyzedError GetThreatLevel()
        {
            AnalyzedError Analysis = new AnalyzedError(_e);
            var ThreatService = new ErrorThreatService();
            Analysis.Lev = ThreatService.GetThreatLevel(_e);
            return Analysis;
        }
    }
}