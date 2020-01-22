using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomAid.ServiceLayer.UserManagement
{
    public class Base64UrlConverter : IConverter
    {
        static readonly char padding = '=';

        public Base64UrlConverter() 
        {
        }

        public Dictionary<string, string> Decode(string encoding)
        {
            string convert;
            if (encoding == null)
            {
                return null;
            }

            // Replace reserved characters
            convert = encoding.Replace('-', '+')
                .Replace('_', '/');

            // Replace padding
            switch (encoding.Length % 4)
            { 
                case 0:
                    // Do nothing
                    break;
                case 2:
                    convert += padding + padding;
                    break;
                case 3:
                    convert += padding;
                    break;
                default:
                    // String is encoded improperly
                    return null;
            }

            byte[] stringAsBytes = Convert.FromBase64String(convert);
            string bytesAsJson = Encoding.UTF8.GetString(stringAsBytes);
            Dictionary<string, string> originalDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(bytesAsJson);

            return originalDict;
        }

        public string Encode(Dictionary<string, string> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("Nothing to encode");
            }

            string valueAsJson = JsonConvert.SerializeObject(dictionary);
            byte[] dictAsBytes = Encoding.UTF8.GetBytes(valueAsJson);

            return CleanString(dictAsBytes);
        }

        public string CleanString(byte[] dictAsBytes)
        {
            string base64String = Convert.ToBase64String(dictAsBytes);

            // Clean up: Encode reserved characters
            string urlSafeString = base64String.Trim(padding)
                .Replace('+', '-')
                .Replace('/', '_');

            return urlSafeString;
        }
    }
}
