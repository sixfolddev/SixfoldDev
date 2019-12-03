using System;
using System.Collections.Generic;
using System.Text;

namespace RoomAid.Authorization
{
    public class AuthZHandler
    {
        public bool AuthIsEnabled(AuthZAttribute authz)
        {
            return authz.EnabledAuthZ;
        }

        public bool AuthCreateAdmin(AuthZAttribute authz)
        {
            return authz.AdminAuthZ[0];
        }

        public bool AuthEnableAccount(AuthZAttribute authz)
        {
            return authz.AdminAuthZ[1];
        }

        public bool AuthEditProfile(AuthZAttribute authz)
        {
            return authz.AccountAuthZ[0];
        }

        public bool AuthViewProfile(AuthZAttribute authz)
        {
            return authz.AccountAuthZ[1];
        }

        public bool AuthDeleteAccount(AuthZAttribute authz)
        {
            return authz.AccountAuthZ[2];
        }

        public bool AuthSearchHousehold(AuthZAttribute authz)
        {
            return authz.SearchHouseholdAuthZ;
        }

        public bool AuthSendMessage(AuthZAttribute authz)
        {
            return authz.MessageAuthZ[0];
        }

        public bool AuthReplyMessage(AuthZAttribute authz)
        {
            return authz.MessageAuthZ[1];
        }

        public bool AuthViewMessage(AuthZAttribute authz)
        {
            return authz.MessageAuthZ[2];
        }

        public bool AuthMarkMessage(AuthZAttribute authz)
        {
            return authz.MessageAuthZ[3];
        }

        public bool AuthDeleteMessage(AuthZAttribute authz)
        {
            return authz.MessageAuthZ[4];
        }

        public bool AuthSendInvite(AuthZAttribute authz)
        {
            return authz.InviteAuthZ[0];
        }

        public bool AuthViewInvite(AuthZAttribute authz)
        {
            return authz.InviteAuthZ[1];
        }

        public bool AuthAcceptInvite(AuthZAttribute authz)
        {
            return authz.InviteAuthZ[2];
        }

        public bool AuthDeclineInvite(AuthZAttribute authz)
        {
            return authz.InviteAuthZ[3];
        }

        public bool AuthCreateHousehold(AuthZAttribute authz)
        {
            return authz.HouseholdAuthZ[0];
        }

        public bool AuthViewHousehold(AuthZAttribute authz)
        {
            return authz.HouseholdAuthZ[1];
        }

        public bool AuthEditHousehold(AuthZAttribute authz)
        {
            return authz.HouseholdAuthZ[2];
        }

        public bool AuthDisbandHousehold(AuthZAttribute authz)
        {
            return authz.HouseholdAuthZ[3];
        }

        public bool AuthViewTenant(AuthZAttribute authz)
        {
            return authz.TenantAuthZ[0];
        }

        public bool AuthPromoteTenant(AuthZAttribute authz)
        {
            return authz.TenantAuthZ[1];
        }

        public bool AuthDemoteTenant(AuthZAttribute authz)
        {
            return authz.TenantAuthZ[2];
        }

        public bool AuthRemoveTenant(AuthZAttribute authz)
        {
            return authz.TenantAuthZ[3];
        }

        public bool AuthLeaveHousehold(AuthZAttribute authz)
        {
            return authz.TenantAuthZ[4];
        }

        public bool AuthCreateExpense(AuthZAttribute authz)
        {
            return authz.ExpenseAuthZ[0];
        }

        public bool AuthViewExpense(AuthZAttribute authz)
        {
            return authz.ExpenseAuthZ[1];
        }

        public bool AuthEditExpense(AuthZAttribute authz)
        {
            return authz.ExpenseAuthZ[2];
        }

        public bool AuthDeleteExpense(AuthZAttribute authz)
        {
            return authz.ExpenseAuthZ[3];
        }

        public bool AuthCreateTask(AuthZAttribute authz)
        {
            return authz.TaskAuthZ[0];
        }

        public bool AuthViewTask(AuthZAttribute authz)
        {
            return authz.TaskAuthZ[1];
        }

        public bool AuthEditTask(AuthZAttribute authz)
        {
            return authz.TaskAuthZ[2];
        }

        public bool AuthDeleteTask(AuthZAttribute authz)
        {
            return authz.TaskAuthZ[3];
        }

        public bool AuthCreateSRequest(AuthZAttribute authz)
        {
            return authz.SRequestAuthZ[0];
        }

        public bool AuthViewSRequest(AuthZAttribute authz)
        {
            return authz.SRequestAuthZ[1];
        }

        public bool AuthEditSRequest(AuthZAttribute authz, string creatorName)
        {
            if (authz.SRequestAuthZ[2] == false)
            {
                return creatorName.Equals(authz.DisplayName);
            }
            return authz.SRequestAuthZ[2];
        }

        public bool AuthDeleteSRequest(AuthZAttribute authz, string creatorName)
        {
            if(authz.SRequestAuthZ[3] == false)
            {
                return creatorName.Equals(authz.DisplayName);
            }
            return authz.SRequestAuthZ[3];
        }



    }
}
