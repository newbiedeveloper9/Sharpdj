using System;
using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents
{
    public class PlaylistCreationViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                if (_playlistName == value) return;
                _playlistName = value;
                NotifyOfPropertyChange(() => PlaylistName);
            }
        }


        #region .ctor
        public PlaylistCreationViewModel()
        {

        }

        public PlaylistCreationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        #endregion .ctor


        public void CreatePlaylist()
        {
            if (PlaylistName.Length <= 0) return;

            _eventAggregator.PublishOnUIThread(
                new NewPlaylistCreated(new PlaylistModel() { Name = PlaylistName }));

            PlaylistName = string.Empty;
        }
    }
}
