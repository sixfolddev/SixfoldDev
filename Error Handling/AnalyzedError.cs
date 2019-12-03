using System;


namespace RoomAid.ErrorHandling
{
    public class AnalyzedError
    {
        public Exception E { get; }
        public Level Lev { get; set; }
        public String Message { get; set; }
        public AnalyzedError(Exception e)
        {
            E = e;
        }
        

    }
}