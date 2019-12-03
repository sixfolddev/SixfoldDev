using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.Service
{
    class ArchiveServiceConfig
    {
        //Variables used 

        //The directory path for log file storages
        private string logStorageDirectory = @"D:\LogStorage\";

        //The directory path for Archived file storages
        private string archiveStorageDirectory = @"D:\ArchiveStorage\";

        //How many days a log file should exist, logs older than this limit should be archived
        //Follow the business requirement, the log life should be 30 days
        private int logLife = 30;

        //The data fromat used for the log file name, shall be used to convert file name to a datetime
        //Follow the business requirement, dateFormat should be yyyyMMdd
        private string dateFormat = "yyyyMMdd";

        //The culture information used for converting file name to datetime
        //en-US for culture info as a default
        private string cultureInfo = "en-US";

        //The drive information for archive storage
        //Get information about testing drive
        private DriveInfo driveOfArchive = new DriveInfo("D");

        //An estimated size for archive storage, should not start archive if the available space is less than this
        //Assuem we always leave 0.25 GB for a minmum allocated space
        private double RequiredSpace = 250000000;

        //The file path for '7z.exe'
        //The default file path for 7z.exe
        private string sevenZipPath = @"D:\7-Zip\7z.exe";

        //The limit times for retry failed process
        //Follow the business requirement, the retry limit is 3
        private int timeOfRetry = 3;

        //Getters
        public string GetLogStorage()
        {
            return logStorageDirectory;
        }

        public string GetArchiveStorage()
        {
            return archiveStorageDirectory;
        }

        public int GetLogLife()
        {
            return logLife;
        }

        public string GetDateFormat()
        {
            return dateFormat;
        }

        public string GetCultureInfo()
        {
            return cultureInfo;
        }

        public DriveInfo GetDriveInfo()
        {
            return driveOfArchive;
        }

        public double GetRequiredSpace()
        {
            return RequiredSpace;
        }

        public string GetSevenZipPath()
        {
            return sevenZipPath;
        }

        public int GetTimeOfRetry()
        {
            return timeOfRetry;
        }
    }
}
