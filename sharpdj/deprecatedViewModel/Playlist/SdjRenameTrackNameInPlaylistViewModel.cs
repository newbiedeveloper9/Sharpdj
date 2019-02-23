using System;
using System.Collections.Generic;
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
    public class SdjRenameTrackNameInPlaylistViewModel : BaseViewModel
    {
        #region .ctor

        public SdjRenameTrackNameInPlaylistViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, closeForm, "Rename track");
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, closeForm);
        }

        #endregion .ctor

        #region Properties

        private PlaylistTrackModel _track;
        public PlaylistTrackModel Track
        {
            get => _track;
            set
            {
                AuthorName = value.AuthorName;
                TrackName = value.SongName;
                if (_track == value) return;
                _track = value;
                OnPropertyChanged("TrackModel");
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

        private string _trackName;
        public string TrackName
        {
            get => _trackName;
            set
            {
                if (_trackName == value) return;
                _trackName = value;
                OnPropertyChanged("TrackName");
            }
        }

        #endregion Properties

        #region Methods

        private void closeForm()
        {
            TrackName = string.Empty;
            AuthorName = string.Empty;
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Collapsed;
        }

        #endregion Methods

        #region Commands

        #region RenameTrackCommand
        private RelayCommand _renameTrackCommand;
        public RelayCommand RenameTrackCommand
        {
            get
            {
                return _renameTrackCommand
                       ?? (_renameTrackCommand = new RelayCommand(RenameTrackCommandExecute, RenameTrackCommandCanExecute));
            }
        }

        public bool RenameTrackCommandCanExecute()
        {
            return true;
        }

        public void RenameTrackCommandExecute()
        {
            Track.AuthorName = AuthorName;
            Track.SongName = TrackName;

            closeForm();
        }
        #endregion RenameTrackCommand

        #endregion
    }
}
