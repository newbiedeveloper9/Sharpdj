using System;
using Caliburn.Micro;
using Network;
using SCPackets.ConnectToRoom;
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
                throw new Exception("Todo on success connect with room logic");
            }
            throw new Exception("Todo on error connect with room logic");
        }
    }
}