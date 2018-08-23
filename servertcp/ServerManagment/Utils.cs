using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using servertcp;

namespace Communication.Server
{
    /// <summary>
    /// Utils class is singleton.
    /// </summary>
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
                if (clients != null && clients.CommunicationState == CommunicationStates.Connected)
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


        /// <summary>
        /// Returns command in index 0 and parameters in index 1+
        /// </summary>
        /// <param name="message">text from receiver</param>
        /// <returns></returns>
        public List<string> GetMessageParameters(string message)
        {
            var list = new List<string>();
            var commandEnd = message.IndexOf(' ');
            
            list.Add(message.Remove(commandEnd));
            message = message.Substring(commandEnd+1);
            list.AddRange(message.Split('$'));
            
            return list;
        }

        
        /// <summary>
        /// Returns true if given client is already signed in.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool IsActiveLogin(IScsServerClient client)
        {
             return DataSingleton.Instance.ServerClients[(int)client.ClientId] != null;
        }
    }
}