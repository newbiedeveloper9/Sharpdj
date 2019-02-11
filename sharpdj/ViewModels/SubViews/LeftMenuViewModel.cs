using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.ViewModels.SubViews.LeftMenuComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class LeftMenuViewModel
    {
        public LeftMenuViewModel()
        {
            PlaylistCollection = new BindableCollection<PlaylistModel>()
            {
                new PlaylistModel("Test"),
                new PlaylistModel("Nowa playlista Criseya", true),
                new PlaylistModel("XD"),
            };
        }

        public BindableCollection<PlaylistModel> PlaylistCollection { get; private set; }
        public RoomRectangleViewModel RoomRectangleViewModel { get; set; } = new RoomRectangleViewModel();
        public ObservedRoomViewModel ObservedRoomViewModel { get; set; } = new ObservedRoomViewModel();
    }
}
