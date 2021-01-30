using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Models;

namespace SharpDj.PubSubModels
{
    public class PullPostsRoomPublish : IPullPostsRoomPublish
    {
        public List<ChatMessage> Posts { get; set; }

        public PullPostsRoomPublish(List<ChatMessage> posts)
        {
            Posts = posts;
        }
    }

    public interface IPullPostsRoomPublish
    {
        List<ChatMessage> Posts { get; set; }
    }
}
