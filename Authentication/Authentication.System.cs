using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace AuthenticationSystem
{
    public class Authentication
    {
        private string retrievedSalt;
        private string userID;
        private string password;
        private string hashed;
        private bool authenticated;
        private double sessionID;

        //Create object when user tries to log in
        public Authentication(string userID, string password)
        {
            this.userID = userID;
            this.password = password;
            setRetrievedSalt();
            generateHash();
            authenticated = false;
        }

        //Function that retrieves salt that is tied to user ID
        //If no salt exists, User ID doesn't exist
        public void setRetrievedSalt()
        {
            try
            {
                //pull salt from pw file
                retrievedSalt = "AE2012DEWE193241"; //test salt
            }
            catch (Exception)
            {
                //Catch error handling.  Setting variable to -1 for now
                retrievedSalt = "-1";
            }
        }

        public string getRetrievedSalt()
        {
            return retrievedSalt;
        }
        public string generateHash()
        {
            //Convert salt into byte array for hashing function
            byte[] array = Encoding.ASCII.GetBytes(retrievedSalt);

            hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, //User input password when logging in
                salt: array,        //Salt of user ID turned into byte[]
                prf: KeyDerivationPrf.HMACSHA256,   //Derived Key Function
                iterationCount: 100000,             //Iterations to slow down hashing
                numBytesRequested: 256 / 8));       //32 bytes

            return hashed;
        }

        //Function to retrieve hashed password tied to a userID.
        public string dataStoreHash()
        {
            string storedHash;
            try
            {
                //Retrieve hash connected to user ID from pw file
                storedHash = "ZjhxyGVzc0vJ/GAC5udhJxTp41BI6o2l50UFBEtjsmU=";
            }
            catch (Exception)
            {
                storedHash = "";
            }

            return storedHash;
        }

        //function to check if user ID and password that has been input is correct
        public bool compareHashes()
        {
            if (hashed == dataStoreHash())
            {
                authenticated = true;
            }
            else
                authenticated = false;

            return authenticated;
        }

        public string generateJWT()
        {
            //Json Web token
            //Header, payload, signature
            //const token = base64urlEncoding(header) + '.' + 
            //  base64urlEncoding(payload) + '.' + base64urlEncoding(signature)
            return "hello";
        }
    }
}
