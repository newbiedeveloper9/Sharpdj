using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.ViewModel
{
    public class SdjUserProfileViewModel : BaseViewModel
    {
        #region ctor
        public SdjUserProfileViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
        }
        #endregion

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


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        private uint _totalLikes;
        public uint TotalLikes
        {
            get => _totalLikes;
            set
            {
                if (_totalLikes == value) return;
                _totalLikes = value;
                OnPropertyChanged("TotalLikes");
            }
        }

        private uint _totalDislikes;
        public uint TotalDislikes
        {
            get => _totalDislikes;
            set
            {
                if (_totalDislikes == value) return;
                _totalDislikes = value;
                OnPropertyChanged("TotalDislikes");
            }
        }

        private uint _totalPlayedTracks;
        public uint TotalPlayedTracks
        {
            get => _totalPlayedTracks;
            set
            {
                if (_totalPlayedTracks == value) return;
                _totalPlayedTracks = value;
                OnPropertyChanged("TotalPlayedTracks");
            }
        }

        private DateTime _registrationDate;
        public DateTime RegistrationDate
        {
            get => _registrationDate;
            set
            {
                if (_registrationDate == value) return;
                _registrationDate = value;
                OnPropertyChanged("RegistrationDate");
            }
        }

        private DateTime _lastSeen;
        public DateTime LastSeen
        {
            get => _lastSeen;
            set
            {
                if (_lastSeen == value) return;
                _lastSeen = value;
                OnPropertyChanged("LastSeen");
            }
        }

        private string _commentText;
        public string CommentText
        {
            get => _commentText;
            set
            {
                if (_commentText == value) return;
                _commentText = value;
                OnPropertyChanged("CommentText");
            }
        }

        #endregion

        #region Methods
        #endregion

        #region Commands
        #endregion
    }
}
