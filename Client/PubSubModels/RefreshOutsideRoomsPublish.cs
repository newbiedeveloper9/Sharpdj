using SCPackets.Models;
using System.Collections.Generic;

namespace SharpDj.PubSubModels
{
    class RefreshOutsideRoomsPublish : IRefreshOutsideRoomsPublish
    {
        public IEnumerable<RoomOutsideModel> Rooms { get; }


        public RefreshOutsideRoomsPublish(IEnumerable<RoomOutsideModel> rooms)
        {
            Rooms = rooms;
        }
    }

    public interface IRefreshOutsideRoomsPublish
    {
        IEnumerable<RoomOutsideModel> Rooms { get; }
    }
}
