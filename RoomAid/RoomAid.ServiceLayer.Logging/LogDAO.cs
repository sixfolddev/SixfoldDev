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
            ILogHandler DSHandler = new DataStoreHandler();
            ILogHandler FHandler = new FileHandler();

            try
            {
                DSHandler.WriteLog(logMessage);

                // Log to file
                FHandler.WriteLog(logMessage);
            }
            catch (IOException)
            {
                DSHandler.DeleteLog(logMessage);
                FHandler.DeleteLog(logMessage);
            }

            return true;
        }
    }
}
