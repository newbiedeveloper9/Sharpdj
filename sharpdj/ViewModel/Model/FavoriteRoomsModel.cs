using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.Enums;

namespace SharpDj.ViewModel.Model
{
   public class FavoriteRoomsModel : BaseViewModel
    {
        #region .ctor
        public FavoriteRoomsModel(SdjMainViewModel main)
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

        private int _songsInQueue;
        public int SongsInQueue
        {
            get => _songsInQueue;
            set
            {
                if (_songsInQueue == value) return;
                _songsInQueue = value;
                OnPropertyChanged("SongsInQueue");
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

        #region Methods


        #endregion Methods

        #region Commands

        #region EnterFavoriteRoomCommand
        private RelayCommand _enterFavoriteRoomCommand;
        public RelayCommand EnterFavoriteRoomCommand
        {
            get
            {
                return _enterFavoriteRoomCommand
                       ?? (_enterFavoriteRoomCommand = new RelayCommand(EnterFavoriteRoomCommandExecute, EnterFavoriteRoomCommandCanExecute));
            }
        }

        public bool EnterFavoriteRoomCommandCanExecute()
        {
            return true;
        }

        public void EnterFavoriteRoomCommandExecute()
        {
            SdjMainViewModel.MainViewVisibility = MainView.Room;
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
