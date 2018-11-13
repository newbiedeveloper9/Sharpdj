using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Shared;

namespace Communication.Client
{
    public class UserClient
    {
        public UserClient()
        {
            
        }

        public UserClient(long id, string username, Rank rank, List<Dj> songs)
        {
            Id = id;
            Username = username;
            Rank = rank;
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public Rank Rank { get; set; } = Rank.User;
    }
}
