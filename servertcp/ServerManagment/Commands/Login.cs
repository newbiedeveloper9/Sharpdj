﻿using System;
using System.Collections.Generic;
using Communication.Shared;
using Hik.Communication.Scs.Server;
using servertcp;
using servertcp.Sql;

namespace Communication.Server.Logic.Commands
{
    public class LoginCommand : ICommand
    {
        public string CommandText { get; } = Shared.Commands.Instance.CommandsDictionary["Login"]; 

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var login = parameters[1];
            var password = parameters[2];
            
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

                       if (Utils.Instance.IsActiveLogin(client))
                           //Receiver_Disconnect(null, new ServerReceiverEvents.DisconnectEventArgs(e.Client));
        
                        DataSingleton.Instance.ServerClients[(int)tmpClient.Client.ClientId] = tmpClient;
                        ServerSender.Success(client, messageId, tmpClient.Username);
                        SqlUserCommands.AddActionInfo(getUserID, Utils.Instance.GetIpOfClient(client),
                        SqlUserCommands.Actions.Login);
                    }
                    else
                        ServerSender.Error(client, messageId);
                }
                else
                    ServerSender.Error(client, messageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error(client, messageId);
            }
        }
    }
}