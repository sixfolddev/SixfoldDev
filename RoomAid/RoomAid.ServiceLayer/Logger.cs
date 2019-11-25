using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer
{
    public class Logger
    {
        private static LogLevels.Levels _defaultLevel = LogLevels.Levels.Info;
        public static void Log()
        {
            Log("No information received.");
        }

        public static void Log(string text)
        {
            Log(text, _defaultLevel);
        }

        public static void Log(string text, LogLevels.Levels level)
        {
            var time = DateTime.UtcNow;
            string className = GetCallingMethod().ReflectedType.Name; // Base class
            string methodName = GetCallingMethod().Name;
            // TODO: Grab session id and user id
            string userId = "";
            string sessionId = "";

            var logMessage = new LogMessage(time, className, methodName, level, userId, sessionId, text);
            LogDAO.WriteLog(logMessage);
        }

        private static MethodBase GetCallingMethod()
        {
            var stackTrace = new StackTrace();
            var currentMethod = MethodBase.GetCurrentMethod();
            for (var i = 0; i < stackTrace.FrameCount; i++)
            {
                string methodName = stackTrace.GetFrame(i).GetMethod().Name;
                if ((!methodName.Equals("Log")) && (!methodName.Equals(currentMethod.Name)))
                {
                    var stackFrame = new StackFrame(i, true);
                    return stackFrame.GetMethod();
                }
            }
            return currentMethod;
        }
    }
}
