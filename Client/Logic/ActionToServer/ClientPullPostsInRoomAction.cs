using System;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.PullRoomChat;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientPullPostsInRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientPullPostsInRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(PullRoomChatResponse response, Connection connection)
        {
            if (response.Result == PullRoomChatResult.Success)
            {
                _eventAggregator.PublishOnUIThread(
                    new PullPostsRoomPublish(response.Posts));
            }
            else if (response.Result == PullRoomChatResult.EOF)
            {
                _eventAggregator.PublishOnUIThread(
                    new EofPostsRoomPublish());
            }
        }
    }
}