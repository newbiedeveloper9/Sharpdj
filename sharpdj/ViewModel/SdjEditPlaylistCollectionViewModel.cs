using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.Enums;
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
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, closeForm);
            SdjTitleBarForUserControlsViewModel.FormName = "Rename Playlist";
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

        #region CreatePlaylistCommand
        private RelayCommand _createPlaylistCommand;
        public RelayCommand CreatePlaylistCommand
        {
            get
            {
                return _createPlaylistCommand
                       ?? (_createPlaylistCommand = new RelayCommand(CreatePlaylistCommandExecute, CreatePlaylistCommandCanExecute));
            }
        }

        public bool CreatePlaylistCommandCanExecute()
        {
            return true;
        }

        public void CreatePlaylistCommandExecute()
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
