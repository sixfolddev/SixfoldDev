using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RoomAid.ServiceLayer
{
    public static class LogDAO
    {
        public static bool WriteLog(LogMessage logMessage)
        {
            DataStoreHandler DSHandler = new DataStoreHandler();
            FileHandler FHandler = new FileHandler();

            try
            {
                DSHandler.WriteLog(logMessage);
                FHandler.WriteLog(logMessage);
            }
            catch (IOException e)
            {
                DSHandler.DeleteLog(logMessage);
                FHandler.DeleteLog(logMessage);
            }

            return true;
        }
    }
}
