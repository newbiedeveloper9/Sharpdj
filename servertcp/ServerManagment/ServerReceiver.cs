using System.Collections.Generic;
using System.Linq;
using Communication.Server;
using Communication.Server.Logic;
using Hik.Communication.Scs.Server;
using servertcp.ServerManagment.Commands;

namespace servertcp.ServerManagment
{
    public class ServerReceiver
    {
        private readonly List<ICommand> _commands;
        private readonly IScsServer _server;

        public ServerReceiver(IScsServer server)
        {
            this._server = server;

            _commands = new List<ICommand>
            {
                new LoginCommand(),
                new RegisterCommand(),
                new DisconnectCommand(),
                new AfterLoginData(),
                new JoinRoom(),
                new SendMessageChatCommand(server),
                new JoinQueueCommand(),
            };
        }

        public void ParseMessage(IScsServerClient client, string message, string messageId)
        {
            var command = Communication.Shared.Commands.Instance.GetMessageCommand(message);
            var commandClass = _commands.FirstOrDefault(x => x.CommandText.Equals(command));
            commandClass?.Run(client, Communication.Shared.Commands.Instance.GetMessageParameters(message), messageId);

            /*#region Disconnect

            if (message.Equals(Commands.Disconnect))
            {
                OnDisconnect(new DisconnectEventArgs(client));
            }

            #endregion Disconnect

            #region Login

            else if (message.StartsWith(Commands.Login))
            {
                var rgx = new Regex(ServerReceiver.MessagesPattern.LoginRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var login = match.Groups[1].Value;
                    var password = match.Groups[2].Value;

                    OnLogin(new LoginEventArgs(login, password, client, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion Login

            #region Register

            else if (message.StartsWith(Commands.Register))
            {
                var rgx = new Regex(MessagesPattern.RegisterRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var login = match.Groups[1].Value;
                    var password = match.Groups[2].Value;
                    var email = match.Groups[3].Value;

                    OnRegister(new RegisterEventArgs(login, password, email, client, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion Register

            #region ChangePassword

            else if (message.StartsWith(Commands.UserAccount.ChangePassword))
            {
                var rgx = new Regex(MessagesPattern.ChangePasswordRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newPassword = match.Groups[2].Value;

                    OnChangePassword(new ChangePasswordEventArgs(client, password, newPassword, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion ChangePassword

            #region ChangeUsername

            else if (message.StartsWith(Commands.UserAccount.ChangeUsername))
            {
                var rgx = new Regex(MessagesPattern.ChangeUsernameRgx);
                var match = rgx.Match(message);
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newUsername = match.Groups[2].Value;

                    OnChangeUsername(new ChangeUsernameEventArgs(client, password, newUsername, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion ChangeUsername

            #region ChangeLogin

            else if (message.StartsWith(Commands.UserAccount.ChangeLogin))
            {
                var rgx = new Regex(MessagesPattern.ChangeLoginRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newLogin = match.Groups[2].Value;

                    OnChangeLogin(new ChangeLoginEventArgs(client, password, newLogin, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion ChangeLogin

            #region ChangeRank

            else if (message.StartsWith(Commands.UserAccount.ChangeRank))
            {
                var rgx = new Regex(MessagesPattern.ChangeRankRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var password = match.Groups[1].Value;
                    var newRank = match.Groups[2].Value;

                    OnChangeRank(new ChangeRankEventArgs(client, password,
                        (Rank) Enum.Parse(typeof(Rank), newRank, true), messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
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
                var rgx = new Regex(MessagesPattern.JoinRoomRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var roomId = match.Groups[1].Value;

                    OnJoinRoom(new JoinRoomEventArgs(Convert.ToInt32(roomId), client, messageId));
                }
                else
                    ServerSender.Error(client, messageId);
            }

            #endregion JoinRoom 

            #region CreateRoom

            else if (message.StartsWith(Commands.Client.CreateRoom))
            {
                var rgx = new Regex(MessagesPattern.CreateRoomRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var name = match.Groups[1].Value;
                    var image = match.Groups[2].Value;
                    var description = match.Groups[3].Value;

                    OnCreateRoom(new CreateRoomEventArgs(client, name, image, description, messageId));
                }
                else
                    ServerSender.Error(client, messageId);
            }

            #endregion CreateRoom

            #region AfterLogin

            else if (message.StartsWith(Commands.Client.AfterLogin))
            {
                OnAfterLogin(new AfterLoginEventArgs(client, messageId));
            }

            #endregion AfterLogin

            #region JoinQueue

            else if (message.StartsWith(Commands.Client.Room.JoinQueue))
            {
                var rgx = new Regex(MessagesPattern.JoinQueueRgx);
                var match = rgx.Match(message);
                
                if (match.Success)
                {
                    var json = match.Groups[1].Value;

                    OnJoinQueue(new JoinQueueEventArgs(client, json, messageId));
                }
                else
                {
                    ServerSender.Error(client, messageId);
                }
            }

            #endregion*/
        }

        #region Methods

        #endregion
    }
}