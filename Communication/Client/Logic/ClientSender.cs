using System;
using System.Security;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messengers;

namespace Communication.Client.Logic
{
    public class ClientSender
    {
        private readonly ConnectionUtility _clientUtility;

        public ClientSender(IScsClient client, RequestReplyMessenger<IScsClient> clientRequestReply)
        {
            _clientUtility = new ConnectionUtility(client, clientRequestReply);
        }

        public string Register(string login, SecureString password, string email) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["Register"],
                login,
                new System.Net.NetworkCredential(string.Empty, password).Password,
                email);

        public string Login(string login, SecureString password) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["Login"],
                login,
                new System.Net.NetworkCredential(string.Empty, password).Password);
 
        public void GetPeoples() =>
            _clientUtility.SendMessage(
                Commands.Instance.CommandsDictionary["GetPeoples"]);
        
        public void Disconnect() =>
            _clientUtility.SendMessage(
                Commands.Instance.CommandsDictionary["Disconnect"]);
        
        #region UserAccount

        public string ChangePassword(string password, string newPassword) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["ChangePassword"],
                password, newPassword);

        public string ChangeUsername(string password, string newUsername) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["ChangeUsername"],
                password, newUsername);

        public string ChangeLogin(string password, string newLogin) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["ChangeLogin"],
                password, newLogin);

        public string ChangeRank(string password, Rank newRank) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["ChangeRank"],
                password, newRank.ToString());

        #endregion

        #region ClientActions

        public string RoomJoin(int roomId) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["JoinRoom"],
                roomId.ToString());

        public string CreateRoom(string name, string image, string description) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["CreateRoom"],
                name, image, description);

        public string AfterLogin() =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Instance.CommandsDictionary["AfterLogin"]);

        #endregion
    }
}