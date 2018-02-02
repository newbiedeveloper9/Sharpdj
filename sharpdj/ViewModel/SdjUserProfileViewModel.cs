using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;

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

        private uint _totalLikes = 10;
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

        private uint _totalDislikes = 20;
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

        private uint _totalPlayedTracks = 30;
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

        private DateTime _registrationDate = new DateTime(2017,10,17,13,12,11);
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

        private DateTime _lastSeen = new DateTime(2018, 01, 29, 17, 31, 59);
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

        private bool _isMaximized = false;
        public bool IsMaximized
        {
            get => _isMaximized;
            set
            {
                if (_isMaximized == value) return;
                _isMaximized = value;
                OnPropertyChanged("IsMaximized");
            }
        }

        #endregion

        #region Methods
        #endregion

        #region Commands

        #region CloseProfile
        private RelayCommand _closeProfile;
        public RelayCommand CloseProfile
        {
            get
            {
                return _closeProfile
                       ?? (_closeProfile = new RelayCommand(CloseProfileExecute, CloseProfileCanExecute));
            }
        }

        public bool CloseProfileCanExecute()
        {
            return true;
        }

        public void CloseProfileExecute()
        {
            SdjMainViewModel.UserProfileVisibility = UserProfile.Collapsed;
        }
        #endregion

        #region MaximizeProfile
        private RelayCommand _maximizeProfile;
        public RelayCommand MaximizeProfile
        {
            get
            {
                return _maximizeProfile
                       ?? (_maximizeProfile = new RelayCommand(MaximizeProfileExecute, MaximizeProfileCanExecute));
            }
        }

        public bool MaximizeProfileCanExecute()
        {
            return true;
        }

        public void MaximizeProfileExecute()
        {
            IsMaximized = !IsMaximized;
        }
        #endregion

        #endregion
    }
}
