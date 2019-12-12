using System.IO;
namespace RoomAid.ManagerLayer.Archive
{
    class ArchiveConfig
    {
        //Variables used 

        //The directory path for log file storages
        private const string LOG_STORAGE_DIRECTORY = @"D:\LogStorage\";

        //The directory path for Archived file storages
        private const string ARCHIVE_STORAGE_DIRECTORY = @"D:\ArchiveStorage\";

        //How many days a log file should exist, logs older than this limit should be archived
        //Follow the business requirement, the log life should be 30 days
        private const int LOG_LIFE = 30;

        //The data fromat used for the log file name, shall be used to convert file name to a datetime
        //Follow the business requirement, DATE_FORMAT should be yyyyMMdd
        private const string DATE_FORMAT = "yyyyMMdd";

        //The culture information used for converting file name to datetime
        //en-US for culture info as a default
        private const string CULTURE_INFO = "en-US";

        //The drive information for archive storage
        //Get information about testing drive
        private readonly DriveInfo driveOfArchive = new DriveInfo("D");

        //An estimated size for archive storage, should not start archive if the available space is less than this
        //Assuem we always leave 0.25 GB for a minmum allocated space
        private const double SPACE_REQUIRED = 250000000;

        //The file path for '7z.exe'
        //The default file path for 7z.exe
        private const string SEVEN_ZIP_PATH = @"D:\7-Zip\7z.exe";

        //The limit times for retry failed process
        //Follow the business requirement, the retry limit is 3
        private const int TIME_OF_RETRY = 3;

        //Getters
        public string GetLogStorage()
        {
            return LOG_STORAGE_DIRECTORY;
        }

        public string GetArchiveStorage()
        {
            return ARCHIVE_STORAGE_DIRECTORY;
        }

        public int GetLogLife()
        {
            return LOG_LIFE;
        }

        public string GetDateFormat()
        {
            return DATE_FORMAT;
        }

        public string GetCultureInfo()
        {
            return CULTURE_INFO;
        }

        public DriveInfo GetDriveInfo()
        {
            return driveOfArchive;
        }

        public double GetRequiredSpace()
        {
            return SPACE_REQUIRED;
        }

        public string GetSevenZipPath()
        {
            return SEVEN_ZIP_PATH;
        }

        public int GetTimeOfRetry()
        {
            return TIME_OF_RETRY;
        }
    }
}
