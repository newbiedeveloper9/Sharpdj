using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Models;

namespace SharpDj.PubSubModels
{
    public class NewRoomCreatedPublish : INewRoomCreatedPublish
    {
        public NewRoomCreatedPublish(RoomOutsideModel roomOutsideModel)
        {
            RoomOutsideModel = roomOutsideModel;
        }

        public RoomOutsideModel RoomOutsideModel { get; }
    }

    public interface INewRoomCreatedPublish
    {
        RoomOutsideModel RoomOutsideModel { get;}
    }
}
