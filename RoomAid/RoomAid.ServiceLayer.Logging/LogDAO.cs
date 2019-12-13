using System;
using System.Collections.Generic;
using System.Configuration;
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
                if(!FHandler.WriteLog(logMessage)) // If it returns fails and returns false
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
                if (!FHandler.DeleteLog(logMessage)) // If it returns fails and returns false
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
        /// Method Retry() will do the retry for certain method as the business rule required
        /// </summary>
        /// <param name="method">The method that needed to be retried, such as AddToCompress or DeleteLog </param>
        /// <param name="fileName">The name of the file which should be compressed or deleted</param>
        /// <returns>True if the retry successfully, otherwise return false</returns>
        private static bool Retry(Func<LogMessage, bool> method, LogMessage msg)
        {
            //Set a bool as the retry result
            bool retrySuccess = false;

            //Retry until it reached the limit time of retry or it successed
            int retryLimit = Int32.Parse(ConfigurationManager.AppSettings["retryLimit"]);
            for (int i = 0; i < retryLimit; i++)
            {
                //Call method again to check if certain method can be executed successfully
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
