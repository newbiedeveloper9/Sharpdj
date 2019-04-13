using System.Linq;
using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.Models;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.LeftMenuComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class LeftMenuViewModel : PropertyChangedBase,
        IHandle<IPlaylistCollectionChanged>, IHandle<INewPlaylistCreated>
    {
        private readonly IEventAggregator _eventAggregator;
        public LeftMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

/*            PlaylistCollection = new BindableCollection<PlaylistModel>()
            {
                new PlaylistModel(){Name = "test"},
                new PlaylistModel(){Name = "Testowa długa playlista xd", IsActive = true},
                new PlaylistModel(){Name = "xd"},
            };*/
        }

        public LeftMenuViewModel()
        {
            
        }

        public BindableCollection<PlaylistModel> PlaylistCollection { get; private set; }
        public RoomRectangleViewModel RoomRectangleViewModel { get; private set; } = new RoomRectangleViewModel();
        public ObservedRoomViewModel ObservedRoomViewModel { get; private set; } = new ObservedRoomViewModel();

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

        public void Handle(IPlaylistCollectionChanged message)
        {
            PlaylistCollection = message.PlaylistCollection;
        }

        public void Handle(INewPlaylistCreated message)
        {
            PlaylistCollection.Add(message.Model);
        }
    }
}
