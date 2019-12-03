using System;


namespace RoomAid.ErrorHandling
{/// <summary>
/// Handles the repsonses of the system based on the severity and type of exception
/// </summary>
    public class ErrorResponseManager
    {
        private readonly Exception _e;
        public ErrorResponseManager(Exception e)
        {
            _e = e;
        }
        /// <summary>
        /// calls method To create responseservices depending on type of exception and level of severity, and then executes them
        /// </summary>
        /// <param name="e"></param>
        /// <param name="level"></param>
        public void GetResponse(AnalyzedError Err)
        {
            IErrorResponseService ResponseService;
            if(Err.Lev == Level.Fatal)
            {
                 ResponseService = new FatalResponseService(_e);
            }
            else if (Err.Lev == Level.Error)
            {
                 ResponseService = new ErrorResponseService(_e);
            }
            else
            {
                 ResponseService = new WarningResponseService(_e);
            }

            ResponseService.GetResponse();
            try
            {
                //Log(Err, E.ToString())
            }
            catch (Exception caught)
            {
                var Failure =new FatalResponseService(caught);
                Failure.GetResponse();
            }
        }

    }
}