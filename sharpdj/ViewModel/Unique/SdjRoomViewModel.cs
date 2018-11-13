using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using Communication.Client;
using Communication.Server.Logic;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Logic.Client;
using SharpDj.Logic.Helpers;
using SharpDj.View.UserControls;
using SharpDj.ViewModel.Model;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace SharpDj.ViewModel
{
    public class SdjRoomViewModel : BaseViewModel
    {
        public SdjVlcPlayer VlcPlayer { get; set; }

        #region .ctor

        public SdjRoomViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            RoomMessageCollection = new ObservableCollection<RoomMessageModel>();
            UserList = new MyUserList(main);

            VlcPlayer = new SdjVlcPlayer();
            MyVlcPlayer.Add(VlcPlayer);

            var libDirectory = new DirectoryInfo(Path.Combine(FilesPath.LocalPath,
                "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            VlcPlayer.VlcPlayer.SourceProvider.CreatePlayer(libDirectory);
        }

        #endregion .ctor

        #region Properties

        private MyUserList _userList;

        public MyUserList UserList
        {
            get => _userList;
            set
            {
                if (_userList == value) return;
                _userList = value;
                OnPropertyChanged("UserList");
            }
        }


        public int RoomId { get; set; }

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


        public ObservableCollection<object> MyVlcPlayer { get; set; } = new ObservableCollection<object>();


        private string _songTitle;

        public string SongTitle
        {
            get => _songTitle;
            set
            {
                if (_songTitle == value) return;
                _songTitle = value;
                OnPropertyChanged("SongTitle");
            }
        }

        private ushort _likes = 10;

        public ushort Likes
        {
            get => _likes;
            set
            {
                if (_likes == value) return;
                _likes = value;
                OnPropertyChanged("Likes");
            }
        }

        private ushort _dislikes = 20;

        public ushort Dislikes
        {
            get => _dislikes;
            set
            {
                if (_dislikes == value) return;
                _dislikes = value;
                OnPropertyChanged("Dislikes");
            }
        }

        private sbyte _songsQueue = 30;

        public sbyte SongsQueue
        {
            get => _songsQueue;
            set
            {
                if (_songsQueue == value) return;
                _songsQueue = value;
                OnPropertyChanged("SongsQueue");
            }
        }

        private string _timeLeft = "4:00";

        public string TimeLeft
        {
            get => _timeLeft;
            set
            {
                if (_timeLeft == value) return;
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }

        private sbyte _volume = 100;

        public sbyte Volume
        {
            get => _volume;
            set
            {
                if (_volume == value) return;
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private string _roomName = "RoomName";

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

        private string _hostName = "HostName";

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

        private bool _inQueue = false;

        public bool InQueue
        {
            get => _inQueue;
            set
            {
                if (_inQueue == value) return;
                _inQueue = value;
                OnPropertyChanged("InQueue");
            }
        }


        private ObservableCollection<RoomMessageModel> _roomMessageCollection;

        public ObservableCollection<RoomMessageModel> RoomMessageCollection
        {
            get => _roomMessageCollection;
            set
            {
                if (_roomMessageCollection == value) return;
                _roomMessageCollection = value;
                OnPropertyChanged("RoomMessageCollection");
            }
        }


        private string _myChatMessage;

        public string MyChatMessage
        {
            get => _myChatMessage;
            set
            {
                if (_myChatMessage == value) return;
                _myChatMessage = value;
                OnPropertyChanged("MyChatMessage");
            }
        }

        #endregion Properties

        #region Methods

        public class MyUserList : List<UserClient>
        {
            private readonly SdjMainViewModel _sdjMainViewModel;

            public MyUserList(SdjMainViewModel sdjMainViewModel)
            {
                _sdjMainViewModel = sdjMainViewModel;
            }

            public new void Add(UserClient tmp)
            {
                base.Add(tmp);
                this.RefreshMembersData();
            }

            public new void RemoveAt(UserClient tmp)
            {
                for (int i = 0; i < base.Count; i++)
                {
                    Console.WriteLine(base[i].Username);
                }

                base.RemoveAt(base.FindIndex(x => x.Id == tmp.Id));
                this.RefreshMembersData();

                for (int i = 0; i < base.Count; i++)
                {
                    Console.WriteLine(base[i].Username);
                }
            }

            private void RefreshMembersData()
            {
                _sdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = this.Count;
                _sdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfAdministrationInRoom =
                    this.Count(x => x.Rank > 0);
            }
        }

        #endregion Methods

        #region Commands

        #region LikeTrackCommand

        private RelayCommand _likeTrackCommand;

        public RelayCommand LikeTrackCommand
        {
            get
            {
                return _likeTrackCommand
                       ?? (_likeTrackCommand = new RelayCommand(LikeTrackCommandExecute, LikeTrackCommandCanExecute));
            }
        }

        public bool LikeTrackCommandCanExecute()
        {
            return true;
        }

        public void LikeTrackCommandExecute()
        {
            Likes++;
        }

        #endregion

        #region DislikeTrackCommand

        private RelayCommand _dislikeTrackCommand;

        public RelayCommand DislikeTrackCommand
        {
            get
            {
                return _dislikeTrackCommand
                       ?? (_dislikeTrackCommand =
                           new RelayCommand(DislikeTrackCommandExecute, DislikeTrackCommandCanExecute));
            }
        }

        public bool DislikeTrackCommandCanExecute()
        {
            return true;
        }

        public void DislikeTrackCommandExecute()
        {
        }

        #endregion

        #region AddTrackToPlaylistCommand

        private RelayCommand _addTrackToPlaylistCommand;

        public RelayCommand AddTrackToPlaylistCommand
        {
            get
            {
                return _addTrackToPlaylistCommand
                       ?? (_addTrackToPlaylistCommand = new RelayCommand(AddTrackToPlaylistCommandExecute,
                           AddTrackToPlaylistCommandCanExecute));
            }
        }

        public bool AddTrackToPlaylistCommandCanExecute()
        {
            return true;
        }

        public void AddTrackToPlaylistCommandExecute()
        {
        }

        #endregion

        #region StopPlayTrackCommand

        private RelayCommand _stopPlayTrackCommand;

        public RelayCommand StopPlayTrackCommand
        {
            get
            {
                return _stopPlayTrackCommand
                       ?? (_stopPlayTrackCommand =
                           new RelayCommand(StopPlayTrackCommandExecute, StopPlayTrackCommandCanExecute));
            }
        }

        public bool StopPlayTrackCommandCanExecute()
        {
            return true;
        }

        public void StopPlayTrackCommandExecute()
        {
        }

        #endregion

        #region LeftQueueCommand

        private RelayCommand _leftQueueCommand;

        public RelayCommand LeftQueueCommand
        {
            get
            {
                return _leftQueueCommand
                       ?? (_leftQueueCommand = new RelayCommand(LeftQueueCommandExecute, LeftQueueCommandCanExecute));
            }
        }

        public bool LeftQueueCommandCanExecute()
        {
            return InQueue;
        }

        public void LeftQueueCommandExecute()
        {
            InQueue = false;
        }

        #endregion

        #region JoinQueueCommand

        private RelayCommand _joinQueueCommand;

        public RelayCommand JoinQueueCommand
        {
            get
            {
                return _joinQueueCommand
                       ?? (_joinQueueCommand = new RelayCommand(JoinQueueCommandExecute, JoinQueueCommandCanExecute));
            }
        }

        private bool JoinQueueCommandCanExecute()
        {
            return !InQueue;
        }

        public void JoinQueueCommandExecute()
        {
            Task.Factory.StartNew(() =>
            {
                var tracks = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsActive)
                    ?.Tracks;
                var dj = new Dj();
                if (tracks != null)
                    foreach (var track in tracks)
                    {
                        int seconds = (int) TimeSpan.Parse(track.SongDuration).TotalSeconds;
                        dj.Track.Add(new Dj.Song(seconds, track.SongId));
                    }
                
                var json = JsonConvert.SerializeObject(dj);

                var response = SdjMainViewModel.Client.Sender.JoinQueue(json);
                var validation = ServerValidation.ServerResponseValidation(response);
                switch (validation.Item2)
                {
                    case ServerValidation.ResponseValidationEnum.NullOrEmpty:
                        Debug.Log("JoinQueue", "Null or empty response");
                        break;
                    case ServerValidation.ResponseValidationEnum.Success:
                        InQueue = true;
                        break;
                    default:
                        Debug.Log("JoinQueue", "Error with joining queue");
                        break;                    
                }
            });
        }

        #endregion

        #region SendMessageCommand

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand
                       ?? (_sendMessageCommand =
                           new RelayCommand(SendMessageCommandExecute, SendMessageCommandCanExecute));
            }
        }

        public bool SendMessageCommandCanExecute()
        {
            return true;
        }

        public void SendMessageCommandExecute()
        {
            SdjMainViewModel.Client.Sender.SendMessage(MyChatMessage, RoomId.ToString());
        }

        #endregion

        #endregion Commands
    }
}