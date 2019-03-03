using System;
using System.Collections.Generic;
using Communication.Server;
using Communication.Shared.Data;
using Hik.Communication.Scs.Server;
using Server.Security;
using Server.Sql;

namespace Server.ServerManagment.Commands
{
    public class LoginCommand : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["Login"]; 
        
        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);
            
            var login = parameters[0];
            var password = parameters[1];
            
            try
            {
                if (SqlUserCommands.LoginExists(login))
                {
                    var hashedPass = Scrypt.Hash(password, SqlUserCommands.GetSalt(login));

                    if (SqlUserCommands.CheckPassword(hashedPass, login))
                    {
                        var getUserID = SqlUserCommands.GetUserId(login);
                        var rank = SqlUserCommands.GetUserRank(getUserID);

                        var tmpClient = new ServerClient(client)
                        {
                            Rank = (Rank)rank,
                            Id = getUserID,
                            Username = login,
                            Login = login
                        };

                       // if (Utils.Instance.IsActiveLogin(client))
                           //Receiver_Disconnect(null, new ServerReceiverEvents.DisconnectEventArgs(e.Client)); ///TODO
        
                        DataSingleton.Instance.ServerClients[(int)tmpClient.Client.ClientId] = tmpClient;
                        sender.Success(messageId, tmpClient.Username);
                        SqlUserCommands.AddActionInfo(getUserID, Utils.Instance.GetIpOfClient(client),
                        SqlUserCommands.Actions.Login);
                    }
                    else
                        sender.Error(messageId);
                }
                else
                    sender.Error(messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sender.Error(messageId);
            }
        }
    }
}