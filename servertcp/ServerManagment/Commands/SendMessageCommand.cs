using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Communication.Client.Logic;
using Communication.Server;
using Communication.Server.Logic;
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

        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["SendMessage"];

        public void Run(IScsServerClient client,
            List<string> parameters,
            string messageId)
        {
            string text = parameters[0];
            int roomId = Convert.ToInt32(parameters[1]);

            if (!string.IsNullOrWhiteSpace(text))
            {
                var room = DataSingleton.Instance.Rooms.GetAllItems()
                    .First(x => x.Id == roomId);

                foreach (var clientInstance in room.InsideInfo.Clients)
                {
                    var clientServerInstance = DataSingleton.Instance.ServerClients.GetAllItems()
                        .First(x => x.Id == clientInstance.Id);
                    var sender = new ServerSender(clientServerInstance.Client);

                    sender.SendMessage(text, roomId,
                        (int) DataSingleton.Instance.ServerClients[client.ClientId].Id);
                }
            }
        }
    }
}