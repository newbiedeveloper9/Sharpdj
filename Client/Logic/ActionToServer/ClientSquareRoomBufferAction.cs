using System;
using System.Diagnostics;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.Buffers;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientSquareRoomBufferAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientSquareRoomBufferAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(SquareRoomBufferRequest request, Connection connection)
        {
            foreach (var newRoom in request.InsertRooms.ToReadonlyList())
            {
                _eventAggregator.PublishOnBackgroundThread(
                    new NewRoomCreatedPublish(newRoom));
            }

            foreach (var updatedRoom in request.UpdatedRooms)
            {
                _eventAggregator.PublishOnUIThread(
                    new UpdateOutsideRoomPublish(updatedRoom));
            }
        }
    }
}