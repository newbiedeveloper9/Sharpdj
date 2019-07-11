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
        public RoomInfoForOpen(RoomOutsideModel room)
        {
            if (room != null)
                UserInfoSingleton.Instance.ActiveRoom = RoomModel.ToClientModel(room);
        }
    }

    public interface IRoomInfoForOpen
    {
    }
}
