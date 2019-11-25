using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    internal class FileHandler : ILogHandler
    {
        public bool WriteLog(LogMessage logMessage)
        {
            return false;
        }

        public bool DeleteLog(LogMessage logMessage)
        {
            return false;
        }
    }
}
