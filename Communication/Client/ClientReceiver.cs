using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Communication.Shared;
using Hik.Communication.Scs.Client;

namespace Communication.Client
{
    public class ClientReceiver
    {
        public class MessagesPattern
        {
            /// <summary>
            /// <para>
            /// 1- Username
            /// </para>
            /// </summary>
            public const string UserDisconnectRgx = Commands.Disconnect + " (.*)";

            /// <summary>
            /// <para>
            /// 1 - Username
            /// 2- Rank
            /// </para>
            /// </summary>
            public const string SuccesfulLoginRgx = Commands.SuccessfulLogin + "(.*) (.*)";


            /// <summary>
            /// <para>
            /// 1 - Username
            /// 2 - Rank
            /// </para>
            /// </summary>
            public const string GetPeopleRgx = "(.*) (.*)";
        }

        public void ParseMessage(IScsClient client, string message)
        {
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
                        Rank = (Rank)Enum.Parse(typeof(Rank), rank, true),
                        Username = username
                    };

                    Console.WriteLine("Succesful login, {0}!\n Your rank: {1}", username, clientServer.Rank);
                    OnSuccessfulLogin(EventArgs.Empty);
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
                                       Rank = (Rank)Enum.Parse(typeof(Rank), rank, true)
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
        }

        #region Events
        public event EventHandler<UserDisconnectEventArgs> UserDisconnect;

        protected virtual void OnUserDisconnect(UserDisconnectEventArgs e)
        {
            var handler = UserDisconnect;
            handler?.Invoke(this, e);
        }

        #region Succesful
        public event EventHandler SuccessfulLogin;
        public event EventHandler SuccesfulRegister;
        public event EventHandler<GetPeopleEventArgs> GetPeople;

        protected virtual void OnSuccessfulLogin(EventArgs e)
        {
            EventHandler eh = SuccessfulLogin;
            eh?.Invoke(this, e);
        }
        protected virtual void OnSuccesfulRegister(EventArgs e)
        {
            EventHandler eh = SuccesfulRegister;
            eh?.Invoke(this, e);
        }
        protected virtual void OnGetPeople(GetPeopleEventArgs e)
        {
            var handler = GetPeople;
            handler?.Invoke(this, e);
        }


        public class GetPeopleEventArgs : System.EventArgs
        {
            public GetPeopleEventArgs(List<UserClient> userClients)
            {
                this.UserClients = userClients;
            }

            public List<UserClient> UserClients { get; private set; }
        }
        #endregion
        #region Errors
        public event EventHandler LoginErr;
        public event EventHandler RegisterErr;
        public event EventHandler RegisterAccExistErr;
        public event EventHandler GetPeopleErr;


        protected virtual void OnLoginErr(EventArgs e)
        {
            EventHandler eh = LoginErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnRegisterErr(EventArgs e)
        {
            EventHandler eh = RegisterErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnRegisterAccExistErr(EventArgs e)
        {
            EventHandler eh = RegisterAccExistErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnGetPeopleErr(EventArgs e)
        {
            EventHandler eh = GetPeopleErr;
            eh?.Invoke(this, e);
        }
        #endregion
        #region UserAccount Errors

        public event EventHandler ChangePasswordError;
        public event EventHandler ChangeUsernameError;
        public event EventHandler ChangeLoginError;
        public event EventHandler ChangeRankError;


        protected virtual void OnChangePasswordError(EventArgs e)
        {
            EventHandler eh = ChangePasswordError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeUsernameError(EventArgs e)
        {
            EventHandler eh = ChangeUsernameError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeLoginError(EventArgs e)
        {
            EventHandler eh = ChangeLoginError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeRankError(EventArgs e)
        {
            EventHandler eh = ChangeRankError;
            eh?.Invoke(this, e);
        }
        #endregion
        #region UserAccount Successful
        public event EventHandler ChangePasswordSuccessful;
        public event EventHandler ChangeUsernameSuccessful;
        public event EventHandler ChangeLoginSuccessful;
        public event EventHandler ChangeRankSuccessful;


        protected virtual void OnChangePasswordSuccessful(EventArgs e)
        {
            EventHandler eh = ChangePasswordSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeUsernameSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeUsernameSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeLoginSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeLoginSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeRankSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeRankSuccessful;
            eh?.Invoke(this, e);
        }
        #endregion

        public class UserDisconnectEventArgs : System.EventArgs
        {
            public UserDisconnectEventArgs(string username)
            {
                this.Username = username;
            }

            public string Username { get; private set; }
        }
        #endregion
    }
}
