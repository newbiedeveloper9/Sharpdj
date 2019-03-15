using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.LoginPacket;
using SharpDj.PubSubModels;
using System;
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
                if (iterator > 0)
                    Thread.Sleep(3900);
                iterator++;
                _connection = ConnectionFactory.CreateTcpConnection("127.0.0.1", 5666, out Result);

                var mess = Result == ConnectionResult.Connected
                    ? new MessageQueue("Connection", "Successfully connected")
                    : new MessageQueue("Reconnecting", $"Attempt number {iterator}");

                _eventAggregator.PublishOnUIThread(mess);

            } while (Result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            _connection.EnableLogging = true;
            _connection.LogIntoStream(Console.OpenStandardOutput());
            _packetsList.RegisterPackets(_connection, this);
            _sender = new ClientSender(_eventAggregator, _connection, this);
#if DEBUG
            _connection.Send(new LoginRequest("1515", "151515"), this);
#endif
        }
    }
}
