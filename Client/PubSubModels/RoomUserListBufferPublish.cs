using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Buffers;

namespace SharpDj.PubSubModels
{
    public class RoomUserListBufferPublish : IRoomUserListBufferPublish
    {
        public RoomUserListBufferRequest UsersBuffer { get; set; }

        public RoomUserListBufferPublish(RoomUserListBufferRequest usersBuffer)
        {
            UsersBuffer = usersBuffer;
        }
    }

    public interface IRoomUserListBufferPublish
    {
        RoomUserListBufferRequest UsersBuffer { get; set; }
    }
}
