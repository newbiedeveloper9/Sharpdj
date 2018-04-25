using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SharpDj.Core;
using SharpDj.Enums;

namespace SharpDj.ViewModel.Model
{
    public class PlaylistModel : BaseViewModel
    {
        #region .ctor
        public PlaylistModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            Tracks = new ObservableCollection<PlaylistTrackModel>();
        }
        #endregion .ctor

        #region Properties
        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
            }
        }



        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get => _backgroundBrush;
            set
            {
                if (_backgroundBrush == value) return;
                _backgroundBrush = value;
                OnPropertyChanged("BackgroundBrush");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged("IsSelected");

                if (value)
                {
                    BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 56, 77));
                    SdjMainViewModel.SdjPlaylistViewModel.TrackCollection = Tracks;
                    SdjMainViewModel.SdjPlaylistViewModel.PlaylistMode = PlaylistMode.Playlist;
                }
                else
                {
                    BackgroundBrush = Brushes.Transparent;
                }
            }
        }


        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;

                    _isActive = value;
                
                OnPropertyChanged("IsActive");
             
            }
        }


        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                if (_playlistName == value) return;
                _playlistName = value;
                OnPropertyChanged("PlaylistName");
            }
        }

        private int _tracksInPlaylist;
        public int TracksInPlaylist
        {
            get => _tracksInPlaylist;
            set
            {
                if (_tracksInPlaylist == value) return;
                _tracksInPlaylist = value;
                OnPropertyChanged("TracksInPlaylist");
            }
        }

        private ObservableCollection<PlaylistTrackModel> _tracks;

        public ObservableCollection<PlaylistTrackModel> Tracks
        {
            get => _tracks;
            set
            {
                if (_tracks == value) return;
                _tracks = value;
                OnPropertyChanged("Tracks");
                TracksInPlaylist = _tracks.Count;
            }
        }

        #endregion Properties

        #region Methods

        public void AddTrack(PlaylistTrackModel track)
        {
            Tracks.Add(track);
            TracksInPlaylist++;
        }

        #endregion Methods

        #region Commands


        #region SelectPlaylistCommand

        private RelayCommand _selectPlaylistCommand;
        public RelayCommand SelectPlaylistCommand
        {
            get
            {
                return _selectPlaylistCommand
                       ?? (_selectPlaylistCommand = new RelayCommand(SelectPlaylistCommandExecute, SelectPlaylistCommandCanExecute));
            }
        }

        public bool SelectPlaylistCommandCanExecute()
        {
            return true;
        }

        public void SelectPlaylistCommandExecute()
        {

            if (IsSelected)
            {
                SdjMainViewModel.SdjPlaylistViewModel.ActivatePlaylistCommandExecute();
            }
            else
            {
                foreach (var playlist in SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection)
                {
                    playlist.IsSelected = false;
                }

                IsSelected = true;
            }

        }
        #endregion


        #region PlaylistOnEnterCommand
        private RelayCommand _playlistOnEnterCommand;
        public RelayCommand PlaylistOnEnterCommand
        {
            get
            {
                return _playlistOnEnterCommand
                       ?? (_playlistOnEnterCommand = new RelayCommand(PlaylistOnEnterCommandExecute, PlaylistOnEnterCommandCanExecute));
            }
        }

        public bool PlaylistOnEnterCommandCanExecute()
        {
            return true;
        }

        public void PlaylistOnEnterCommandExecute()
        {
            if (!IsSelected)
            {
                BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 174, 239));
            }
        }
        #endregion

        #region PlaylistOnLeaveCommand
        private RelayCommand _playlistOnLeaveCommand;

        public RelayCommand PlaylistOnLeaveCommand
        {
            get
            {
                return _playlistOnLeaveCommand
                       ?? (_playlistOnLeaveCommand = new RelayCommand(PlaylistOnLeaveCommandExecute, PlaylistOnLeaveCommandCanExecute));
            }
        }

        public bool PlaylistOnLeaveCommandCanExecute()
        {
            return true;
        }

        public void PlaylistOnLeaveCommandExecute()
        {
            if (!IsSelected)
            {
                BackgroundBrush = Brushes.Transparent;
            }
        }
        #endregion


        #endregion Commands


    }
}
