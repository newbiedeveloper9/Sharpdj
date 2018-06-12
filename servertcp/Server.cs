using System;
using Communication.Server;
using Communication.Shared;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace servertcp
{
    public class Server
    {
        public int Port { get; set; } = 21007;
        public IScsServer _server { get; set; }
        private ServerReceiver _receiver;

        public void Start()
        {
            _receiver = new ServerReceiver();
            _server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(Port));

            new Receiver(_receiver, _server);

            _server.ClientConnected += Server_ClientConnected;
            _server.ClientDisconnected += Server_ClientDisconnected;
            _server.Start();


            Console.WriteLine("Enter something to close server");
            Console.ReadLine();
            _server.Stop();
        }

        void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            var client = e.Client;
            Utils.SendMessageToAllClients(_server, Utils.GetIpOfClient(client) + " connected with port " + Utils.GetPortOfClient(client));
            Console.WriteLine(Utils.GetIpOfClient(client) + " connected with port " + Utils.GetPortOfClient(client));

            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            if (message == null)
                return;

            var client = sender as IScsServerClient;
            if (client == null)
                return;


            _receiver.ParseMessage(client, message.Text, message.MessageId);
            Console.WriteLine("[{0}:{1}] Message: {2}", Utils.GetIpOfClient(client), Utils.GetPortOfClient(client), message.Text);
        }

        void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            var client = e.Client;
            Console.WriteLine("disconnect");
            Utils.SendMessageToAllClients(_server, Commands.Disconnect + " " + Utils.GetIpOfClient(client));
        }
    }
}