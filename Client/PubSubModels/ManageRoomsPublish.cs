using System.Collections.Generic;
using SharpDj.Common.DTO;

namespace SharpDj.PubSubModels
{
    public class ManageRoomsPublish : IManageRoomsPublish
    {
        public ICollection<RoomDetailsDTO> RoomModelsList { get; set; }

        public ManageRoomsPublish(ICollection<RoomDetailsDTO> roomModelsList)
        {
            RoomModelsList = roomModelsList;
        }
    }

    public interface IManageRoomsPublish
    {
        ICollection<RoomDetailsDTO> RoomModelsList { get; set; }
    }
}
