using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    /// <summary>
    /// Class that handles User Retrieval and Editing 
    /// </summary>
    public class UserEditingService
    {
        public User Person { get; set; }

        public UserEditingService()
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
