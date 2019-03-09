using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.LoginPacket;
using SharpDj.PubSubModels;

namespace SharpDj.Logic
{
    public class ClientSender : IHandle<ISendPacket>
    {
        private IClient _instance;
        private readonly IEventAggregator _eventAggregator;
        private readonly Connection _connection;
        public ClientSender(IEventAggregator eventAggregator, Connection connection, IClient instance)
        {
            _connection = connection;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _instance = instance;
        }

        public void Handle(ISendPacket message)
        {
            _connection.Send(message.Packet, _instance);
        }
    }
}
