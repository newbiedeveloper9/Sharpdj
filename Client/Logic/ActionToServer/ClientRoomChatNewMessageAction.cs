using Caliburn.Micro;
using Network;
using SCPackets.RoomChatNewMessageClient;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    class ClientRoomChatNewMessageAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientRoomChatNewMessageAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(RoomChatNewMessageRequest request, Connection connection)
        {
            if (request != null)
                _eventAggregator.PublishOnUIThread(
                    new ChatNewMessagePublish(request));
        }
    }
}
