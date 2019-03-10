using Caliburn.Micro;
using Network;
using SCPackets;
using SharpDj.PubSubModels;
using System.Threading;

namespace SharpDj.Logic
{
    public class ClientConnection : IClient
    {
        public ConnectionResult Result = ConnectionResult.TCPConnectionNotAlive;

        private TcpConnection _connection;
        private readonly IEventAggregator _eventAggregator;
        private readonly ClientPacketsToHandleList _packetsList;
        private ClientSender _sender;

        public ClientConnection(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _packetsList = new ClientPacketsToHandleList(_eventAggregator);
        }

        public void Init()
        {
            int iterator = 0;
            do
            {
                iterator++;
                _connection = ConnectionFactory.CreateSecureTcpConnection("127.0.0.1", 5666, out Result, 2048);

                var mess = Result == ConnectionResult.Connected
                    ? new MessageQueue("Connection", "Successfully connected")
                    : new MessageQueue("Reconnecting", $"Attempt number {iterator}");

                _eventAggregator.PublishOnUIThread(mess);

                Thread.Sleep(4000);
            } while (Result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            _packetsList.RegisterPackets(_connection, this);
            _sender = new ClientSender(_eventAggregator, _connection, this);
        }
    }
}
