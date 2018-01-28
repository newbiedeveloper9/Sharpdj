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
        public string Username { get; set; }
        public Rank Rank { get; set; } = Rank.User;
    }
}
