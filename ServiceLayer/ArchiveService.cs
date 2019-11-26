using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;


namespace RoomAid.ServiceLayer.Service
{
    public class ArchiveService
    {
        private string logStorageDirectory;
        private string archiveStorageDirectory;
        private int logLife;
        private string dateFormat;
        private string cultureInfo;
        private DriveInfo driveOfArchive;
        private double allocatedSpace;//Assuem we always leave 0.5 GB for a minmum allocated space
        private string sevenZipPath;
        private int timeOfRetry;
        //Constructor
        public ArchiveService()
        {
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
            string logDate = fileName.Substring(0, 8);
            DateTime.TryParseExact(logDate, dateFormat, new CultureInfo(cultureInfo),
                DateTimeStyles.None, out DateTime logDateTime);
            if ((DateTime.UtcNow - logDateTime).TotalDays > logLife)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Method GetFileNames will go through the log storage, check all log files and collect log files that are
        /// old enough to be archive into a list.
        /// </summary>
        /// <returns>resultSet the list of log files that should be archived</returns>
        public List<string> GetFileNames()
        {
           var resultSet = new List<string>();
            foreach (var logFile in Directory.GetFiles(logStorageDirectory))
            {
                string fileName = Path.GetFileName(logFile);
                if (Archiveable(fileName) == true)
                {
                    resultSet.Add(fileName);
                }
            }
            
            return resultSet;
        }

        public bool IsSpaceEnough(DriveInfo driveOfArchive, double requiredSpace)
        {
            if (driveOfArchive.AvailableFreeSpace < requiredSpace || !driveOfArchive.IsReady)
            {
                
                return false;
            }
            return true;
        }
        public bool RunArchive()
        {
            //Before any step of archive started, the space check is required, if the free space is
            //not less than the allocated space, or the drive is not available, the archive process should not start
            //The admin shall be notified and this archive period shall be logged as failure
            if(IsSpaceEnough(driveOfArchive, allocatedSpace) == false)
            {
                //log.createLog(failure);
                //notify admin
                return false;
            }
            //Before start the archive, system need to make sure that 7z.exe is installed in the machine
            if (File.Exists(sevenZipPath) == false)
            {
                return false;
            }
            var resultSet = new List<string>(); 
            resultSet = GetFileNames();
            //if the result set is empty, the archive process must be stopped.
            if (resultSet.Count == 0)
            {
                //log.createLog(failure);
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
            catch (Exception e)
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
            catch (Exception e)
            {
                //log.createLog(failure);
                return false;
            }
            return true;

        }
    }
}
