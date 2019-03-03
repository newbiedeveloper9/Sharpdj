using System;
using System.Security;
using System.Text.RegularExpressions;
using Communication.Client;
using Communication.Client.User;
using Communication.Shared;
using Hik.Communication.Scs.Communication.Messages;
using SharpDj.Core;
using SharpDj.Enums.Menu;
using SharpDj.Enums.User;
using SharpDj.Logic.Client;
using SharpDj.Logic.Helpers;

namespace SharpDj.ViewModel.Unique
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
            private get => _password;
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

                if (!string.IsNullOrEmpty(value))
                {
                    Password = null;
                    Debug.Log("Login", "Error");
                }
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
                       ?? (_loginAsGuestCommand =
                           new RelayCommand(LoginAsGuestCommandExecute, LoginAsGuestCommandCanExecute));
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
/*
            if (string.IsNullOrEmpty(resp))
                return;

            if (resp.Equals(Commands.Instance.CommandsDictionary["Error"]))
            {
                ErrorNotify = "Login error";
            }
            else
            {
                //TODO fix this, refactor plxxxxx.
                var rgx = new Regex(Commands.Instance.CommandsDictionary["Success"] + "(.*)");
                var match = rgx.Match(resp);
                if (match.Success)
                {
                    var username = match.Groups[1].Value;
                    SdjMainViewModel.Profile = new UserClient() {Username = username};
                    ClientInfo.Instance.UserState = UserState.Logged;
                    SdjMainViewModel.MainViewVisibility = MainView.Main;

                    Debug.Log("Login", "Success");
                }
                else
                {
                    ErrorNotify = "Login error";
                }
            }*/

            var resp = SdjMainViewModel.Client.Sender.Login(Login, Password);
            var validation = ServerValidation.ServerResponseValidation(resp);

            switch (validation.Item2)
            {
                case ServerValidation.ResponseValidationEnum.NullOrEmpty:
                    ErrorNotify = "Error with Request/Response";
                    break;
                case ServerValidation.ResponseValidationEnum.Success:
                    var username = validation.Item1;
                    SdjMainViewModel.Profile = new UserClient() {Username = username};
                    ClientInfo.Instance.UserState = UserState.Logged;
                    SdjMainViewModel.MainViewVisibility = MainView.Main;

                    Debug.Log("Login", "Success");
                    break;
                default:
                    ErrorNotify = "Login error";
                    break;
            }
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