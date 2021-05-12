using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using Network.Packets;
using SCPackets;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

namespace SharpDj.Logic
{
    public class ClientSender : IHandle<ISendPacket>
    {
        private readonly IEventAggregator _eventAggregator;
        private Connection _connection;

        public ClientSender(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void SetConnection(Connection connection)
        {
            _connection = connection;
        }

        public async Task<T> Handle<T>(Packet message) 
            where T : ResponsePacket
        {
            if (_connection != null && _connection.IsAlive)
            {
                try
                {
                    var result =  await _connection.SendAsync<T>(message).ConfigureAwait(false);
                    return result;
                }
                catch (OperationCanceledException e)
                {
                    new ExceptionLogger(e);
                }
            }

            _eventAggregator.Unsubscribe(this);
            await _eventAggregator.PublishOnUIThreadAsync(new Reconnect());
            return null;
        }
        
        public void Handle(ISendPacket message)
        {
            throw new NotImplementedException();
        }
    }
}
