using System;
using System.Collections.Generic;
using System.Text;

namespace RoomAid.Authorization
{
    public class AuthZFactory
    {
        public AuthZAttribute CreateAnonAuthZ()
        {
            AuthZAttribute authZ = new AuthZAttribute();
            return authZ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="adminAuthZ"></param>
        /// <returns></returns>
        public AuthZAttribute CreateUserAuthZ(string displayName, bool[] adminAuthZ)
        {
            bool enabledAuthZ = true;
            bool[] accountAuthZ = new bool[3] { true, true, true };
            bool searchHouseholdAuthZ = true;
            bool[] messageAuthZ = new bool[5] { true, true, true, true, true };
            bool[] inviteAuthZ = new bool[4] { false, true, true, true };
            bool[] householdAuthZ = new bool[4] { true, true, false, false };
            bool[] tenantAuthZ = new bool[5] { false, false, false, false, false };
            bool[] expenseAuthZ = new bool[4] { false, false, false, false };
            bool[] taskAuthZ = new bool[4] { false, false, false, false };
            bool[] sRequestAuthZ = new bool[4] { false, false, false, false };

            //TODO: redo design document to reflect new parameters needed.
            AuthZAttribute authZ = new AuthZAttribute(displayName, 0, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        public AuthZAttribute CreateHostAuthZ(string displayName, int householdID, bool[] adminAuthZ)
        {
            bool enabledAuthZ = true;
            bool[] accountAuthZ = new bool[3] { true, true, true };
            bool searchHouseholdAuthZ = true;
            bool[] messageAuthZ = new bool[5] { false, true, true, true, true };
            bool[] inviteAuthZ = new bool[4] { true, true, false, true };
            bool[] householdAuthZ = new bool[4] { false, true, true, true };
            bool[] tenantAuthZ = new bool[5] { true, true, true, true, false };
            bool[] expenseAuthZ = new bool[4] { true, true, true, true };
            bool[] taskAuthZ = new bool[4] { true, true, true, true };
            bool[] sRequestAuthZ = new bool[4] { true, true, true, true };

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        public AuthZAttribute CreateCoHostAuthZ(string displayName, int householdID, bool[] adminAuthZ)
        {
            bool enabledAuthZ = true;
            bool[] accountAuthZ = new bool[3] { true, true, true };
            bool searchHouseholdAuthZ = true;
            bool[] messageAuthZ = new bool[5] { false, true, true, true, true };
            bool[] inviteAuthZ = new bool[4] { true, true, false, true };
            bool[] householdAuthZ = new bool[4] { false, true, false, false };
            bool[] tenantAuthZ = new bool[5] { true, false, false, false, true };
            bool[] expenseAuthZ = new bool[4] { true, true, true, true };
            bool[] taskAuthZ = new bool[4] { true, true, true, true };
            bool[] sRequestAuthZ = new bool[4] { true, true, true, true };

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        public AuthZAttribute CreateTenantAuthZ(string displayName, int householdID, bool[] adminAuthZ)
        {
            bool enabledAuthZ = true;
            bool[] accountAuthZ = new bool[3] { true, true, true };
            bool searchHouseholdAuthZ = true;
            bool[] messageAuthZ = new bool[5] { false, true, true, true, true };
            bool[] inviteAuthZ = new bool[4] { false, true, false, true };
            bool[] householdAuthZ = new bool[4] { false, true, false, false };
            bool[] tenantAuthZ = new bool[5] { true, false, false, false, true };
            bool[] expenseAuthZ = new bool[4] { false, true, false, false };
            bool[] taskAuthZ = new bool[4] { false, true, false, false };
            bool[] sRequestAuthZ = new bool[4] { true, true, false, false };

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }
    }
}
