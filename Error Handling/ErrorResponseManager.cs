using System;


namespace ErrorHandling
{/// <summary>
/// Handles the repsonses of the system based on the severity and type of exception
/// </summary>
    public class ErrorResponseManager
    {
        public ErrorResponseManager()
        { }
        /// <summary>
        /// calls method To perform responses depending on type of exception and level of severity
        /// </summary>
        /// <param name="e"></param>
        /// <param name="level"></param>
        public void GetResponse(Exception e, Level level)
        {
            IErrorResponseService ResponseService;
            if(level == Level.Fatal)
            {
                 ResponseService = new FatalResponseService(e);
            }
            else if (level == Level.Error)
            {
                 ResponseService = new ErrorResponseService(e);
            }
            else if (level == Level.Warning)
            {
                 ResponseService = new WarningResponseService(e);
            }
            else
            {
                 ResponseService = new CatchNonUrgentResponseService(e);
            }

            ResponseService.GetResponse();
        }

    }
}