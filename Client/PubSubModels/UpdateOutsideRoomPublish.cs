using SharpDj.Common.DTO;

namespace SharpDj.PubSubModels
{
    public class UpdateOutsideRoomPublish : IUpdateOutsideRoomPublish
    {
        public PreviewRoomDTO RoomOutsideModel { get; set; }

        public UpdateOutsideRoomPublish(PreviewRoomDTO roomOutsideModel)
        {
            RoomOutsideModel = roomOutsideModel;
        }
    }

    public interface IUpdateOutsideRoomPublish
    {
        PreviewRoomDTO RoomOutsideModel { get; set; }  
    }
}
