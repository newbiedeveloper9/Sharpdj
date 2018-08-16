using System;
using System.Collections.Generic;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;

namespace Communication.Server.Logic
{
    public static class ServerSender
    {
        public static void Success(IScsServerClient client, string messageId, params string[] param)
        {
            if (param.Length > 0)
            {
                client.SendMessage(new ScsTextMessage(
                    string.Format(Shared.Commands.Instance.CommandsDictionary["Success"] + param[0]), messageId));
            }
            else
            {
                client.SendMessage(new ScsTextMessage(
                    string.Format(Shared.Commands.Instance.CommandsDictionary["Success"]), messageId));
            }
        }    

        public static void Error(IScsServerClient client, string messageId, params string[] param)
        {
            client.SendMessage(new ScsTextMessage(
                string.Format(Shared.Commands.Instance.CommandsDictionary["Error"]), messageId));
        }

        public static class ServerCoreMethods
        {
            public static void GetPeopleList(IScsServerClient client, IEnumerable<UserClient> clientsList)
            {
                var message = Shared.Commands.Instance.CommandsDictionary["GetPeoples"];
                foreach (var userClient in clientsList)
                    message += $"\n{userClient.Username}${userClient.Rank}";
                client.SendMessage(new ScsTextMessage(message));
            }

            public static void JoinRoom(IScsServerClient client, Room.InsindeInfo room, string messId)
            {
                string output = JsonConvert.SerializeObject(room);
                client.SendMessage(new ScsTextMessage(output, messId));
            }

            public static void GetRooms(IScsServerClient client, List<Room> room, string messId)
            {
                var output = JsonConvert.SerializeObject(room);

                client.SendMessage(new ScsTextMessage(output, messId));
            }
        }
    }
}