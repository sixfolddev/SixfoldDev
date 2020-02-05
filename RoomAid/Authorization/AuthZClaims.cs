using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    class AuthZClaims
    {
        public enum AuthZEnum
        {
            None,
            EnabledAccount,
            CreateAdmin,
            EnableACcount,
            DisableACcount,
            EditProfile,
            ViewProfile,
            DeleteAccount,
            SearchHousehold,
            SendMessage,
            ReplyMessage,
            ViewMessage,
            MarkMessage,
            DeleteMessage,
            SendInvite,
            ViewInvite,
            AcceptInvite,
            DeclineInvite,
            ViewTenant,
            PromoteTenant,
            DemoteTenant,
            RemoveTenant,
            LeaveHousehold,
            CreateExpense,
            ViewExpense,
            EditExpense,
            DeleteExpense,
            CreateTask,
            ViewTask,
            EditTask,
            DeleteTask,
            CreateSupplyRequest,
            ViewSupplyRequest,
            EditSupplyRequest,
            DeleteSupplyRequest
        }

        public string UserID { get; }
        public string HouseholdID { get; }
        public AuthZEnum[] Claims { get; }

        
    }
}
