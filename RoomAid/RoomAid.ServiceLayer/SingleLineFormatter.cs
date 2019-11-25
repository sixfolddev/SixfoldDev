using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    internal class SingleLineFormatter : ILogFormatter
    {
        public string FormatLog(LogMessage logMessage)
        {
            return string.Format("{0:yyyy.MM.dd HH:mm:ss},{1},{2},[{3}],{4},{5},{6}", logMessage.Time, logMessage.CallingClass, logMessage.CallingMethod, logMessage.Level, logMessage.UserID, logMessage.SessionID, logMessage.Text);
        }
    }
}
