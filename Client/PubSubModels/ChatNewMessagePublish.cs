using SCPackets.Packets.RoomNewMessageRequest;

namespace SharpDj.PubSubModels
{
    class ChatNewMessagePublish : IChatNewMessagePublish
    {
        public RoomNewMessageRequest Message { get; set; }

        public ChatNewMessagePublish(RoomNewMessageRequest message)
        {
            Message = message;
        }
    }

    public interface IChatNewMessagePublish
    {
        RoomNewMessageRequest Message { get; set; }
    }
}
