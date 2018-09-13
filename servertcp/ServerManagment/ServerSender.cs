using System;
using System.Collections.Generic;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;

namespace Communication.Server.Logic
{
    public class ServerSender
    {
        private readonly ConnectionUtility _senderUtility;

        public ServerSender(IScsServerClient client)
        {
            _senderUtility = new ConnectionUtility(client);
        }

        public void Success(string messageId, params string[] param) =>
            _senderUtility.ReplyToMessage(Shared.Commands.Instance.CommandsDictionary["Success"], messageId, param);

        public void Error(string messageId, params string[] param) =>
            _senderUtility.ReplyToMessage(Shared.Commands.Instance.CommandsDictionary["Error"], messageId, param);

        public void GetPeopleList(IEnumerable<UserClient> clientsList)
        {
            var message = Shared.Commands.Instance.CommandsDictionary["GetPeoples"];
            foreach (var userClient in clientsList)
                message += $"\n{userClient.Username}${userClient.Rank}";
            _senderUtility.SendMessage(message);
        }

        public void JoinRoom(Room.InsindeInfo room, string messId)
        {
            string output = JsonConvert.SerializeObject(room);
            _senderUtility.ReplyToMessage(output, messId);
        }

        public void GetRooms(List<Room> room, string messId)
        {
            string output = JsonConvert.SerializeObject(room);
            _senderUtility.ReplyToMessage(output, messId);
        }  

        public void SendMessage(string text, string roomId, int userId)
        {
            _senderUtility.SendMessage(Shared.Commands.Instance.CommandsDictionary["SendMessage"],
                text, roomId, userId.ToString());
        }

        public void UpdateUserListInsideRoom(List<UserClient> users, string roomId)
        {
            string output = JsonConvert.SerializeObject(users);
            _senderUtility.SendMessage(Commands.Instance.CommandsDictionary["UpdateUserListInsideRoom"],
                output, roomId);
        }
    }
}