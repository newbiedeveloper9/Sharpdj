using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
    public class ServerGuest
    {
        public IScsServerClient Client;
        public string Ip { get; set; }
        public int Id { get; set; } = 0;
    }
}
