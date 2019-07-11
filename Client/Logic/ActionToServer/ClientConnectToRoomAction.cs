using System;
using Caliburn.Micro;
using Network;
using SCPackets.ConnectToRoom;
using SharpDj.PubSubModels;
using Result = SCPackets.ConnectToRoom.Result;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientConnectToRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientConnectToRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(ConnectToRoomResponse response, Connection connection)
        {
            if (response.Result == Result.Success || response.Result == Result.AlreadyConnected)
            {
                
                _eventAggregator.PublishOnUIThread(new RoomInfoForOpen(response.RoomOutsideModel));
                return;
            }
            throw new Exception("Todo on error connect with room logic");
        }
    }
}