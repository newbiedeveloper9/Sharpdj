using SharpDj.Common.DTO;

namespace SharpDj.PubSubModels
{
    public class ManageRoomsUpdatedPublish : IManageEditedRoomPublish
    {
        public RoomDetailsDTO Room { get; set; }

        public ManageRoomsUpdatedPublish(RoomDetailsDTO room)
        {
            Room = room;
        }
    }

    public interface IManageEditedRoomPublish
    {
        RoomDetailsDTO Room { get; set; }
    }
}
