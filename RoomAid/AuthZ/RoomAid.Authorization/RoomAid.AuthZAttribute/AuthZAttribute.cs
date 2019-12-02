using System;

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
            _enabledAuthZ = false;
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
        /// Qualifier of Admin privileges
        /// </summary>
        public bool[] AdminAuthZ { get => _adminAuthZ;  }

        /// <summary>
        /// Qualifier of User account privileges
        /// </summary>
        public bool[] AccountAuthZ { get => _accountAuthZ; set => _accountAuthZ = value; }

        /// <summary>
        /// Qualifier of User search privileges
        /// </summary>
        public bool SearchHouseholdAuthZ { get => _searchHouseholdAuthZ; set => _searchHouseholdAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] MessageAuthZ { get => _messageAuthZ; set => _messageAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] InviteAuthZ { get => _inviteAuthZ; set => _inviteAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] HouseholdAuthZ { get => _householdAuthZ; set => _householdAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] TenantAuthZ { get => _tenantAuthZ; set => _tenantAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] ExpenseAuthZ { get => _expenseAuthZ; set => _expenseAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] TaskAuthZ { get => _taskAuthZ; set => _taskAuthZ = value; }

        /// <summary>
        /// Qualifier of User message privileges 
        /// </summary>
        public bool[] SRequestAuthZ { get => _sRequestAuthZ; set => _sRequestAuthZ = value; }
    }
}
