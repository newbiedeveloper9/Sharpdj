using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.RoomOutsideUpdate;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientRoomOutsideUpdateAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientRoomOutsideUpdateAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(RoomOutsideUpdateRequest req, Connection connection)
        {
             _eventAggregator.PublishOnBackgroundThread(
                 new UpdateOutsideRoomPublish(req.RoomOutsideModel));
        } 
    }
}
