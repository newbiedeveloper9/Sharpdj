using System;
using Caliburn.Micro;
using Network;
using SCPackets;
using SharpDj.Enums;
using SharpDj.Interfaces;

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
