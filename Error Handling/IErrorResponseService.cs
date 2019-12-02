﻿using System;


namespace RoomAid.ErrorHandling
{
    interface IErrorResponseService 
    {
        /// <summary>
        /// Helps with organizing differennt types of responses depending on threat level
        /// </summary>
        /// 
        Exception E
        {
            get;
            set;
        }
        

        void GetResponse();
    }
}
