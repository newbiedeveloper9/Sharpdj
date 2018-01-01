using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SharpDj.Enums;
using SharpDj.Models;
using SharpDj.View.UserControls;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjMainViewModel : BaseViewModel
    {
        private Client _client;


        #region .ctor

        public SdjMainViewModel()
        {
            SdjRoomViewModel = new SdjRoomViewModel(this);
            SdjBottomBarViewModel = new SdjBottomBarViewModel(this);
            SdjLeftBarViewModel = new SdjLeftBarViewModel(this);
            SdjPlaylistViewModel = new SdjPlaylistViewModel(this);
            SdjStateButtonViewModel = new SdjStateButtonViewModel(this);
            SdjAddPlaylistCollectionViewModel = new SdjAddPlaylistCollectionView(this);

            for (int i = 0; i < 5; i++)
            {
                RoomCollection.Add(new RoomSquareModel(this) { AdminsInRoom = 10, RoomName = "nazwa pokoju", PeopleInRoom = 2137, RoomDescription = "description", HostName = "Yhvsak" });
                RoomCollection.Add(new RoomSquareModel(this) { AdminsInRoom = 10, RoomName = "jakas nazwa XDDDD", PeopleInRoom = 99, RoomDescription = "dddsadasdawd", HostName = "Jeff Diggins" });
                FavoriteCollection.Add(new FavoriteRoomsModel(this) { HostName = "Crisey", PeopleInRoom = 12, RoomName = "Jakis Pokój", SongsInQueue = 9 });
                FavoriteCollection.Add(new FavoriteRoomsModel(this) { HostName = "Zonk", PeopleInRoom = 91, RoomName = "Monstercat", SongsInQueue = 16 });
            }
        }

        #endregion .ctor

        #region Properties

        #region ObservableCollection
        public ObservableCollection<RoomSquareModel> RoomCollection { get; set; } = new ObservableCollection<RoomSquareModel>();
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
                _mainViewVisibility = value;
                OnPropertyChanged("MainViewVisibility");
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


        private SdjAddPlaylistCollectionView _sdjAddPlaylistCollectionViewModel;
        public SdjAddPlaylistCollectionView SdjAddPlaylistCollectionViewModel
        {
            get => _sdjAddPlaylistCollectionViewModel;
            set
            {
                if (_sdjAddPlaylistCollectionViewModel == value) return;
                _sdjAddPlaylistCollectionViewModel = value;
                OnPropertyChanged("SdjAddPlaylistCollectionViewModel");
            }
        }

        #endregion ViewModels

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Commands

        #region MainView

        #region Main


        #region MainOnRoomClickCommand

        #endregion

        #endregion Main

        #endregion

        #region LoginView

        #region LoginAsGuestCommand
        private RelayCommand _loginAsGuestCommand;
        public RelayCommand LoginAsGuestCommand
        {
            get
            {
                return _loginAsGuestCommand
                       ?? (_loginAsGuestCommand = new RelayCommand(LoginAsGuestCommandExecute, LoginAsGuestCommandCanExecute));
            }
        }

        public bool LoginAsGuestCommandCanExecute()
        {

            return true;

        }

        public void LoginAsGuestCommandExecute()
        {
            _client = new Client();
            _client.Start();

            MainViewVisibility = MainView.Main;
        }
        #endregion

        #endregion LoginView

        #endregion Commands

    }
}
