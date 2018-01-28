using System;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;

namespace Communication.Client
{
    public class ClientSender
    {
        public void Register(IScsClient client, string login, string password, string email)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Register + "{0} {1} {2}", login, password, email)));
        }

        public void Login(IScsClient client, string login, string password)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Login + "{0} {1}", login, password)));
        }

        public void GetPeoples(IScsClient client)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.GetPeoples)));
        }

        #region UserAccount
        public void ChangePassword(IScsClient client, string password, string newPassword)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangePassword + "{0} {1}", password, newPassword)));
        }
        public void ChangeUsername(IScsClient client, string password, string newUsername)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeUsername + "{0} {1}", password, newUsername)));
        }
        public void ChangeLogin(IScsClient client, string password, string newLogin)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeLogin + "{0} {1}", password, newLogin)));
        }
        public void ChangeRank(IScsClient client, string password, Rank newRank)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.UserAccount.ChangeRank + "{0} {1}", password, newRank)));
        }
        #endregion
        public void Disconnect(IScsClient client)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(Commands.Disconnect)));
        }
    }
}
