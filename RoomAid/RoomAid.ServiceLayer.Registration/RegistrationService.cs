using System;
using System.IO;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RoomAid.ServiceLayer.Registration
{
    public class RegistrationService
    {
        /// <summary>
        /// Method NameCheck() is used to check if the firstname and last name entered by user is valid.
        /// According to the requirement, firstname and lastname can be up to 200 characters.
        ///can be alphanumeric with special characters. 
        /// </summary>
        /// <param name="input">The input string for either firstname or lastname, should be passed from fortnend or controller</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult NameCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["nameLength"]);
            Iresult result = CheckLength(input, nameLength);
            return result;
        }

        /// <summary>
        /// Method PasswordCheck() is used to check if thepassword entered by user is valid.
        /// According to the requirement, password can be up to 2000 characters.
        ///Can be alphanumeric with special characters.
        ///The allowed special characters are every special character on the US standard keyboard except for < and >.
        ///Should be a minimum of 12 characters.
        ///The password should be compared a list of values known to be commonly-used, expected, or compromised:
        ///Passwords obtained from previous breach corpuses.
        ///Words contained in a dictionary.
        ///Repetitive or sequential characters (e.g. ‘1234’, ‘bbbbbb’).
        ///Context specific words, such as the name of the application or the current username.
        ///The method will call all check method to valid the password
        /// </summary>
        /// <param name="input">The input string password, should be passed from fortnend or controller</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult PasswordCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["passwordLength"]);

            //check password length
            Iresult result = CheckLength(input, nameLength);
            if (!result.isSuccess)
            {
                return result;
            }

            string message = "";
            bool ifPass = true;

            //check min password length
            if (input.Length < Int32.Parse(ConfigurationManager.AppSettings["passwordMinLength"]))
            {
                result = new CheckResult("Your password needs at least " + ConfigurationManager.AppSettings["passwordMinLength"]
                    + " characters", false);
                return result;
            }
            
            //check if the password contains < and >
            if (input.Contains("<") || input.Contains(">"))
            {
                message = message + "\nPassword cannot contain '<' or '>' !";
                ifPass = false;
            }

            //check if the password contains repetitive contents
            string repetitiveCheckResult = RepetitiveCheck(input);
            if (repetitiveCheckResult!=null)
            {
                message = message + "\n" + ConfigurationManager.AppSettings["passwordRepetitive"]+"'"
                    + repetitiveCheckResult + "'";
                ifPass = false;
            }

            //check if the password contains sequential contents
            string sequentialCheckResult = SequentialCheck(input);
            if (sequentialCheckResult != null)
            {
                message = message + "\n" + ConfigurationManager.AppSettings["passwordSequential"] + "'"
                  + sequentialCheckResult + "'";
                ifPass = false;
            }

            //check if the password contains words in a list that contains most used password and most used words in dictionary
            string ListCheckResult = PasswordListCheck(input);
            if (ListCheckResult != null)
            {
                message = message + "\n "  + ConfigurationManager.AppSettings["passwordCommon"] + ListCheckResult;
                ifPass = false;
            }
            result = new CheckResult(message, ifPass);

            return result;
        }

        /// <summary>
        /// Method PasswordUserNameCheck() is used to check if the password entered by user is valid.
        /// check if the password contain or is the same input as email/username
        /// </summary>
        /// <param name="input">The input string password, should be passed from fortnend or controller</param>
        /// <param name="userName">The username (should be an email) should be passed from fortnend or controller</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult PasswordUserNameCheck(string input, string userName)
        {
            Iresult result = new CheckResult(ConfigurationManager.AppSettings["messagePass"], true);
            if (input.Contains(userName))
            {
               result = new CheckResult(ConfigurationManager.AppSettings["passwordUserName"], false); 
            }
            return result;
        }

        /// <summary>
        /// Method EmailCheck() is used to check if the email entered by user is valid.
        /// According to the requirement: o	Can be up to 200 characters.
        ///Can be alphanumeric with special characters.
        ///Must be in email format(name @ domain).
        ///has to be unique among users.
        ///will check if the format is correct and then search database to make sure the email is not used
        /// </summary>
        /// <param name="input">The input string email, should be passed from fortnend or controller</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        public Iresult EmailCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["emailLength"]);
            Iresult result = CheckLength(input, nameLength);

           bool isEmail = Regex.IsMatch(input,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (!isEmail)
            {
                result = new CheckResult("Not a valid Email Address!", false);
                return result;
            }

            //If length check passed, continue to check if email is already used, if not, stop checking
            string message = result.message;
            bool ifPass = result.isSuccess;

            //TODO: check if email is already registered in database

            result = new CheckResult(message, ifPass);
            return result;
           
        }

        /// <summary>
        /// Method CheckLength() is used to check if the input is empty or too long
        /// <param name="input">The input string , should be passed from fortnend or controller</param>
        /// <returns>Iresult result the object that contains a message and if the check is true or false</returns>
        private Iresult CheckLength(string input,int length)
        {
            
            Iresult result = null;
            if (string.IsNullOrWhiteSpace(input))
            {
                result = new CheckResult(ConfigurationManager.AppSettings["messageNameEmpty"], false);
            }
            else if (input.Length > length)
            {
                 result = new CheckResult("Your name should not be longer than "+ ConfigurationManager.AppSettings["nameLength"]
                     +"characters ", false);
            }
            else
            {
                result = new CheckResult(ConfigurationManager.AppSettings["messagePass"], true);
            }
            return result;
        }

        /// <summary>
        /// Method Repetitive(Check) is used to check if the input contains repetitive contents based on a check range in config
        /// <param name="input">The input string , should be passed from fortnend or controller</param>
        /// <returns>string result return string that found repetitive or a null</returns>
        private string RepetitiveCheck(string input)
        {
            int checkRange = Int32.Parse(ConfigurationManager.AppSettings["repetitiveRange"]);

            if (checkRange > input.Length)
            {
                checkRange = input.Length;
            }

          
            for (int i = 1; i < input.Length-checkRange; i++)
            {
                bool ifRepetitive = true;
                string result = "" + input[i];
                for (int j = 1; j < checkRange; j++)
                {
                   
                    
                    if (input[i]!=input[i+j])
                    {
                        ifRepetitive = false;
                    }

                    else
                    {
                        result = result + input[i+j];
                    }
                }

                if (ifRepetitive)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Method SequentialCheck() is used to check if the input contains sequential contents based on a check range in config
        /// <param name="input">The input string , should be passed from fortnend or controller</param>
        /// <returns>string result return string that found sequential or a null</returns>
        private string SequentialCheck(string input)
        {

           char[] charList = input.ToCharArray();
           for(int i =1; i < input.Length - 1; i++)
            {
                if (charList[i - 1].CompareTo(charList[i]) == -1 && charList[i].CompareTo(charList[i + 1])== -1)
                {
                    string result = input.Substring(i-1,3);
                    for(int j = i+1;j< input.Length; j++)
                    {
                        if (charList[j].CompareTo(charList[j + 1]) == -1)
                        {
                            result = result + charList[j + 1];
                        }
                        else{
                            return result;
                        }
                    }
                }

                else if (charList[i - 1].CompareTo(charList[i]) == 1 && charList[i].CompareTo(charList[i + 1]) == 1)
                {
                    string result = input.Substring(i - 1, 3);
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        if (charList[j].CompareTo(charList[j + 1]) == 1)
                        {
                            result = result + charList[j + 1];
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
            
        }

        /// <summary>
        /// Method PasswordListCheck() is used to check if the input password contains contents that can be found in a check list, the list contains many common used words 
        /// and password
        /// <param name="input">The input string , should be passed from fortnend or controller</param>
        /// <returns>string result return string that found in list or a null</returns>
        private string PasswordListCheck(string input)
        {
            List<string> passwordList = File.ReadAllLines(Path.GetFullPath(ConfigurationManager.AppSettings["passwordListName"])).ToList();

            foreach (string word in passwordList)
            {
                if (input.Contains(word))
                {
                    return word;
                }
            }
            return null;
        }


    }
}
