using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Client.User;
using Communication.Shared;
using Communication.Shared.Data;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;

namespace Communication.Server
{
    public class ServerClient
    {
        private string _username;

        public readonly IScsServerClient Client;

        public ServerClient(IScsServerClient client)
        {
            Client = client;
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public Rank Rank { get; set; } = Rank.User;

        public string Username
        {
            get => string.IsNullOrWhiteSpace(_username) ? Login : _username;
            set => _username = value;
        }

        public UserClient ToUserClient()
        {
            return new UserClient
            {
                Id = Id,
                Rank = Rank,
                Username = Username
            };
        }
    }


}
