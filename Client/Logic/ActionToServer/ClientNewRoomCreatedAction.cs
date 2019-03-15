using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.NewRoomCreated;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientNewRoomCreatedAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientNewRoomCreatedAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(NewRoomCreatedRequest request, Connection connection)
        {
            _eventAggregator.PublishOnUIThread(
                new NewRoomCreatedPublish(request?.Room));
        }
    }
}
