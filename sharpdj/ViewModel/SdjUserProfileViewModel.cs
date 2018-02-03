using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.ViewModel.Helpers;

namespace SharpDj.ViewModel
{
    public class SdjUserProfileViewModel : BaseViewModel
    {
        #region ctor
        public SdjUserProfileViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, CloseForm);
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, CloseForm);
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

        private SdjTitleBarForUserControlsViewModel _sdjTitleBarForUserControlsViewModel;
        public SdjTitleBarForUserControlsViewModel SdjTitleBarForUserControlsViewModel
        {
            get => _sdjTitleBarForUserControlsViewModel;
            set
            {
                if (_sdjTitleBarForUserControlsViewModel == value) return;
                _sdjTitleBarForUserControlsViewModel = value;
                OnPropertyChanged("SdjTitleBarForUserControlsViewModel");
            }
        }

        private SdjBackgroundForFormsViewModel _sdjBackgroundForFormsViewModel;
        public SdjBackgroundForFormsViewModel SdjBackgroundForFormsViewModel
        {
            get => _sdjBackgroundForFormsViewModel;
            set
            {
                if (_sdjBackgroundForFormsViewModel == value) return;
                _sdjBackgroundForFormsViewModel = value;
                OnPropertyChanged("SdjBackgroundForFormsViewModel");
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
                SdjTitleBarForUserControlsViewModel.FormName = "Profile - " + Username;
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

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value) return;
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        #endregion

        #region Methods
        private void CloseForm()
        {
            SdjMainViewModel.SdjUserProfileViewModel.IsVisible = false;
        }
        #endregion

        #region Commands

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

        #region MinimalizeFormCommand
        private RelayCommand _minimalizeFormCommand;
        public RelayCommand MinimalizeFormCommand
        {
            get
            {
                return _minimalizeFormCommand
                       ?? (_minimalizeFormCommand = new RelayCommand(MinimalizeFormCommandExecute, MinimalizeFormCommandCanExecute));
            }
        }

        public bool MinimalizeFormCommandCanExecute()
        {
            return true;
        }

        public void MinimalizeFormCommandExecute()
        {
            SdjMainViewModel.SdjUserProfileViewModel.IsVisible = !SdjMainViewModel.SdjUserProfileViewModel.IsVisible;
        }
        #endregion

        #endregion
    }
}
