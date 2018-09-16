using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Server;
using Communication.Server.Logic;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment.Commands
{
    public class JoinRoom : ICommand
    {
        public string CommandText { get; } = 
            Communication.Shared.Commands.Instance.CommandsDictionary["JoinRoom"];

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);
            var roomId = parameters[0];

            if (!Utils.Instance.IsActiveLogin(client)) return;
            //select client class object wchich refer to user
            var userclient = DataSingleton.Instance.ServerClients[(int) client.ClientId].ToUserClient();
            //select room to join
            var room = DataSingleton.Instance.Rooms.GetAllItems().
                FirstOrDefault(x => x.Id == Convert.ToInt32(roomId));
            //Set user to max 1 room, remove from other instances
            var getRoomsWithSpecificUser = DataSingleton.Instance.Rooms.GetAllItems()
                .Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
            foreach (var roomActive in getRoomsWithSpecificUser)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userclient.Id);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

            //add client to room
            room?.InsideInfo.Clients.Add(userclient);

            if (room?.InsideInfo.Clients != null)
                foreach (var clientInstance in room.InsideInfo.Clients)
                {
                    if (clientInstance.Id == userclient.Id)
                        continue;

                    var clientServerInstance = DataSingleton.Instance.ServerClients.GetAllItems()
                        .First(x => x.Id == clientInstance.Id);
                    var clientServerSender = new ServerSender(clientServerInstance.Client);
                    
                    clientServerSender.UpdateUserListInsideRoom(userclient, roomId);
                }

            sender.JoinRoom(room?.InsideInfo, messageId);
        }
    }
}