using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjPlaylistViewModel : BaseViewModel
    {
        #region .ctor

        public SdjPlaylistViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            for (int i = 0; i < 10; i++)
            {
                TrackCollection.Add(new PlaylistTrackModel(main) { AuthorName = "Crisey", SongDuration = "3:20", SongName = "Monstercat jakis tam" });
                TrackCollection.Add(new PlaylistTrackModel(main) { AuthorName = "Zonk", SongDuration = "2:13,7", SongName = "Tylko chińskie bajeczki XD" });
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Chińska playlista", TracksInPlaylist = i });
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Zonkowate cos", TracksInPlaylist = 10+i });
            }
            PlaylistCollection[0].IsActive = true;

        }

        #endregion .ctor

        #region Properties
        public ObservableCollection<PlaylistTrackModel> TrackCollection { get; set; } = new ObservableCollection<PlaylistTrackModel>();
        public ObservableCollection<PlaylistModel> PlaylistCollection { get; set; } = new ObservableCollection<PlaylistModel>();


        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
                OnPropertyChanged("SdjMainViewModel");
            }
        }


        private Playlist _playlistVisibility = Playlist.Collapsed;
        public Playlist PlaylistVisibility
        {
            get => _playlistVisibility;
            set
            {
                if (_playlistVisibility == value) return;
                _playlistVisibility = value;
                OnPropertyChanged("PlaylistVisibility");
            }
        }

        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands


        #endregion Commands
    }
}
