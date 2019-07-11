using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.RoomChatNewMessageClient;
using SCPackets.SendRoomChatMessage;

namespace SharpDj.PubSubModels
{
    class ChatNewMessagePublish : IChatNewMessagePublish
    {
        public RoomChatNewMessageRequest Message { get; set; }

        public ChatNewMessagePublish(RoomChatNewMessageRequest message)
        {
            Message = message;
        }
    }

    public interface IChatNewMessagePublish
    {
        RoomChatNewMessageRequest Message { get; set; }
    }
}
