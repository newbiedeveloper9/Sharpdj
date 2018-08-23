using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Menu;
using SharpDj.Enums.Playlist;
using SharpDj.Logic.Client;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using SharpDj.Models.Helpers;
using SharpDj.View.UserControls;
using SharpDj.ViewModel.Model;
using SharpDj.ViewModel.Unique;

namespace SharpDj.ViewModel
{
    public class SdjMainViewModel : BaseViewModel
    {
        public readonly Client Client;
        private readonly ClientLogic _clientLogic;
        #region .ctor

        public SdjMainViewModel()
        {            
            SdjRoomViewModel = new SdjRoomViewModel(this);
            SdjBottomBarViewModel = new SdjBottomBarViewModel(this);
            SdjLeftBarViewModel = new SdjLeftBarViewModel(this);
            SdjPlaylistViewModel = new SdjPlaylistViewModel(this);
            SdjStateButtonViewModel = new SdjStateButtonViewModel(this);
            SdjAddPlaylistCollectionViewModel = new SdjAddPlaylistCollectionViewModel(this);
            SdjEditPlaylistCollectionViewModel = new SdjEditPlaylistCollectionViewModel(this);
            SdjRemovePlaylistCollectionViewModel = new SdjRemovePlaylistCollectionViewModel(this);
            SdjAddTrackToPlaylistCollectionViewModel = new SdjAddTrackToPlaylistCollectionViewModel(this);
            SdjRenameTrackNameInPlaylistViewModel = new SdjRenameTrackNameInPlaylistViewModel(this);
            SdjLoginViewModel = new SdjLoginViewModel(this);
            SdjRegisterViewModel = new SdjRegisterViewModel(this);
            SdjUserProfileViewModel = new SdjUserProfileViewModel(this);
            SdjFeedbackViewModel = new SdjFeedbackViewModel(this);

            MainViewVisibility = MainView.Login;
            Profile = new UserClient();

            for (int i = 0; i < 7; i++)
            {
                FavoriteCollection.Add(new FavoriteRoomsModel(this){});
            }

            Client = new Client();
            Client.Start(this);
            _clientLogic = new ClientLogic(this);
        }

        #endregion .ctor

        #region Properties

        private UserClient _profile;
        public UserClient Profile
        {
            get => _profile;
            set
            {
                if (_profile == value) return;
                _profile = value;
                OnPropertyChanged("Profile");

                SdjLeftBarViewModel.Username = value.Username;
            }
        }

        #region ObservableCollection

        private ObservableCollection<RoomSquareModel> _roomCollection = new ObservableCollection<RoomSquareModel>();
        public ObservableCollection<RoomSquareModel> RoomCollection
        {
            get => _roomCollection;
            set
            {
                if (_roomCollection == value) return;
                _roomCollection = value;
                OnPropertyChanged("RoomCollection");
            }
        }

        public ObservableCollection<FavoriteRoomsModel> FavoriteCollection { get; set; } = new ObservableCollection<FavoriteRoomsModel>();
        #endregion

        #region Visibility
        private MainView _mainViewVisibility = MainView.Main;
        public MainView MainViewVisibility
        {
            get => _mainViewVisibility;
            set
            {
                if (_mainViewVisibility == value) return;
                if (_mainViewVisibility == MainView.Login)
                {
                    SdjLoginViewModel.ErrorNotify = string.Empty;
                    SdjLoginViewModel.Login = string.Empty;
                    SdjLoginViewModel.Password = null;
                    SdjLoginViewModel.RememberMe = false;
                }
                else if (_mainViewVisibility == MainView.Register)
                {
                    SdjRegisterViewModel.ErrorNotify = string.Empty;
                    SdjRegisterViewModel.Login = string.Empty;
                    SdjRegisterViewModel.Nickname = string.Empty;
                    SdjRegisterViewModel.Email = string.Empty;
                    SdjRegisterViewModel.Password = null;
                }
                _mainViewVisibility = value;
              
                OnPropertyChanged("MainViewVisibility");

                if (value == MainView.Room && SdjRoomViewModel.RoomId != SdjBottomBarViewModel.BottomBarRoomId)
                {
                    SdjBottomBarViewModel.BottomBarRoomName = string.Empty;
                    SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = 0;
                    SdjRoomViewModel.RoomName = string.Empty;
                    SdjRoomViewModel.HostName = string.Empty;
                    SdjBottomBarViewModel.BottomBarSizeOfPlaylistInRoom = 0;
                    SdjBottomBarViewModel.BottomBarMaxSizeOfPlaylistInRoom = 30;
                    SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = 0;
                    SdjBottomBarViewModel.BottomBarNumberOfAdministrationInRoom =
                        0;
                    SdjRoomViewModel.SongsQueue = 0;
                    SdjRoomViewModel.SongTitle = string.Empty;
                    SdjBottomBarViewModel.BottomBarTitleOfActuallySong = string.Empty;
                }
                
            }
        }

        private PlaylistState _playlistStateCollectionVisibility = PlaylistState.Collapsed;
        public PlaylistState PlaylistStateCollectionVisibility
        {
            get => _playlistStateCollectionVisibility;
            set
            {
                if (_playlistStateCollectionVisibility == value) return;
                _playlistStateCollectionVisibility = value;
                OnPropertyChanged("PlaylistStateCollectionVisibility");
            }
        }

        #endregion

        #region ViewModels

        private SdjBottomBarViewModel _sdjBottomBarViewModel;
        public SdjBottomBarViewModel SdjBottomBarViewModel
        {
            get => _sdjBottomBarViewModel;
            set
            {
                if (_sdjBottomBarViewModel == value) return;
                _sdjBottomBarViewModel = value;
                OnPropertyChanged("SdjBottomBarViewModel");
            }
        }

        private SdjLeftBarViewModel _sdjLeftBarViewModel;
        public SdjLeftBarViewModel SdjLeftBarViewModel
        {
            get => _sdjLeftBarViewModel;
            set
            {
                if (_sdjLeftBarViewModel == value) return;
                _sdjLeftBarViewModel = value;
                OnPropertyChanged("SdjLeftBarViewModel");
            }
        }

        private SdjPlaylistViewModel _sdjPlaylistViewModel;
        public SdjPlaylistViewModel SdjPlaylistViewModel
        {
            get => _sdjPlaylistViewModel;
            set
            {
                if (_sdjPlaylistViewModel == value) return;
                _sdjPlaylistViewModel = value;
                OnPropertyChanged("SdjPlaylistViewModel");
            }
        }

        private SdjRoomViewModel _sdjRoomViewModel;
        public SdjRoomViewModel SdjRoomViewModel
        {
            get => _sdjRoomViewModel;
            set
            {
                if (_sdjRoomViewModel == value) return;
                _sdjRoomViewModel = value;
                OnPropertyChanged("SdjRoomViewModel");
            }
        }

        private SdjStateButtonViewModel _sdjStateButtonViewModel;
        public SdjStateButtonViewModel SdjStateButtonViewModel
        {
            get => _sdjStateButtonViewModel;
            set
            {
                if (_sdjStateButtonViewModel == value) return;
                _sdjStateButtonViewModel = value;
                OnPropertyChanged("SdjStateButtonViewModel");
            }
        }


        private SdjAddPlaylistCollectionViewModel _sdjAddPlaylistCollectionViewModelModel;
        public SdjAddPlaylistCollectionViewModel SdjAddPlaylistCollectionViewModel
        {
            get => _sdjAddPlaylistCollectionViewModelModel;
            set
            {
                if (_sdjAddPlaylistCollectionViewModelModel == value) return;
                _sdjAddPlaylistCollectionViewModelModel = value;
                OnPropertyChanged("SdjAddPlaylistCollectionViewModel");
            }
        }

        private SdjEditPlaylistCollectionViewModel _sdjEditPlaylistCollectionViewModelModel;
        public SdjEditPlaylistCollectionViewModel SdjEditPlaylistCollectionViewModel
        {
            get => _sdjEditPlaylistCollectionViewModelModel;
            set
            {
                if (_sdjEditPlaylistCollectionViewModelModel == value) return;
                _sdjEditPlaylistCollectionViewModelModel = value;
                OnPropertyChanged("SdjEditPlaylistCollectionViewModel");
            }
        }

        private SdjRemovePlaylistCollectionViewModel _sdjRemovePlaylistCollectionViewModel;
        public SdjRemovePlaylistCollectionViewModel SdjRemovePlaylistCollectionViewModel
        {
            get => _sdjRemovePlaylistCollectionViewModel;
            set
            {
                if (_sdjRemovePlaylistCollectionViewModel == value) return;
                _sdjRemovePlaylistCollectionViewModel = value;
                OnPropertyChanged("SdjRemovePlaylistCollectionViewModel");
            }
        }

        private SdjLoginViewModel _sdjLoginViewModel;
        public SdjLoginViewModel SdjLoginViewModel
        {
            get => _sdjLoginViewModel;
            set
            {
                if (_sdjLoginViewModel == value) return;
                _sdjLoginViewModel = value;
                OnPropertyChanged("SdjLoginViewModel");
            }
        }

        private SdjRegisterViewModel _sdjRegisterViewModel;
        public SdjRegisterViewModel SdjRegisterViewModel
        {
            get => _sdjRegisterViewModel;
            set
            {
                if (_sdjRegisterViewModel == value) return;
                _sdjRegisterViewModel = value;
                OnPropertyChanged("SdjRegisterViewModel");
            }
        }

        private SdjUserProfileViewModel _sdjUserProfileViewModel;
        public SdjUserProfileViewModel SdjUserProfileViewModel
        {
            get => _sdjUserProfileViewModel;
            set
            {
                if (_sdjUserProfileViewModel == value) return;
                _sdjUserProfileViewModel = value;
                OnPropertyChanged("SdjUserProfileViewModel");
            }
        }


        private SdjFeedbackViewModel _sdjFeedbackViewModel;
        public SdjFeedbackViewModel SdjFeedbackViewModel
        {
            get => _sdjFeedbackViewModel;
            set
            {
                if (_sdjFeedbackViewModel == value) return;
                _sdjFeedbackViewModel = value;
                OnPropertyChanged("SdjFeedbackViewModel");
            }
        }


        private SdjAddTrackToPlaylistCollectionViewModel _sdjAddTrackToPlaylistCollectionViewModel;
        public SdjAddTrackToPlaylistCollectionViewModel SdjAddTrackToPlaylistCollectionViewModel
        {
            get => _sdjAddTrackToPlaylistCollectionViewModel;
            set
            {
                if (_sdjAddTrackToPlaylistCollectionViewModel == value) return;
                _sdjAddTrackToPlaylistCollectionViewModel = value;
                OnPropertyChanged("SdjAddTrackToPlaylistCollectionViewModel");
            }
        }

        private SdjRenameTrackNameInPlaylistViewModel _sdjRenameTrackNameInPlaylistViewModel;
        public SdjRenameTrackNameInPlaylistViewModel SdjRenameTrackNameInPlaylistViewModel
        {
            get => _sdjRenameTrackNameInPlaylistViewModel;
            set
            {
                if (_sdjRenameTrackNameInPlaylistViewModel == value) return;
                _sdjRenameTrackNameInPlaylistViewModel = value;
                OnPropertyChanged("SdjRenameTrackNameInPlaylistViewModel");
            }
        }

        #endregion ViewModels

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Commands

        #region RefreshCommand
        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                       ?? (_refreshCommand = new RelayCommand(RefreshCommandExecute, RefreshCommandCanExecute));
            }
        }

        public bool RefreshCommandCanExecute()
        {
            return true;
        }

        public void RefreshCommandExecute()
        {
            _clientLogic.RefreshInfo();
        }
        #endregion


        #endregion Commands

    }
}
