﻿using SharpDj.Core;
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

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite == value) return;
                _isFavorite = value;
                OnPropertyChanged("IsFavorite");
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


        private int _roomId;
        public int RoomId
        {
            get => _roomId;
            set
            {
                if (_roomId == value) return;
                _roomId = value;
                OnPropertyChanged("RoomId");
            }
        }

        private bool _isMouseOver;
        public bool IsMouseOver
        {
            get => _isMouseOver;
            set
            {
                if (_isMouseOver == value) return;
                _isMouseOver = value;
                OnPropertyChanged("IsMouseOver");
            }
        }

        #endregion Properties

        #region Commands

        #region MainOnRoomClickCommand
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
        #endregion

        #region SetRoomAsFavoriteCommand
        private RelayCommand _setRoomAsFavoriteCommand;
        public RelayCommand SetRoomAsFavoriteCommand
        {
            get
            {
                return _setRoomAsFavoriteCommand
                       ?? (_setRoomAsFavoriteCommand = new RelayCommand(SetRoomAsFavoriteCommandExecute, SetRoomAsFavoriteCommandCanExecute));
            }
        }

        public bool SetRoomAsFavoriteCommandCanExecute()
        {
            return true;
        }

        public void SetRoomAsFavoriteCommandExecute()
        {
            IsFavorite = !IsFavorite;
        }
        #endregion

        #region OnMouseEnterCommand
        private RelayCommand _onMouseEnterCommand;
        public RelayCommand OnMouseEnterCommand
        {
            get
            {
                return _onMouseEnterCommand
                       ?? (_onMouseEnterCommand = new RelayCommand(OnMouseEnterCommandExecute, OnMouseEnterCommandCanExecute));
            }
        }

        public bool OnMouseEnterCommandCanExecute()
        {
            return true;
        }

        public void OnMouseEnterCommandExecute()
        {
            IsMouseOver = true;
        }
        #endregion

        #region OnMouseLeaveCommand
        private RelayCommand _onMouseLeaveCommand;
        public RelayCommand OnMouseLeaveCommand
        {
            get
            {
                return _onMouseLeaveCommand
                       ?? (_onMouseLeaveCommand = new RelayCommand(OnMouseLeaveCommandExecute, OnMouseLeaveCommandCanExecute));
            }
        }

        public bool OnMouseLeaveCommandCanExecute()
        {
            return true;
        }

        public void OnMouseLeaveCommandExecute()
        {
            IsMouseOver = false;
        }
        #endregion

        #endregion Commands
    }
}
