using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Menu;
using SharpDj.Models.Client;
using SharpDj.ViewModel.Model;

namespace SharpDj.ViewModel
{
    public class SdjLoginViewModel : BaseViewModel
    {
        #region .ctor
        public SdjLoginViewModel(SdjMainViewModel main)
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

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (_login == value) return;
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                if (_rememberMe == value) return;
                _rememberMe = value;
                OnPropertyChanged("RememberMe");
            }
        }

        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _errorNotify;
        public string ErrorNotify
        {
            get => _errorNotify;
            set
            {
                if (_errorNotify == value) return;
                _errorNotify = value;
                OnPropertyChanged("ErrorNotify");
            }
        }

        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands

        #region LoginAsGuestCommand
        private RelayCommand _loginAsGuestCommand;
        public RelayCommand LoginAsGuestCommand
        {
            get
            {
                return _loginAsGuestCommand
                       ?? (_loginAsGuestCommand = new RelayCommand(LoginAsGuestCommandExecute, LoginAsGuestCommandCanExecute));
            }
        }

        public bool LoginAsGuestCommandCanExecute()
        {
            return true;
        }

        public void LoginAsGuestCommandExecute()
        {

        }
        #endregion

        #region LoginCommand
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand
                       ?? (_loginCommand = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute));
            }
        }

        public bool LoginCommandCanExecute()
        {
            return true;
        }

        public void LoginCommandExecute()
        {
            var password = new System.Net.NetworkCredential(string.Empty, Password).Password;
            SdjMainViewModel.Client.Sender.Login(Login, password);
          
        }
        #endregion

        #region RegisterCommand
        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand
                       ?? (_registerCommand = new RelayCommand(RegisterCommandExecute, RegisterCommandCanExecute));
            }
        }

        public bool RegisterCommandCanExecute()
        {
            return true;
        }

        public void RegisterCommandExecute()
        {
            SdjMainViewModel.MainViewVisibility = MainView.Register;
        }
        #endregion

        #endregion Commands
    }
}
