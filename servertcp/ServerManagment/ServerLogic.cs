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
using Communication.Server.Logic;
using Communication.Shared;
using CryptSharp.Utility;
using Hik.Collections;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;
using servertcp.Sql;

namespace servertcp.ServerManagment
{
    class ServerLogic
    {
        private readonly IScsServer _server;
        private readonly ThreadSafeSortedList<long, ServerClient> _clients;
        private readonly ThreadSafeSortedList<long, Room> _rooms;
        private int roomCount = 0;

        public List<ServerClient> ServerClients => (from client in _clients.GetAllItems() select client).ToList();
        public List<UserClient> UserClients => (from client in _clients.GetAllItems() select client.ToUserClient()).ToList();

        public ServerLogic(ServerReceiver receiver, IScsServer server)
        {
            _clients = new ThreadSafeSortedList<long, ServerClient>();
            _rooms = new ThreadSafeSortedList<long, Room>();
            _server = server;

            using (var reader = SqlHelper.ExecuteDataReader("SELECT * FROM Room"))
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var host = reader["Host"].ToString();
                    var image = reader["ImageUrl"].ToString();
                    var description = reader["Description"].ToString();
                    _rooms[roomCount] = new Room(id, name, host, image, description);
                    _rooms[roomCount].InsideInfo = new Room.InsindeInfo(new List<UserClient>(), new List<Songs>(), _rooms[roomCount]);
                    roomCount++;
                }

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

            SetEvents(receiver);
        }

        private void SetEvents(ServerReceiverEvents receiver)
        {
            _server.ClientDisconnected += Client_Disconnected;
            receiver.Disconnect += Receiver_Disconnect;
            receiver.Register += Receiver_Register;
            receiver.Login += Receiver_Login;
            receiver.ChangePassword += Receiver_ChangePassword;
            receiver.ChangeUsername += Receiver_ChangeUsername;
            receiver.ChangeRank += Receiver_ChangeRank;
            receiver.ChangeLogin += Receiver_ChangeLogin;
            receiver.GetPeople += Receiver_GetPeople;
            receiver.JoinRoom += Receiver_JoinRoom;
            receiver.CreateRoom += Receiver_CreateRoom;
            receiver.AfterLogin += Receiver_AfterLogin;
            receiver.JoinQueue += Receiver_JoinQueue;
            
            PeriodicTask.StartNew(1000, TrackRefresh);
        }

        private void TrackRefresh()
        {
            foreach (var room in _rooms.GetAllItems())
            {
                if (room.InsideInfo.Djs.Count == 0) continue;
                room.InsideInfo.TimeLeft--;
                
                if (room.InsideInfo.TimeLeft <= 0)
                {
                    room.InsideInfo.NextDj();
                    Console.WriteLine(room.InsideInfo.Djs[0].Video[0].Id + " new " + room.InsideInfo.TimeLeft);
                }
            }
        }

        private void Client_Disconnected(object sender, ServerClientEventArgs e)
        {
            //TODO
        }


        private void Receiver_JoinQueue(object sender, ServerReceiverEvents.JoinQueueEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (!IsActiveLogin(e.Client)) return;

                var source = JsonConvert.DeserializeObject<Songs>(e.Json);

                var userclient = _clients[e.Client.ClientId].ToUserClient();
                source.Host = userclient.Username;

                var tmp = _rooms.GetAllItems()
                    .FirstOrDefault(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
                if (tmp == null) return;
                
                tmp?.InsideInfo.Djs.Add(source);
                var json = JsonConvert.SerializeObject(tmp?.InsideInfo);
                
                {
                    foreach (var client in tmp.InsideInfo?.Clients)
                    {
                        ServerClients.FirstOrDefault(x => x.Id == client.Id)?.Client
                            .SendMessage(new ScsTextMessage("updatedj$" + json));
                    }
                }
            });
        }

        private void Receiver_AfterLogin(object sender, ServerReceiverEvents.AfterLoginEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            ServerSender.ServerCoreMethods.GetRooms(e.Client, _rooms.GetAllItems(), e.MessageId);
        }

        private void Receiver_CreateRoom(object sender, ServerReceiverEvents.CreateRoomEventArgs e)
        {
            if (!IsActiveLogin(e.Client))
            {
                ServerSender.Error(e.Client, e.MessageId);
                return;
            }

            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (SqlUserCommands.Room.Create(e.Name, login, e.Image, e.Description))
            {
                SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client), SqlUserCommands.Actions.CreateRoom);

                _rooms[roomCount] = new Room(_rooms.Count, e.Name, login, e.Image, e.Description);
                _rooms[roomCount].InsideInfo = new Room.InsindeInfo(new List<UserClient>(), new List<Songs>(), _rooms[roomCount]);
                roomCount++;
                ServerSender.Success(e.Client, e.MessageId);
            }
            else
                ServerSender.Error(e.Client, e.MessageId);
        }

        private void Receiver_JoinRoom(object sender, ServerReceiverEvents.JoinRoomEventArgs e)
        {
            if (!IsActiveLogin(e.Client))
            {
                ServerSender.Error(e.Client, e.MessageId);
            };

            //select client class object wchich refer to user
            var userclient = _clients[e.Client.ClientId].ToUserClient();

            //select room to join
            var room = _rooms.GetAllItems().FirstOrDefault(x => x.Id == e.RoomId);

            //Set user to max 1 room, remove from other instances
            var getRoomsWithSpecificUser = _rooms.GetAllItems().Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
            foreach (var roomActive in getRoomsWithSpecificUser)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userclient.Id);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

            //add client to room
            room?.InsideInfo.Clients.Add(userclient);

            //send info about room
            ServerSender.ServerCoreMethods.JoinRoom(e.Client, room?.InsideInfo, e.MessageId);
        }

        private void Receiver_GetPeople(object sender, ServerReceiverEvents.GetPeopleEventArgs e)
        {
            if (!IsActiveLogin(e.Client)) return;

            ServerSender.ServerCoreMethods.GetPeopleList(e.Client, UserClients);
        }

        private void Receiver_Register(object sender, ServerReceiverEvents.RegisterEventArgs e)
        {
            try
            {
                if (!SqlUserCommands.LoginExists(e.Login))
                {
                    var salt = Scrypt.GenerateSalt();

                    if (SqlUserCommands.CreateUser(e.Login, Scrypt.Hash(e.Password, salt), salt, e.Login))
                    {
                        ServerSender.Success(e.Client, e.MessageId);
                        var getUserID = SqlUserCommands.GetUserId(e.Login);

                        SqlUserCommands.AddActionInfo(getUserID, Utils.GetIpOfClient(e.Client),
                            SqlUserCommands.Actions.Register);
                    }
                    else
                        ServerSender.Error(e.Client, e.MessageId);
                }
                else
                    ServerSender.Error(e.Client, e.MessageId); //TODO acc exist param
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error(e.Client, e.MessageId);
            }
        }

        private void Receiver_Login(object sender, ServerReceiverEvents.LoginEventArgs e)
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

                        if (IsActiveLogin(e.Client))
                            Receiver_Disconnect(null, new ServerReceiverEvents.DisconnectEventArgs(e.Client));

                        _clients[client.Client.ClientId] = client;
                        ServerSender.Success(e.Client, e.MessageId);
                        SqlUserCommands.AddActionInfo(getUserID, Utils.GetIpOfClient(e.Client),
                            SqlUserCommands.Actions.Login);
                    }
                    else
                        ServerSender.Error(e.Client, e.MessageId);
                }
                else
                    ServerSender.Error(e.Client, e.MessageId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ServerSender.Error(e.Client, e.MessageId);
            }
        }

        private void Receiver_ChangeLogin(object sender, ServerReceiverEvents.ChangeLoginEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (IsActiveLogin(e.Client))
            {
                var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
                if (Sql.SqlUserCommands.CheckPassword(pass, login))
                {
                    Sql.SqlUserCommands.DataChange.ChangeLogin(userId, e.NewLogin);
                    ServerSender.Success(e.Client, e.MessageId);

                    Sql.SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client),
                        SqlUserCommands.Actions.ChangeLogin);
                }
                else
                    ServerSender.Error(e.Client, e.MessageId);
            }
        }

        private void Receiver_ChangeRank(object sender, ServerReceiverEvents.ChangeRankEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
            if (Sql.SqlUserCommands.CheckPassword(pass, login))
            {
                Sql.SqlUserCommands.DataChange.ChangeRank(userId, e.Rank);
                ServerSender.Success(e.Client, e.MessageId);

                Sql.SqlUserCommands.AddActionInfo(userId, Utils.GetIpOfClient(e.Client),
                    SqlUserCommands.Actions.ChangeRank);
            }
            else
                ServerSender.Error(e.Client, e.MessageId);
        }

        private void Receiver_ChangeUsername(object sender, ServerReceiverEvents.ChangeUsernameEventArgs e)
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

        private void Receiver_ChangePassword(object sender, ServerReceiverEvents.ChangePasswordEventArgs e)
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

        private void Receiver_Disconnect(object sender, ServerReceiverEvents.DisconnectEventArgs e)
        {
            var login = _clients[e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            var tmp = _rooms.GetAllItems().Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userId));
            foreach (var roomActive in tmp)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userId);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

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
