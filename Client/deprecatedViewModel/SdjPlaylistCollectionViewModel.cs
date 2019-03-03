using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjPlaylistCollectionViewModel : BaseViewModel
    {

        #region .ctor

        public SdjPlaylistCollectionViewModel(SdjMainViewModel main)
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

        private PlaylistCollectionView _playlistCollectionViewCollectionVisibility = PlaylistCollectionView.Collapsed;
        public PlaylistCollectionView PlaylistCollectionViewCollectionVisibility
        {
            get => _playlistCollectionViewCollectionVisibility;
            set
            {
                if (_playlistCollectionViewCollectionVisibility == value) return;
                _playlistCollectionViewCollectionVisibility = value;
                OnPropertyChanged("PlaylistCollectionViewCollectionVisibility");
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
            SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Add(new PlaylistModel(SdjMainViewModel) { PlaylistName = PlaylistName });

           // SdjMainViewModel.SdjPlaylistViewModel.SetLastPlaylistSelected();
            SdjMainViewModel.SdjPlaylistViewModel
                .PlaylistCollection[SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Count-1].IsSelected = true;

            CloseAddPlaylistCommandExecute();
        }
        #endregion

        #region CloseAddPlaylistCommand
        private RelayCommand _closeAddPlaylistCommand;
        public RelayCommand CloseAddPlaylistCommand
        {
            get
            {
                return _closeAddPlaylistCommand
                       ?? (_closeAddPlaylistCommand = new RelayCommand(CloseAddPlaylistCommandExecute, CloseAddPlaylistCommandCanExecute));
            }
        }

        public bool CloseAddPlaylistCommandCanExecute()
        {
            return true;
        }

        public void CloseAddPlaylistCommandExecute()
        {
            PlaylistName = string.Empty;
            PlaylistCollectionViewCollectionVisibility = PlaylistCollectionView.Collapsed;
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
            CloseAddPlaylistCommandExecute();
        }
        #endregion

        #endregion Commands

    }
}
