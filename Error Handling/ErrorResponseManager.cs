using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Error_Handling
{
    public class ErrorResponseManager
    {
        public ErrorResponseManager()
        { }

        public void GetResponse(Exception e)
        {
            ErrorResponseService Response = new ErrorResponseService();
            Response.GetResponse(e);
        }

    }
}