using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Models.Client;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjRoomViewModel : BaseViewModel
    {

        #region .ctor

        public SdjRoomViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            RoomMessageCollection = new ObservableCollection<RoomMessageModel>();
            var mess = new RoomMessageModel(main);
            mess.Message = "Testowa wiadomość Testowa wiadomość Testowa wiadomość Testowa wiadomość Te xdd xdd dstowa wiadomość Testowa wiadomość Testowa wiadomość Testowa wiadomość Testowa wiadomość";
            mess.Time = "13:03";
            mess.Username = "Crisey";
           
            for (int i = 0; i < 10; i++)
            {
                RoomMessageCollection.Add(mess);
            }
        }

        #endregion .ctor

        #region Properties

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


        #endregion Properties

        #region Methods


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
                       ?? (_dislikeTrackCommand = new RelayCommand(DislikeTrackCommandExecute, DislikeTrackCommandCanExecute));
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
                       ?? (_addTrackToPlaylistCommand = new RelayCommand(AddTrackToPlaylistCommandExecute, AddTrackToPlaylistCommandCanExecute));
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
                       ?? (_stopPlayTrackCommand = new RelayCommand(StopPlayTrackCommandExecute, StopPlayTrackCommandCanExecute));
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
            return true;
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

        public bool JoinQueueCommandCanExecute()
        {
            return true;
        }

        public void JoinQueueCommandExecute()
        {
            InQueue = true;
            Task.Factory.StartNew(() =>
            {
                var tracks = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsActive)?.Tracks;
                Songs dj = new Songs();
                foreach (var track in tracks)
                {
                    int seconds = (int)TimeSpan.Parse(track.SongDuration).TotalSeconds;
                    dj.Video.Add(new Songs.Song(seconds, track.SongId));
                }
                var source = JsonConvert.SerializeObject(dj);
                ClientInfo.Instance.Client.SendMessage(new ScsTextMessage("joinqueue$"+source));
            });
        }
        #endregion


        #endregion Commands

    }
}
