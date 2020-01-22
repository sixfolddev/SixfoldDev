using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public interface IConverter
    {
        string Encode(Dictionary<string, string> dictionary);
        Dictionary<string, string> Decode(string encoding);
    }
}
