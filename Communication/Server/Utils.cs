using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
    public static class Utils
    {
        public static void SendMessageToAllClients(IScsServer server, string message)
        {
            foreach (var clients in server.Clients.GetAllItems())
            {
                clients.SendMessage(new ScsTextMessage(message));
            }
        }

        public static void SendMessage(IScsServerClient client, string message)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format(message)));
        }

        public static string GetIpOfClient(IScsServerClient client)
        {
            var all = client.RemoteEndPoint.ToString();
            var ipStart = all.IndexOf("//");
            var ipWithPort = all.Substring(ipStart + 2);
            var portStart = ipWithPort.IndexOf(":");
            var ip = ipWithPort.Remove(portStart);

            return ip;
        }

        public static int GetPortOfClient(IScsServerClient client)
        {
            var all = client.RemoteEndPoint.ToString();
            var portStart = all.LastIndexOf(":");
            var port = all.Substring(portStart + 1);

            return Convert.ToInt32(port);
        }
    }
}
