using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.SendRoomChatMessage;

namespace SharpDj.PubSubModels
{
    class RoomChatMessageStatePublish : IRoomChatMessageStatePublish
    {
        public Result Result { get; set; }

        public RoomChatMessageStatePublish(Result result)
        {
            Result = result;
        }
    }

    public interface IRoomChatMessageStatePublish
    {
       SCPackets.SendRoomChatMessage.Result Result { get; set; }
    }
}
