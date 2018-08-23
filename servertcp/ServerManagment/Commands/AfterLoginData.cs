using System.Collections.Generic;
using Communication.Server;
using Communication.Server.Logic;
using Communication.Server.Logic.Commands;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment.Commands
{
    public class AfterLoginData : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["AfterLogin"];

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);

            if (!Utils.Instance.IsActiveLogin(client)) return;
            sender.GetRooms(DataSingleton.Instance.Rooms.GetAllItems(), messageId);
        }
    }
}