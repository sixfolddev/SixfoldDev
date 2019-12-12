using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using RoomAid.ServiceLayer.Archiving;
using System.Configuration;

namespace RoomAid.ManagerLayer.Archiving
{
    public class ArchiveManager
    {
        //The error _message for logging
        private string _message;

        //Constructor
        public ArchiveManager()
        {

            //Default _message, should return successed if all steps returns true
            _message = "Archive Successed";

        }

        /// <summary>
        /// RunArchive() method is the main method which will call each method step by step to finish the archive
        /// Only if all steps were successful, the method shall return true, otherwise the method shall return false
        /// and create _message based on reasons of failure
        /// </summary>
        /// <returns>true for archive successed, ortherwise false</returns>
        public bool RunArchive()
        {
            var config = ConfigurationManager.AppSettings;
            //set default value for RunArchive as false to run the while loop only once
            bool ifSuccess = false;
            while (ifSuccess==false)
            {
                //Make sure the loop only run once, change false to true
                ifSuccess = true;
               
                //Before any step of archive started, the space check is required, if the free space is
                //not less than the required space, or the drive is not available, the archive process should not start
                //The admin shall be notified and this archive period shall be logged as failure
                DriveInfo archiveDrive = new DriveInfo(config["driveInfo"]);
                double spaceRequired = double.Parse(config["spaceRequired"]);
                if (IsSpaceEnough(archiveDrive, spaceRequired) == false)
                {
                         ifSuccess = false;
                         break;
                }
           
                //Before start the archive, system need to make sure that 7z.exe is installed in the machine
                if (File.Exists(config["sevenZipPath"]) == false)
                {
                    //Write the _message to explain the failure
                    _message =config["sevenZipNotFound"];
                    ifSuccess = false;
                    break;
                }
                //Before start the archive, system should check if the log storage can be found
                if (!Directory.Exists(@config["logStorage"]))
                {
                    //Write the _message to explain the failure
                    _message = config["logStorageNotFound"];
                    ifSuccess = false;
                    break;
                }

                //Beofer start archive, system should check if the archive storage directory can be found, if not, then create a new folder
                if (!Directory.Exists(@config["archiveStorage"]))
                {
                    Directory.CreateDirectory(@config["archiveStorage"]);
                }

                //Create a new list for resultSet to store all file names that are needed to be archived
                List<string> resultSet = GetFileNames();

                //if the result set is empty, the archive process must be stopped.
                if (resultSet.Count == 0)
                {
                    //Write the _message to explain the failure
                    _message = config["noLogToArchive"];
                    ifSuccess = false;
                    break;
                }

                //Output the compressed file
                IArchiveService archiver = new SevenZipArchiveService();

                //If the file out put is not successed, get failed file names from archiver and notify the admin
                if (archiver.FileOutPut(resultSet) == false)
                {
                    ifSuccess = false;
                    _message = config["outPutFail"] + archiver.GetMessage();
                    break;
                }   
            }
       
            //Only if all steps were oeprated succeffully, the archive process could return true
            return ifSuccess;
        }

        /// <summary>
        /// Method Archiveable will check the given filename to see if this log file is old enough to be
        /// archived. 
        /// </summary>
        /// <param name="fileName">log file that being checked</param>
        /// <returns>True if it is old enough, False if it is not</returns>
       private bool Archiveable(string fileName)
        {
            //Split the file name to remove ".csv"
            string[] split = fileName.Split('.');

            //After split, the first element in string[] should be the log's created date
            string logDate = split[0];

            //Convert and check if it is a datetime
            string dateFormat = ConfigurationManager.AppSettings["dateFormat"];
            string cultureInfo = ConfigurationManager.AppSettings["cultureInfo"];
            int logLife = Int32.Parse(ConfigurationManager.AppSettings["logLife"]);
            bool isDateTime = DateTime.TryParseExact(logDate, dateFormat, new CultureInfo(cultureInfo),
                DateTimeStyles.None, out DateTime logDateTime);

            //Check if the log's life is old enough, return true if file name is correct and it is old enough
            //Else, return false
            if ((DateTime.UtcNow - logDateTime).TotalDays > logLife && isDateTime == true)
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
        private List<string> GetFileNames()
        {
            //Create the list for all file names that should be archived
            var resultSet = new List<string>();

            //Use a for loop to go through the log storage path
            foreach (string logFile in Directory.GetFiles(ConfigurationManager.AppSettings["logStorage"]))
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
        private bool IsSpaceEnough(DriveInfo driveOfArchive, double requiredSpace)
        {
            //Check if the drive's available free space is less than the required space, it also check if
            //the drive is curretly not available
            if (driveOfArchive.AvailableFreeSpace < requiredSpace || !driveOfArchive.IsReady)
            {
                //Write the _message to explain the failure
                _message = ConfigurationManager.AppSettings["spaceNotEnough"];

                //return false if space is not enough or the drive is not available
                return false;
            }

            //return true otherwise
            return true;
        }

        /// <summary>
        /// Method GetMessage() will return the _message to the caller, the _message shall explain if the archive is
        /// successed or the reason of failure, if the reason of failure is file out put failure, message shall also include
        /// a name list of failed files with their error message
        /// </summary>
        /// <returns> The _message </returns>
        public string GetMessage()
        {
            return _message;
        }

    }
}
