using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
   public sealed class Utils
    {
        private static readonly Lazy<Utils> lazy = 
            new Lazy<Utils>(() => new Utils());

        public static Utils Instance => lazy.Value;
        
        private Utils()
        {
            
        }
        
        public void SendMessageToAllClients(IScsServer server, string message)
        {
            foreach (var clients in server.Clients.GetAllItems())
            {
                if(clients!=null && clients.CommunicationState==CommunicationStates.Connected)
                clients.SendMessage(new ScsTextMessage(message));
            }
        }

        public string GetIpOfClient(IScsServerClient client)
        {
            var all = client.RemoteEndPoint.ToString();
            var ipStart = all.IndexOf("//");
            var ipWithPort = all.Substring(ipStart + 2);
            var portStart = ipWithPort.IndexOf(":");
            var ip = ipWithPort.Remove(portStart);

            return ip;
        }

        public int GetPortOfClient(IScsServerClient client)
        {
            var all = client.RemoteEndPoint.ToString();
            var portStart = all.LastIndexOf(":");
            var port = all.Substring(portStart + 1);

            return Convert.ToInt32(port);
        }

        public string GetCommandFromMessage(string message)
        {
            return string.Empty;
        }

        public List<String> GetParametersFromMessagE(string message)
        {
         return new List<string>();   
        }
    }
}
