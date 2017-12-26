using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.ViewModel
{
    public class SdjRoomViewModel : BaseViewModel
    {

        #region .ctor

        public SdjRoomViewModel(SdjMainViewModel main)
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

        private int _likes = 10;
        public int Likes
        {
            get => _likes;
            set
            {
                if (_likes == value) return;
                _likes = value;
                OnPropertyChanged("Likes");
            }
        }

        private int _dislikes = 20;
        public int Dislikes
        {
            get => _dislikes;
            set
            {
                if (_dislikes == value) return;
                _dislikes = value;
                OnPropertyChanged("Dislikes");
            }
        }

        private int _songsQueue = 30;
        public int SongsQueue
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

        private int _volume = 50;
        public int Volume
        {
            get => _volume;
            set
            {
                if (_volume == value) return;
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }

        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands


        #region dwa
     
        #endregion

        #endregion Commands


    }
}
