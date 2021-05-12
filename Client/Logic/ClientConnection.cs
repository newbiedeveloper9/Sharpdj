using Caliburn.Micro;
using Network;
using SCPackets;
using SharpDj.PubSubModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Network.RSA;
using SCPackets.Packets.AuthKeyLogin;

namespace SharpDj.Logic
{
    public class ClientConnection
    {
        private readonly Config _config;
        private readonly ClientSender _sender;
        private TcpConnection _connection;

        public ConnectionResult Result = ConnectionResult.TCPConnectionNotAlive;
        private readonly IEventAggregator _eventAggregator;


        public ClientConnection(IEventAggregator eventAggregator, Config config, ClientSender sender)
        {
            _eventAggregator = eventAggregator;
            _config = config;
            _sender = sender;
        }

        public async Task InitializeConnection()
        {
            Console.WriteLine($"Connecting with socket {_config.Ip}:{_config.Port}");

            int iterator = 0;
            do
            {
                if (iterator > 0)
                {
                    Thread.Sleep(3900);
                }

                var tcpConnection = await ConnectionFactory.CreateTcpConnectionAsync(_config.Ip, _config.Port);
                _connection = tcpConnection.Item1;
                Result = tcpConnection.Item2;

                if (Result != ConnectionResult.Connected)
                {
                    await _eventAggregator.PublishOnUIThreadAsync(new MessageQueue("Reconnect", $"Attempt number {iterator}"));
                    iterator++;
                }
            } while (Result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            _connection.EnableLogging = true;
            _connection.LogIntoStream(Console.OpenStandardError());
            _sender.SetConnection(_connection);

            if (!string.IsNullOrWhiteSpace(_config.AuthenticationKey))
            {
                _connection.Send(new AuthKeyLoginRequest(_config.AuthenticationKey), this);
            }
        }
    }
}
