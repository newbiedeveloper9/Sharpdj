using System;
using Communication.Server;
using Communication.Shared;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment
{
    public class Server
    {
        private const int Port = 1433;
        private IScsServer _server;
        private ServerReceiver _receiver;

        public void Start()
        {
            _receiver = new ServerReceiver();
            _server = ScsServerFactory.CreateServer(new ScsTcpEndPoint("192.168.0.103",Port));

            new ServerLogic(_receiver, _server);

            _server.ClientConnected += Client_Connected;
          
            _server.Start();


            Console.WriteLine("Enter something to close server");
            Console.ReadLine();
            _server.Stop();
        }

        private void Client_Connected(object sender, ServerClientEventArgs e)
        {
            var client = e.Client;
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
    }
}