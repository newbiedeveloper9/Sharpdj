using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.LeftMenuComponents
{
    public class RoomRectangleViewModel : PropertyChangedBase
    {
        public BindableCollection<RoomModel> ActiveRoomCollection { get; private set; }

        public RoomRectangleViewModel()
        {
            var dicPic = @"..\..\..\..\Images\3.jpg";

            ActiveRoomCollection = new BindableCollection<RoomModel>()
            {
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 1", Status = RoomModel.Activity.Active},
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 2", Status = RoomModel.Activity.Before},
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 3", Status = RoomModel.Activity.Nothing},
            };
        }
    }
}
