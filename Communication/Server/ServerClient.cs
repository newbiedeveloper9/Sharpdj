
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;

namespace Communication.Server
{
    public class ServerClient
    {
        private string _username;

        [JsonIgnore]
        public IScsServerClient Client;

        public ServerClient(IScsServerClient client)
        {
            Client = client;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
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
                Rank = Rank,
                Username = Username
            };
        }
    }


}
