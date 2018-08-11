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
        private class MessagesPattern
        {
            public const string UserDisconnectRgx = Commands.Disconnect + " (.*)";
            public const string SuccesfulLoginRgx = Commands.SuccessfulLogin + @"(.*)\$(.*)";
            public const string GetPeopleRgx = @"(.*)\$(.*)";
            public const string UpdateDjRgx = Commands.Client.Room.UpdateDj + @"\$(.*)";
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