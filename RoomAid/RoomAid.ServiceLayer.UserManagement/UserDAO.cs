using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class UserDAO
    {
        private readonly String _connectionString;
        public UserDAO()
        { 
            _connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        /// <summary>
        /// uses commands in order to pull details of a user from the sql server 
        /// used to get all of the different variables before editing them and sending them back to the server to update
        /// stores it inside of a user object 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User UserRetrieve(String email)
        {
            SqlConnection Cn = new SqlConnection( _connectionString);
            SqlCommand CommandStrings = new SqlCommand(ConfigurationManager.AppSettings["gatherUserStringVariables"], Cn);
            String[] UserDetails = new String[4];
            SqlCommand CommandDate = new SqlCommand(ConfigurationManager.AppSettings["gatherUserDate"], Cn);
            DateTime DoB = new DateTime();

            using (Cn)
            {
                using (var Read = CommandStrings.ExecuteReader())
                {
                    while(Read.Read())
                    {
                        UserDetails[0] = Read.GetString(0);
                        UserDetails[1] = Read.GetString(1);
                        UserDetails[2] = Read.GetString(2);
                        UserDetails[3] = Read.GetString(3);
                    }
                }
                using (var Read = CommandDate.ExecuteReader())
                {
                    while(Read.Read())
                    {
                        DoB = Read.GetDateTime(0);
                    }
                }
            }

            User Person = new User(email, UserDetails[0], UserDetails[1], UserDetails[3], DoB, UserDetails[2]);
            return Person;

        }
        /// <summary>
        /// attempts to insert changes to user into the sql database
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Boolean UserSend(User person)
        {
            SqlConnection Cn = new SqlConnection(_connectionString);
            String temp = ConfigurationManager.AppSettings["updateUser"];
            temp = String.Format(temp, person.FirstName, person.LastName, person.Gender, person.AccountStatus);
            SqlCommand Command = new SqlCommand(temp,Cn);
            using (Cn)
            {
                try
                {
                    Command.ExecuteNonQuery();
                }
                catch(Exception)
                {
                    return false;
                }
            }

                return true;
        }


    }
}
