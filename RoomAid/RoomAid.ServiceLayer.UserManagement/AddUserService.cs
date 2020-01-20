using RoomAid.ServiceLayer.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class AddUserService
    {

        /// <summary>
        /// Method AddUser will use query to insert new user and password into the database
        /// <param name="user">The user object that is already created</param>
        /// <param name="password">user's password which is hashed with salt</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult AddUser(User newUser, string hashedPassword)
        {
            Iresult result = new CheckResult("Successfully added new user!",false);
            try
            {
                //TODO: query for database to insert new user in users table and password in password table
            }
            catch (Exception e)
            {
                result = new CheckResult(e.Message, false);
            }
            return result;
        }

    }
}
