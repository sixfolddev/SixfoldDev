using System;


namespace RoomAid.ErrorHandling
{/// <summary>
/// Handles the repsonses of the system based on the severity and type of exception
/// </summary>
    public class ErrorResponseManager
    {
        public Exception E { get; set; }
        public ErrorResponseManager(Exception e)
        {
            E = e;
        }
        /// <summary>
        /// calls method To create responseservices depending on type of exception and level of severity, and then executes them
        /// </summary>
        /// <param name="e"></param>
        /// <param name="level"></param>
        public void GetResponse(Level level)
        {
            IErrorResponseService ResponseService;
            if(level == Level.Fatal)
            {
                 ResponseService = new FatalResponseService(E);
            }
            else if (level == Level.Error)
            {
                 ResponseService = new ErrorResponseService(E);
            }
            else
            {
                 ResponseService = new WarningResponseService(E);
            }

            ResponseService.GetResponse();
            try
            {
                //Log(Lev, Exceptoine)
            }
            catch (Exception caught)
            {
                var Failure =new FatalResponseService(caught);
                Failure.GetResponse();
            }
        }

    }
}