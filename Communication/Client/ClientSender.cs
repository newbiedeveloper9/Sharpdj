using System;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;

namespace Communication.Client
{
    public class ClientSender
    {
        private IScsClient Client { get; set; }

        public ClientSender(IScsClient client)
        {
            Client = client;
        }

        public void Register(string login, string password, string email)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Register + "{0} {1} {2}", login, password, email)));
        }

        public void Login(string login, string password)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Login + "{0} {1}", login, password)));
        }

        public void GetPeoples()
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.GetPeoples)));
        }

        #region UserAccount
        public void ChangePassword(string password, string newPassword)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangePassword + "{0} {1}", password, newPassword)));
        }

        public void ChangeUsername(string password, string newUsername)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeUsername + "{0} {1}", password, newUsername)));
        }

        public void ChangeLogin(string password, string newLogin)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeLogin + "{0} {1}", password, newLogin)));
        }

        public void ChangeRank(string password, Rank newRank)
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeRank + "{0} {1}", password, newRank)));
        }

        #endregion

        public void Disconnect()
        {
            Client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Disconnect)));
        }
    }
}
