using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
    public static class ServerSender
    {
        public static class Succesful
        {
            public static void SuccessfulLogin(IScsServerClient client, ServerClient serverClient)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.SuccessfulLogin + "{0} {1}", serverClient.Username, serverClient.Rank)));
            }

            public static void SuccessfulRegister(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.SuccessfulRegister)));
            }

            public static void SuccessfulChangedPassword(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Succesful.SuccesfulChangePassword)));
            }

            public static void SuccessfulChangedUsername(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Succesful.SuccesfulChangeUsername)));
            }

            public static void SuccessfulChangedRank(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Succesful.SuccesfulChangeRank)));
            }
        }

        public static class Error
        {
            public static void LoginError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.Errors.LoginErr)));
            }

            public static void RegisterError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.Errors.RegisterErr)));
            }

            public static void RegisterAccExistError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.Errors.RegisterAccExistErr)));
            }

            #region UserAccount

            public static void ChangePasswordError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Errors.ChangePasswordErr)));
            }

            public static void ChangeUsernameError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Errors.ChangeUsernameErr)));
            }

            public static void ChangeLoginError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Errors.ChangeLoginErr)));
            }

            public static void ChangeRankError(IScsServerClient client)
            {
                client.SendMessage(new ScsTextMessage(
                    String.Format(Commands.UserAccount.Errors.ChangeRankErr)));
            }

            #endregion UserAccount
        }

        public static class ServerCoreMethods
        {
            public static void GetPeopleList(IScsServerClient client, List<Communication.Client.UserClient> clientsList)
            {
                var message = Commands.GetPeoples;
                foreach (var userClient in clientsList)
                    message += $"\n{userClient.Username} {userClient.Rank}";
                client.SendMessage(new ScsTextMessage(message));
            }

            public static class Error
            {
                public static void GetPeopleListError(IScsServerClient client)
                {
                    client.SendMessage(new ScsTextMessage(
                        String.Format(Commands.Errors.GetPeoplesErr)));
                }
            }
        }
    }
}