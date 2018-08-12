using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Shared
{
    public class Commands
    {
       //TODO create dictionary with commands. Actually i have 2much refactoring to do.
        
        public const string Register = "reg ";
        public const string SuccessfulRegister = "successreg";
        public const string Login = "login ";
        public const string SuccessfulLogin = "successlogin ";
        public const string Disconnect = "disconnect";

        public const string GetPeoples = "getpeoples";

        public class UserAccount
        {                                                         
            public const string ChangePassword = "chgpass ";      
            public const string ChangeUsername = "chgusername ";  
            public const string ChangeLogin = "chglogin ";        
            public const string ChangeRank = "chgrank ";          

            public class Errors
            {                                                               
                public const string ChangePasswordErr = "chgpasserr";       
                public const string ChangeUsernameErr = "chgusernameerr";   
                public const string ChangeLoginErr = "chgloginerr";         
                public const string ChangeRankErr = "chgrankerr";           
            }

            public class Succesful
            {
                public const string SuccesfulChangePassword = "successchgpass";
                public const string SuccesfulChangeUsername = "successchgusername";
                public const string SuccesfulChangeLogin = "successchglogin";
                public const string SuccesfulChangeRank = "successchgrank";
            }
        }

        public const string Success = "success ";
        public const string Error = "error ";

        public class Errors
        {
            public const string RegisterErr = "failreg";
            public const string RegisterAccExistErr = "failaccexist";
            public const string LoginErr = "faillogin";
            public const string GetPeoplesErr = "failgetpeoples";
        }

        public class Client
        {
            public const string JoinRoom = "joinroom ";
            public const string CreateRoom = "createroom ";
            public const string AfterLogin = "afterlogin";

            public class Room
            {
                public const string JoinQueue = "joinqueue $";
                public const string UpdateDj = "updatedj";

            }  
        }
    }
}
