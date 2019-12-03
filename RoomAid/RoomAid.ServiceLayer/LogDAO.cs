using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RoomAid.ServiceLayer.Logging
{
    public static class LogDAO
    {
        // <Summary>
        // Write the created log to both data store locations.
        // If either fails, delete the log from the successful location to ensure that 
        // both logs have the same data.
        // </Summary>
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
