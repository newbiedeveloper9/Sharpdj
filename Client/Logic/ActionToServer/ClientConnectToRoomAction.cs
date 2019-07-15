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
            IRoomInfoForOpen publish;

            switch (response.Result)
            {
                case Result.Success:
                    publish = new RoomInfoForOpen(response.RoomOutsideModel, response.UserList);
                    break;
                case Result.AlreadyConnected:
                    publish = new RoomInfoForOpen();
                    break;
                default:
                    throw new Exception("Todo on error connect with room logic");
            }

            _eventAggregator.PublishOnUIThread(publish);
        }
    }
}