using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjEditPlaylistCollectionViewModel : BaseViewModel
    {
        #region .ctor

        public SdjEditPlaylistCollectionViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControls = new SdjTitleBarForUserControls(main, closeForm);
            SdjTitleBarForUserControls.FormName = "Rename Playlist";
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

        private SdjTitleBarForUserControls _sdjTitleBarForUserControls;
        public SdjTitleBarForUserControls SdjTitleBarForUserControls
        {
            get => _sdjTitleBarForUserControls;
            set
            {
                if (_sdjTitleBarForUserControls == value) return;
                _sdjTitleBarForUserControls = value;
                OnPropertyChanged("SdjTitleBarForUserControls");
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

        #region MouseUpAwayFromEditPlaylistCommand
        private RelayCommand _mouseUpAwayFromEditPlaylistCommand;
        public RelayCommand MouseUpAwayFromEditPlaylistCommand
        {
            get
            {
                return _mouseUpAwayFromEditPlaylistCommand
                       ?? (_mouseUpAwayFromEditPlaylistCommand = new RelayCommand(MouseUpAwayFromEditPlaylistCommandExecute, MouseUpAwayFromEditPlaylistCommandCanExecute));
            }
        }

        public bool MouseUpAwayFromEditPlaylistCommandCanExecute()
        {
            return true;
        }

        public void MouseUpAwayFromEditPlaylistCommandExecute()
        {
            closeForm();
        }
        #endregion

        #endregion Commands

    }
}
