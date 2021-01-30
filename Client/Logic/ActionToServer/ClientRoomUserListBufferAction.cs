using System;
using System.IO;
using System.Net;
using System.Windows;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.Buffers;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientRoomUserListBufferAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientRoomUserListBufferAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(RoomUserListBufferRequest request, Connection connection)
        {
            _eventAggregator.PublishOnUIThread(
                new RoomUserListBufferPublish(request));
        }
    }
}