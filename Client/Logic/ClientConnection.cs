using Caliburn.Micro;
using Network;
using SCPackets;
using System;
using System.Threading;

namespace SharpDj.Logic
{
    public class ClientConnection : IClient
    {
        private ConnectionResult result = ConnectionResult.TCPConnectionNotAlive;

        private readonly TcpConnection _connection;
        private readonly IEventAggregator _eventAggregator;
        private readonly ClientPacketsToHandleList _packetsList;
        private ClientSender _sender;

        public ClientConnection(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _packetsList = new ClientPacketsToHandleList(_eventAggregator);

            int iterator = 0;
            do
            {
                if (iterator > 0) Console.WriteLine("Reconnecting. Attempt number {0}", iterator);
                _connection = ConnectionFactory.CreateSecureTcpConnection("127.0.0.1", 5666, out result, 2048);
                Thread.Sleep(2500);
                iterator++;
            } while (result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            System.Console.WriteLine("Connection with server established.");
            _packetsList.RegisterPackets(_connection, this);
            _sender = new ClientSender(_eventAggregator, _connection, this);
        }
    }
}
