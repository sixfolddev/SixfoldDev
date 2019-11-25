using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    class LogDAO
    {
        public static bool WriteLog(LogMessage logMessage)
        {
            try
            {
                DataStoreHandler DSHandler = new DataStoreHandler();
                FileHandler FHandler = new FileHandler();
                DSHandler.WriteLog(logMessage);
                FHandler.WriteLog(logMessage);
            }
            catch (Exception e)
            {
                // TODO: Delete log entry that was stored

                return false;
            }

            return true;
        }
    }
}
