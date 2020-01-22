using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    /// <summary>
    /// Class that handles User Retrieval and Editing and Sending
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

        /// <summary>
        /// attempts to send the user info back to the database
        /// </summary>
        /// <returns></returns>
        public Boolean SendUserInfo()
        {
            ///TODO Make sure to actual make an authz check before the action 
            UserDAO UserSend = new UserDAO();
            return UserSend.UserSend(Person);
        }
    }
}
