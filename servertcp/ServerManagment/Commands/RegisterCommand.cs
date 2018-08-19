using System;
using System.Collections.Generic;
using Communication.Server;
using Communication.Server.Logic;
using Communication.Server.Logic.Commands;
using Hik.Communication.Scs.Server;
using servertcp.Sql;

namespace servertcp.ServerManagment.Commands
{
    public class RegisterCommand : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["Register"];
        
        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);
            
            var login = parameters[1];
            var password = parameters[2];
            var email = parameters[3];       

            try
            {
                if (!SqlUserCommands.LoginExists(login))
                {
                    var salt = Scrypt.GenerateSalt();

                    if (SqlUserCommands.CreateUser(login, Scrypt.Hash(password, salt), salt, login))
                    {
                        sender.Success(messageId);
                        var getUserID = SqlUserCommands.GetUserId(login);

                        SqlUserCommands.AddActionInfo(getUserID, Utils.Instance.GetIpOfClient(client),
                            SqlUserCommands.Actions.Register);
                    }
                    else
                        sender.Error(messageId);
                }
                else
                    sender.Error(messageId); //TODO acc exist param
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sender.Error(messageId);
            }
        }
    }
}