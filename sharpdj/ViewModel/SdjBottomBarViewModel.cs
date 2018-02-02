using System;
using SharpDj.Enums;

namespace SharpDj.ViewModel
{
    public class SdjBottomBarViewModel : BaseViewModel
    {
        #region .ctor

        public SdjBottomBarViewModel(SdjMainViewModel main)
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

        #region BottomBar

        private int _bottomBarNumberOfPeopleInRoom;
        public int BottomBarNumberOfPeopleInRoom
        {
            get => _bottomBarNumberOfPeopleInRoom;
            set
            {
                if (_bottomBarNumberOfPeopleInRoom == value) return;
                _bottomBarNumberOfPeopleInRoom = value;
                OnPropertyChanged("BottomBarNumberOfPeopleInRoom");
            }
        }

        private int _bottomBarNumberOfAdministrationInRoom;
        public int BottomBarNumberOfAdministrationInRoom
        {
            get => _bottomBarNumberOfAdministrationInRoom;
            set
            {
                if (_bottomBarNumberOfAdministrationInRoom == value) return;
                _bottomBarNumberOfAdministrationInRoom = value;
                OnPropertyChanged("BottomBarNumberOfAdministrationInRoom");
            }
        }

        private int _bottomBarSizeOfPlaylistInRoom;
        public int BottomBarSizeOfPlaylistInRoom
        {
            get => _bottomBarSizeOfPlaylistInRoom;
            set
            {
                if (_bottomBarSizeOfPlaylistInRoom == value) return;
                _bottomBarSizeOfPlaylistInRoom = value;
                OnPropertyChanged("BottomBarSizeOfPlaylistInRoom");
            }
        }

        private int _bottomBarMaxSizeOfPlaylistInRoom;
        public int BottomBarMaxSizeOfPlaylistInRoom
        {
            get => _bottomBarMaxSizeOfPlaylistInRoom;
            set
            {
                if (_bottomBarMaxSizeOfPlaylistInRoom == value) return;
                _bottomBarMaxSizeOfPlaylistInRoom = value;
                OnPropertyChanged("BottomBarMaxSizeOfPlaylistInRoom");
            }
        }

        private string _bottomBarTitleOfActuallySong;
        public string BottomBarTitleOfActuallySong
        {
            get => _bottomBarTitleOfActuallySong;
            set
            {
                if (_bottomBarTitleOfActuallySong == value) return;
                _bottomBarTitleOfActuallySong = value;
                OnPropertyChanged("BottomBarTitleOfActuallySong");
            }
        }

        #endregion

        #endregion Properties

        #region Methods



        #endregion Methods

        #region Commands

        #region AvatarOnClick
        private RelayCommand _avatarOnClickCommand;
        public RelayCommand AvatarOnClick
        {
            get
            {
                return _avatarOnClickCommand
                       ?? (_avatarOnClickCommand = new RelayCommand(AvatarOnClickExecute, AvatarOnClickCanExecute));
            }
        }

        public bool AvatarOnClickCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility != LeftBar.Visible)
                return true;
            return false;
        }

        public void AvatarOnClickExecute()
        {
            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Visible;
        }
        #endregion

        #region BottomBarAddToPlaylistCommand
        private RelayCommand _bottomBarAddToPlaylistCommand;
        public RelayCommand BottomBarAddToPlaylistCommand
        {
            get
            {
                return _bottomBarAddToPlaylistCommand
                       ?? (_bottomBarAddToPlaylistCommand = new RelayCommand(BottomBarAddToPlaylistCommandExecute, BottomBarAddToPlaylistCommandCanExecute));
            }
        }

        public bool BottomBarAddToPlaylistCommandCanExecute()
        {
            return true;
        }

        public void BottomBarAddToPlaylistCommandExecute()
        {
            if (SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility == Playlist.Collapsed)
            {
                SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility = Playlist.Visible;
            }
            else
            {
                SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility = Playlist.Collapsed;
            }
            Console.WriteLine(SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility);
        }
        #endregion

        #endregion Commands

    }
}