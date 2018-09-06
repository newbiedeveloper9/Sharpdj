using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Communication.Client.Logic;
using Communication.Server;
using Communication.Server.Logic;
using Communication.Server.Logic.Commands;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment.Commands
{
    public class SendMessageCommand : ICommand
    {
        private readonly IScsServer _server;

        public SendMessageCommand(IScsServer server)
        {
            this._server = server;
        }

        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["SendMessage"];

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            string text = parameters[1];
            int roomId = Convert.ToInt32(parameters[2]);

            if (!string.IsNullOrWhiteSpace(text))
            {
                var sender = new ServerSender(client);
                var room = DataSingleton.Instance.Rooms.GetAllItems().First(x => x.Id == roomId);
                
                foreach (var clientInstance in room.InsideInfo.Clients)
                {
                    if (clientInstance != null)
                    {
                        sender.SendMessage(text, roomId);
                    }
                }
            }
        }
    }
}