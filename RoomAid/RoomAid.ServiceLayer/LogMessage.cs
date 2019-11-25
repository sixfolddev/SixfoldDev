using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    public class LogMessage
    {
        public DateTime Time { get; set; }
        public LogLevels.Levels Level { get; set; }
        public string UserID { get; set; }
        public string SessionID { get; set; }
        public string CallingClass { get; set; }
        public string CallingMethod { get; set; }
        public string Text { get; set; }

        public LogMessage(DateTime time, string className, string methodName, LogLevels.Levels level, string user, string session,string text)
        {
            Level = level;
            Time = time;
            UserID = user;
            SessionID = session;
            CallingClass = className;
            CallingMethod = methodName;
            Text = text;
        }
    }
}
