using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Error_Handling
{
    public class ErrorThreatManager
    {
        public ErrorThreatManager()
        {  }
        /// <summary>
        /// Calls the Overloaded GetThreatLevel Method of ErrorThreatService
        /// </summary>
        /// <param name="exceptione"></param>
        /// <returns>Level</returns>
        public Level GetThreatLevel(Exception exceptione)
        {
            ErrorThreatService ThreatService = new ErrorThreatService();
            return ThreatService.GetThreatLevel(exceptione);
        }
    }
}