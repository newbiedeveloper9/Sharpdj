using System;
using Caliburn.Micro;
using Network;
using SCPackets.Ping;
using SharpDj.PubSubModels;
using Result = SCPackets.Ping;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientPingAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientPingAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(PingRequest req, Connection connection)
        {
            _eventAggregator.PublishOnUIThread(new PubSubModels.MessageQueue("Ping", "ping from server"));
        }
    }
}