using System;
using System.Collections.Generic;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class UserSession
    {
        // Private backing fields
        private string _token;
        private string _sessionId;
        private Int64 _issueTime;
        private Int64 _expirationTime;
        private string _userEmail;

        // Public accessors
        public string Token { get; set; }
        public string SessionId { get; set; }
        public Int64 IssueTime { get; set; }
        public Int64 ExpirationTime { get; set; }
        public string UserEmail { get; set; }
        public User UserCurrentSession { get; set; }

        // Empty default constructor
        public UserSession()
        {
            Token = _token;
            SessionId = _sessionId;
            IssueTime = _issueTime;
            ExpirationTime = _expirationTime;
            UserEmail = _userEmail;
            UserCurrentSession = new User();
        }

        public UserSession(string token, string sid, Int64 iat, Int64 exp, string email, User user)
        {
            Token = token;
            SessionId = sid;
            IssueTime = iat;
            ExpirationTime = exp;
            UserEmail = email;
            UserCurrentSession = user;
        }
    }
}
