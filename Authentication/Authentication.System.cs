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

        public Authentication(string userID, string password)
        {
            this.userID = userID;
            this.password = password;
            setRetrievedSalt();
            generateHash();
        }
        public void setRetrievedSalt()
        {
            try
            {
                //pull salt from pw file
                retrievedSalt = "AE2019"; //test salt
            }
            catch (Exception)
            {
                retrievedSalt = "-1";
            }
            
            //return salt from password file
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
                password: password,
                salt: array,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            Console.WriteLine("Hashed: {hashed}");

            return hashed;
        }

        public string dataStoreHash()
        {
            string storedHash;
            try
            {
                storedHash = "test";
            }
            catch (Exception)
            {
                storedHash = "-1";
            }

            return storedHash;
        }

        public bool compareHashes()
        {
            //bool compare = false;

            if (hashed == dataStoreHash())
                return true;
            else
                return false;
        }
    }
}
