using SharpDj.Common.DTO;
using System.Collections.Generic;

namespace SharpDj.PubSubModels
{
    class RefreshOutsideRoomsPublish : IRefreshOutsideRoomsPublish
    {
        public IEnumerable<PreviewRoomDTO> Rooms { get; }

        public RefreshOutsideRoomsPublish(IEnumerable<PreviewRoomDTO> rooms)
        {
            Rooms = rooms;
        }
    }

    public interface IRefreshOutsideRoomsPublish
    {
        IEnumerable<PreviewRoomDTO> Rooms { get; }
    }
}
