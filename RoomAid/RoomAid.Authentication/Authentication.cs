using System;
using System.Security.Cryptography;
using System.Text;

namespace RoomAid.Authentication
{
    public class Authentication
    {
        private string userID;
        private string password;
        private bool _authenticated;
        
        //Create object when user tries to log in
        public Authentication(string userID, string password)
        {
            //Get salt from database tied to input account ID
            //Call method to hash input password and salt
            //Call method to retrieve stored hash password
            //Compare generated password with stored password
            //Don't store into variables
            this.userID = userID;
            this.password = password;
            _authenticated = false;
            if (CompareHashes())
                _authenticated = true;
            else
                _authenticated = false;
        }
        public bool CompareHashes()
        {
            if (GenerateHash(userID, password) == DataStoreHash())
            {
                _authenticated = true;
            }
            else
                _authenticated = false;

            return _authenticated;
        }

        public string GenerateHash(string userID, string password)
        {
            int iterations = 100000;
            //concatenate salt and input password to run through hashing

            var hash = new Rfc2898DeriveBytes(password, GetSalt(userID), 
                iterations, HashAlgorithmName.SHA256);
            var passwordToCheck = Encoding.Default.GetString(hash.GetBytes(32));

            return passwordToCheck;
        }
        public string DataStoreHash()
        {
            string storedHash;
            try
            {
                //Retrieve hash connected to user ID from pw file
                storedHash = "f8qÈessKÉü`\u0002æça'\u0014éãPHê\u008d¥çE\u0005\u0004Kc²e";
            }
            catch (Exception)
            {
                //If hashed pw cannot be retrieved, authentication will fail because
                //the comparison will fail
                storedHash = "";
            }

            return storedHash;
        }
        public byte[] GetSalt(string userID)
        {
            try
            {
                //pull salt from pw file
                return Encoding.ASCII.GetBytes("AE2012DEWE193241"); //test salt
            }
            catch (Exception)
            {
                //Catch error handling.
                //Returns error back to login screen if salt doesn't exist 
                //which means account doesn't exist.
                return Encoding.ASCII.GetBytes("");
            }
        }
        public bool GetAuthenticated()
        {
            return _authenticated;
        }
        public byte[] GenerateSalt()
        {
            var salt = new byte[32];

            var random = new RNGCryptoServiceProvider();
            random.GetBytes(salt);

            return salt;
        }
    }
}

