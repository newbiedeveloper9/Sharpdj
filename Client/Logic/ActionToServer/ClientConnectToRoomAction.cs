using System;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.ConnectToRoom;
using SharpDj.PubSubModels;

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
                case ConnectToRoomResult.Success:
                    publish = new RoomInfoForOpen(response.PreviewRoom, response.UserList);
                    break;
                case ConnectToRoomResult.AlreadyConnected:
                    publish = new RoomInfoForOpen();
                    break;
                default:
                    throw new Exception("Todo on error connect with room logic");
            }

            _eventAggregator.PublishOnUIThread(publish);
        }
    }
}