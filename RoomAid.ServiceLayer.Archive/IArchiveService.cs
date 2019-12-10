
using System.Collections.Generic;


namespace RoomAid.ServiceLayer.Archive
{
    public interface IArchiveService
    {
        bool FileOutPut(List<string> resultSet);
        bool DeleteLog(string fileName);

        string GetMessage();

    }
}
