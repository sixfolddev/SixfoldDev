using System;


namespace RoomAid.ErrorHandling
{
    public class AnalyzedError
    {
        private readonly Exception _e;
        public Level Lev { get; set; }
        public AnalyzedError(Exception e)
        {
            _e = e;
        }
        

    }
}