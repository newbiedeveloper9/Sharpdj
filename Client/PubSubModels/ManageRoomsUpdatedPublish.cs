
using SCPackets.CreateRoom.Container;

namespace SharpDj.PubSubModels
{
    public class ManageRoomsUpdatedPublish : IManageEditedRoomPublish
    {
        public RoomModel Room { get; set; }

        public ManageRoomsUpdatedPublish(RoomModel room)
        {
            Room = room;
        }
    }

    public interface IManageEditedRoomPublish
    {
        RoomModel Room { get; set; }
    }
}
