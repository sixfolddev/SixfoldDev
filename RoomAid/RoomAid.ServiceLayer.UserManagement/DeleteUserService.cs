using RoomAid.ServiceLayer.Registration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class DeleteUserService
    {
        /// <summary>
        /// DeleteUser will delete any user based on the primary key UserEmail. Since all emails must be unique, only one user will be deleted.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        //TODO: Include retry system if deletion fails
        public Iresult DeleteUser(User user)
        {
            Iresult result = new CheckResult("Successfully deleted user", true);
            try
            {
                //TODO: Configure SQL database to establish connection to User database
                string sqlConnection = "";
                string query = "DELETE FROM Users WHERE UserEmail = " + user.UserEmail;
                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    connection.Open();
                    using (SqlCommand delete = new SqlCommand(query))
                    {
                        delete.ExecuteNonQuery();
                    }
                    connection.Close();
                    //Logs that a user has been deleted from the Users database
                    //TODO: Acquire encrypted email for logs
                    Logging.Logger.Log("User (encrypted email) successfully deleted from User Database");
                }
            }
            catch(Exception e)
            {
                result = new CheckResult(e.Message, false);
            }
            return result;
        }

        //HACK: These are basically the same methods. Determine a way to shorten code!
        public Iresult DeleteStoredPassword(User user)
        {
            Iresult result = new CheckResult("Successfully deleted user", true);
            try
            {
                //TODO: Configure SQL database to establish connection to UserLogin database
                string sqlConnection = "";
                string query = "DELETE FROM UserLogin WHERE UserEmail = " + user.UserEmail;
                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    connection.Open();
                    using (SqlCommand delete = new SqlCommand(query))
                    {
                        delete.ExecuteNonQuery();
                    }
                    connection.Close();
                    //Logs that a user has been deleted from the UserLogin database
                    //TODO: Acquire encrypted email for logs
                    Logging.Logger.Log("User (encrypted email) successfully deleted from UserLogin Database");
                }
            }
            catch (Exception e)
            {
                result = new CheckResult(e.Message, false);
            }
            return result;
        }
    }
}
