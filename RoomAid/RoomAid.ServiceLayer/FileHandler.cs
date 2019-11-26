using System;
using System.IO;
using System.Text;

namespace RoomAid.ServiceLayer
{
    internal class FileHandler : ILogHandler
    {
        private readonly string _directory;
        private readonly ILogFormatter _formatter;

        /// <summary>
        /// Default constructor. Initializes a directory to store the log files and the formatter to
        /// format the log message.
        /// </summary>
        public FileHandler()
        {
            _directory = @"C:\LogFiles"; // Temporary directory
            _formatter = new SingleLineFormatter();
        }

        /// <summary>
        /// Writes a log entry to a .csv file. If it fails, tries again up to 3x before throwing an exception.
        /// </summary>
        /// <param name="logMessage">Log entry to be written to file</param>
        /// <returns>true if write is successful, false otherwise</returns>
        public bool WriteLog(LogMessage logMessage)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    var directory = new DirectoryInfo(_directory);
                    if (!directory.Exists)
                    {
                        directory.Create();
                    }
                    string path = Path.Combine(_directory, logMessage.Time.ToString("yyyyMMdd"), ".csv");
                    StreamWriter writer;
                    if (!File.Exists(path)) // If file doesn't exist, create and write parameter names as first line
                    {
                        using (writer = new StreamWriter(path, false, Encoding.UTF8))
                        {
                            writer.WriteLine(logMessage.GetParamNames());
                        }
                    }
                    using (writer = new StreamWriter(path, true, Encoding.UTF8)) // UTF8 Encoding recommended for .NET Framework 4.7.2
                    {
                        writer.WriteLine(_formatter.FormatLog(logMessage)); // TODO: Write async
                    }
                    writer.Close();
                    return true;
                }
                catch (IOException e)
                {
                    if (i == 3)
                    {
                        throw e;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Searches if a log entry exists in a .csv file and deletes it. If it fails, tries again up to 3x 
        /// before throwing an exception.
        /// </summary>
        /// <param name="logMessage">Log entry to search for and delete</param>
        /// <returns>true if delete is successful, false otherwise</returns>
        public bool DeleteLog(LogMessage logMessage)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    string path = Path.Combine(_directory, logMessage.Time.ToString("yyyyMMdd"), ".csv");
                    using (var reader = new StreamReader(path))
                    {
                        string line;
                        StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] tokens = line.Split(',');
                            if (!(tokens[0].Equals(logMessage.LogGUID.ToString())))
                            {
                                using (writer)
                                {
                                    writer.WriteLine(line); // TODO: Write async
                                }
                            }
                        }
                        writer.Close();
                    }
                    return true;
                }
                catch (IOException e)
                {
                    if (i == 3)
                    {
                        throw e;
                    }
                }
            }
            return false;
        }
    }
}
