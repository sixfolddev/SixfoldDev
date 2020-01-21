using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class UserSession
    {
        public string SessionId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RefreshTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string UserId { get; set; }
        public virtual User UserCurrentSession { get; set; }

    }
}
