using System;
using Caliburn.Micro;
using Network;
using SCPackets.SendRoomChatMessage;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;
using Result = SCPackets.SendRoomChatMessage.Result;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientSendRoomChatMessageAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientSendRoomChatMessageAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(SendRoomChatMessageResponse response, Connection connection)
        {

            if (response.Result != Result.Success)
            {
                _eventAggregator.PublishOnUIThread(new MessageQueue("Chat", "An error occurred in attempt to send message"));
                new ExceptionLogger($"ClientSendRoomChatMessageAction response: {response.Result}");
            }

            _eventAggregator.PublishOnUIThread(
                new RoomChatMessageStatePublish(response.Result));
        }
    }
}