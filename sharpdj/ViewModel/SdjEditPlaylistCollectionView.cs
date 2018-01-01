using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjEditPlaylistCollectionView : BaseViewModel
    {

        #region .ctor

        public SdjEditPlaylistCollectionView(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
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

        private PlaylistCollectionView _EditPlaylistCollectionVisibility = PlaylistCollectionView.Collapsed;
        public PlaylistCollectionView EditPlaylistCollectionVisibility
        {
            get => _EditPlaylistCollectionVisibility;
            set
            {
                if (_EditPlaylistCollectionVisibility == value) return;
                _EditPlaylistCollectionVisibility = value;
                OnPropertyChanged("EditPlaylistCollectionVisibility");
            }
        }

        #endregion Properties

        #region Methods


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
           /* SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.(new PlaylistModel(SdjMainViewModel) { PlaylistName = PlaylistName });

            SdjMainViewModel.SdjPlaylistViewModel.SetLastPlaylistSelected();
            SdjMainViewModel.SdjPlaylistViewModel
                .PlaylistCollection[SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Count - 1].IsSelected = true;

            CloseEditPlaylistCommandExecute();*/
        }
        #endregion

        #region CloseEditPlaylistCommand
        private RelayCommand _closeEditPlaylistCommand;
        public RelayCommand CloseEditPlaylistCommand
        {
            get
            {
                return _closeEditPlaylistCommand
                       ?? (_closeEditPlaylistCommand = new RelayCommand(CloseEditPlaylistCommandExecute, CloseEditPlaylistCommandCanExecute));
            }
        }

        public bool CloseEditPlaylistCommandCanExecute()
        {
            return true;
        }

        public void CloseEditPlaylistCommandExecute()
        {
            PlaylistName = string.Empty;
            EditPlaylistCollectionVisibility = PlaylistCollectionView.Collapsed;
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
            CloseEditPlaylistCommandExecute();
        }
        #endregion

        #endregion Commands

    }
}
