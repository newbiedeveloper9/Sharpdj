using System;
using System.Threading;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messengers;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.ViewModel;


namespace SharpDj.Models.Client
{
    public class Client
    {
        public SdjMainViewModel SdjMainViewModel { get; set; }
        public ClientReceiver Receiver { get; set; } = new ClientReceiver();
        public ClientSender Sender { get; set; }

        public string Ip { get; set; } = "localhost";
        public int Port { get; set; } = 21007;


        public void Start(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            ClientInfo.Client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Ip, Port));
            ClientInfo.Client.MessageReceived += Client_MessageReceived;
            ClientInfo.Client.Disconnected += Client_Disconnected;
            ClientInfo.ReplyMessenger = new RequestReplyMessenger<IScsClient>(ClientInfo.Client);
            ClientInfo.ReplyMessenger.Start();
            ClientInfo.Client.Connect();
            ClientInfo.Client.SendMessage(new ScsTextMessage("login $"));

            Sender = new ClientSender(ClientInfo.Client);
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected");
            SdjMainViewModel.MainViewVisibility = MainView.Login;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;

            if (message == null)
                return;

            Console.WriteLine(message.Text + "\n");
            Receiver.ParseMessage(ClientInfo.Client, message.Text);
        }
    }
}
