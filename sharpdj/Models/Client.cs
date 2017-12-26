using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;

namespace SharpDj.Models
{
    public class Client
    {

        public string Ip { get; set; } = "78.88.84.56";
        public int Port { get; set; } = 21007;


        public void Start()
        {
            ClientInfo.Client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Ip, Port));
            ClientInfo.Client.MessageReceived += Client_MessageReceived;
            ClientInfo.Client.Disconnected += Client_Disconnected;
            ClientInfo.Client.Connect();
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {

        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;

            if (message == null)
                return;

        /*    _receiver = new ClientReceiver();
            _receiver.ParseMessage(client, message.Text);*/
            //TODO: disconnected player
        }
    }

    public static class ClientInfo
    {
        public static IScsClient Client;
    }
}
