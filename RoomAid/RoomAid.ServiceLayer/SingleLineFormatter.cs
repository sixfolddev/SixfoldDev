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
            return string.Format("{0:dd.MM.yyyy HH:mm:ss} | Class: {1} Method:{2} | [{3}] | UID:{4} SID: {5} | {6}",logMessage.Time, logMessage.CallingClass, logMessage.CallingMethod, logMessage.Level,logMessage.UserID,logMessage.SessionID, logMessage.Text);
        }
    }
}
