using System;
using System.Diagnostics;
using System.Reflection;
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
            var logId = Guid.NewGuid();

            var logMessage = new LogMessage(logId, time, className, methodName, level, userId, sessionId, text);
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
