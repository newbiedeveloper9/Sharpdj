using System;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Communication.Client;


namespace SharpDj.Models.Client
{
    public class Client
    {
        public ClientReceiver Receiver { get; set; } = new ClientReceiver();
        public ClientSender Sender { get; set; }

        public string Ip { get; set; } = "localhost";
        public int Port { get; set; } = 21007;


        public void Start()
        {
            ClientInfo.Client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Ip, Port));
            ClientInfo.Client.MessageReceived += Client_MessageReceived;
            ClientInfo.Client.Disconnected += Client_Disconnected;
            ClientInfo.Client.Connect();
            Sender = new ClientSender(ClientInfo.Client);
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {

        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;

            if (message == null)
                return;

            Receiver.ParseMessage(ClientInfo.Client, message.Text);
        }
    }
}
