using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Models;

namespace SharpDj.PubSubModels
{
    public class UpdateOutsideRoomPublish : IUpdateOutsideRoomPublish
    {
        public RoomOutsideModel RoomOutsideModel { get; set; }

        public UpdateOutsideRoomPublish(RoomOutsideModel roomOutsideModel)
        {
            RoomOutsideModel = roomOutsideModel;
        }
    }

    public interface IUpdateOutsideRoomPublish
    {
        RoomOutsideModel RoomOutsideModel { get; set; }  
    }
}
