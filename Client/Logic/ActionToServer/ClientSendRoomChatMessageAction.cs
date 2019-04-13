using Caliburn.Micro;
using Network;
using SCPackets.Disconnect;
using SCPackets.SendRoomChatMessage;

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

        }
    }
}
