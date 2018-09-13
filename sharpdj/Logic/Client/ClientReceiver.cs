using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Communication.Server;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using SharpDj.Logic.Client.Commands;
using SharpDj.Logic.Helpers;
using SharpDj.ViewModel;

namespace Communication.Client.Logic
{
    public class ClientReceiver
    {
        private readonly List<ICommand> _commands;
        private readonly SdjMainViewModel _sdjMainViewModel;

        public ClientReceiver(SdjMainViewModel sdjMainViewModel)
        {
            this._sdjMainViewModel = sdjMainViewModel;
            _commands = new List<ICommand>()
            {
                new UpdateDjCommand(),
                new SendMessageChatCommand(),
                new UpdateUserListInsideRoomCommand(),
            };
        }

        public void ParseMessage(IScsClient client, string message)
        {
            var command = Commands.Instance.GetMessageCommand(message);
            var commandClass = _commands.FirstOrDefault(x => x.CommandText.Equals(command));
            try
            {
                commandClass?.Run(_sdjMainViewModel, Commands.Instance.GetMessageParameters(message));
            }
            catch (Exception e)
            {
                new ExceptionLogger(e);
            }

            /*Need to implement receiver same as in Server*/

//            #region User Disconnected
//
//            if (message.StartsWith(Commands.Disconnect))
//            {
//                Regex rgx = new Regex(MessagesPattern.UserDisconnectRgx);
//                Match match = rgx.Match(message);
//                if (match.Success)
//                {
//                    var username = match.Groups[1].Value;
//                    Console.WriteLine("{0} disconnected", username);
//                    OnUserDisconnect(new UserDisconnectEventArgs(username));
//                }
//                else
//                {
//                    Console.WriteLine("Problem with matching disconnected user.");
//                }
//            }
//
//            #endregion
//
//            #region Get People
//
//            else if (message.StartsWith(Commands.GetPeoples))
//            {
//                var tabs = message.Split('\n').ToList();
//                tabs.RemoveAt(0);
//
//
//                var userClients = (from tab in tabs
//                    let rgx = new Regex(MessagesPattern.GetPeopleRgx)
//                    select rgx.Match(tab)
//                    into match
//                    where match.Success
//                    let username = match.Groups[1].Value
//                    let rank = match.Groups[2].Value
//                    select new UserClient
//                    {
//                        Username = username,
//                        Rank = (Rank) Enum.Parse(typeof(Rank), rank, true)
//                    }).ToList();
//
//                foreach (var userClient in userClients)
//                {
//                    Console.WriteLine("{0} with rank {1}", userClient.Username, userClient.Rank);
//                }
//
//                OnGetPeople(new GetPeopleEventArgs(userClients));
//            }
//
//            #endregion
//
//            #region UpdateDjResponse
//
//            else if (message.StartsWith("updatedj"))
//            {
//                responseActions = new UpdateDjResponse();
//                ParserResponse(message, MessagesPattern.UpdateDjRgx, responseActions);
//            }
//
//            #endregion
        }
    }
}