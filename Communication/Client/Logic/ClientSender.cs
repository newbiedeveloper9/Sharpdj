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

        public void Register(string login, string password, string email) =>
            _clientUtility.SendMessage(
                Commands.Register,
                login, password, email);

        public void Login(string login, string password) =>
            _clientUtility.SendMessage(
                Commands.Login,
                login, password);

        public void GetPeoples() =>
            _clientUtility.SendMessage(
                Commands.GetPeoples);
        
        public void Disconnect() =>
            _clientUtility.SendMessage(
                Commands.Disconnect);
        
        #region UserAccount

        public void ChangePassword(string password, string newPassword) =>
            _clientUtility.SendMessage(
                Commands.UserAccount.ChangePassword,
                password, newPassword);

        public void ChangeUsername(string password, string newUsername) =>
            _clientUtility.SendMessage(
                Commands.UserAccount.ChangeUsername,
                password, newUsername);

        public void ChangeLogin(string password, string newLogin) =>
            _clientUtility.SendMessage(
                Commands.UserAccount.ChangeLogin,
                password, newLogin);

        public void ChangeRank(string password, Rank newRank) =>
            _clientUtility.SendMessage(
                Commands.UserAccount.ChangeRank,
                password, newRank.ToString());

        #endregion

        #region ClientActions

        public void RoomJoin(int roomId) =>
            _clientUtility.SendMessage(
                Commands.Client.JoinRoom,
                roomId.ToString());

        public void CreateRoom(string name, string image, string description) =>
            _clientUtility.SendMessage(
                Commands.Client.CreateRoom,
                name, image, description);

        public string AfterLogin() =>
            _clientUtility.SendMessageAndWaitForResponse(
                Commands.Client.AfterLogin);

        #endregion
    }
}