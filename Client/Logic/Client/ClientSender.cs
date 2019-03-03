using System.Security;
using Communication.Client.Logic;
using Communication.Shared.Data;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messengers;

namespace SharpDj.Logic.Client
{
    public class ClientSender
    {
        private readonly ConnectionUtility _clientUtility;

        public ClientSender(IScsClient client, RequestReplyMessenger<IScsClient> clientRequestReply)
        {
            _clientUtility = new ConnectionUtility(client, clientRequestReply);
        }

         public string JoinQueue(string json) =>
                    _clientUtility.SendMessageAndWaitForResponse(
                        Communication.Shared.Commands.Instance.CommandsDictionary["JoinQueue"],
                        json);

        public string Register(string login, SecureString password, string email) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["Register"],
                login,
                new System.Net.NetworkCredential(string.Empty, password).Password,
                email);

        public string Login(string login, SecureString password) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["Login"],
                login,
                new System.Net.NetworkCredential(string.Empty, password).Password);
 
        public void GetPeoples() =>
            _clientUtility.SendMessage(
                Communication.Shared.Commands.Instance.CommandsDictionary["GetPeoples"]);
        
        public string Disconnect() =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["Disconnect"]);
        
        #region UserAccount

        public string ChangePassword(string password, string newPassword) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["ChangePassword"],
                password, newPassword);

        public string ChangeUsername(string password, string newUsername) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["ChangeUsername"],
                password, newUsername);

        public string ChangeLogin(string password, string newLogin) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["ChangeLogin"],
                password, newLogin);

        public string ChangeRank(string password, Rank newRank) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["ChangeRank"],
                password, newRank.ToString());

        #endregion

        #region ClientActions

        public string RoomJoin(int roomId) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["JoinRoom"],
                roomId.ToString());

        public string CreateRoom(string name, string image, string description) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["CreateRoom"],
                name, image, description);

        public string AfterLogin() =>
            _clientUtility.SendMessageAndWaitForResponse(
                Communication.Shared.Commands.Instance.CommandsDictionary["AfterLogin"]);

        public void SendMessage(string text, string roomId) =>
            _clientUtility.SendMessage(
                Communication.Shared.Commands.Instance.CommandsDictionary["SendMessage"],
                text, roomId);
        

        #endregion
    }
}