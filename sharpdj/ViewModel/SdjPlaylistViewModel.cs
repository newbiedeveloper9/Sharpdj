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
                var tracks = new ObservableCollection<PlaylistTrackModel>();
                for (int j = 0; j < i; j++)
                {
                    tracks.Add(new PlaylistTrackModel(main) { AuthorName = "Crisey " + i + j, SongDuration = "3:20", SongName = "Monstercat jakis tam" });
                    tracks.Add(new PlaylistTrackModel(main) { AuthorName = "Zonk " + j + i + 2, SongDuration = "2:13", SongName = "Tylko chińskie xD" });
                }
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Chińska playlista", Tracks = tracks });
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Zonkowate cos", Tracks = tracks });
            }
            PlaylistCollection[0].IsActive = true;

        }

        #endregion .ctor

        #region Properties

        private ObservableCollection<PlaylistTrackModel> _trackCollection;

        public ObservableCollection<PlaylistTrackModel> TrackCollection
        {
            get => _trackCollection;
            set
            {
                if (_trackCollection == value) return;
                _trackCollection = value;
                OnPropertyChanged("TrackCollection");
            }
        }
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


        #region ActivatePlaylistCommand
        private RelayCommand _activatePlaylistCommand;
        public RelayCommand ActivatePlaylistCommand
        {
            get
            {
                return _activatePlaylistCommand
                       ?? (_activatePlaylistCommand = new RelayCommand(ActivatePlaylistCommandExecute, ActivatePlaylistCommandCanExecute));
            }
        }

        public bool ActivatePlaylistCommandCanExecute()
        {
            return true;
        }

        public void ActivatePlaylistCommandExecute()
        {
            var firstOrDefault = PlaylistCollection.FirstOrDefault(x => x.IsSelected);
            if (firstOrDefault != null)
                firstOrDefault.IsActive = true;
        }
        #endregion

        #endregion Commands
    }
}
