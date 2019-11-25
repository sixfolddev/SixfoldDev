using System;


namespace Error_Handling
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