using System;

namespace RoomAid.ErrorHandling
{
    //Levels are used to determine severity of things logged, as well as helping determine how to handle errors 
    public enum Level { None, Info, Debug, Warning, Error, Fatal };

    /// <summary>
    /// Warning Controller is the Large Controller that handles All operations that are required by 
    /// Warning Handling, divvying out tasks as needed to separate managers
    /// It is passed in any exception e, where individual services down the line will 
    /// gather more accurate info and pass back 
    /// </summary>
    public class ErrorController
    {

        private readonly Exception _exceptione;
        public AnalyzedError Err
        { get;set; }
        public ErrorController(Exception e)
        { 
            _exceptione = e;
        }

        /// <summary>
        /// Handles the Exception contained within the object by assigning managers to the different steps of the tasks
        /// </summary>
        public void Handle()
        {

            Err = ErrorThreatManager.GetThreatLevel(_exceptione);
            ErrorResponseManager ResponseManager = new ErrorResponseManager(_exceptione);
            ResponseManager.GetResponse(Err);
        }
        
        


    }
}