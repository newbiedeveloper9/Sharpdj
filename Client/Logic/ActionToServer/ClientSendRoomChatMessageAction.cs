using System;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.CreateRoomMessage;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientSendRoomChatMessageAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientSendRoomChatMessageAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(CreateRoomMessageResponse response, Connection connection)
        {

            if (response.Result != CreateRoomMessageResult.Success)
            {
                _eventAggregator.PublishOnUIThread(new MessageQueue("Chat", "An error occurred in attempt to send message"));
                _ = new ExceptionLogger($"ClientSendRoomChatMessageAction response: {response.Result}");
            }

            _eventAggregator.PublishOnUIThread(
                new RoomChatMessageStatePublish(response.Result));
        }
    }
}