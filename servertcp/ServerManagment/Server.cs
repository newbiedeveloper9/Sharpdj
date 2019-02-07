using System;
using System.Diagnostics;
using Communication;
using Communication.Server;
using Communication.Server.Logic;
using Communication.Shared;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment
{
    public class Server
    {
        private ServerConfig _config;
        private IScsServer _server;
        private ServerReceiver _receiver;

        public void Start()
        {
            _config = ServerConfig.LoadConfig();            
            _receiver = new ServerReceiver(_server);
            _server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(_config.Ip, _config.Port));

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
            Console.WriteLine(Utils.Instance.GetIpOfClient(client) + " connected with port " + Utils.Instance.GetPortOfClient(client));

            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!(e.Message is ScsTextMessage message))
                return;

            if (!(sender is IScsServerClient client))
                return;


            _receiver.ParseMessage(client, message.Text, message.MessageId);
            Console.WriteLine("[{0}:{1}] Message: {2}", Utils.Instance.GetIpOfClient(client), Utils.Instance.GetPortOfClient(client), message.Text);
        }
    }
}