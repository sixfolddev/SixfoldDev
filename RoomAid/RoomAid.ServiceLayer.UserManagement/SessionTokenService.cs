using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class SessionTokenService
    {
        private JWTService _jwt;
        public SessionTokenService()
        {

        }

        public void StartSession(User user)
        {
            string sessionToken = null;
            // Setup sessions in database to check for active session
            bool ActiveSessionExists = false; // temp placeholder
            if(!ActiveSessionExists)
            {
                sessionToken = _jwt.GenerateJWT(user);
            }
            UserSession session = new UserSession()
            {
                Token = sessionToken,
                SessionId = "", // TODO: generate session id
                IssueTime = _jwt.getTimeNowInSeconds(),
                ExpirationTime = _jwt.getTimeNowInSeconds() + Int32.Parse(ConfigurationManager.AppSettings["sessiontimeout"]),
                UserEmail = user.UserEmail,
                UserCurrentSession = user
            };
        }

        // TODO: Complete method
        public string GetSessionToken(User user) // Pass in User object or masked user id?
        {
            return ""; // Change later
        }
    }
}
