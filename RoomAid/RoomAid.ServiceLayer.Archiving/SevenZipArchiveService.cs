using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;



namespace RoomAid.ServiceLayer.Archiving
{
    public class SevenZipArchiveService:IArchiveService
    {

        //Message catched from deletion or compress
        private string _message;

        //The process that will be used to do the compress
        private Process process;

        //File path for the output compressed file
        private string outPutFilePath = ""; 

        //String that stores all file names that are failed to be added into compressed file, can help the system admin to find certain files
        private string compressFailedFiles;

        //String that stores all file names that are failed to be deleted, can help the system admin to find certain files
        private string deleteFailedFiles;

        //Store the storage path for logs
        private string logStorage;

        public SevenZipArchiveService()

        {
            //Set the file path for the output compressed file
            outPutFilePath = ConfigurationManager.AppSettings["archiveStorage"] + DateTime.Now.ToString(
                ConfigurationManager.AppSettings["dateFormat"]) +
           ConfigurationManager.AppSettings["archiveExtension"];

            //Set the name list as empty
            compressFailedFiles = "";

            //Set the name list as empty
            deleteFailedFiles = "";

            //Get the log storage path from config 
            logStorage = ConfigurationManager.AppSettings["logStorage"];
        }

        /// <summary>
        /// Method FileOutPut() method shall add all files that needed to be archived into a compressed file and then delete the 
        /// original log files
        /// </summary>
        /// <param name="resultSet">The list which stored all file names of files that required to be archived</param>
        /// <returns>True if all files in resultSet are archived and original files were deleted successfully
        /// return false if any file is failed to be archived or deleted</returns>
            public bool FileOutPut(List<string> resultSet)
        {
            //Set the result as true 
            bool ifSuccess = true;

            //Instead of create new process each time, we pass through the same process to do the commandline
            //This will reduce the runtime for archive process from 3 sec to 1 sec
            process = new Process();

            //The process shall use 7z.exe to call the commandline
            process.StartInfo.FileName = ConfigurationManager.AppSettings["sevenZipPath"];

            //Use a for loop to go through every file name in resultSet
            foreach (string fileName in resultSet)
            {
                //Reset the error _message
                _message = "";

                //Call AddToCompress() method to check if certain file is added into compressed file successfully
                bool compressSuccess = AddToCompress(fileName);

                //If any file is failed to be compressed. start to retry compress the file 
                if (compressSuccess == false)
                {
                    //Call Retry Method to do the retry for AddToCompress
                    compressSuccess = Retry(AddToCompress, fileName);

                    //If the retry failed three times, skip this file and start to compress the next file
                    //Add this file's name into the _message, so the admin can know what files have problems
                    if (compressSuccess == false)
                    {
                        compressFailedFiles = compressFailedFiles + fileName + " - " + _message + ",\n ";
                        ifSuccess = false;
                    }
                }

                //If this file is compressed successfully, then start to delete the original log file
                if (compressSuccess == true)
                {
                    //Reset the error _message
                    _message = "";

                    //Call DeleteLog() method to check if it could be deleted successfully
                    bool deleteSuccess = DeleteLog(fileName);

                    //If the deletion failed, start to retry the deletion
                    if (deleteSuccess == false)
                    {
                        //Call Retry Method to do the retry for DeleteLog
                        deleteSuccess = Retry(DeleteLog, fileName);

                        //If the deletion still failed, add this file name into the _message, and return false
                        if (deleteSuccess == false)
                        {
                            deleteFailedFiles = deleteFailedFiles + fileName + " - " + _message + ",\n ";
                            ifSuccess = false;
                        }
                    }
                }

            }

            //Close the process when the for loop is finished
            process.Close();

            //Follow the business requirement, made the output file readonly
            if(ifSuccess == true)
            {
                FileInfo fInfo = new FileInfo(outPutFilePath);
                fInfo.IsReadOnly = true;
            }
            //Return the result if the file out put is successfully
            return ifSuccess;
        }

        /// <summary>
        /// Method AddToCompress method shall add specific file into the compressed file based a given file name 
        /// </summary>
        /// <param name="fileName">The name of the file which should be added into the compressed file</param>
        /// <param name="process">The process passed from FileOutPut method which will run the commandline</param>
        /// <returns>True if the file is added into compressed file successfully, otherwise return false</returns>
        private bool AddToCompress(string fileName)
        {
            try
            {
                //Set path for the log file which needed to be archived
                string filePath = logStorage + fileName;

                //Set the argument as the commanline for 7z to add a file into to the compressed file.
                //a - means add for 7z commandline
                process.StartInfo.Arguments = ConfigurationManager.AppSettings["sevenZipCommandLine"] + outPutFilePath + " " + filePath + "";

                //Hide the console page
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                //Start the process
                process.Start();
                process.WaitForExit();

            }
            catch (Exception e)
            {
                //Write the error _message for certain file
                _message = e.Message;

                //Catch any error during the process such as the file opened or not found at the moment.
                //Return false if the add to compress file failed
                return false;
            }

            //Return true if the process succeed
            return true;

        }

        /// <summary>
        /// Method DeleteLog() will check find the specific file based on the given file name and then delete it.
        /// </summary>
        /// <param name="fileName">The name of the file which should be deleted</param>
        /// <returns>True if the file is deleteled successfully, otherwise return false</returns>
        public bool DeleteLog(string fileName)
        {
            //Get the file path by combineing the storage path and the file name
            string filePath = logStorage + fileName;

            try
            {
                //Make sure the file exists, if the file is already deleted or not exists, it should not be deleted
                if (File.Exists(filePath) == false)
                {
                    //Return false if the file cannot be found
                    return false;
                }

                //If the file exist, delete it
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                //Write the error _message for certain file
                _message = e.Message;

                //Catch the error while deleting the file, such as the file was opened or cannot be find when delete it
                return false;
            }

            //Check again to make sure the file is deleted
            if (File.Exists(filePath) == true)
            {
                //If the file exists, it means the deletion failed, return false
                return false;
            }

            //Return true if deletion was sucessful
            return true;
        }

        /// <summary>
        /// Method Retry() will do the retry for certain method as the business rule required
        /// </summary>
        /// <param name="method">The method that needed to be retried, such as AddToCompress or DeleteLog </param>
        /// <param name="fileName">The name of the file which should be compressed or deleted</param>
        /// <returns>True if the retry successfully, otherwise return false</returns>
        private bool Retry(Func<string, bool> method, string fileName)
        {
            //Set a bool as the retry result
            bool retrySuccess = true;

            //Retry until it reached the limit time of retry or it successed
            int retryLimit = Int32.Parse(ConfigurationManager.AppSettings["retryLimit"]);
            for (int i = 0; i < retryLimit; i++)
            {
                //Call method again to check if certain method can be executed successfully
                retrySuccess = method(fileName);

                //If the result is true, then stop the retry, set retrySuccess as true
                if (retrySuccess == true)
                {
                    break;
                }
            }

            return retrySuccess;
        }

        /// <summary>
        /// Method GetMessage() return a message for admin notification, to show what files are failed to be compress or deleted
        ///This method should only be called when the archive is not successed. 
        /// </summary>
        /// <returns>Notification, the message with file names, file path and error messages</returns>
        public string GetMessage()
        {

            //Set the default notifaction for admin, if the archive is successfull
            String notification = "";

            //Check if the name list for errored files is empty, if not, then write the notification for admin
            if (string.IsNullOrEmpty(compressFailedFiles) == false)
            {
               
                //The notification should includes the file path and all file names for the errored files
                notification = ConfigurationManager.AppSettings["notificationCompress"] + logStorage + "\n"+ compressFailedFiles;
            }

            if (string.IsNullOrEmpty(deleteFailedFiles) == false)
            {
                //The notification should includes the file path and all file names for the errored files
                notification = notification + ConfigurationManager.AppSettings["notificationDelete"] + logStorage + "\n" + deleteFailedFiles;
            }

            return notification;
        }
    }
}
