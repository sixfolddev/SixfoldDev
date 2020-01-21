using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class UserRetrieval
    {
        public User Person { get; set; }

        public UserRetrieval()
        { }

        public void RetrieveUserInfo(String email)
        {
            UserDAO UserRetrieve = new UserDAO();
            Person = UserRetrieve.UserRetrieve(email);
            
        }

        //private Boolean SendUserInfo()
        //{



        //}
    }
}
