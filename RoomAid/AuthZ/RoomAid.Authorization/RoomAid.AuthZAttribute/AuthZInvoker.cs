using System;
using System.Collections.Generic;
using System.Text;

namespace RoomAid.Authorization
{
    public enum AuthRole
    {
        anon,
        user,
        tenant,
        cohost,
        host
    }
    public enum AuthAdmin
    {
        nonadmin,
        admin,
        sysadmin
    }
    public class AuthZInvoker
    {
        private AuthZFactory _authZFactory = new AuthZFactory();
        /// <summary>
        /// Evaluates admin authorization privileges to provide to AuthZFactory
        /// </summary>
        /// <param name="auth">Enumeration of admin authorization privileges.</param>
        /// <returns></returns>
        public bool[] GetAdminAuthZ(AuthAdmin auth)
        {
            switch (auth)
            {
                case AuthAdmin.admin:
                    return new bool[2] { false, true };
                case AuthAdmin.sysadmin:
                    return new bool[2] { true, true };
                default:
                    return new bool[2] { false, false };
            }
        }

        /// <summary>
        /// Creates an AuthZAttribute object depending on authorization parameters.
        /// </summary>
        /// <param name="displayName">Display name of a user. Cannot be null for non anon AuthZ</param>
        /// <param name="householdID">HouseholdID of a user. Value is 0 for anon and user AuthZ</param>
        /// <param name="role">Enumeration of user authorization privileges</param>
        /// <param name="admin">Enumeration of admin authorization privileges</param>
        /// <returns></returns>
        public AuthZAttribute CreateAuthZ(string displayName, int householdID, AuthRole role, AuthAdmin admin)
        {
            try
            {
                bool[] adminAuthZ = GetAdminAuthZ(admin);
                switch (role)
                {
                    case AuthRole.user:
                        if ((displayName == null) || (householdID != 0))
                        {
                            throw new ArgumentException("User display name or householdID is invalid");
                        }   
                        return _authZFactory.CreateUserAuthZ(displayName, adminAuthZ);
                    case AuthRole.host:
                        if ((displayName == null) || (householdID == 0))
                        {
                            throw new ArgumentException("Host display name or householdID is invalid");
                        }
                        return _authZFactory.CreateHostAuthZ(displayName, householdID, adminAuthZ);
                    case AuthRole.cohost:
                        if ((displayName == null) || (householdID == 0))
                        {
                            throw new ArgumentException("CoHost display name or householdID is invalid");
                        }
                        return _authZFactory.CreateCoHostAuthZ(displayName, householdID, adminAuthZ);
                    case AuthRole.tenant:
                        if ((displayName == null) || (householdID == 0))
                        {
                            throw new ArgumentException("Host display name or householdID is invalid");
                        }
                        return _authZFactory.CreateTenantAuthZ(displayName, householdID, adminAuthZ);
                    default:
                        return _authZFactory.CreateAnonAuthZ();
                }

            }
            catch(ArgumentException)
            {
                return _authZFactory.CreateAnonAuthZ();
            }
        }


    }
}
