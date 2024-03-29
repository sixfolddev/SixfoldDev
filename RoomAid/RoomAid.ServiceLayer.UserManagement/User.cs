﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAid.ServiceLayer.UserManagement
{
    // TODO: Implement admin parameter
    public class User
    {
        // Private backing fields
        private string _userEmail;
        private string _firstName;
        private string _lastName;
        private string _accountStatus;
        private DateTime _dateOfBirth; // To calculate age
        private string _gender; // Male or female
        //private bool _admin;

        // Public accessors
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        //public bool Admin { get; set; }

        // Empty default constructor
        public User()
        {
            UserEmail = _userEmail;
            FirstName = _firstName;
            LastName = _lastName;
            AccountStatus = _accountStatus;
            DateOfBirth = _dateOfBirth;
            Gender = _gender;
            //Admin = _admin;
        }

        public User(string email, string fname, string lname, string status, DateTime dob, string gender/*, bool admin*/)
        {
            UserEmail = email;
            FirstName = fname;
            LastName = lname;
            AccountStatus = status;
            DateOfBirth = dob;
            Gender = gender;
            //Admin = admin;
        }
    }
}
