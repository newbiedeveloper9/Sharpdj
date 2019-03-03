using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Server;
using Communication.Server.Logic;
using Hik.Communication.Scs.Server;
using servertcp.Sql;

namespace servertcp.ServerManagment.Commands
{
    public class DisconnectCommand : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["Disconnect"];

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);
            
            try
            {
                var login = DataSingleton.Instance.ServerClients[(int) client.ClientId].Login;
                var userId = SqlUserCommands.GetUserId(login);

                var tmp = DataSingleton.Instance.Rooms.GetAllItems()
                    .Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userId)).ToList();
                if (tmp.Any())
                    foreach (var roomActive in tmp)
                    {
                        var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userId);
                        roomActive.InsideInfo.Clients?.RemoveAt(index);
                    }

                DataSingleton.Instance.ServerClients.Remove(client.ClientId);
                SqlUserCommands.AddActionInfo(userId, Utils.Instance.GetIpOfClient(client),
                    SqlUserCommands.Actions.Logout);
                sender.Success(messageId);
                Console.WriteLine("{0} disconnected", client.ClientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sender.Error(messageId);
            }
        }
    }
}