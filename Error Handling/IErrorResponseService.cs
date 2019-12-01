using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
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
