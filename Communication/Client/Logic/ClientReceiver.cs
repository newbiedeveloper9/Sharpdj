using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Communication.Client.Logic.ResponseActions;
using Communication.Shared;
using Hik.Communication.Scs.Client;

namespace Communication.Client.Logic
{
    public class ClientReceiver : ClientReceiverEvents
    {
        public class MessagesPattern
        {
            public const string UserDisconnectRgx = Commands.Disconnect + " (.*)";
            public const string SuccesfulLoginRgx = Commands.SuccessfulLogin + @"(.*)\$(.*)";
            public const string GetPeopleRgx = @"(.*)\$(.*)";
            public const string UpdateDjRgx = @"updatedj\$(.*)";
        }

        public void ParseMessage(IScsClient client, string message)
        {
            IResponseActions responseActions;

            
            
            #region User Disconnected

            if (message.StartsWith(Commands.Disconnect))
            {
                Regex rgx = new Regex(MessagesPattern.UserDisconnectRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var username = match.Groups[1].Value;
                    Console.WriteLine("{0} disconnected", username);
                    OnUserDisconnect(new UserDisconnectEventArgs(username));
                }
                else
                {
                    Console.WriteLine("Problem with matching disconnected user.");
                }
            }

            #endregion

            #region Login Error

            else if (message.Equals(Commands.Errors.LoginErr))
            {
                Console.WriteLine("Error with login");
                OnLoginErr(EventArgs.Empty);
            }

            #endregion

            #region Register Error

            else if (message.Equals(Commands.Errors.RegisterErr))
            {
                Console.WriteLine("Error with register");
                OnRegisterErr(EventArgs.Empty);
            }

            #endregion

            #region Register Account Exist Error

            else if (message.Equals(Commands.Errors.RegisterAccExistErr))
            {
                Console.WriteLine("Username exist!");
                OnRegisterAccExistErr(EventArgs.Empty);
            }

            #endregion

            #region Successful login

            else if (message.StartsWith(Commands.SuccessfulLogin))
            {
                Regex rgx = new Regex(MessagesPattern.SuccesfulLoginRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var username = match.Groups[1].Value;
                    var rank = match.Groups[2].Value;

                    var clientServer = new UserClient
                    {
                        Rank = (Rank) Enum.Parse(typeof(Rank), rank, true),
                        Username = username
                    };

                    Console.WriteLine("Succesful login, {0}!\n Your rank: {1}", username, clientServer.Rank);
                    OnSuccessfulLogin(new SuccesfulLoginEventArgs(username, clientServer.Rank));
                }
                else
                {
                    Console.WriteLine("Problem with matching succes login.");
                }
            }

            #endregion

            #region Succesful register

            else if (message.Equals(Commands.SuccessfulRegister))
            {
                Console.WriteLine("Succesful register!");
                OnSuccesfulRegister(EventArgs.Empty);
            }

            #endregion

            #region Change Password Error

            else if (message.Equals(Commands.UserAccount.Errors.ChangePasswordErr))
            {
                Console.WriteLine("Error with changing password");
                OnChangePasswordError(EventArgs.Empty);
            }

            #endregion

            #region Change Username Error

            else if (message.Equals(Commands.UserAccount.Errors.ChangeUsernameErr))
            {
                Console.WriteLine("Error with changing username");
                OnChangeUsernameError(EventArgs.Empty);
            }

            #endregion

            #region Change Login Error

            else if (message.Equals(Commands.UserAccount.Errors.ChangeLoginErr))
            {
                Console.WriteLine("Error with changing login");
                OnChangeLoginError(EventArgs.Empty);
            }

            #endregion

            #region Change Rank Error

            else if (message.Equals(Commands.UserAccount.Errors.ChangeRankErr))
            {
                Console.WriteLine("Error with changing rank");
                OnChangeRankError(EventArgs.Empty);
            }

            #endregion

            #region Succesfull Change Password

            else if (message.Equals(Commands.UserAccount.Succesful.SuccesfulChangePassword))
            {
                OnChangePasswordSuccessful(EventArgs.Empty);
                Console.WriteLine("Password changed!");
            }

            #endregion

            #region Succesfull Change Username

            else if (message.Equals(Commands.UserAccount.Succesful.SuccesfulChangeUsername))
            {
                OnChangeUsernameSuccessful(EventArgs.Empty);
                Console.WriteLine("Username changed!");
            }

            #endregion

            #region Succesfull Change Login

            else if (message.Equals(Commands.UserAccount.Succesful.SuccesfulChangeLogin))
            {
                OnChangeLoginSuccessful(EventArgs.Empty);
                Console.WriteLine("Login changed!");
            }

            #endregion

            #region Succesfull Change Rank

            else if (message.Equals(Commands.UserAccount.Succesful.SuccesfulChangeRank))
            {
                OnChangeRankSuccessful(EventArgs.Empty);
                Console.WriteLine("Rank changed!");
            }

            #endregion

            #region Get People

            else if (message.StartsWith(Commands.GetPeoples))
            {
                var tabs = message.Split('\n').ToList();
                tabs.RemoveAt(0);


                var userClients = (from tab in tabs
                    let rgx = new Regex(MessagesPattern.GetPeopleRgx)
                    select rgx.Match(tab)
                    into match
                    where match.Success
                    let username = match.Groups[1].Value
                    let rank = match.Groups[2].Value
                    select new UserClient
                    {
                        Username = username,
                        Rank = (Rank) Enum.Parse(typeof(Rank), rank, true)
                    }).ToList();

                foreach (var userClient in userClients)
                {
                    Console.WriteLine("{0} with rank {1}", userClient.Username, userClient.Rank);
                }

                OnGetPeople(new GetPeopleEventArgs(userClients));
            }

            #endregion

            #region Get People Error

            else if (message.Equals(Commands.Errors.GetPeoplesErr))
            {
                Console.WriteLine("Denied to get peoples list");
                OnGetPeopleErr(EventArgs.Empty);
            }

            #endregion

            #region UpdateDjResponse

            else if (message.StartsWith("updatedj"))
            {
                responseActions = new UpdateDjResponse();
                ParserResponse(message, MessagesPattern.UpdateDjRgx, responseActions);
/*             ;*/
            }

            #endregion
        }

        private bool ParserResponse(string message, string regex, IResponseActions responseActions)
        {
            var rgx = new Regex(regex);
            var match = rgx.Match(message);

            if (match.Success)
            {
                responseActions.OnSuccess(match.Groups);
                return true;
            }

            responseActions.OnFailed(match.Groups);
            return false;
        }
    }
}