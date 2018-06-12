using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;
using Playlist = SharpDj.Enums.Playlist;
using YoutubeExplode;

namespace SharpDj.ViewModel
{
    public class SdjPlaylistViewModel : BaseViewModel
    {
        #region .ctor

        public SdjPlaylistViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            PlaylistCollection = new ObservableCollection<PlaylistModel>();

            for (int i = 0; i < 2; i++)
            {
                //  var tracks = new ObservableCollection<PlaylistTrackModel>();
                /*   for (int j = 0; j < i; j++)
                   {
                       tracks.Add(new PlaylistTrackModel(main) { AuthorName = "Łrisey " + i + j, SongDuration = "3:20", SongName = "Monstercat jakis tam" });
                       tracks.Add(new PlaylistTrackModel(main) { AuthorName = "ąonk " + j + i + 2, SongDuration = "2:13", SongName = "Tylko chińskie xD" });
                   }*/
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Chińska playlista" });
                PlaylistCollection.Add(new PlaylistModel(main) { PlaylistName = "Zonkowate cos" });
            }
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
        private ObservableCollection<PlaylistModel> _playlistCollection;

        public ObservableCollection<PlaylistModel> PlaylistCollection
        {
            get => _playlistCollection;
            set
            {
                if (_playlistCollection == value) return;
                _playlistCollection = value;
                OnPropertyChanged("PlaylistCollection");
            }
        }



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

        private PlaylistMode _playlistMode = PlaylistMode.Playlist;
        public PlaylistMode PlaylistMode
        {
            get => _playlistMode;
            set
            {
                if (_playlistMode == value) return;
                _playlistMode = value;
                OnPropertyChanged("PlaylistMode");
            }
        }


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }


        #endregion Properties

        #region Methods

        public void SetLastPlaylistSelected()
        {
            foreach (var playlistModel in PlaylistCollection)
            {
                playlistModel.IsSelected = false;
            }
        }

        #endregion Methods

        #region Commands

        #region ActivatePlaylistCommand
        private RelayCommand _activatePlaylistCommand;
        public RelayCommand ActivatePlaylistCommand => _activatePlaylistCommand
                                                       ?? (_activatePlaylistCommand = new RelayCommand(ActivatePlaylistCommandExecute, ActivatePlaylistCommandCanExecute));

        public bool ActivatePlaylistCommandCanExecute()
        {
            return true;
        }

        public void ActivatePlaylistCommandExecute()
        {
            var setActive = PlaylistCollection.FirstOrDefault(x => x.IsSelected);
            if (setActive != null)
            {
                foreach (var playlist in PlaylistCollection)
                {
                    playlist.IsActive = false;
                }
                setActive.IsActive = true;
            }
        }
        #endregion

        #region PlaylistAddModel
        private RelayCommand _playlistAddModel;
        public RelayCommand PlaylistAddModel => _playlistAddModel
                                                ?? (_playlistAddModel = new RelayCommand(PlaylistAddModelExecute, PlaylistAddModelCanExecute));

        public bool PlaylistAddModelCanExecute()
        {
            return true;
        }

        public void PlaylistAddModelExecute()
        {
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Add;

        }
        #endregion

        #region PlaylistDeleteModel
        private RelayCommand _playlistDeleteModel;
        public RelayCommand PlaylistDeleteModel => _playlistDeleteModel
                                                   ?? (_playlistDeleteModel = new RelayCommand(PlaylistDeleteModelExecute, PlaylistDeleteModelCanExecute));

        public bool PlaylistDeleteModelCanExecute()
        {
            return true;
        }

        public void PlaylistDeleteModelExecute()
        {
            if (PlaylistCollection.FirstOrDefault(x => x.IsSelected) != null)
                SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Remove;
        }
        #endregion

        #region PlaylistEditModel
        private RelayCommand _playlistEditModel;
        public RelayCommand PlaylistEditModel => _playlistEditModel
                                                 ?? (_playlistEditModel = new RelayCommand(PlaylistEditModelExecute, PlaylistEditModelCanExecute));

        public bool PlaylistEditModelCanExecute()
        {
            return true;
        }

        public void PlaylistEditModelExecute()
        {
            if (PlaylistCollection.FirstOrDefault(x => x.IsSelected) != null)
            {
                SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Rename;
                SdjMainViewModel.SdjEditPlaylistCollectionViewModel.PlaylistName = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsSelected)?.PlaylistName;
            }
        }
        #endregion

        #region AddTrackToPlaylistCommand
        private RelayCommand _addTrackToPlaylistCommand;
        public RelayCommand AddTrackToPlaylistCommand => _addTrackToPlaylistCommand
                                                         ?? (_addTrackToPlaylistCommand = new RelayCommand(AddTrackToPlaylistCommandExecute, AddTrackToPlaylistCommandCanExecute));

        public bool AddTrackToPlaylistCommandCanExecute()
        {
            return true;
        }

        public void AddTrackToPlaylistCommandExecute()
        {
            SdjMainViewModel.SdjAddTrackToPlaylistCollectionViewModel.PlaylistCollection = new ObservableCollection<PlaylistToAddTrack>();
            var track = TrackCollection.FirstOrDefault(x => x.SongTimeVisibility == Visibility.Collapsed);
            foreach (var playlistModel in PlaylistCollection)
            {
                SdjMainViewModel.SdjAddTrackToPlaylistCollectionViewModel.PlaylistCollection.Add(new PlaylistToAddTrack(SdjMainViewModel, playlistModel.PlaylistName, playlistModel.TracksInPlaylist, playlistModel, track));
            }
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.AddTrack;
        }
        #endregion

        #region OnEnterSearchVideoCommand
        private RelayCommand _onEnterSearchVideoCommand;
        public RelayCommand OnEnterSearchVideoCommand => _onEnterSearchVideoCommand
                                                         ?? (_onEnterSearchVideoCommand = new RelayCommand(OnEnterSearchVideoCommandExecute, OnEnterSearchVideoCommandCanExecute));

        public bool OnEnterSearchVideoCommandCanExecute()
        {
            return true;
        }

        public async void OnEnterSearchVideoCommandExecute()
        {
            PlaylistMode = PlaylistMode.Search;
            await QueryVideos();
        }

        private async Task QueryVideos()
        {
            var client = new YoutubeClient();
            var query = await client.SearchVideosAsync(SearchText, 1);
            TrackCollection = new ObservableCollection<PlaylistTrackModel>();

            foreach (var item in query)
            {
                TrackCollection.Add(new PlaylistTrackModel(SdjMainViewModel)
                {
                    SongName = item.Title,
                    AuthorName = item.Author,
                    SongDuration = item.Duration.ToString(),
                    SongId = item.Id  
                });
            }
        }
        #endregion

        #endregion Commands

    }
}
