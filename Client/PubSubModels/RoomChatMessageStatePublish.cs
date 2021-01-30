using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Packets.CreateRoomMessage;

namespace SharpDj.PubSubModels
{
    class RoomChatMessageStatePublish : IRoomChatMessageStatePublish
    {
        public CreateRoomMessageResult Result { get; set; }

        public RoomChatMessageStatePublish(CreateRoomMessageResult result)
        {
            Result = result;
        }
    }

    public interface IRoomChatMessageStatePublish
    {
       CreateRoomMessageResult Result { get; set; }
    }
}
