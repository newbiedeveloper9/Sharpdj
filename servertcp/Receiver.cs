using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Server;
using Communication.Shared;
using CryptSharp.Utility;
using Hik.Collections;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;
using servertcp.Sql;

namespace servertcp
{
    class Receiver
    {
        private IScsServer _server;
        private readonly ThreadSafeSortedList<long, ServerClient> _clients;
        private readonly ThreadSafeSortedList<long, Room> _rooms;

        public List<ServerClient> ServerClients => (from client in _clients.GetAllItems() select client).ToList();
        public List<UserClient> UserClients => (from client in _clients.GetAllItems() select client.ToUserClient()).ToList();

        public Receiver(ServerReceiver receiver, IScsServer server)
        {
            this._server = server;

            _clients = new ThreadSafeSortedList<long, ServerClient>();
            _rooms = new ThreadSafeSortedList<long, Room>();

            _rooms[0] = new Room(0, "test0", "host0", "image0", "description0", new Room.InsindeInfo(new List<UserClient>(), new List<Songs>()));
            _rooms[1] = new Room(1, "test1", "host1", "image1", "description1", new Room.InsindeInfo(new List<UserClient>(), new List<Songs>()));
            _rooms[2] = new Room(2, "test2", "host2", "image2", "description2", new Room.InsindeInfo(new List<UserClient>(), new List<Songs>()));
            _rooms[3] = new Room(3, "test3", "host3", "image3", "description3", new Room.InsindeInfo(new List<UserClient>(), new List<Songs>()));

            _rooms[0].InsideInfo.Djs.Add(new Songs("host", new List<Songs.Song>()));
            _rooms[0].InsideInfo.Djs.Add(new Songs("test", new List<Songs.Song>()));

            _rooms[1].InsideInfo.Djs.Add(new Songs("host", new List<Songs.Song>()));
            _rooms[1].InsideInfo.Djs.Add(new Songs("test", new List<Songs.Song>()));
            _rooms[1].InsideInfo.Djs[0].Video.Add(new Songs.Song(10, "mj-v6zCnEaw"));
            _rooms[1].InsideInfo.Djs[0].Video.Add(new Songs.Song(5, "JSQsIMgj1OM"));
            _rooms[1].InsideInfo.Djs[1].Video.Add(new Songs.Song(5, "JSQsIMgj1OM"));


            _rooms[0].InsideInfo.Djs[0].Video.Add(new Songs.Song(10, "mj-v6zCnEaw"));
            _rooms[0].InsideInfo.Djs[0].Video.Add(new Songs.Song(5, "JSQsIMgj1OM"));
            _rooms[0].InsideInfo.Djs[1].Video.Add(new Songs.Song(3, "mj-v6zCnEaw"));
            _rooms[0].InsideInfo.Djs[1].Video.Add(new Songs.Song(2, "JSQsIMgj1OM"));
            _rooms[0].InsideInfo.Djs[1].Video.Add(new Songs.Song(3, "JSQsIMgj1OM"));
            _rooms[0].InsideInfo.TimeLeft = _rooms[0].InsideInfo.Djs[0].Video[0].Time;

            receiver.Disconnect += _communication_Disconnect;
            receiver.Register += _communication_Register;
            receiver.Login += _communication_Login;
            receiver.ChangePassword += Receiver_ChangePassword;
            receiver.ChangeUsername += Receiver_ChangeUsername;
            receiver.ChangeRank += Receiver_ChangeRank;
            receiver.ChangeLogin += Receiver_ChangeLogin;
            receiver.GetPeople += Receiver_GetPeople;
            receiver.JoinRoom += Receiver_JoinRoom;
            receiver.CreateRoom += Receiver_CreateRoom;
            receiver.AfterLogin += Receiver_AfterLogin;

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    foreach (var room in _rooms.GetAllItems())
                    {
                        if (room.InsideInfo.Djs.Count == 0) continue;
                        room.InsideInfo.TimeLeft--;
                        if (room.InsideInfo.TimeLeft <= 0)
                        {
                            room.InsideInfo.NextDj();
                            Console.WriteLine(room.InsideInfo.Djs[0].Video[0].Id + "   /" + room.InsideInfo.TimeLeft);
                        }
                    }
                }
            });
        }

        private void Receiver_AfterLogin(object sender, ServerReceiver.AfterLoginEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            ServerSender.ServerCoreMethods.GetRooms(e.Client, _rooms.GetAllItems(), e.MessageId);
        }

        private void Receiver_CreateRoom(object sender, ServerReceiver.CreateRoomEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (SqlUserCommands.Room.Create(e.Name, login, e.Image, e.Description))
            {
                //room is created propelly
                SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client), SqlUserCommands.Actions.CreateRoom);
            }
            else
            {
                Console.WriteLine("create room fail");
            }
        }

        private void Receiver_JoinRoom(object sender, ServerReceiver.JoinRoomEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            //select client class object wchich refer to user
            var userclient = _clients[e.Client.ClientId].ToUserClient();

            //select room to join
            var room = _rooms.GetAllItems().FirstOrDefault(x => x.Id == e.RoomId);

            //Set user to max 1 room, remove from others
            var tmp = _rooms.GetAllItems().Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
            foreach (var roomActive in tmp)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userclient.Id);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

            //add client to room
            room?.InsideInfo.Clients.Add(userclient);

            //send info about room
            ServerSender.ServerCoreMethods.JoinRoom(e.Client, room?.InsideInfo, e.MessageId);
        }

        private void Receiver_GetPeople(object sender, ServerReceiver.GetPeopleEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            ServerSender.ServerCoreMethods.GetPeopleList(e.Client, UserClients);
        }

        private void _communication_Register(object sender, ServerReceiver.RegisterEventArgs e)
        {
            try
            {
                if (!SqlUserCommands.LoginExists(e.Login))
                {
                    var salt = Scrypt.GenerateSalt();

                    if (SqlUserCommands.CreateUser(e.Login, Scrypt.Hash(e.Password, salt), salt, e.Login))
                    {
                        ServerSender.Succesful.SuccessfulRegister(e.Client);
                        var getUserID = SqlUserCommands.GetUserId(e.Login);

                        SqlUserCommands.AddActionInfo(getUserID, Utils.GetIpOfClient(e.Client),
                            SqlUserCommands.Actions.Register);
                    }
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

        private void _communication_Login(object sender, ServerReceiver.LoginEventArgs e)
        {
            try
            {
                if (SqlUserCommands.LoginExists(e.Login))
                {
                    var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(e.Login));

                    if (SqlUserCommands.CheckPassword(pass, e.Login))
                    {
                        var getUserID = SqlUserCommands.GetUserId(e.Login);
                        var rank = SqlUserCommands.GetUserRank(getUserID);

                        var client = new ServerClient(e.Client)
                        {
                            Rank = (Rank)rank,
                            Id = getUserID,
                            Username = e.Login,
                            Login = e.Login
                        };

                        _clients[client.Client.ClientId] = client;
                        ServerSender.Succesful.SuccessfulLogin(e.Client, client);
                        SqlUserCommands.AddActionInfo(getUserID, Utils.GetIpOfClient(e.Client),
                            SqlUserCommands.Actions.Login);
                    }
                    else
                        ServerSender.Error.LoginError(e.Client);
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

        private void Receiver_ChangeLogin(object sender, ServerReceiver.ChangeLoginEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (IsActiveLogin(e.Client))
            {
                var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
                if (Sql.SqlUserCommands.CheckPassword(pass, login))
                {
                    Sql.SqlUserCommands.DataChange.ChangeLogin(userId, e.NewLogin);
                    ServerSender.Succesful.SuccessfulChangedRank(e.Client);

                    Sql.SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client),
                        SqlUserCommands.Actions.ChangeLogin);
                }
                else
                    ServerSender.Error.ChangeLoginError(e.Client);
            }
        }

        private void Receiver_ChangeRank(object sender, ServerReceiver.ChangeRankEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
            if (Sql.SqlUserCommands.CheckPassword(pass, login))
            {
                Sql.SqlUserCommands.DataChange.ChangeRank(userId, e.Rank);
                ServerSender.Succesful.SuccessfulChangedRank(e.Client);

                Sql.SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client),
                    SqlUserCommands.Actions.ChangeRank);
            }
            else
                ServerSender.Error.ChangeLoginError(e.Client);
        }

        private void Receiver_ChangeUsername(object sender, ServerReceiver.ChangeUsernameEventArgs e)
        {
            /*   var client = _clients[e.Client.ClientId];

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
                   ServerSender.Error.ChangeUsernameError(client.Client);*/
        }

        private void Receiver_ChangePassword(object sender, ServerReceiver.ChangePasswordEventArgs e)
        {
            /*   var client = _clients[e.Client.ClientId];

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
                   ServerSender.Error.ChangePasswordError(client.Client);*/
        }

        private void _communication_Disconnect(object sender, ServerReceiver.DisconnectEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client), SqlUserCommands.Actions.Logout);

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
