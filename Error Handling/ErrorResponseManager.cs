using System;


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
            if(Err.Lev == Level.Fatal)
            {
                 ResponseService = new FatalResponseService(Err);
            }
            else if (Err.Lev == Level.Error)
            {
                 ResponseService = new ErrorResponseService(Err);
            }
            else
            {
                 ResponseService = new WarningResponseService(Err);
            }
            try
            {
                //Log(Err, E.ToString())
            }
            catch (Exception caught)
            {
                var Failure = new FatalResponseService(new AnalyzedError(caught)
                {
                    Lev = Level.Fatal
                });
                Failure.GetResponse();
            }

            return ResponseService.GetResponse();
        }

    }
}