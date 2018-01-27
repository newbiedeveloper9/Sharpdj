using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Server;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace SharpServer
{
    public class Server
    {
        private ServerReceiver _serverReceiver;

        public Server()
        {
            ServerConfig.Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(ServerConfig.Port));
            ServerConfig.Server.Start();

            ServerConfig.Server.ClientConnected += Server_ClientConnected;
            ServerConfig.Server.ClientDisconnected += Server_ClientDisconnected;


        }

        private void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            var client = e.Client;
            Console.WriteLine(Utils.GetIpOfClient(e.Client) + " Connected!");
            client.MessageReceived += MessageReceivedFromClient;
        }

        private void MessageReceivedFromClient(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            if (message == null)
                return;

            var client = sender as IScsServerClient;

            if (client == null)
                return;

            _serverReceiver.ParseMessage(client, message.Text);
        }

        private void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            Console.WriteLine("dc");
        }

    }
}
