using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Client.User;
using Communication.Shared;
using Communication.Shared.Data;
using Hik.Communication.Scs.Server;
using Server.Sql;

namespace Server.ServerManagment
{
    class ServerLogic
    {
        private readonly IScsServer _server;

        private int roomCount = 0;

        public ServerLogic(ServerReceiver receiver, IScsServer server)
        {
            _server = server;

            using (var reader = SqlHelper.ExecuteDataReader("SELECT * FROM Room"))
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader["Id"].ToString());
                    var name = reader["Name"].ToString();
                    var host = reader["Host"].ToString();
                    var image = reader["ImageUrl"].ToString();
                    var description = reader["Description"].ToString();
                    DataSingleton.Instance.Rooms[roomCount] = new Room(id, name, host, image, description);
                    DataSingleton.Instance.Rooms[roomCount].InsideInfo = new Room.InsindeInfo(new List<UserClient>(),
                        new List<Dj>(), DataSingleton.Instance.Rooms[roomCount]);
                    roomCount++;
                }

            PeriodicTask.StartNew(1000, TrackRefresh);
        }

        private void TrackRefresh()
        {
            foreach (var room in DataSingleton.Instance.Rooms.GetAllItems())
            {
                if (room.InsideInfo.Djs.Count == 0) continue;
                room.InsideInfo.TimeLeft--;

                if (room.InsideInfo.TimeLeft <= 0)
                {
                    room.InsideInfo.NextDj();
                    foreach (var user in room.InsideInfo.Clients)
                    {
                        var client = new ServerSender(DataSingleton.Instance.ServerClients.GetAllItems()
                            .First(x => x.Id.Equals(user.Id)).Client);
                        
                        client.ChangeTrack(room.InsideInfo.Djs[0], room.Id.ToString());
                    }

                    Console.WriteLine(room.InsideInfo.Djs[0].Track[0].Id + " new " + room.InsideInfo.TimeLeft);
                }
            }
        }

        private void Client_Disconnected(object sender, ServerClientEventArgs e)
        {
            //TODO
        }


/*        private void Receiver_JoinQueue(object sender, ServerReceiverEvents.JoinQueueEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (!Utils.Instance.IsActiveLogin(e.Client)) return;

                var source = JsonConvert.DeserializeObject<Songs>(e.Json);

                var userclient = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].ToUserClient();
                source.Host = userclient.Username;

                var tmp = DataSingleton.Instance.Rooms.GetAllItems()
                    .FirstOrDefault(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
                if (tmp == null) return;
                
                tmp?.InsideInfo.Djs.Add(source);
                var json = JsonConvert.SerializeObject(tmp?.InsideInfo);
                
                {
                    foreach (var client in tmp.InsideInfo?.Clients)
                    {
                        DataSingleton.Instance.ServerClients.GetAllItems().FirstOrDefault(x => x.Id == client.Id)?.Client
                            .SendMessage(new ScsTextMessage("updatedj$" + json));
                    }
                }
            });
        }

        private void Receiver_AfterLogin(object sender, ServerReceiverEvents.AfterLoginEventArgs e)
        {
            if (!Utils.Instance.IsActiveLogin(e.Client)) return;

//            ServerSender.ServerCoreMethods.GetRooms(e.Client, DataSingleton.Instance.Rooms.GetAllItems(), e.MessageId);
        }

        private void Receiver_CreateRoom(object sender, ServerReceiverEvents.CreateRoomEventArgs e)
        {
            if (!Utils.Instance.IsActiveLogin(e.Client))
            {
          //      ServerSender.Error(e.Client, e.MessageId);
                return;
            }

            var login = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (SqlUserCommands.Room.Create(e.Name, login, e.Image, e.Description))
            {
                SqlUserCommands.AddActionInfo(userId, Utils.Instance.GetIpOfClient(e.Client), SqlUserCommands.Actions.CreateRoom);

                DataSingleton.Instance.Rooms[roomCount] = new Room(DataSingleton.Instance.ServerClients.Count, e.Name, login, e.Image, e.Description);
                DataSingleton.Instance.Rooms[roomCount].InsideInfo = new Room.InsindeInfo(new List<UserClient>(), new List<Songs>(), DataSingleton.Instance.Rooms[roomCount]);
                roomCount++;
         //       ServerSender.Success(e.Client, e.MessageId);
            }
           // else
         //       ServerSender.Error(e.Client, e.MessageId);
        }

        private void Receiver_JoinRoom(object sender, ServerReceiverEvents.JoinRoomEventArgs e)
        {
            if (!Utils.Instance.IsActiveLogin(e.Client))
            {
          //      ServerSender.Error(e.Client, e.MessageId);
            };

            //select client class object wchich refer to user
            var userclient = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].ToUserClient();

            //select room to join
            var room = DataSingleton.Instance.Rooms.GetAllItems().FirstOrDefault(x => x.Id == e.RoomId);

            //Set user to max 1 room, remove from other instances
            var getRoomsWithSpecificUser = DataSingleton.Instance.Rooms.GetAllItems().Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userclient.Id));
            foreach (var roomActive in getRoomsWithSpecificUser)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userclient.Id);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

            //add client to room
            room?.InsideInfo.Clients.Add(userclient);

            //send info about room
       //     ServerSender.ServerCoreMethods.JoinRoom(e.Client, room?.InsideInfo, e.MessageId);
        }

        private void Receiver_GetPeople(object sender, ServerReceiverEvents.GetPeopleEventArgs e)
        {
            if (!Utils.Instance.IsActiveLogin(e.Client)) return;

        //    ServerSender.ServerCoreMethods.GetPeopleList(e.Client, DataSingleton.Instance.UserClients);
        }

        private void Receiver_ChangeLogin(object sender, ServerReceiverEvents.ChangeLoginEventArgs e)
        {
            var login = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            if (Utils.Instance.IsActiveLogin(e.Client))
            {
                var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
                if (Sql.SqlUserCommands.CheckPassword(pass, login))
                {
                    Sql.SqlUserCommands.DataChange.ChangeLogin(userId, e.NewLogin);
             //       ServerSender.Success(e.Client, e.MessageId);

                    Sql.SqlUserCommands.AddActionInfo(userId, Utils.Instance.GetIpOfClient(e.Client),
                        SqlUserCommands.Actions.ChangeLogin);
                }
             //   else
             //       ServerSender.Error(e.Client, e.MessageId);
            }
        }

        private void Receiver_ChangeRank(object sender, ServerReceiverEvents.ChangeRankEventArgs e)
        {
            var login = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            var pass = Scrypt.Hash(e.Password, SqlUserCommands.GetSalt(login));
            if (Sql.SqlUserCommands.CheckPassword(pass, login))
            {
                Sql.SqlUserCommands.DataChange.ChangeRank(userId, e.Rank);
             //   ServerSender.Success(e.Client, e.MessageId);

                Sql.SqlUserCommands.AddActionInfo(userId, Utils.Instance.GetIpOfClient(e.Client),
                    SqlUserCommands.Actions.ChangeRank);
            }
          //  else
             //   ServerSender.Error(e.Client, e.MessageId);
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
                   ServerSender.Error.ChangeUsernameError(client.Client);#1#
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
                   ServerSender.Error.ChangePasswordError(client.Client);#1#
        }

        private void Receiver_Disconnect(object sender, ServerReceiverEvents.DisconnectEventArgs e)
        {
            
            var login = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId].Login;
            var userId = SqlUserCommands.GetUserId(login);

            var tmp = DataSingleton.Instance.Rooms.GetAllItems().Where(x => x.InsideInfo.Clients.Exists(y => y.Id == userId));
            foreach (var roomActive in tmp)
            {
                var index = roomActive.InsideInfo.Clients.FindIndex(x => x.Id == userId);
                roomActive.InsideInfo.Clients.RemoveAt(index);
            }

            SqlUserCommands.AddActionInfo(userId, Utils.Instance.GetIpOfClient(e.Client), SqlUserCommands.Actions.Logout);

            var client = DataSingleton.Instance.ServerClients[(int)e.Client.ClientId];

            DataSingleton.Instance.ServerClients.Remove(e.Client.ClientId);
            Console.WriteLine("{0} disconnected", client.Username);
        }*/
    }
}