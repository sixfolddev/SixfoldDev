using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using RoomAid.ServiceLayer.Archive;


namespace RoomAid.ServiceLayer.Archive
{
    public class SevenZipArchiveService:IArchiveService
    {

        //The error _message for logging
        private string _message;

        //Message catched from deletion or compress
        private string _errorMessage;

        private ArchiveConfig _config;

        public SevenZipArchiveService()

        {
            _config = new ArchiveConfig();
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

            //Message string used for admin notification, start at the storage path to help the admin find the files with problems
            string compressFailedFiles = "";
            string deleteFailedFiles = "";


            //Instead of create new process each time, we pass through the same process to do the commandline
            //This will reduce the runtime for archive process from 3 sec to 1 sec
            Process process = new Process();

            //The process shall use 7z.exe to call the commandline
            process.StartInfo.FileName = _config.GetSevenZipPath();

            //Use a for loop to go through every file name in resultSet
            foreach (string fileName in resultSet)
            {
                //Reset the error _message
                _errorMessage = "";

                //Call AddToCompress() method to check if certain file is added into compressed file successfully
                bool compressSuccess = AddToCompress(fileName, process);

                //If any file is failed to be compressed. start to retry compress the file 
                if (compressSuccess == false)
                {
                    //Retry until it reached the limit time of retry or it successed
                    for (int i = 0; i < _config.GetTimeOfRetry(); i++)
                    {
                        //Call AddToCompress() method again to check if certain file is added into compressed file successfully
                        compressSuccess = AddToCompress(fileName, process);

                        //If the result is true, then stop the retry, set ifSuccess as true
                        if (compressSuccess == true)
                        {
                            ifSuccess = true;
                            break;
                            //Notify admin with compressFailed
                        }

                    }

                    //If the retry failed three times, skip this file and start to compress the next file
                    //Add this file's name into the _message, so the admin can know what files have problems
                    if (compressSuccess == false)
                    {
                        compressFailedFiles = compressFailedFiles + fileName + " " + _errorMessage + ",\n ";
                        ifSuccess = false;
                    }
                }

                //If this file is compressed successfully, then start to delete the original log file
                if (compressSuccess == true)
                {
                    //Reset the error _message
                    _errorMessage = "";

                    //Call DeleteLog() method to check if it could be deleted successfully
                    bool deleteSuccess = DeleteLog(fileName);

                    //If the deletion failed, start to retry the deletion
                    if (deleteSuccess == false)
                    {
                        //Use a for loop to retry the deletion until it return true, or reach the limit retry time
                        for (int i = 0; i < _config.GetTimeOfRetry(); i++)
                        {

                            //Call DeleteLog() method again
                            deleteSuccess = DeleteLog(fileName);
                            if (deleteSuccess == true)
                            {
                                //if retry successed, stop the retry
                                ifSuccess = true;
                                break;
                                //Notify admin with compressFailed
                            }

                        }

                        //If the deletion still failed, add this file name into the _message, and return false
                        if (deleteSuccess == false)
                        {
                            deleteFailedFiles = deleteFailedFiles + fileName + " " + _errorMessage + ",\n ";
                            ifSuccess = false;
                        }
                    }
                }

            }

            //Close the process when the for loop is finished
            process.Close();

            String notification = "";

            //Check if the name list for errored files is empty, if not, then write the notification for admin
            if (ifSuccess == false)
            {
                if (string.IsNullOrEmpty(compressFailedFiles) == false)
                {
                    //The notification should includes the file path and all file names for the errored files
                    notification = "Compress Failed: One or multiple files " +
                                               "could not be added into the compressed file:\nFile Path: "
                                               + _config.GetLogStorage() + "\nFile Names: " + compressFailedFiles;
                }
                if (string.IsNullOrEmpty(deleteFailedFiles) == false)
                {
                    //The notification should includes the file path and all file names for the errored files
                    notification = notification + "Deletion Failed: One or multiple files " +
                            "could not be deleted:\nFile Path: " + _config.GetLogStorage() + "\nFile Names: " + deleteFailedFiles;
                }

                //Write the _message to explain the failure
                _message = "File Out Put Failure: Failed to compress or delete one or multiple files";
                //ToDo: Notify the admin
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
        public bool AddToCompress(string fileName, Process process)
        {
            try
            {
                //Set the file path for the output compressed file
                string outPutFilePath = _config.GetArchiveStorage() + DateTime.Now.ToString(_config.GetDateFormat()) +
                    ".7z";

                //Set path for the log file which needed to be archived
                string filePath = _config.GetLogStorage() + fileName;

                //Set the argument as the commanline for 7z to add a file into to the compressed file.
                //a - means add for 7z commandline
                process.StartInfo.Arguments = " a -t7z " + outPutFilePath + " " + filePath + "";

                //Hide the console page
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                //Start the process
                process.Start();
                process.WaitForExit();

            }
            catch (Exception e)
            {
                //Write the error _message for certain file
                _errorMessage = e.ToString();

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
            string filePath = _config.GetLogStorage() + fileName;

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
                _errorMessage = e.ToString();

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
    }
}
