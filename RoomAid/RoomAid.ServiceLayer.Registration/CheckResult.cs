

namespace RoomAid.ServiceLayer.Registration
{
    public class CheckResult : Iresult
    {

        public CheckResult(string reason, bool isSuccess)
        {
            this.message = reason;
            this.isSuccess = isSuccess;
        }

        public string message { get; }

        public bool isSuccess { get; }
    }
}
