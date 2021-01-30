using Caliburn.Micro;
using Network;
using SCPackets;
using SharpDj.PubSubModels;

namespace SharpDj.Logic
{
    public class ClientSender : IHandle<ISendPacket>
    {
        private readonly object _instance;
        private readonly IEventAggregator _eventAggregator;
        private readonly Connection _connection;
        public ClientSender(IEventAggregator eventAggregator, Connection connection, object instance)
        {
            _connection = connection;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _instance = instance;
        }

        public void Handle(ISendPacket message)
        {
            if (_connection.IsAlive)
            {
                if (message.UseInstance)
                    _connection.Send(message.Packet, _instance);
                else
                    _connection.Send(message.Packet);

                return;
            }

            _eventAggregator.Unsubscribe(this);
            _eventAggregator.PublishOnUIThread(new Reconnect());
        }
    }
}
