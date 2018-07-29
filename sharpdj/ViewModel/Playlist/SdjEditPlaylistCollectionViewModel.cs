using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SdjEditPlaylistCollectionViewModel : BaseViewModel
    {
        #region .ctor

        public SdjEditPlaylistCollectionViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, closeForm, "Rename playlist");
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
        #endregion Properties

        #region Methods

        private void closeForm()
        {
            PlaylistName = string.Empty;
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Collapsed;
        }

        #endregion Methods

        #region Commands

        #region RenamePlaylistCommand
        private RelayCommand _renamePlaylistCommand;
        public RelayCommand RenamePlaylistCommand
        {
            get
            {
                return _renamePlaylistCommand
                       ?? (_renamePlaylistCommand = new RelayCommand(RenamePlaylistCommandExecute, RenamePlaylistCommandCanExecute));
            }
        }

        public bool RenamePlaylistCommandCanExecute()
        {
            return true;
        }

        public void RenamePlaylistCommandExecute()
        {
            var firstOrDefault = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsSelected);
            if (firstOrDefault != null)
                firstOrDefault
                    .PlaylistName = PlaylistName;
            closeForm();
        }
        #endregion

        #endregion Commands

    }
}
