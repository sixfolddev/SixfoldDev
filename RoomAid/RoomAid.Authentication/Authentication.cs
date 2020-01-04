using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace RoomAid.Authentication
{
    public class Authentication
    {
        private string _retrievedSalt;
        private string _userID;
        private string _password;
        private string _hashed;
        private bool _authenticated;

        //Create object when user tries to log in
        public Authentication(string userID, string password)
        {
            this._userID = userID;
            this._password = password;
            _retrievedSalt = "";
            SetRetrievedSalt();
            GenerateHash();
            _authenticated = false;
        }

        //Function that retrieves salt that is tied to user ID
        //If no salt exists, User ID doesn't exist
        public void SetRetrievedSalt()
        {
            try
            {
                //pull salt from pw file
                _retrievedSalt = "AE2012DEWE193241"; //test salt
            }
            catch (Exception)
            {
                //Catch error handling.
                //Returns error back to login screen if salt doesn't exist 
                //which means account doesn't exist.
                _retrievedSalt = "";
            }
        }

        public string GetRetrievedSalt()
        {
            return _retrievedSalt;
        }

        //Generate a hash with user entered pw and salt tied to user account
        public string GenerateHash()
        {
            //Convert salt into byte array for hashing function
            byte[] array = Encoding.ASCII.GetBytes(_retrievedSalt);

            _hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: _password, //User input password when logging in
                salt: array,        //Salt of user ID turned into byte[]
                prf: KeyDerivationPrf.HMACSHA256,   //Derived Key Function
                iterationCount: 100000,             //Iterations to slow down hashing
                numBytesRequested: 256 / 8));       //32 bytes hash

            return _hashed;
        }

        //Function to retrieve hashed password tied to a userID.
        //hashed pw not stored in variable for security reasons
        public string DataStoreHash()
        {
            string storedHash;
            try
            {
                //Retrieve hash connected to user ID from pw file
                storedHash = "ZjhxyGVzc0vJ/GAC5udhJxTp41BI6o2l50UFBEtjsmU=";
            }
            catch (Exception)
            {
                //If hashed pw cannot be retrieved, authentication will fail because
                //the comparison will fail
                storedHash = "";
            }

            return storedHash;
        }

        //function to check if user ID and password that has been input is correct
        public bool CompareHashes()
        {
            if (_hashed == DataStoreHash())
            {
                _authenticated = true;
            }
            else
                _authenticated = false;

            return _authenticated;
        }
    }
}

