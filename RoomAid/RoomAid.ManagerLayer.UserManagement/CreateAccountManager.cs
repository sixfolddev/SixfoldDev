using RoomAid.ServiceLayer.Registration;
using RoomAid.ServiceLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ManagerLayer.UserManagement
{
    public class CreateAccountManager
    {
        //For normal User
        /// <summary>
        /// Method CreateAccount() will check all user input and see if they are valid 
        /// <param name="email">The input email, should be passed from fortnend or controller, used as username</param>
        /// <param name="fname">User first name passed from frontend</param>
        /// <param name="lname">User last name passed from frontend</param>
        /// <param name="dob">User's date of birth passed from frontend</param>
        /// <param name="gender">User's gender passed from frontend</param>
        /// <param name="password">User's password passed from frontend</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult CreateAccount(string email, string fname, string lname, DateTime dob, string gender, string password, string repassword)
        {
            //TODO: check session

            RegistrationService rs = new RegistrationService();
            string message = "";
            bool ifSuccess = false;
            Iresult checkResult = rs.EmailCheck(email);

            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;

            checkResult = rs.NameCheck(fname);
            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;

            checkResult = rs.NameCheck(lname);
            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;

            checkResult = rs.AgeCheck(dob);
            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;


            checkResult = rs.PasswordCheck(password);
            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;

            checkResult = rs.PasswordUserNameCheck(password, email);
            message = message + checkResult.message;
            ifSuccess = checkResult.isSuccess;

            if (password!=repassword)
            {
                message = message + "/n"+ConfigurationManager.AppSettings["passwordNotMatch"];
                ifSuccess = false;
            }

            if (ifSuccess)
            {
                User newUser = new User(email, fname, lname, "Enable", dob, gender);
                //TODO: Hash the password
                //TODO: Store the salt
                //TODO: Call the service to add user
                AddUserService ad = new AddUserService();

                checkResult = ad.AddUser(newUser, password);
                message = message + checkResult.message;
                ifSuccess = checkResult.isSuccess;
            }


            return new CheckResult(message, ifSuccess);
        }

        //For admin account
        /// <summary>
        /// Method CreateAccount() will check if the new admin account is valid 
        /// <param name="admin">The user object that is already created</param>
        /// <param name="password">admin's password passed from frontend</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult CreateAccount(User admin, string password)
        {
            //TODO: check session
            RegistrationService rs = new RegistrationService();
            Iresult checkResult = null;
            if (admin != null)
            {
                checkResult = rs.PasswordCheck(password);
                if (checkResult.isSuccess)
                {
                    //TODO: Call the service to add user
                }
            }
            else
            {
                return new CheckResult("no account detected!",false);
            }

            return checkResult;
        }
    }
}
