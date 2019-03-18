using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.CreateRoom.Container;

namespace SharpDj.PubSubModels
{
    public class CreatedRoomPublish : ICreatedRoomPublish
    {
        public RoomModel Room { get; set; }

        public CreatedRoomPublish(RoomModel room)
        {
            Room = room;
        }
    }

    public interface ICreatedRoomPublish
    {
        RoomModel Room { get; set; }
    }
}
