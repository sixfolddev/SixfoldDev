using System;
using System.Collections.Generic;
using System.Configuration;
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
                if(!FHandler.WriteLog(logMessage)) // Returns false if logging has failed
                {
                    if(!Retry(FHandler.WriteLog, logMessage)) // Returns false if three tries fail
                    {
                        throw new IOException(); // Call Daniel's exception handler??
                    }
                }
                
            }
            catch (IOException)
            {
                DSHandler.DeleteLog(logMessage);

                // Delete log file
                if (!FHandler.DeleteLog(logMessage)) // Returns false if deleting has failed
                {
                    if (!Retry(FHandler.DeleteLog, logMessage)) // Returns false if three tries fail
                    {
                        throw new IOException(); // Call Daniel's exception handler??
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// This will try to log the log three times, as required by the business rules
        /// </summary>
        /// <param name="method">The method that needed to be retried</param>
        /// <param name="msg">The log message that is being retried for logging</param>
        /// <returns>True if the retry is successful, false otherwise</returns>
        private static bool Retry(Func<LogMessage, bool> method, LogMessage msg)
        {
            //Set a bool as the retry result
            bool retrySuccess = false;

            //Retry until it reached the limit time of retry or it successed
            int retryLimit = Int32.Parse(ConfigurationManager.AppSettings["retryLimit"]);
            for (int i = 0; i < retryLimit; i++)
            {
                //Call method again to check if the certain method can be executed successfully
                retrySuccess = method(msg);

                // If the result is true, then stop the retry
                if (retrySuccess)
                {
                    break;
                }
            }

            return retrySuccess; // Return false
        }
    }
}
