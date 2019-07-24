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
        public List<RoomPostModel> Posts { get; set; }

        public PullPostsRoomPublish(List<RoomPostModel> posts)
        {
            Posts = posts;
        }
    }

    public interface IPullPostsRoomPublish
    {
        List<RoomPostModel> Posts { get; set; }
    }
}
