using System.Linq;
using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.Models;
using SharpDj.ViewModels.SubViews.LeftMenuComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class LeftMenuViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public LeftMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            PlaylistCollection = new BindableCollection<PlaylistModel>()
            {
                new PlaylistModel("Test"),
                new PlaylistModel("Nowa playlista Criseya", true),
                new PlaylistModel("XD"),
            };
        }

        public LeftMenuViewModel()
        {
            
        }

        public BindableCollection<PlaylistModel> PlaylistCollection { get; private set; }
        public RoomRectangleViewModel RoomRectangleViewModel { get; set; } = new RoomRectangleViewModel();
        public ObservedRoomViewModel ObservedRoomViewModel { get; set; } = new ObservedRoomViewModel();

        public void ActivatePlaylist(PlaylistModel model)
        {
            if (PlaylistCollection != null)
                PlaylistCollection.FirstOrDefault(x => x.IsActive).IsActive = false;
            model.IsActive = true;
        }

        public void ShowPlaylist()
        {
            _eventAggregator.PublishOnUIThread(NavigateMainView.Playlist);
        }
    }
}
