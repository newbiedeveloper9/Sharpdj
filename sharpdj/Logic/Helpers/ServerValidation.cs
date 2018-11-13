using System;
using System.Text.RegularExpressions;
using Communication.Shared;

namespace SharpDj.Logic.Helpers
{
    public class ServerValidation
    {
        public enum ResponseValidationEnum
        {
            NullOrEmpty,
            Error,
            Success
        }
        
        public static Tuple<string, ResponseValidationEnum> ServerResponseValidation(string response)
        {
            if(string.IsNullOrEmpty(response))
                return new Tuple<string, ResponseValidationEnum>(string.Empty, ResponseValidationEnum.NullOrEmpty);
                    
            if (response.Equals(Commands.Instance.CommandsDictionary["Error"]))
                return new Tuple<string, ResponseValidationEnum>(string.Empty, ResponseValidationEnum.Error);
            else
            {
                var rgx = new Regex($"{Commands.Instance.CommandsDictionary["Success"]} (.*)");
                var match = rgx.Match(response);
                
                return match.Success ?
                    new Tuple<string, ResponseValidationEnum>(match.Groups[1].Value, ResponseValidationEnum.Success) :
                    new Tuple<string, ResponseValidationEnum>(string.Empty, ResponseValidationEnum.Error);
            }
        }
    }
}