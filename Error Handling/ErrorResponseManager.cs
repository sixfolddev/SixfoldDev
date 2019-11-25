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
            ErrorResponseService Response = new ErrorResponseService();
            Response.GetResponse(e, level);
        }

    }
}