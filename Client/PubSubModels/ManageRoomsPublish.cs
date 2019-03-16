using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.CreateRoom.Container;

namespace SharpDj.PubSubModels
{
    public class ManageRoomsPublish : IManageRoomsPublish
    {
        public ICollection<RoomModel> RoomModelsList { get; set; }

        public ManageRoomsPublish(ICollection<RoomModel> roomModelsList)
        {
            RoomModelsList = roomModelsList;
        }
    }

    public interface IManageRoomsPublish
    {
        ICollection<RoomModel> RoomModelsList { get; set; }
    }
}
