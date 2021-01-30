using System.Collections.Generic;
using SCPackets.Models;
using SharpDj.Common.DTO;
using SharpDj.Logic.Helpers;
using SharpDj.Models;

namespace SharpDj.PubSubModels
{
    public class RoomInfoForOpen : IRoomInfoForOpen
    {
        public List<UserClient> UserList { get; set; }
        public PreviewRoomDTO OutsideModel { get; set; }

        public RoomInfoForOpen(PreviewRoomDTO room, List<UserClient> userList)
        {
            if (room != null)
                UserInfoSingleton.Instance.ActiveRoom = RoomModel.ToClientModel(room);

            OutsideModel = room;
            UserList = userList;
        }

        public RoomInfoForOpen()
        {
            
        }
    }

    public interface IRoomInfoForOpen
    {
        List<UserClient> UserList { get; set; }
        PreviewRoomDTO OutsideModel { get; set; }
    }
}
