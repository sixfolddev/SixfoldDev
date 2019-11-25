using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    public interface ILogFormatter
    {
        string FormatLog(LogMessage logMessage);
    }
}
