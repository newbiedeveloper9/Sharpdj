using System.Collections.Generic;
using Communication.Client;
using Communication.Shared;
using Communication.Shared.Data;
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

        public void AddUserToRoom(UserClient user, string roomId)
        {
            string output = JsonConvert.SerializeObject(user);
            _senderUtility.SendMessage(Commands.Instance.CommandsDictionary["AddUserToRoom"],
                output, roomId);
        }

        public void ChangeTrack(Dj dj, string roomId)
        {
            string output = JsonConvert.SerializeObject(dj);
            _senderUtility.SendMessage(Commands.Instance.CommandsDictionary["ChangeTrack"],
                output, roomId);

        }

        public void RemoveUserFromRoom(UserClient user, string roomId)
        {
            string output = JsonConvert.SerializeObject(user);
            _senderUtility.SendMessage(Commands.Instance.CommandsDictionary["RemoveUserFromRoom"],
                output, roomId);
        }
    }
}