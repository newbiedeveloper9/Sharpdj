using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Communication.Server;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment
{
    public sealed class ServerReceiver : ServerReceiverEvents
    {
        private class MessagesPattern
        {
            public const string RegisterRgx = Commands.Register + @"(.*)\$(.*)\$(.*)";
            public const string LoginRgx = Commands.Login + @"(.*)\$(.*)";
            public const string ChangePasswordRgx = Commands.UserAccount.ChangePassword + @"(.*)\$(.*)";
            public const string ChangeUsernameRgx = Commands.UserAccount.ChangeUsername + @"(.*)\$(.*)";
            public const string ChangeLoginRgx = Commands.UserAccount.ChangeLogin + @"(.*)\$(.*)";
            public const string ChangeRankRgx = Commands.UserAccount.ChangeRank + @"(.*)\$(.*)";
            public const string JoinRoomRgx = Commands.Client.JoinRoom + "([0-9]+)";
            public const string CreateRoomRgx = Commands.Client.CreateRoom + @"(.*)\$(.*)\$(.*)";
            public const string JoinQueueRgx = @"joinqueue\$(.*)";
        }

        public void ParseMessage(IScsServerClient client, string message, string messageId)
        {
            #region Disconnect

            if (message.Equals(Commands.Disconnect))
            {
                OnDisconnect(new DisconnectEventArgs(client));
            }

            #endregion Disconnect

            #region Login

            else if (message.StartsWith(Commands.Login))
            {
                Regex rgx = new Regex(ServerReceiver.MessagesPattern.LoginRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var login = match.Groups[1].Value;
                    var password = match.Groups[2].Value;

                    OnLogin(new LoginEventArgs(login, password, client));
                }
                else
                {
                    Console.WriteLine(Commands.Errors.LoginErr);
                    ServerSender.Error.LoginError(client);
                }
            }

            #endregion Login

            #region Register

            else if (message.StartsWith(Commands.Register))
            {
                Regex rgx = new Regex(MessagesPattern.RegisterRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var login = match.Groups[1].Value;
                    var password = match.Groups[2].Value;
                    var email = match.Groups[3].Value;

                    OnRegister(new RegisterEventArgs(login, password, email, client));
                }
                else
                {
                    Console.WriteLine("Fail in register");
                    ServerSender.Error.RegisterError(client);
                }
            }

            #endregion Register

            #region ChangePassword

            else if (message.StartsWith(Commands.UserAccount.ChangePassword))
            {
                Regex rgx = new Regex(MessagesPattern.ChangePasswordRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newPassword = match.Groups[2].Value;

                    OnChangePassword(new ChangePasswordEventArgs(client, password, newPassword));
                }
                else
                {
                    Console.WriteLine("Fail with changing password");
                    ServerSender.Error.ChangePasswordError(client);
                }
            }

            #endregion ChangePassword

            #region ChangeUsername

            else if (message.StartsWith(Commands.UserAccount.ChangeUsername))
            {
                Regex rgx = new Regex(MessagesPattern.ChangeUsernameRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newUsername = match.Groups[2].Value;

                    OnChangeUsername(new ChangeUsernameEventArgs(client, password, newUsername));
                }
                else
                {
                    Console.WriteLine("Fail with changing username");
                    ServerSender.Error.ChangeUsernameError(client);
                }
            }

            #endregion ChangeUsername

            #region ChangeLogin

            else if (message.StartsWith(Commands.UserAccount.ChangeLogin))
            {
                Regex rgx = new Regex(MessagesPattern.ChangeLoginRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newLogin = match.Groups[2].Value;

                    OnChangeLogin(new ChangeLoginEventArgs(client, password, newLogin));
                }
                else
                {
                    Console.WriteLine("Fail with changing login");
                    ServerSender.Error.ChangeLoginError(client);
                }
            }

            #endregion ChangeLogin

            #region ChangeRank

            else if (message.StartsWith(Commands.UserAccount.ChangeRank))
            {
                Regex rgx = new Regex(MessagesPattern.ChangeRankRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newRank = match.Groups[2].Value;

                    OnChangeRank(new ChangeRankEventArgs(client, password,
                        (Rank) Enum.Parse(typeof(Rank), newRank, true)));
                }
                else
                {
                    Console.WriteLine("Fail with changing rank");
                    ServerSender.Error.ChangeRankError(client);
                }
            }

            #endregion

            #region GetPeoples

            else if (message.Equals(Commands.GetPeoples))
            {
                OnGetPeople(new GetPeopleEventArgs(client));
            }

            #endregion GetPeoples

            #region JoinRoom

            else if (message.StartsWith(Commands.Client.JoinRoom))
            {
                Regex rgx = new Regex(MessagesPattern.JoinRoomRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var roomId = match.Groups[1].Value;

                    OnJoinRoom(new JoinRoomEventArgs(Convert.ToInt32(roomId), client, messageId));
                }
            }

            #endregion JoinRoom 

            #region CreateRoom

            else if (message.StartsWith(Commands.Client.CreateRoom))
            {
                Regex rgx = new Regex(MessagesPattern.CreateRoomRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var name = match.Groups[1].Value;
                    var image = match.Groups[2].Value;
                    var description = match.Groups[3].Value;

                    OnCreateRoom(new CreateRoomEventArgs(client, name, image, description));
                }
            }

            #endregion CreateRoom

            #region AfterLogin

            else if (message.StartsWith(Commands.Client.AfterLogin))
            {
                OnAfterLogin(new AfterLoginEventArgs(client, messageId));
            }

            #endregion AfterLogin

            #region JoinQueue

            else if (message.StartsWith("joinqueue$"))
            {
                Regex rgx = new Regex(MessagesPattern.JoinQueueRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var json = match.Groups[1].Value;

                    OnJoinQueue(new JoinQueueEventArgs(client, json));
                }
            }

            #endregion
        }

        #region Methods

        #endregion
    }
}