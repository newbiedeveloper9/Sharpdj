using SharpDj.Common.DTO;

namespace SharpDj.PubSubModels
{
    public class CreatedRoomPublish : ICreatedRoomPublish
    {
        public RoomDetailsDTO Room { get; set; }

        public CreatedRoomPublish(RoomDetailsDTO room)
        {
            Room = room;
        }
    }

    public interface ICreatedRoomPublish
    {
        RoomDetailsDTO Room { get; set; }
    }
}
