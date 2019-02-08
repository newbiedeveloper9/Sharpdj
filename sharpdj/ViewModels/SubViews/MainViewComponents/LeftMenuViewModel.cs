using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.ViewModels.SubViews.MainViewComponents.LeftMenuComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class LeftMenuViewModel : PropertyChangedBase
    {
        public LeftMenuViewModel()
        {
            PlaylistCollection = new BindableCollection<PlaylistModel>()
            {
                new PlaylistModel("Test"),
                new PlaylistModel("Nowa playlista Criseya"),
                new PlaylistModel("XD"),
            };
        
        }
     
        public BindableCollection<PlaylistModel> PlaylistCollection { get; private set; }
        public RoomRectangleViewModel RoomRectangleViewModel { get; set; } = new RoomRectangleViewModel();
        public ObservedRoomViewModel ObservedRoomViewModel { get; set; } = new ObservedRoomViewModel();
    }
}
