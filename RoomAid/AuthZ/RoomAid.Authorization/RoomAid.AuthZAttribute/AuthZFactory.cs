using System;
using System.Collections.Generic;
using System.Text;

namespace RoomAid.Authorization
{
    class AuthZFactory
    {
        AuthZAttribute CreateAnonAuthZ()
        {
            AuthZAttribute authZ = new AuthZAttribute();
            return authZ;
        }

        AuthZAttribute CreateUserAuthZ(string displayName, int householdID, bool[] adminAuthZ)
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

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        AuthZAttribute CreateHostAuthZ(string displayName, int householdID, bool[] adminAuthZ)
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

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        AuthZAttribute CreateCoHostAuthZ(string displayName, int householdID, bool[] adminAuthZ)
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

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }

        AuthZAttribute CreateTenantAuthZ(string displayName, int householdID, bool[] adminAuthZ)
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

            AuthZAttribute authZ = new AuthZAttribute(displayName, householdID, enabledAuthZ, adminAuthZ,
                accountAuthZ, searchHouseholdAuthZ, messageAuthZ, inviteAuthZ, householdAuthZ, tenantAuthZ, expenseAuthZ, taskAuthZ, sRequestAuthZ);
            return authZ;
        }
    }
}
