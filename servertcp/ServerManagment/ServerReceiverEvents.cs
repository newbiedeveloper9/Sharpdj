using System;
using Communication.Shared;
using Hik.Communication.Scs.Server;

namespace Communication.Server.Logic
{
    public class ServerReceiverEvents
    {
       #region Events

        public event EventHandler<DisconnectEventArgs> Disconnect;
        public event EventHandler<RegisterEventArgs> Register;
        public event EventHandler<GetPeopleEventArgs> GetPeople;
        public event EventHandler<JoinRoomEventArgs> JoinRoom;
        public event EventHandler<CreateRoomEventArgs> CreateRoom;
        public event EventHandler<AfterLoginEventArgs> AfterLogin;
        public event EventHandler<JoinQueueEventArgs> JoinQueue;


        public void OnJoinQueue(JoinQueueEventArgs e)
        {
            var handler = JoinQueue;
            handler?.Invoke(this, e);
        }

        public void OnAfterLogin(AfterLoginEventArgs e)
        {
            var handler = AfterLogin;
            handler?.Invoke(this, e);
        }

        public void OnCreateRoom(CreateRoomEventArgs e)
        {
            var handler = CreateRoom;
            handler?.Invoke(this, e);
        }

        public void OnJoinRoom(JoinRoomEventArgs e)
        {
            var handler = JoinRoom;
            handler?.Invoke(this, e);
        }

        public void OnGetPeople(GetPeopleEventArgs e)
        {
            EventHandler<GetPeopleEventArgs> eh = GetPeople;
            eh?.Invoke(this, e);
        }

        public void OnDisconnect(DisconnectEventArgs e)
        {
            EventHandler<DisconnectEventArgs> eh = Disconnect;
            eh?.Invoke(this, e);
        }



        public void OnRegister(RegisterEventArgs e)
        {
            var handler = Register;
            handler?.Invoke(this, e);
        }

        public class JoinQueueEventArgs : System.EventArgs
        {
            public JoinQueueEventArgs(IScsServerClient client, string json, string messageId)
            {
                this.Client = client;
                this.Json = json;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Json { get; private set; }
            public string MessageId { get; private set; }
        }

        public class AfterLoginEventArgs : System.EventArgs
        {
            public AfterLoginEventArgs(IScsServerClient client, string messageId)
            {
                this.Client = client;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string MessageId { get; private set; }
        }

        public class JoinRoomEventArgs : System.EventArgs
        {
            public JoinRoomEventArgs(int roomId, IScsServerClient client, string messageId)
            {
                this.RoomId = roomId;
                this.Client = client;
                this.MessageId = messageId;
            }

            public int RoomId { get; private set; }
            public IScsServerClient Client { get; private set; }
            public string MessageId { get; private set; }
        }

        public class CreateRoomEventArgs : System.EventArgs
        {
            public CreateRoomEventArgs(IScsServerClient client, string name, string image, string description, string messageId)
            {
                this.Client = client;
                this.Name = name;
                this.Image = image;
                this.Description = description;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Name { get; private set; }
            public string Image { get; private set; }
            public string Description { get; private set; }
            public string MessageId { get; private set; }
        }

        public class RegisterEventArgs : System.EventArgs
        {
            public RegisterEventArgs(string login, string password, string email, IScsServerClient client, string messageId)
            {
                this.Login = login;
                this.Password = password;
                this.Email = email;
                this.Client = client;
                this.MessageId = messageId;
            }

            public string Login { get; private set; }
            public string Password { get; private set; }
            public string Email { get; private set; }
            public IScsServerClient Client { get; private set; }
            public string MessageId { get; private set; }
        }

      

        public class DisconnectEventArgs : System.EventArgs
        {
            public DisconnectEventArgs(IScsServerClient client)
            {
                this.Client = client;
            }

            public IScsServerClient Client { get; private set; }
        }

        public class GetPeopleEventArgs : System.EventArgs
        {
            public GetPeopleEventArgs(IScsServerClient client)
            {
                this.Client = client;
            }

            public IScsServerClient Client { get; private set; }
        }

        #region UserAccount

        public event EventHandler<ChangePasswordEventArgs> ChangePassword;
        public event EventHandler<ChangeUsernameEventArgs> ChangeUsername;
        public event EventHandler<ChangeLoginEventArgs> ChangeLogin;
        public event EventHandler<ChangeRankEventArgs> ChangeRank;


        public void OnChangeRank(ChangeRankEventArgs e)
        {
            var handler = ChangeRank;
            handler?.Invoke(this, e);
        }

        public void OnChangeLogin(ChangeLoginEventArgs e)
        {
            var handler = ChangeLogin;
            handler?.Invoke(this, e);
        }

        public void OnChangeUsername(ChangeUsernameEventArgs e)
        {
            var handler = ChangeUsername;
            handler?.Invoke(this, e);
        }

        public void OnChangePassword(ChangePasswordEventArgs e)
        {
            var handler = ChangePassword;
            handler?.Invoke(this, e);
        }


        public class ChangeRankEventArgs : System.EventArgs
        {
            public ChangeRankEventArgs(IScsServerClient client, string password, Rank rank, string messageId)
            {
                this.Client = client;
                this.Password = password;
                this.Rank = rank;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Password { get; private set; }
            public Rank Rank { get; private set; }
            public string MessageId { get; private set; }
        }

        public class ChangeLoginEventArgs : System.EventArgs
        {
            public ChangeLoginEventArgs(IScsServerClient client, string password, string newLogin, string messageId)
            {
                this.Client = client;
                this.Password = password;
                this.NewLogin = newLogin;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Password { get; private set; }
            public string NewLogin { get; private set; }
            public string MessageId { get; private set; }
        }

        public class ChangeUsernameEventArgs : System.EventArgs
        {
            public ChangeUsernameEventArgs(IScsServerClient client, string password, string newUsername, string messageId)
            {
                this.Client = client;
                this.Password = password;
                this.NewUsername = newUsername;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Password { get; private set; }
            public string NewUsername { get; private set; }
            public string MessageId { get; private set; }
        }

        public class ChangePasswordEventArgs : System.EventArgs
        {
            public ChangePasswordEventArgs(IScsServerClient client, string password, string newPassword, string messageId)
            {
                this.Client = client;
                this.Password = password;
                this.NewPassword = newPassword;
                this.MessageId = messageId;
            }

            public IScsServerClient Client { get; private set; }
            public string Password { get; private set; }
            public string NewPassword { get; private set; }
            public string MessageId { get; private set; }
        }

        #endregion

        #endregion
    }
}