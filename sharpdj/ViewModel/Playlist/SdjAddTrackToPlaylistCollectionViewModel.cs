using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Playlist;
using SharpDj.ViewModel.Helpers;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjAddTrackToPlaylistCollectionViewModel : BaseViewModel
    {
        #region .ctor

        public SdjAddTrackToPlaylistCollectionViewModel(SdjMainViewModel main)
        {
            PlaylistCollection = new ObservableCollection<PlaylistToAddTrack>();
            SdjMainViewModel = main;
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, closeForm, "Add Track to Playlist");
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, closeForm);
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
                OnPropertyChanged("SdjMainViewModel");
            }
        }

        private SdjTitleBarForUserControlsViewModel _sdjTitleBarForUserControlsViewModel;
        public SdjTitleBarForUserControlsViewModel SdjTitleBarForUserControlsViewModel
        {
            get => _sdjTitleBarForUserControlsViewModel;
            set
            {
                if (_sdjTitleBarForUserControlsViewModel == value) return;
                _sdjTitleBarForUserControlsViewModel = value;
                OnPropertyChanged("SdjTitleBarForUserControlsViewModel");
            }
        }

        private SdjBackgroundForFormsViewModel _sdjBackgroundForFormsViewModel;
        public SdjBackgroundForFormsViewModel SdjBackgroundForFormsViewModel
        {
            get => _sdjBackgroundForFormsViewModel;
            set
            {
                if (_sdjBackgroundForFormsViewModel == value) return;
                _sdjBackgroundForFormsViewModel = value;
                OnPropertyChanged("SdjBackgroundForFormsViewModel");
            }
        }

        private ObservableCollection<PlaylistToAddTrack> _playlistCollection;
        public ObservableCollection<PlaylistToAddTrack> PlaylistCollection
        {
            get => _playlistCollection;
            set
            {
                if (_playlistCollection == value) return;
                _playlistCollection = value;
                OnPropertyChanged("PlaylistCollection");
            }
        }

        private PlaylistTrackModel _trackToAdd;
        public PlaylistTrackModel TrackToAdd
        {
            get => _trackToAdd;
            set
            {
                if (_trackToAdd == value) return;
                _trackToAdd = value;
                OnPropertyChanged("TrackToAdd");
            }
        }

        #endregion Properties

        #region Methods

        private void closeForm()
        {
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Collapsed;
        }

        #endregion Methods

        #region Commands

        #endregion Commands
    }
}
