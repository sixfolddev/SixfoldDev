using System;

namespace RoomAid.ServiceLayer.Registration
{
    public interface Iresult
    {
        string message { get; }
        bool isSuccess { get; }
    }
}
