using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Playlist;
using SharpDj.Logic.Helpers;
using SharpDj.Models.Helpers;
using SharpDj.Models.Helpers.Updater;
using SharpDj.ViewModel.Model;
using Playlist = SharpDj.Enums.Playlist.Playlist;
using YoutubeExplode;

namespace SharpDj.ViewModel
{
    public class SdjPlaylistViewModel : BaseViewModel
    {
        #region .ctor

        public SdjPlaylistViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;

            var src = File.ReadAllText(FilesPath.Instance.PlaylistConfig);
            PlaylistCollection = JsonConvert.DeserializeObject<ObservableCollection<PlaylistModel>>(src);
            PlaylistCollectionSavedState = JsonConvert.DeserializeObject<ObservableCollection<PlaylistModel>>(src);

            if (PlaylistCollection != null && PlaylistCollectionSavedState != null)
                for (var index = 0; index < PlaylistCollection.Count; index++)
                {
                    PlaylistCollection[index].SdjMainViewModel = SdjMainViewModel;
                    PlaylistCollectionSavedState[index].SdjMainViewModel = SdjMainViewModel;

                    for (var i = 0; i < PlaylistCollection[index].Tracks.Count; i++)
                    {
                        PlaylistCollection[index].Tracks[i].SdjMainViewModel = SdjMainViewModel;
                        PlaylistCollectionSavedState[index].Tracks[i].SdjMainViewModel = SdjMainViewModel;
                    }
                }
            else
            {
                PlaylistCollection = new ObservableCollection<PlaylistModel>();
                PlaylistCollectionSavedState = new ObservableCollection<PlaylistModel>();
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

        private ObservableCollection<PlaylistModel> _playlistCollectionSavedState;

        public ObservableCollection<PlaylistModel> PlaylistCollectionSavedState
        {
            get => _playlistCollectionSavedState;
            set
            {
                if (_playlistCollectionSavedState == value) return;
                _playlistCollectionSavedState = value;
                OnPropertyChanged("PlaylistCollectionSavedState");
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

                if (value == Playlist.Collapsed)
                {
                    string serialized = JsonConvert.SerializeObject(PlaylistCollection);
                    string serializedSavedState = JsonConvert.SerializeObject(PlaylistCollectionSavedState);

                    if (!serializedSavedState.Equals(serialized))
                    {
                        if (!Directory.Exists("config"))
                            Directory.CreateDirectory("config");

                        File.WriteAllText(FilesPath.Instance.PlaylistConfig, serialized);
                        PlaylistCollectionSavedState = Clone.ClonWithJson(PlaylistCollection);
                    }
                }
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

        public void RemoveSelectionFromPlaylists()
        {
            foreach (var playlistModel in PlaylistCollection)
            {
                playlistModel.IsSelected = false;
            }
        }

        private void SaveApply()
        {
        }

        #endregion Methods

        #region Commands

        #region ActivatePlaylistCommand

        private RelayCommand _activatePlaylistCommand;

        public RelayCommand ActivatePlaylistCommand => _activatePlaylistCommand
                                                       ?? (_activatePlaylistCommand =
                                                           new RelayCommand(ActivatePlaylistCommandExecute,
                                                               ActivatePlaylistCommandCanExecute));

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
                                                ?? (_playlistAddModel = new RelayCommand(PlaylistAddModelExecute,
                                                    PlaylistAddModelCanExecute));

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
                                                   ?? (_playlistDeleteModel =
                                                       new RelayCommand(PlaylistDeleteModelExecute,
                                                           PlaylistDeleteModelCanExecute));

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
                                                 ?? (_playlistEditModel = new RelayCommand(PlaylistEditModelExecute,
                                                     PlaylistEditModelCanExecute));

        public bool PlaylistEditModelCanExecute()
        {
            return true;
        }

        public void PlaylistEditModelExecute()
        {
            if (PlaylistCollection.FirstOrDefault(x => x.IsSelected) != null)
            {
                SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Rename;
                SdjMainViewModel.SdjEditPlaylistCollectionViewModel.PlaylistName = SdjMainViewModel.SdjPlaylistViewModel
                    .PlaylistCollection.FirstOrDefault(x => x.IsSelected)?.PlaylistName;
            }
        }

        #endregion

        #region AddTrackToPlaylistCommand

        private RelayCommand _addTrackToPlaylistCommand;

        public RelayCommand AddTrackToPlaylistCommand => _addTrackToPlaylistCommand
                                                         ?? (_addTrackToPlaylistCommand =
                                                             new RelayCommand(AddTrackToPlaylistCommandExecute,
                                                                 AddTrackToPlaylistCommandCanExecute));

        public bool AddTrackToPlaylistCommandCanExecute()
        {
            return true;
        }

        public void AddTrackToPlaylistCommandExecute()
        {
            SdjMainViewModel.SdjAddTrackToPlaylistCollectionViewModel.PlaylistCollection =
                new ObservableCollection<PlaylistToAddTrack>();
            var track = TrackCollection.FirstOrDefault(x => x.SongTimeVisibility == Visibility.Collapsed);
            foreach (var playlistModel in PlaylistCollection)
            {
                SdjMainViewModel.SdjAddTrackToPlaylistCollectionViewModel.PlaylistCollection.Add(
                    new PlaylistToAddTrack(SdjMainViewModel, playlistModel, track));
            }

            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.AddTrack;
        }

        #endregion

        #region OnEnterSearchVideoCommand

        private RelayCommand _onEnterSearchVideoCommand;

        public RelayCommand OnEnterSearchVideoCommand => _onEnterSearchVideoCommand
                                                         ?? (_onEnterSearchVideoCommand =
                                                             new RelayCommand(OnEnterSearchVideoCommandExecute,
                                                                 OnEnterSearchVideoCommandCanExecute));

        public bool OnEnterSearchVideoCommandCanExecute()
        {
            return true;
        }

        public async void OnEnterSearchVideoCommandExecute()
        {
            RemoveSelectionFromPlaylists();
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