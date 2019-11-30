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
        /// Method GetFileNames will go through the log storage, for each file under the storage path, Archiveable()
        /// method shall be called to check if the log file should be archived, and if the return is true, certain
        /// file's file name shall be added into a list.
        /// </summary>
        /// <returns>resultSet the list of log files that should be archived</returns>
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
            List<string> resultSet = GetFileNames();
            //if the result set is empty, the archive process must be stopped.
            if (resultSet.Count == 0)
            {
                message = "No files are required to be archived";
                return false;
            }
            if (FileOutPut(resultSet) == false)
            {

                return false;
            }
            //Only if all steps were oeprated succeffully, the archive process could return true
            return true;
        }
        public bool FileOutPut(List<string> resultSet)
        {
            bool ifSuccess = true;
            string deleteFailed = @"D:\LogStorage\";
            string compressFailed = @"D:\LogStorage\";
            foreach (string fileName in resultSet)
            {

                bool compressSuccess = AddToSevenZip(fileName);

                if (compressSuccess == false)
                {
                    for (int i = 0; i< timeOfRetry;i++)
                    {
                        compressSuccess = AddToSevenZip(fileName);
                        if (compressSuccess == true)
                        {
                            ifSuccess = true;
                            break;
                            //Notify admin with compressFailed
                        }
                       
                    }
                    if(compressSuccess == false)
                    {
                        compressFailed = compressFailed + fileName;
                        ifSuccess = false;
                    } 
                }   
                if (compressSuccess == true)
                {
                    bool deleteSuccess = DeleteLog(fileName);
                    if(deleteSuccess == false)
                    {
                        for (int i = 0; i < timeOfRetry; i++)
                        {
                            deleteSuccess = DeleteLog(fileName);
                            if (deleteSuccess == true)
                            {
                                ifSuccess = true;
                                break;
                                //Notify admin with compressFailed
                            }

                        }
                        if (deleteSuccess == false)
                        {
                            deleteFailed = deleteFailed + fileName;
                            ifSuccess = false;
                        }
                    }
                }
               
            }
            return ifSuccess;
        }

        public bool AddToSevenZip(string fileName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = sevenZipPath;
                string outPutFilePath = archiveStorageDirectory + DateTime.Now.ToString(dateFormat) +
                    ".7z";
                string filePath = logStorageDirectory + fileName;
                process.StartInfo.Arguments = " a -t7z " + outPutFilePath + " " + filePath + "";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception)
            {
                //log.createLog(failure);
               
                return false;
            }
            return true;

        }

        public bool DeleteLog(string fileName)
        {
            string filePath = logStorageDirectory + fileName;
            try
            {
                if (File.Exists(filePath) == false)
                {
                    return false;
                }
                File.Delete(filePath);
            }
            catch (Exception)
            {
                //log.createLog(failure);
                return false;
            }
            return true;

        }
    }
}
