using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.LeftMenuComponents
{
    public class RoomRectangleViewModel : Screen
    {
        public BindableCollection<RoomModel> ActiveRoomCollection { get; private set; }

        public RoomRectangleViewModel()
        {
          
        }

        protected override void OnViewLoaded(object view)
        {
            var dicPic = @"..\..\..\..\Images\3.jpg";

            ActiveRoomCollection = new BindableCollection<RoomModel>()
            {
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 1", Status = RoomModel.Activity.Active},
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 2", Status = RoomModel.Activity.Sleep},
                new RoomModel(){ImageSource = dicPic, Name = "Testowy pokój 3", Status = RoomModel.Activity.InActive},
            };

            base.OnViewLoaded(view);
        }
    }
}
