using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Playlist;

namespace SharpDj.ViewModel.Model
{
    public class PlaylistTrackModel :BaseViewModel
    {
        #region .ctor
        public PlaylistTrackModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
        }

        #endregion .ctor

        #region Properties

        [JsonIgnore]
        private SdjMainViewModel _sdjMainViewModel;
        [JsonIgnore]
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
            }
        }

        private string _songId;
        public string SongId
        {
            get => _songId;
            set
            {
                if (_songId == value) return;
                _songId = value;
                OnPropertyChanged("SongId");
            }
        }

        private string _songName;
        public string SongName
        {
            get => _songName;
            set
            {
                if (_songName == value) return;
                _songName = value;
                OnPropertyChanged("SongName");
            }
        }

        private string _authorName;
        public string AuthorName
        {
            get => _authorName;
            set
            {
                if (_authorName == value) return;
                _authorName = value;
                OnPropertyChanged("AuthorName");
            }
        }

        private string _songDuration;
        public string SongDuration
        {
            get => _songDuration;
            set
            {
                if (_songDuration == value) return;
                _songDuration = value;
                OnPropertyChanged("SongDuration");
            }
        }

        [JsonIgnore]
        private Brush _backgroundBrush = new SolidColorBrush(Color.FromArgb(247, 0, 56, 77));
        [JsonIgnore]
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

        private Visibility _songOptionsVisibility = Visibility.Collapsed;
        public Visibility SongOptionsVisibility
        {
            get => _songOptionsVisibility;
            set
            {
                if (_songOptionsVisibility == value) return;
                _songOptionsVisibility = value;
                OnPropertyChanged("SongOptionsVisibility");

                SongTimeVisibility = value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility _songTimeVisibility = Visibility.Visible;
        public Visibility SongTimeVisibility
        {
            get => _songTimeVisibility;
            set
            {
                if (_songTimeVisibility == value) return;
                _songTimeVisibility = value;
                OnPropertyChanged("SongTimeVisibility");
            }
        }
        #endregion Properties

        #region Methods

        public bool EqualsSequel(PlaylistTrackModel tmp)
        {
            return SongId.Equals(tmp.SongId) && SongName.Equals(tmp.SongName) && AuthorName.Equals(tmp.AuthorName) && SongDuration.Equals(tmp.SongDuration);
        }

        #endregion Methods

        #region Commands

        #region SongOptionsSetVisibleCommand
        [JsonIgnore]
        private RelayCommand _songOptionsSetVisibleCommand;
        [JsonIgnore]
        public RelayCommand SongOptionsSetVisibleCommand
        {
            get
            {
                return _songOptionsSetVisibleCommand
                       ?? (_songOptionsSetVisibleCommand = new RelayCommand(SongOptionsSetVisibleCommandExecute, SongOptionsSetVisibleCommandCanExecute));
            }
        }

        public bool SongOptionsSetVisibleCommandCanExecute()
        {
            return true;
        }

        public void SongOptionsSetVisibleCommandExecute()
        {
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 1, 110, 151));
            SongOptionsVisibility = Visibility.Visible;

        }
        #endregion

        #region SongOptionsSetHiddenCommand
        [JsonIgnore]
        private RelayCommand _songOptionsSetHiddenCommand;
        [JsonIgnore]
        public RelayCommand SongOptionsSetHiddenCommand
        {
            get
            {
                return _songOptionsSetHiddenCommand
                       ?? (_songOptionsSetHiddenCommand = new RelayCommand(SongOptionsSetHiddenCommandExecute, SongOptionsSetHiddenCommandCanExecute));
            }
        }

        public bool SongOptionsSetHiddenCommandCanExecute()
        {
            return true;
        }

        public void SongOptionsSetHiddenCommandExecute()
        {
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(247, 0, 56, 77));
            SongOptionsVisibility = Visibility.Collapsed;
        }

        #endregion

        #region TrackMoveDownCommand
        [JsonIgnore]
        private RelayCommand _trackMoveDownCommand;
        [JsonIgnore]
        public RelayCommand TrackMoveDownCommand
        {
            get
            {
                return _trackMoveDownCommand
                       ?? (_trackMoveDownCommand = new RelayCommand(TrackMoveDownCommandExecute, TrackMoveDownCommandCanExecute));
            }
        }

        public bool TrackMoveDownCommandCanExecute()
        {
            return true;
        }

        public void TrackMoveDownCommandExecute()
        {
            var index = SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.IndexOf(this);
            if (index >= SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.Count-1) return;
            SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.Move(index, index + 1);

           //HasChanges = true;
        }
        #endregion

        #region TrackMoveUpCommand
        [JsonIgnore]
        private RelayCommand _trackMoveUpCommand;
        [JsonIgnore]
        public RelayCommand TrackMoveUpCommand
        {
            get
            {
                return _trackMoveUpCommand
                       ?? (_trackMoveUpCommand = new RelayCommand(TrackMoveUpCommandExecute, TrackMoveUpCommandCanExecute));
            }
        }

        public bool TrackMoveUpCommandCanExecute()
        {
            return true;
        }

        public void TrackMoveUpCommandExecute()
        {
            var index = SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.IndexOf(this);
            if (index <= 0) return;
            SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.Move(index, index - 1);

            //                HasChanges = true;
        }
        #endregion

        #region TrackDeleteCommand
        [JsonIgnore]
        private RelayCommand _trackDeleteCommand;
        [JsonIgnore]
        public RelayCommand TrackDeleteCommand
        {
            get
            {
                return _trackDeleteCommand
                       ?? (_trackDeleteCommand = new RelayCommand(TrackDeleteCommandExecute, TrackDeleteCommandCanExecute));
            }
        }

        public bool TrackDeleteCommandCanExecute()
        {
            return true;
        }

        public void TrackDeleteCommandExecute()
        {
            SdjMainViewModel.SdjPlaylistViewModel.TrackCollection.Remove(this);
            var firstOrDefault = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsSelected);
            if (firstOrDefault != null)
                firstOrDefault
                    .TracksInPlaylist--;
        }
        #endregion

        #region TrackRenameCommand
        [JsonIgnore]
        private RelayCommand _trackRenameCommand;
        [JsonIgnore]
        public RelayCommand TrackRenameCommand
        {
            get
            {
                return _trackRenameCommand
                       ?? (_trackRenameCommand = new RelayCommand(TrackRenameCommandExecute, TrackRenameCommandCanExecute));
            }
        }

        public bool TrackRenameCommandCanExecute()
        {
            return true;
        }

        public void TrackRenameCommandExecute()
        {
            SdjMainViewModel.SdjRenameTrackNameInPlaylistViewModel.Track = this;
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.RenameTrack;
        }
        #endregion

        #endregion Commands
    }
}
