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
        public static void Log()
        {

        }

        private static MethodBase GetCallingMethod()
        {
            var stackTrace = new StackTrace();
            var currentMethod = MethodBase.GetCurrentMethod();
            for (var i = 0; i < stackTrace.FrameCount; i++)
            {
                var methodName = stackTrace.GetFrame(i).GetMethod().Name;
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
