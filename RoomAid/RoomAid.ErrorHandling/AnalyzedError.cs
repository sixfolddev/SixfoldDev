using System;
using RoomAid.ServiceLayer.Logging;

namespace RoomAid.ErrorHandling
{
    public class AnalyzedError
    {
        public Exception E { get; }
        public LogLevels.Levels Lev { get; set; }
        public String Message { get; set; }
        public AnalyzedError(Exception e)
        {
            E = e;
        }
        

    }
}