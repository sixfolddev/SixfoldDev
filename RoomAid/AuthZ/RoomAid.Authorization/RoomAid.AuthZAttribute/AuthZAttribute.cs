﻿using System;

namespace RoomAid.Authorization
{
    public class AuthZAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        private string _displayName;
        private int _householdID;
        private bool _enabledAuthZ;
        private bool[] _adminAuthZ;
        private bool[] _accountAuthZ;
        private bool _searchHouseholdAuthZ;
        private bool[] _messageAuthZ;
        private bool[] _inviteAuthZ;
        private bool[] _householdAuthZ;
        private bool[] _tenantAuthZ;
        private bool[] _expenseAuthZ;
        private bool[] _taskAuthZ;
        private bool[] _sRequestAuthZ;

        /// <summary>
        /// Default Constructor. Sets all authorization paramters to false or some equivalent.
        /// </summary>
        public AuthZAttribute()
        {
            _displayName = null;
            _householdID = 0; //Hack review how to evaluate non household ID
            _enabledAuthZ = true; //TODO determine if this value should be true. by default even anonymous users should have access to some features... 
            _adminAuthZ = new bool[2] { false, false};
            _accountAuthZ = new bool[3] { false, false, false };
            _searchHouseholdAuthZ = false;
            _messageAuthZ = new bool[5] { false, false, false, false, false };
            _inviteAuthZ = new bool[4] { false, false, false, false };
            _householdAuthZ = new bool[4] { false, false, false, false };
            _tenantAuthZ = new bool[5] { false, false, false, false, false };
            _expenseAuthZ = new bool[4] { false, false, false, false };
            _taskAuthZ = new bool[4] { false, false, false, false };
            _sRequestAuthZ = new bool[4] { false, false, false, false };
        }

        /// <summary>
        /// Constructor to be used in AuthZFactory methods to create an appropriate Authorization object.
        /// //TODO: Include error handling for bool[] sizes.
        /// </summary>
        /// <param name="displayName"> a string with maximum of 200 character length.</param>
        /// <param name="householdID"> a 0 or positive integer.</param>
        /// <param name="enabledAuthZ"> describes whether account is enabled.</param>
        /// <param name="adminAuthZ"> describes admin privileges. length of 2.</param>
        /// <param name="accountAuthZ"> describes user account privileges. length of 3.</param>
        /// <param name="searchHouseholdAuthZ"> describes user search privileges. </param>
        /// <param name="messageAuthZ"> describes user message prvileges. length of 5.</param>
        /// <param name="inviteAuthZ"> describes user invite privileges. length of 4.</param>
        /// <param name="householdAuthZ"> describes user household privileges. length of 4.</param>
        /// <param name="tenantAuthZ"> describes user tenant management prvileges. length of 5.</param>
        /// <param name="expenseAuthZ"> describes user household expense prvileges. length of 4.</param>
        /// <param name="taskAuthZ"> describes user household expense prvileges. length of 4.</param>
        /// <param name="sRequestAuthZ"> describes user household expense prvileges. length of 4.</param>
        public AuthZAttribute(string displayName, int householdID, bool enabledAuthZ, bool[] adminAuthZ, bool[] accountAuthZ, bool searchHouseholdAuthZ,
            bool[] messageAuthZ, bool[] inviteAuthZ, bool[] householdAuthZ, bool[] tenantAuthZ, bool[] expenseAuthZ, bool[] taskAuthZ, bool[] sRequestAuthZ)
        {
            _displayName = displayName;
            _householdID = householdID;
            _enabledAuthZ = enabledAuthZ;
            _adminAuthZ = adminAuthZ;
            _accountAuthZ = accountAuthZ;
            _searchHouseholdAuthZ = searchHouseholdAuthZ;
            _messageAuthZ = messageAuthZ;
            _inviteAuthZ = inviteAuthZ;
            _householdAuthZ = householdAuthZ;
            _tenantAuthZ = tenantAuthZ;
            _expenseAuthZ = expenseAuthZ;
            _taskAuthZ = taskAuthZ;
            _sRequestAuthZ = sRequestAuthZ;
        }

        //TODO: Evaluate whether properties should be public. Whether I should create custom functions to see if someone can edit these fields.

        /// <summary>
        /// Return DisplayName. DisplayName should not be modifiable as it is a unique identifier
        /// </summary>
        public string DisplayName { get => _displayName; }

        /// <summary>
        /// HouseholdID of a user. Value of 0 if no household is found.
        /// </summary>
        public int HouseholdID { get => _householdID; set => _householdID = value; }

        /// <summary>
        /// Evaluation of whether a user is enabled. 
        /// //TODO: Include authorization so that only admin accounts can use the set method.
        /// </summary>
        public bool EnabledAuthZ { get => _enabledAuthZ; set => _enabledAuthZ = value; }

        /// <summary>
        /// Qualifier of Admin privileges. AdminAuthZ[0]: canCreateAdmin, AdminAuthZ[1]: canEnableAccount
        /// </summary>
        public bool[] AdminAuthZ { get => _adminAuthZ;  }

        /// <summary>
        /// Qualifier of User account privileges. AccountAuthZ[0]:Edit, AccountAuthZ[1]: View, AccountAuthZ[2]: Delete
        /// </summary>
        public bool[] AccountAuthZ { get => _accountAuthZ; set => _accountAuthZ = value; }

        /// <summary>
        /// Qualifier of User search privileges.
        /// </summary>
        public bool SearchHouseholdAuthZ { get => _searchHouseholdAuthZ; set => _searchHouseholdAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges. MessageAuthZ[0]:Send, MessageAuthZ[1]: Reply, MessageAuthZ[2]: View
        /// MessageAuthZ[3]: Mark, MessageAuthZ[4]: Delete
        /// </summary>
        public bool[] MessageAuthZ { get => _messageAuthZ; set => _messageAuthZ = value; }

        /// <summary>
        /// Qualifier of User invite privileges. InviteAuthZ[0]: Send, InviteAuthZ[1]: View, InviteAuthZ[2]: Accept, InviteAuthZ[3]: Decline
        /// </summary>
        public bool[] InviteAuthZ { get => _inviteAuthZ; set => _inviteAuthZ = value; }

        /// <summary>
        /// Qualifier of User household privileges. HouseholdAuthZ[0]: Create, HouseholdAuthZ[1]: View, HouseholdAuthZ[2]: Edit, HouseholdAuthZ[3]: Disband
        /// </summary>
        public bool[] HouseholdAuthZ { get => _householdAuthZ; set => _householdAuthZ = value; }

        /// <summary>
        /// Qualifier of User tenant privileges. TenantAuthZ[0]: Create, TenantAuthZ[1]: Promote, TenantAuthZ[2]: Demote, TenantAuthZ[3]: Remove, TenantAuthZ[4]: Leave
        /// </summary>
        public bool[] TenantAuthZ { get => _tenantAuthZ; set => _tenantAuthZ = value; }

        /// <summary>
        /// Qualifier of User expense privileges. ExpenseAuthZ[0]: Create, ExpenseAuthZ[1]: View, ExpenseAuthZ[2]: Edit, ExpenseAuthZ[3]: Delete
        /// </summary>
        public bool[] ExpenseAuthZ { get => _expenseAuthZ; set => _expenseAuthZ = value; }

        /// <summary>
        /// Qualifier of User task privileges. TaskAuthZ[0]: Create, TaskAuthZ[1]: View, TaskAuthZ[2]: Edit, TaskAuthZ[3]: Delete
        /// </summary>
        public bool[] TaskAuthZ { get => _taskAuthZ; set => _taskAuthZ = value; }

        /// <summary>
        /// Qualifier of User supply request privileges. SRequestAuthZ[0]: Create, SRequestAuthZ[1]: View, SRequestAuthZ[2]: Edit, SRequestAuthZ[3]: Delete
        /// </summary>
        public bool[] SRequestAuthZ { get => _sRequestAuthZ; set => _sRequestAuthZ = value; }
    }
}
