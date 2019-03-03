using System;
using System.Collections.Generic;
using System.Linq;
using Hik.Communication.Scs.Server;

namespace Server.ServerManagment.Commands
{
    public class SendMessageChatCommand : ICommand
    {
        private readonly IScsServer _server;

        public SendMessageChatCommand(IScsServer server)
        {
            this._server = server;
        }

        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["SendMessage"];

        public void Run(IScsServerClient client,
            List<string> parameters,
            string messageId)
        {            
            if (!Utils.Instance.IsActiveLogin(client)) return;

            string text = parameters[0];
            string roomId = parameters[1];

            if (!string.IsNullOrWhiteSpace(text))
            {
                var room = DataSingleton.Instance.Rooms.GetAllItems()
                    .First(x => x.Id == Convert.ToInt32(roomId));

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