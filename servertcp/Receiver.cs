using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Server;
using CryptSharp.Utility;
using Hik.Collections;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;
using servertcp.Sql;

namespace servertcp
{
    class Receiver
    {
        private IScsServer server;
        private readonly ThreadSafeSortedList<long, ServerClient> _clients;

        public List<ServerClient> Clients => (from client in _clients.GetAllItems() select client).ToList();
        public List<UserClient> UserClients => (from client in _clients.GetAllItems() select client.ToUserClient()).ToList();

        public Receiver(ServerReceiver receiver, IScsServer server)
        {
            receiver.Disconnect += _communication_Disconnect;
            receiver.Register += _communication_Register;
            receiver.Login += _communication_Login;
            receiver.ChangePassword += Receiver_ChangePassword;
            receiver.ChangeUsername += Receiver_ChangeUsername;
            receiver.ChangeRank += Receiver_ChangeRank;
            receiver.ChangeLogin += Receiver_ChangeLogin;
            receiver.GetPeople += Receiver_GetPeople;
            _clients = new ThreadSafeSortedList<long, ServerClient>();
            this.server = server;
        }

        private void Receiver_GetPeople(object sender, ServerReceiver.GetPeopleEventArgs e)
        {
            if (IsActiveLogin(e.Client))
            {
                ServerSender.ServerCoreMethods.GetPeopleList(e.Client, UserClients);
                Console.WriteLine("Accepted");
            }
            else
            {
                Console.WriteLine("Denied");
            }
        }

        private void Receiver_ChangeLogin(object sender, ServerReceiver.ChangeLoginEventArgs e)
        {
            var client = _clients[e.Client.ClientId];

            var startPath = Environment.CurrentDirectory;
            var usersPath = startPath + "/Users/";
            if (!Directory.Exists(usersPath))
            {
                Directory.CreateDirectory(usersPath);
            }

            var accPath = usersPath + client.Login + ".json";

            if (File.Exists(accPath))
            {
                var jsonSource = File.ReadAllText(accPath);

                var clientJson = JsonConvert.DeserializeObject<ServerClient>(jsonSource);

                var pass = Scrypt.Hash(e.Password, client.Salt);
                if (pass.Equals(client.Password))
                {
                    client.Login = e.NewLogin;
                    clientJson.Login = client.Login;
                    var json = JsonConvert.SerializeObject(clientJson, Formatting.Indented);

                    File.Delete(accPath);
                    var newAccPath = usersPath + client.Login + ".json";

                    File.WriteAllText(newAccPath, json);

                    ServerSender.Succesful.SuccessfulChangedRank(client.Client);
                }
                else
                    ServerSender.Error.ChangeLoginError(client.Client);
            }
            else
                ServerSender.Error.ChangeLoginError(client.Client);
        }

        private void Receiver_ChangeRank(object sender, ServerReceiver.ChangeRankEventArgs e)
        {
            {
                var client = _clients[e.Client.ClientId];
                var startPath = Environment.CurrentDirectory;
                var usersPath = startPath + "/Users/";
                if (!Directory.Exists(usersPath))
                {
                    Directory.CreateDirectory(usersPath);
                }

                var accPath = usersPath + client.Login + ".json";

                if (File.Exists(accPath))
                {
                    var jsonSource = File.ReadAllText(accPath);

                    var clientJson = JsonConvert.DeserializeObject<ServerClient>(jsonSource);

                    var pass = Scrypt.Hash(e.Password, client.Salt);
                    if (pass.Equals(client.Password))
                    {
                        client.Rank = e.Rank;
                        clientJson.Rank = client.Rank;
                        var json = JsonConvert.SerializeObject(clientJson, Formatting.Indented);

                        File.WriteAllText(accPath, json);
                        ServerSender.Succesful.SuccessfulChangedRank(client.Client);
                    }
                    else
                        ServerSender.Error.ChangeRankError(client.Client);
                }
                else
                    ServerSender.Error.ChangeRankError(client.Client);
            }
        }

        private void Receiver_ChangeUsername(object sender, ServerReceiver.ChangeUsernameEventArgs e)
        {
            var client = _clients[e.Client.ClientId];

            var startPath = Environment.CurrentDirectory;
            var usersPath = startPath + "/Users/";
            if (!Directory.Exists(usersPath))
            {
                Directory.CreateDirectory(usersPath);
            }

            var accPath = usersPath + client.Login + ".json";

            if (File.Exists(accPath))
            {
                var jsonSource = File.ReadAllText(accPath);

                var clientJson = JsonConvert.DeserializeObject<ServerClient>(jsonSource);

                var pass = Scrypt.Hash(e.Password, client.Salt);
                if (pass.Equals(client.Password))
                {
                    client.Username = e.NewUsername;
                    clientJson.Username = client.Username;
                    var json = JsonConvert.SerializeObject(clientJson, Formatting.Indented);

                    File.WriteAllText(accPath, json);

                    ServerSender.Succesful.SuccessfulChangedUsername(client.Client);
                }
                else
                    ServerSender.Error.ChangeUsernameError(client.Client);
            }
            else
                ServerSender.Error.ChangeUsernameError(client.Client);
        }

        private void Receiver_ChangePassword(object sender, ServerReceiver.ChangePasswordEventArgs e)
        {
            var client = _clients[e.Client.ClientId];

            var startPath = Environment.CurrentDirectory;
            var usersPath = startPath + "/Users/";
            if (!Directory.Exists(usersPath))
            {
                Directory.CreateDirectory(usersPath);
            }

            var accPath = usersPath + client.Login + ".json";

            if (File.Exists(accPath))
            {
                var jsonSource = File.ReadAllText(accPath);

                var clientJson = JsonConvert.DeserializeObject<ServerClient>(jsonSource);

                var pass = Scrypt.Hash(e.Password, client.Salt);
                if (pass.Equals(client.Password))
                {
                    client.Password = Scrypt.Hash(e.NewPassword, client.Salt);

                    clientJson.Password = client.Password;
                    var json = JsonConvert.SerializeObject(clientJson, Formatting.Indented);

                    File.WriteAllText(accPath, json);

                    ServerSender.Succesful.SuccessfulChangedPassword(client.Client);
                }
                else
                    ServerSender.Error.ChangePasswordError(client.Client);
            }
            else
                ServerSender.Error.ChangePasswordError(client.Client);
        }

        private void _communication_Login(object sender, ServerReceiver.LoginEventArgs e)
        {
            try
            {
                if (SqlUserCommands.LoginExists(e.Login))
                {
                    var salt = SqlUserCommands.GetSalt(e.Login);
                    var pass = Scrypt.Hash(e.Password, salt);

                    if (SqlUserCommands.CheckPassword(pass, e.Login))
                    {
                        var client = new ServerClient(e.Client);
                        client.Username = e.Login;
                        _clients[e.Client.ClientId] = client;
                        ServerSender.Succesful.SuccessfulLogin(e.Client, client);
                        SqlUserCommands.AddLoginInfo(e.Login, Utils.GetIpOfClient(e.Client));
                    }
                    else
                    {
                        ServerSender.Error.LoginError(e.Client);
                    }
                }
                else
                    ServerSender.Error.LoginError(e.Client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error.LoginError(e.Client);
            }
        }

        private void _communication_Register(object sender, ServerReceiver.RegisterEventArgs e)
        {
            try
            {
                if (!SqlUserCommands.LoginExists(e.Login))
                {
                    var salt = Scrypt.GenerateSalt();
                    if (SqlUserCommands.CreateUser(e.Login, Scrypt.Hash(e.Password, salt), salt, e.Login))
                        ServerSender.Succesful.SuccessfulRegister(e.Client);
                    else
                        ServerSender.Error.RegisterError(e.Client);
                }
                else
                    ServerSender.Error.RegisterAccExistError(e.Client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error.RegisterError(e.Client);
            }
        }

        private void _communication_Disconnect(object sender, ServerReceiver.DisconnectEventArgs e)
        {
            var client = _clients[e.Client.ClientId];

            _clients.Remove(e.Client.ClientId);
            Console.WriteLine("{0} disconnected", client.Username);
        }

        private bool IsActiveLogin(IScsServerClient client)
        {
            return _clients[client.ClientId] != null;
        }
    }
}
