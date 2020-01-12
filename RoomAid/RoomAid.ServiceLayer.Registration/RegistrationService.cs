using System;
using System.IO;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RoomAid.ServiceLayer.Registration
{
    public class RegistrationService
    {
        public Iresult NameCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["nameLength"]);
            Iresult result = CheckLength(input, nameLength);
            return result;
        }

       public Iresult PasswordCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["passwordLength"]);
            Iresult result = CheckLength(input, nameLength);
            if (result.isSuccess == false)
            {
                return result;
            }
            if(input.Length< Int32.Parse(ConfigurationManager.AppSettings["passwordMinLength"])){
                result = new CheckResult("Password is too short!", false);
                return result;
            }

            if (input.Contains("<") || input.Contains(">"))
            {
                result = new CheckResult("Password cannot contain '<' or '>' !", false);
                return result;
            }

            string repetitiveCheckResult = RepetitiveCheck(input);
            if (repetitiveCheckResult!=null)
            {
                result = new CheckResult("Password contains repetitive contents: '"+repetitiveCheckResult+"'", false);
                return result;
            }

            string sequentialCheckResult = SequentialCheck(input);
            if (sequentialCheckResult != null)
            {
                result = new CheckResult("Password contains repetitive contents: '"+ sequentialCheckResult+"'", false);
                return result;
            }

            string ListCheckResult = ListCheck(input);
            if (ListCheckResult != null)
            {
                result = new CheckResult("'"+ListCheckResult+"' can be found in dictionary!", false);
                return result;
            }

            return result;
        }

        public Iresult PasswordUserNameCheck(string input, string userName)
        {
            Iresult result = new CheckResult("Password is valid!", true);
            if (input == userName)
            {
               result = new CheckResult("You cannot use username as a password!", false);
                
            }
            return result;
        }

        public Iresult EmailCheck(string input)
        {
            int nameLength = Int32.Parse(ConfigurationManager.AppSettings["emailLength"]);
            Iresult result = CheckLength(input, nameLength);

           bool isEmail =  new EmailAddressAttribute().IsValid(input);

            if (!isEmail)
            {
                result = new CheckResult("Not a valid Email Address!", false);
                return result;
            }
            //TODO: check if email is already registered in database
            return result;
           
        }
        private Iresult CheckLength(string input,int length)
        {
            
            Iresult result = null;
            if (string.IsNullOrWhiteSpace(input))
            {
                result = new CheckResult("No input!", false);
            }
            else if (input.Length > length)
            {
                 result = new CheckResult("input is too long!", false);
            }
            else
            {
                result = new CheckResult("input is valid!", true);
            }
            return result;
        }

        private string RepetitiveCheck(string input)
        {
            int checkRange = Int32.Parse(ConfigurationManager.AppSettings["repetitiveRange"]);
            if (checkRange > input.Length)
            {
                checkRange = input.Length;
            }
            string result = "";
            for (int i = 0; i < input.Length-checkRange; i++)
            {
                bool ifRepetitive = true;
                result = result + input[i];

                for (int j = i + 1; i < checkRange - 1; i++)
                {
                    result = "";
                    
                    if (input[i]!=input[j])
                    {
                        ifRepetitive = false;
                    }
                    else
                    {
                        result = result + input[j];
                    }
                }

                if (ifRepetitive)
                {
                    return result;
                }
            }
            return null;
        }

        private string SequentialCheck(string input)
        {

            Byte[] charList = Encoding.ASCII.GetBytes(input);
            int checkRange = Int32.Parse(ConfigurationManager.AppSettings["sequtentialRange"]);
            if (checkRange > input.Length)
            {
                checkRange = input.Length;
            }
            string result = "";
            for (int i =0; i < charList.Length - checkRange; i++)
            {
                bool ifSequential = true;
                result = result + input[i];
                for(int j=i+1; i < checkRange - 1; i++)
                {
                   
                    if (charList[j] - charList[i] != 1&&charList[i]-charList[j]!=1)
                    {
                        ifSequential = false;
                    }
                    else
                    {
                        result = result += charList[j];
                    }
                }
                if (ifSequential)
                {
                    return result;
                }
            }
            return null;
            
        }

        private string ListCheck(string input)
        {
            List<string> wordList = File.ReadAllLines(Path.GetFullPath(ConfigurationManager.AppSettings["wordListName"])).ToList();
            foreach(string word in wordList)
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
