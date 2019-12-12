using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.Logging
{
    public interface ILogHandler
    {
        bool WriteLog(LogMessage logMessage); // Change to include paramter for numTrials
        bool DeleteLog(LogMessage logMessage);
    }
}
