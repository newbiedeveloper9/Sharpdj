using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Server;
using servertcp;
using servertcp.Sql;

namespace Communication.Server.Logic.Commands
{
    public class RegisterCommand : ICommand
    {
        public string CommandText { get; } = Shared.Commands.Instance.CommandsDictionary["Register"];
        
        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
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
                        ServerSender.Success(client, messageId);
                        var getUserID = SqlUserCommands.GetUserId(login);

                        SqlUserCommands.AddActionInfo(getUserID, Utils.Instance.GetIpOfClient(client),
                            SqlUserCommands.Actions.Register);
                    }
                    else
                        ServerSender.Error(client, messageId);
                }
                else
                    ServerSender.Error(client, messageId); //TODO acc exist param
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error(client, messageId);
            }
        }
    }
}