using RoomAid.ServiceLayer.Registration;
using System;


namespace RoomAid.ServiceLayer.UserManagement
{
    
    public class User
    {
        // Private backing fields
        //private string _userId;
        private readonly string _userEmail;
        private string _firstName;
        private string _lastName;
        private string _accountStatus;
        private DateTime _dateOfBirth; // To calculate age
        private string _gender; // Male or female

        // Public accessors
        //public string UserId { get; set; }
        public string UserEmail { get; }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                var Service = new RegistrationService();
                var Result = Service.NameCheck(value);
                if(Result.isSuccess)
                {
                    _firstName = value;
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                var Service = new RegistrationService();
                var Result = Service.NameCheck(value);
                if(Result.isSuccess)
                {
                    _lastName = value;
                }
            }
        }
        public string AccountStatus
        {
            get
            {
                return _accountStatus;
            }
            set
            {
                if (value.Equals("Enabled") || value.Equals("Disabled"))
                {
                    _accountStatus = value;
                }
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                if (value.GetType() == typeof(DateTime))
                    _dateOfBirth = value;
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (value.Equals("Male") || value.Equals("Female") || value.Equals("Other"))
                    _gender = value;
            }
        }


        // Empty default constructor
        public User()
        {
            UserEmail = _userEmail;
            FirstName = _firstName;
            LastName = _lastName;
            AccountStatus = _accountStatus;
            DateOfBirth = _dateOfBirth;
            Gender = _gender;
        }

        public User(string email, string fname, string lname, string status, DateTime dob, string gender)
        {
            UserEmail = email;
            FirstName = fname;
            LastName = lname;
            AccountStatus = status;
            DateOfBirth = dob;
            Gender = gender;
        }
    }
}
