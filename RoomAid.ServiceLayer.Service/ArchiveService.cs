using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;


namespace RoomAid.ServiceLayer.Service
{
    public class ArchiveService
    {
        //Variables used 

        //
        private string logStorageDirectory;
        private string archiveStorageDirectory;
        private int logLife;
        private string dateFormat;
        private string cultureInfo;
        private DriveInfo driveOfArchive;
        private double allocatedSpace;//Assuem we always leave 0.5 GB for a minmum allocated space
        private string sevenZipPath;
        private int timeOfRetry;
        private string message;
        //Constructor
        public ArchiveService()
        {
            //All these attributes should be store in a configuration file for furture development
            //Hard code is for testing.
            logStorageDirectory = @"D:\LogStorage\";
            archiveStorageDirectory = @"D:\ArchiveStorage\";
            logLife = 30;
            dateFormat = "yyyyMMdd";
            cultureInfo = "en-US";
            driveOfArchive = new DriveInfo("D");
            allocatedSpace = 250000000;
            sevenZipPath = @"D:\7-Zip\7z.exe";
            timeOfRetry = 3;
            message = "Archive Successed";
            
        }

        /// <summary>
        /// Method Archiveable will check the given filename to see if this log file is old enough to be
        /// archived. 
        /// </summary>
        /// <param name="fileName">log file that being checked</param>
        /// <returns>True if it is old enough, False if it is not</returns>
        public bool Archiveable(string fileName)
        {
            //Split the file name to remove ".csv"
            string[] split = fileName.Split('.');

            //After split, the first element in string[] should be the log's created date
            string logDate = split[0];

            //Convert and check if it is a datetime
            bool isDateTime = DateTime.TryParseExact(logDate, dateFormat, new CultureInfo(cultureInfo),
                DateTimeStyles.None, out DateTime logDateTime);

            //Check if the log's life is old enough, return true if file name is correct and it is old enough
            //Else, return false
            if ((DateTime.UtcNow - logDateTime).TotalDays > logLife&& isDateTime==true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method GetFileNames will go through the log storage, for each file under the storage path, Archiveable()
        /// method shall be called to check if the log file should be archived, and if the return is true, certain
        /// file's file name shall be added into a list.
        /// </summary>
        /// <returns>resultSet the list of log files that should be archived</returns>
        public List<string> GetFileNames()
        {
            //Create the list for all file names that should be archived
            var resultSet = new List<string>();

            //Use a for loop to go through the log storage path
            foreach (string logFile in Directory.GetFiles(logStorageDirectory))
            {
                //Get the file name
                string fileName = Path.GetFileName(logFile);

                //Call Archiveable() method with the file name
                if (Archiveable(fileName) == true)
                {
                    //If the method return true then add the file name into the list
                    resultSet.Add(fileName);
                }
            }
            
            //return the list once the for loop ended
            return resultSet;
        }

        /// <summary>
        /// Method IsPsaceEnough() will check if the storage has enough space for archiving.
        /// </summary>
        /// <param name="driveOfArchive">the information of drive where the storage at</param>
        /// <param name="requiredSpace">the estimated required space for archiving</param>
        /// <returns>True if the space is enough and the drive is availabe, otherwise return false</returns>
        public bool IsSpaceEnough(DriveInfo driveOfArchive, double requiredSpace)
        {
            //Check if the drive's available free space is less than the required space, it also check if
            //the drive is curretly not available
            if (driveOfArchive.AvailableFreeSpace < requiredSpace || !driveOfArchive.IsReady)
            {
                //return false if space is not enough or the drive is not available
                return false;
            }

            //return true otherwise
            return true;
        }

        /// <summary>
        /// RunArchive() method is the main method which will call each method step by step to finish the archive
        /// Only if all steps were successful, the method shall return true, otherwise the method shall return false
        /// and create message based on reasons of failure
        /// </summary>
        /// <returns>true for archive successed, ortherwise false</returns>
        public bool RunArchive()
        {
            //Before any step of archive started, the space check is required, if the free space is
            //not less than the allocated space, or the drive is not available, the archive process should not start
            //The admin shall be notified and this archive period shall be logged as failure
            if(IsSpaceEnough(driveOfArchive, allocatedSpace) == false)
            {
                message = "Insufficient space for archiving.";
                return false;
            }

            //Before start the archive, system need to make sure that 7z.exe is installed in the machine
            if (File.Exists(sevenZipPath) == false)
            {
                message = "Cannot found 7z.exe for archiving";
                return false;
            }

            //Create a new list for resultSet to store all file names that are needed to be archived
            List<string> resultSet = GetFileNames();

            //if the result set is empty, the archive process must be stopped.
            if (resultSet.Count == 0)
            {
                message = "No files are required to be archived";
                return false;
            }

            //Output the compressed file
            if (FileOutPut(resultSet) == false)
            {
                message = "Failed to compress or delete one or multiple files";
                return false;
            }
            //Only if all steps were oeprated succeffully, the archive process could return true
            return true;
        }

        /// <summary>
        /// Method FileOutPut() method shall add all files that needed to be archived into a compressed file and then delete the 
        /// original log files
        /// </summary>
        /// <param name="resuktSet">The list which stored all file names of files that required to be archived</param>
        /// <returns>True if all files in resultSet are archived and original files were deleted successfully
        /// return false if any file is failed to be archived or deleted</returns>
        public bool FileOutPut(List<string> resultSet)
        {
            //Set the result as true 
            bool ifSuccess = true;

            //Message string used for admin notification, start at the storage path to help the admin find the files with problems
            string deleteFailed = logStorageDirectory;
            string compressFailed = archiveStorageDirectory;

            //Instead of create new process each time, we pass through the same process to do the commandline
            //This will reduce the runtime for archive process from 3 sec to 1 sec
            Process process = new Process();

            //The process shall use 7z.exe to call the commandline
            process.StartInfo.FileName = sevenZipPath;

            //Use a for loop to go through every file name in resultSet
            foreach (string fileName in resultSet)
            {
                //Call AddToSevenZip() method to check if certain file is added into compressed file successfully
                bool compressSuccess = AddToSevenZip(fileName, process);

                //If any file is failed to be compressed. start to retry compress the file 
                if (compressSuccess == false)
                {
                    //Retry until it reached the limit time of retry or it successed
                    for (int i = 0; i< timeOfRetry;i++)
                    {
                        //Call AddToSevenZip() method again to check if certain file is added into compressed file successfully
                        compressSuccess = AddToSevenZip(fileName, process);

                        //If the result is true, then stop the retry, set ifSuccess as true
                        if (compressSuccess == true)
                        {
                            ifSuccess = true;
                            break;
                            //Notify admin with compressFailed
                        }
                       
                    }

                    //If the retry failed three times, skip this file and start to compress the next file
                    //Add this file's name into the message, so the admin can know what files have problems
                    if(compressSuccess == false)
                    {
                        compressFailed = compressFailed + fileName+"， \n";
                        ifSuccess = false;
                    } 
                }   

                //If this file is compressed successfully, then start to delete the original log file
                if (compressSuccess == true)
                {
                    //Call DeleteLog() method to check if it could be deleted successfully
                    bool deleteSuccess = DeleteLog(fileName);

                    //If the deletion failed, start to retry the deletion
                    if(deleteSuccess == false)
                    {
                        //Use a for loop to retry the deletion until it return true, or reach the limit retry time
                        for (int i = 0; i < timeOfRetry; i++)
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

                        //If the deletion still failed, add this file name into the message, and return false
                        if (deleteSuccess == false)
                        {
                            deleteFailed = deleteFailed + fileName;
                            ifSuccess = false;
                        }
                    }
                }
               
            }

            //Close the process when the for loop is finished
            process.Close();

            //Return the result if the file out put is successfully
            return ifSuccess;
        }

        /// <summary>
        /// Method AddToSevenZip method shall add specific file into the compressed file based a given file name 
        /// </summary>
        /// <param name="fileName">The name of the file which should be added into the compressed file</param>
        /// <param name="process">The process passed from FileOutPut method which will run the commandline</param>
        /// <returns>True if the file is added into compressed file successfully, otherwise return false</returns>
        public bool AddToSevenZip(string fileName, Process process)
        {
            try
            {
                //Set the file path for the output compressed file
                string outPutFilePath = archiveStorageDirectory + DateTime.Now.ToString(dateFormat) +
                    ".7z";

                //Set path for the log file which needed to be archived
                string filePath = logStorageDirectory + fileName;

                //Set the argument as the commanline for 7z to add a file into to the compressed file.
                //a - means add for 7z commandline
                process.StartInfo.Arguments = " a -t7z " + outPutFilePath + " " + filePath + "";

                //Hide the console page
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                //Start the process
                process.Start();
                process.WaitForExit();
                
            }
            catch (Exception)
            {
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
            string filePath = logStorageDirectory + fileName;

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
            catch (Exception)
            {
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
