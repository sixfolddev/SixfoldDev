using System;
using System.IO;

namespace RoomAid.ServiceLayer.Logging
{
    class FileWriter
    {
        public void WriteFile(string filePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException();
            }
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                File.Create(filePath);
            }
        }
    }
}
