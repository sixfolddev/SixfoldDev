using System;


namespace ErrorHandling
{
    public class ErrorResponseManager
    {
        public ErrorResponseManager()
        { }

        public void GetResponse(Exception e, Level level)
        {
            ErrorResponseService Response = new ErrorResponseService();
            Response.GetResponse(e, level);
        }

    }
}