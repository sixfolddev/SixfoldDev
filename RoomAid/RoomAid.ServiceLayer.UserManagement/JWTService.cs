using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class JWTService
    {
        private Base64UrlConverter _encoder;
        private static string secret_key = ConfigurationManager.AppSettings["secret_key"];
        static readonly int sessiontimeout = Int32.Parse(ConfigurationManager.AppSettings["sessiontimeout"]); // 20 minute session timeout

        // Claims
        public const string ISSUED_AT_TIME = "iat";
        public const string EXPIRATION_TIME = "exp";
        public const string JWT_ID = "jti";
        public const string EMAIL = "email";
        public const string ADMIN = "admin";

        public Base64UrlConverter Encoder 
        {
            get
            {
                return _encoder;
            }
            set
            {
                this._encoder = new Base64UrlConverter();
            }
        }

        public JWTService()
        {   
        }

        private string GenerateJWTHeader()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("alg", "HS256"); // HMAC SHA256
            header.Add("typ", "JWT");
            string encodedHeader = Encoder.Encode(header);

            return encodedHeader;
        }

        private string GenerateJWTPayload(User user)
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();
            claims.Add(ISSUED_AT_TIME, getTimeNowInSeconds().ToString());
            claims.Add(EXPIRATION_TIME, (getTimeNowInSeconds() + sessiontimeout).ToString());
            claims.Add(JWT_ID, Guid.NewGuid().ToString());
            claims.Add(EMAIL, user.UserEmail); // TODO: encrypt email
            claims.Add(ADMIN, user.Admin.ToString());
            string encodedPayload = Encoder.Encode(claims);

            return encodedPayload;
        }

        public Int64 getTimeNowInSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        private string GenerateJWTSignature(string key, string header, string payload)
        {
            byte[] headPayInBytes = Encoding.UTF8.GetBytes(header + "." + payload);
            byte[] keyInBytes = Encoding.UTF8.GetBytes(key);
            HMACSHA256 hash = new HMACSHA256(keyInBytes);
            byte[] signature = hash.ComputeHash(headPayInBytes);
            hash.Dispose();
            string encodedSignature = Encoder.CleanString(signature);

            return encodedSignature;
        }

        // Token = header.payload.signature
        public string GenerateJWT(User user)
        {
            string header = GenerateJWTHeader();
            string payload = GenerateJWTPayload(user);
            string signature = GenerateJWTSignature(secret_key, header, payload);

            string token = $"{header}.{payload}.{signature}";
            
            return token;
        }
    }
}
