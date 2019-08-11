using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Models;
using SharpDj.Enums;
using SharpDj.Logic.Helpers;
using SharpDj.Models;

namespace SharpDj.PubSubModels
{
    public class RoomInfoForOpen : IRoomInfoForOpen
    {
        public List<UserClientModel> UserList { get; set; }
        public RoomOutsideModel OutsideModel { get; set; }

        public RoomInfoForOpen(RoomOutsideModel room, List<UserClientModel> userList)
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
        List<UserClientModel> UserList { get; set; }
        RoomOutsideModel OutsideModel { get; set; }
    }
}
