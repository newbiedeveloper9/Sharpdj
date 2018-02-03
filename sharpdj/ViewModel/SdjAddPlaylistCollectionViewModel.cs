using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjAddPlaylistCollectionViewModel : BaseViewModel
    {

        #region .ctor

        public SdjAddPlaylistCollectionViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControls = new SdjTitleBarForUserControls(main, closeForm);
            SdjTitleBarForUserControls.FormName = "Add Playlist";
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
            SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Add(new PlaylistModel(SdjMainViewModel) { PlaylistName = PlaylistName });

            SdjMainViewModel.SdjPlaylistViewModel.SetLastPlaylistSelected();
            SdjMainViewModel.SdjPlaylistViewModel
                .PlaylistCollection[SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Count-1].IsSelected = true;

            closeForm();
        }
        #endregion

        #region MouseUpAwayFromAddPlaylistCommand
        private RelayCommand _mouseUpAwayFromAddPlaylistCommand;
        public RelayCommand MouseUpAwayFromAddPlaylistCommand
        {
            get
            {
                return _mouseUpAwayFromAddPlaylistCommand
                       ?? (_mouseUpAwayFromAddPlaylistCommand = new RelayCommand(MouseUpAwayFromAddPlaylistCommandExecute, MouseUpAwayFromAddPlaylistCommandCanExecute));
            }
        }

        public bool MouseUpAwayFromAddPlaylistCommandCanExecute()
        {
            return true;
        }

        public void MouseUpAwayFromAddPlaylistCommandExecute()
        {
          closeForm();
        }
        #endregion

        #endregion Commands

    }
}
