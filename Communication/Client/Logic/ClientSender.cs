using System;
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

        public string Register(string login, string password, string email) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Register,
                login, password, email);

        public string Login(string login, string password) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Login,
                login, password);

        public void GetPeoples() =>
            _clientUtility.SendMessage(
                Commands.GetPeoples);
        
        public void Disconnect() =>
            _clientUtility.SendMessage(
                Commands.Disconnect);
        
        #region UserAccount

        public string ChangePassword(string password, string newPassword) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.UserAccount.ChangePassword,
                password, newPassword);

        public string ChangeUsername(string password, string newUsername) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.UserAccount.ChangeUsername,
                password, newUsername);

        public string ChangeLogin(string password, string newLogin) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.UserAccount.ChangeLogin,
                password, newLogin);

        public string ChangeRank(string password, Rank newRank) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.UserAccount.ChangeRank,
                password, newRank.ToString());

        #endregion

        #region ClientActions

        public string RoomJoin(int roomId) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Client.JoinRoom,
                roomId.ToString());

        public string CreateRoom(string name, string image, string description) =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Client.CreateRoom,
                name, image, description);

        public string AfterLogin() =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Client.AfterLogin);

        #endregion
    }
}