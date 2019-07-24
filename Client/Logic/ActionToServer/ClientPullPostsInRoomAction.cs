using System;
using Caliburn.Micro;
using Network;
using SCPackets.PullPostsInRoom;
using SharpDj.PubSubModels;
using Result = SCPackets.PullPostsInRoom.Result;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientPullPostsInRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientPullPostsInRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(PullPostsInRoomResponse response, Connection connection)
        {
            if (response.Result == Result.Success)
            {
                _eventAggregator.PublishOnUIThread(
                    new PullPostsRoomPublish(response.Posts));
            }
            else if (response.Result == Result.EOF)
            {
                _eventAggregator.PublishOnUIThread(
                    new EofPostsRoomPublish());
            }
        }
    }
}