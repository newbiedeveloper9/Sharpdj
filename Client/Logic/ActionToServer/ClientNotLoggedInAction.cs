using System;
using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.Packets.NotLoggedIn;
using SharpDj.Enums;
using SharpDj.Interfaces;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientNotLoggedInAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientNotLoggedInAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(NotLoggedInRequest req, Connection conn)
        {
            _eventAggregator.PublishOnUIThread(new NotLoggedIn());
        }
    }
}
