using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace SharpServer
{
    public static class ServerConfig
    {
        public static int Port = 21007;
        public static IScsServer Server;
    
    }
}
