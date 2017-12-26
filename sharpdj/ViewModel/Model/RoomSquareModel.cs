using SharpDj.Enums;

namespace SharpDj.ViewModel.Model
{
    public class RoomSquareModel : BaseViewModel
    {
        #region .ctor
        public RoomSquareModel(SdjMainViewModel main)
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
            }
        }


        private int _peopleInRoom;
        public int PeopleInRoom
        {
            get => _peopleInRoom;
            set
            {
                if (_peopleInRoom == value) return;
                _peopleInRoom = value;
                OnPropertyChanged("PeopleInRoom");
            }
        }

        private int _adminsInRoom;
        public int AdminsInRoom
        {
            get => _adminsInRoom;
            set
            {
                if (_adminsInRoom == value) return;
                _adminsInRoom = value;
                OnPropertyChanged("AdminsInRoom");
            }
        }

        private string _roomName;
        public string RoomName
        {
            get => _roomName;
            set
            {
                if (_roomName == value) return;
                _roomName = value;
                OnPropertyChanged("RoomName");
            }
        }

        private string _roomDescription;
        public string RoomDescription
        {
            get => _roomDescription;
            set
            {
                if (_roomDescription == value) return;
                _roomDescription = value;
                OnPropertyChanged("RoomDescription");
            }
        }

        private string _hostName;
        public string HostName
        {
            get => _hostName;
            set
            {
                if (_hostName == value) return;
                _hostName = value;
                OnPropertyChanged("HostName");
            }
        }

        #endregion Properties


        #region Commands
        private RelayCommand _mainOnRoomClickCommand;
        public RelayCommand MainOnRoomClickCommand
        {
            get
            {
                return _mainOnRoomClickCommand
                       ?? (_mainOnRoomClickCommand = new RelayCommand(MainOnRoomClickCommandExecute, MainOnRoomClickCommandCanExecute));
            }
        }

        public bool MainOnRoomClickCommandCanExecute()
        {
            return true;
        }

        public void MainOnRoomClickCommandExecute()
        {
            SdjMainViewModel.MainViewVisibility = MainView.Room;
        }
        #endregion Commands
    }
}
