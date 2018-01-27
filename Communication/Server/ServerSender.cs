using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
    public class ServerSender
    {
        public static class Succesful
        {
            public static void LoginAsGuest(IScsServerClient client)
            {
               Utils.SendMessage(client, CommandsPacket.Succesfuls.SuccesfulLoginAsGuest.ToString());
            }
        }
    }
}
