using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.LoginPacket;
using SharpDj.PubSubModels;
using System;
using System.Threading;
using Network.RSA;
using SCPackets.AuthKeyLogin;
using SCPackets.SendRoomChatMessage;

namespace SharpDj.Logic
{
    public class ClientConnection : IClient
    {
        public ConnectionResult Result = ConnectionResult.TCPConnectionNotAlive;

        private readonly Config _config;

        private TcpConnection _connection;
        private readonly IEventAggregator _eventAggregator;
        private readonly ClientPacketsToHandleList _packetsList;
        private ClientSender _sender;

        public ClientConnection(IEventAggregator eventAggregator, Config config)
        {
            _eventAggregator = eventAggregator;
            _config = config;
            _packetsList = new ClientPacketsToHandleList(_eventAggregator);

            Console.WriteLine($"Connecting with socket {_config.Ip}:{_config.Port}");
        }

        public void Init()
        {
            int iterator = 0;
            do
            {
                if (iterator > 0)
                    Thread.Sleep(3900);
                iterator++;

                _connection = ConnectionFactory.CreateSecureTcpConnection(_config.Ip, _config.Port, out Result, 2048);

                if (Result != ConnectionResult.Connected)
                    _eventAggregator.PublishOnUIThread(new MessageQueue("Reconnect", $"Attempt number {iterator}"));

            } while (Result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            _connection.EnableLogging = true;
            _connection.LogIntoStream(Console.OpenStandardError());

            _packetsList.RegisterPackets(_connection, this);
            _sender = new ClientSender(_eventAggregator, _connection, this);
            if (!string.IsNullOrWhiteSpace(_config.AuthenticationKey))
                _connection.Send(new AuthKeyLoginRequest(_config.AuthenticationKey), this);
        }
    }
}
