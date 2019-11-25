using System.IO;
using System.Text;

namespace RoomAid.ServiceLayer
{
    internal class FileHandler : ILogHandler
    {
        private readonly string _directory;
        private readonly ILogFormatter formatter;

        public FileHandler()
        {
            _directory = @"C:\LogFiles"; // Temporary directory
            formatter = new SingleLineFormatter();
        }

        public bool WriteLog(LogMessage logMessage)
        {
            var directory = new DirectoryInfo(_directory);
            if (!directory.Exists)
            {
                directory.Create();
            }
            string path = Path.Combine(_directory, logMessage.Time.ToString("yyyyMMdd"), ".csv");
            using (var writer = new StreamWriter(path, true, Encoding.UTF8)) // UTF8 Encoding recommended for .NET Framework 4.7.2
            {
                writer.WriteLine(formatter.FormatLog(logMessage)); // TODO: Write async
            }
            return false;
        }

        public bool DeleteLog(LogMessage logMessage)
        {
            return false;
        }
    }
}
