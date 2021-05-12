using Caliburn.Micro;
using Network;
using SCPackets.Packets.RoomNewMessageRequest;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    class ClientRoomChatNewMessageAction : IAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientRoomChatNewMessageAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(RoomNewMessageRequest request, Connection connection)
        {
            if (request != null)
                _eventAggregator.PublishOnUIThread(
                    new ChatNewMessagePublish(request));
        }
    }
}
