using System;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.Disconnect;
using SharpDj.Enums;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientDisconnectAction : IAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientDisconnectAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(DisconnectResponse response, Connection connection)
        {
            if(response.Result == DisconnectResult.Success)
                _eventAggregator.PublishOnUIThread(new NotLoggedIn());
        }
    }
}

