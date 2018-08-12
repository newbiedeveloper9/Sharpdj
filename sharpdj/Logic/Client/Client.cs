using System;
using System.Threading;
using Communication.Client.Logic;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using SharpDj.Enums.Menu;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client
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
#if  DEBUG
                    Thread.Sleep(1000);
#endif
                    ClientInfo.Instance.Client.Connect();
                }
                catch (TimeoutException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
           // ClientInfo.Instance.Client.SendMessage(new ScsTextMessage("login $"));

            Sender = new ClientSender(ClientInfo.Instance.Client, ClientInfo.Instance.ReplyMessenger);
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
