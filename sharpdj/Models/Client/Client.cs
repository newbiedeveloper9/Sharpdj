using System;
using System.Threading;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Communication;
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

        public string Ip { get; set; } = "192.168.0.103";
        public int Port { get; set; } = 1433;


        public void Start(SdjMainViewModel main)
        {
            
            SdjMainViewModel = main;
            ClientInfo.Instance.Client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Ip, Port));
            ClientInfo.Instance.Client.MessageReceived += Client_MessageReceived;
            ClientInfo.Instance.Client.Disconnected += Client_Disconnected;
            ClientInfo.Instance.ReplyMessenger = new RequestReplyMessenger<IScsClient>(ClientInfo.Instance.Client);
            ClientInfo.Instance.ReplyMessenger.Start();
            while (ClientInfo.Instance.Client.CommunicationState == CommunicationStates.Disconnected)
            {
                try
                {
                    ClientInfo.Instance.Client.Connect();
                }
                catch (TimeoutException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            ClientInfo.Instance.Client.SendMessage(new ScsTextMessage("login $"));

            Sender = new ClientSender(ClientInfo.Instance.Client);
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
            Receiver.ParseMessage(ClientInfo.Instance.Client, message.Text);
        }
    }
}
