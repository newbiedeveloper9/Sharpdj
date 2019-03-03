using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Shared;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Helpers;
using SharpDj.Enums.Menu;
using SharpDj.ViewModel.Helpers;

namespace SharpDj.ViewModel
{
    public class SdjLeftBarViewModel : BaseViewModel
    {
        #region .ctor

        public SdjLeftBarViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, DoOnAnyAction);
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


        private LeftBar _leftBarVisibility = LeftBar.Collapsed;
        public LeftBar LeftBarVisibility
        {
            get => _leftBarVisibility;
            set
            {
                if (_leftBarVisibility == value) return;
                _leftBarVisibility = value;
                OnPropertyChanged("LeftBarVisibility");
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

        #endregion Properties

        #region Methods

        void DoOnAnyAction()
        {
            SdjMainViewModel.SdjUserProfileViewModel.IsVisible = false;
            LeftBarVisibility = LeftBar.Collapsed;
        }

        #endregion Methods

        #region Commands

        #region LeftBarOnProfileCommand
        private RelayCommand _leftBarOnProfileCommand;
        public RelayCommand LeftBarOnProfileCommand
        {
            get
            {
                return _leftBarOnProfileCommand
                       ?? (_leftBarOnProfileCommand = new RelayCommand(LeftBarOnProfileCommandExecute, LeftBarOnProfileCommandCanExecute));
            }
        }

        public bool LeftBarOnProfileCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnProfileCommandExecute()
        {
            SdjMainViewModel.SdjUserProfileViewModel.IsVisible = true;
            LeftBarVisibility = LeftBar.Collapsed;

            SdjMainViewModel.SdjUserProfileViewModel.Username = Username;

        }
        #endregion

        #region LeftBarOnFavoritesCommand
        private RelayCommand _leftBarOnFavoritesCommand;
        public RelayCommand LeftBarOnFavoritesCommand
        {
            get
            {
                return _leftBarOnFavoritesCommand
                       ?? (_leftBarOnFavoritesCommand = new RelayCommand(LeftBarOnFavoritesCommandExecute, LeftBarOnFavoritesCommandCanExecute));
            }
        }

        public bool LeftBarOnFavoritesCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnFavoritesCommandExecute()
        {
            DoOnAnyAction();

        }
        #endregion

        #region LeftBarOnPlaylistCommand
        private RelayCommand _leftBarOnPlaylistCommand;
        public RelayCommand LeftBarOnPlaylistCommand
        {
            get
            {
                return _leftBarOnPlaylistCommand
                       ?? (_leftBarOnPlaylistCommand = new RelayCommand(LeftBarOnPlaylistCommandExecute, LeftBarOnPlaylistCommandCanExecute));
            }
        }

        public bool LeftBarOnPlaylistCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnPlaylistCommandExecute()
        {
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarAddToPlaylistCommandExecute();
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarOnRoomsCommand
        private RelayCommand _leftBarOnRoomsCommandCommand;
        public RelayCommand LeftBarOnRoomsCommand
        {
            get
            {
                return _leftBarOnRoomsCommandCommand
                       ?? (_leftBarOnRoomsCommandCommand = new RelayCommand(LeftBarOnRoomsCommandExecute, LeftBarOnRoomsCommandCanExecute));
            }
        }

        public bool LeftBarOnRoomsCommandCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                return true;
            return false;

        }

        public void LeftBarOnRoomsCommandExecute()
        {
            SdjMainViewModel.MainViewVisibility = MainView.Main;
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarOnFriendsCommand
        private RelayCommand _leftBarOnFriendsCommand;
        public RelayCommand LeftBarOnFriendsCommand
        {
            get
            {
                return _leftBarOnFriendsCommand
                       ?? (_leftBarOnFriendsCommand = new RelayCommand(LeftBarOnFriendsCommandExecute, LeftBarOnFriendsCommandCanExecute));
            }
        }

        public bool LeftBarOnFriendsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnFriendsCommandExecute()
        {
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarOnPluginsCommand
        private RelayCommand _leftBarOnPluginsCommand;
        public RelayCommand LeftBarOnPluginsCommand
        {
            get
            {
                return _leftBarOnPluginsCommand
                       ?? (_leftBarOnPluginsCommand = new RelayCommand(LeftBarOnPluginsCommandExecute, LeftBarOnPluginsCommandCanExecute));
            }
        }

        public bool LeftBarOnPluginsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnPluginsCommandExecute()
        {
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarOnOptionsCommand
        private RelayCommand _leftBarOnOptionsCommand;
        public RelayCommand LeftBarOnOptionsCommand
        {
            get
            {
                return _leftBarOnOptionsCommand
                       ?? (_leftBarOnOptionsCommand = new RelayCommand(LeftBarOnOptionsCommandExecute, LeftBarOnOptionsCommandCanExecute));
            }
        }

        public bool LeftBarOnOptionsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnOptionsCommandExecute()
        {
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarOnReportBugCommand
        private RelayCommand _leftBarOnReportBugCommand;
        public RelayCommand LeftBarOnReportBugCommand
        {
            get
            {
                return _leftBarOnReportBugCommand
                       ?? (_leftBarOnReportBugCommand = new RelayCommand(LeftBarOnReportBugCommandExecute, LeftBarOnReportBugCommandCanExecute));
            }
        }

        public bool LeftBarOnReportBugCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnReportBugCommandExecute()
        {
            SdjMainViewModel.SdjFeedbackViewModel.IsVisible = true;
            DoOnAnyAction();
        }
        #endregion

        #region LeftBarAboutCommand
        private RelayCommand _leftBarAboutCommand;
        public RelayCommand LeftBarAboutCommand
        {
            get
            {
                return _leftBarAboutCommand
                       ?? (_leftBarAboutCommand = new RelayCommand(LeftBarAboutCommandExecute, LeftBarAboutCommandCanExecute));
            }
        }

        public bool LeftBarAboutCommandCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                return true;
            return false;

        }

        public void LeftBarAboutCommandExecute()
        {
            SdjMainViewModel.MainViewVisibility = MainView.About;
            DoOnAnyAction();

        }
        #endregion

        #region LeftBarOnLogoutCommand
        private RelayCommand _leftBarOnLogoutCommand;
        public RelayCommand LeftBarOnLogoutCommand
        {
            get
            {
                return _leftBarOnLogoutCommand
                       ?? (_leftBarOnLogoutCommand = new RelayCommand(LeftBarOnLogoutCommandExecute, LeftBarOnLogoutCommandCanExecute));
            }
        }

        public bool LeftBarOnLogoutCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnLogoutCommandExecute()
        {
            var resp = SdjMainViewModel.Client.Sender.Disconnect();
            if (resp.Equals(Commands.Instance.CommandsDictionary["Error"]))
            {
                Debug.Log("Disconnect", "Error");
            }
            else
            {
                SdjMainViewModel.MainViewVisibility = MainView.Login;
                Debug.Log("Disconnect", "Success");
            }
        }
        #endregion
        
        
        #region TestCommand
        private RelayCommand _testCommand;
        public RelayCommand TestCommand
        {
            get
            {
                return _testCommand
                       ?? (_testCommand = new RelayCommand(TestCommandExecute, TestCommandCanExecute));
            }
        }

        public bool TestCommandCanExecute()
        {
            return true;
        }

        public void TestCommandExecute()
        {
            SdjMainViewModel.GetImageVisibility = GetImageVisibility.Main;
        }
        #endregion

        #endregion Commands
    }
}
