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
        public ConnectionResult Result = ConnectionResult.TCPConnectionNotAlive;

        private readonly Config _config;

        private readonly IEnumerable<IAction> _actionRegisterList;
        private TcpConnection _connection;

        private readonly IEventAggregator _eventAggregator;

        private ClientSender _sender;

        public ClientConnection(IEventAggregator eventAggregator, Config config)
        {
            _actionRegisterList = IoC.GetAll<IAction>();
            _eventAggregator = eventAggregator;
            _config = config;
            // _packetsList = new ClientPacketsToHandleList(_eventAggregator);

            Console.WriteLine($"Connecting with socket {_config.Ip}:{_config.Port}");
        }

        public async Task Init()
        {
            int iterator = 0;
            do
            {
                if (iterator > 0)
                    Thread.Sleep(3900);
                iterator++;

                var tcpConnection = await ConnectionFactory.CreateTcpConnectionAsync(_config.Ip, _config.Port);
                _connection = tcpConnection.Item1;
                Result = tcpConnection.Item2;

                if (Result != ConnectionResult.Connected)
                {
                    await _eventAggregator.PublishOnUIThreadAsync(new MessageQueue("Reconnect", $"Attempt number {iterator}"));
                }
            } while (Result != ConnectionResult.Connected);

            ConnectionEstablished();
        }

        private void ConnectionEstablished()
        {
            _connection.EnableLogging = true;
            _connection.LogIntoStream(Console.OpenStandardError());

            foreach (var action in _actionRegisterList)
            {
                action.RegisterPacket(_connection, this);
            }

            _sender = new ClientSender(_eventAggregator, _connection, this);
            if (!string.IsNullOrWhiteSpace(_config.AuthenticationKey))
            {
                _connection.Send(new AuthKeyLoginRequest(_config.AuthenticationKey), this);
            }
        }
    }
}
