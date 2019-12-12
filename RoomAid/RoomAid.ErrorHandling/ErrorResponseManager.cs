using System;
using RoomAid.ServiceLayer.Logging;

namespace RoomAid.ErrorHandling

{/// <summary>
/// Handles the repsonses of the system based on the severity and type of exception
/// </summary>
    public class ErrorResponseManager
    {

        public ErrorResponseManager()
        {
            
        }
        /// <summary>
        /// calls method To create responseservices depending on type of exception and level of severity, and then executes them
        /// </summary>
        /// <param name="e"></param>
        /// <param name="level"></param>
        public AnalyzedError GetResponse(AnalyzedError Err)
        {
            IErrorResponseService ResponseService;
            if(Err.Lev == LogLevels.Levels.Fatal)
            {
                 ResponseService = new FatalResponseService(Err);
            }
            else if (Err.Lev == LogLevels.Levels.Error)
            {
                 ResponseService = new ErrorResponseService(Err);
            }
            else
            {
                 ResponseService = new WarningResponseService(Err);
            }
            try
            {
                Logger.Log(Err.ToString());
            }
            catch (Exception caught)
            {
                var Failure = new FatalResponseService(new AnalyzedError(caught)
                {
                    Lev = LogLevels.Levels.Fatal
                }) ;
                Failure.GetResponse();
            }

            return ResponseService.GetResponse();
        }

    }
}