using SharpDj.Common.DTO;

namespace SharpDj.PubSubModels
{
    public class NewRoomCreatedPublish : INewRoomCreatedPublish
    {
        public NewRoomCreatedPublish(PreviewRoomDTO roomOutsideModel)
        {
            RoomOutsideModel = roomOutsideModel;
        }

        public PreviewRoomDTO RoomOutsideModel { get; }
    }

    public interface INewRoomCreatedPublish
    {
        PreviewRoomDTO RoomOutsideModel { get;}
    }
}
