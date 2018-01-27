using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Server;
using Hik.Collections;

namespace SharpServer
{
    public class Receiver
    {

        private readonly ThreadSafeSortedList<long, ServerGuest> _clients;

        public List<ServerGuest> Clients => (from client in _clients.GetAllItems() select client).ToList();

        public Receiver(ServerReceiver serverReceiver)
        {
            _clients = new ThreadSafeSortedList<long, ServerGuest>();

            serverReceiver.LoginAsGuest += _serverReceiver_LoginAsGuest;

        }

        private void _serverReceiver_LoginAsGuest(object sender, ServerReceiver.LoginAsGuestEventArgs e)
        {
            var client = new ServerGuest
            {
                Client = e.Client,
                Ip = Utils.GetIpOfClient(e.Client)
            };


            ServerSender.Succesful.LoginAsGuest(e.Client);
            _clients[e.Client.ClientId] = client;
        }
    }
}
