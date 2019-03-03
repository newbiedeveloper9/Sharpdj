using System;
using System.Security;
using Communication.Shared;
using SharpDj.Core;
using SharpDj.Enums.Menu;

namespace SharpDj.ViewModel.Unique
{
    public class SdjRegisterViewModel : BaseViewModel
    {
        #region .ctor

        public SdjRegisterViewModel(SdjMainViewModel main)
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

        private string _nickname;

        public string Nickname
        {
            get => _nickname;
            set
            {
                if (_nickname == value) return;
                _nickname = value;
                OnPropertyChanged("Nickname");
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

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
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
                    Debug.Log("Register", "Error");
                }
            }
        }

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Commands

        #region RegisterMeCommand

        private RelayCommand _registerMeCommand;

        public RelayCommand RegisterMeCommand
        {
            get
            {
                return _registerMeCommand
                       ?? (_registerMeCommand =
                           new RelayCommand(RegisterMeCommandExecute, RegisterMeCommandCanExecute));
            }
        }

        public bool RegisterMeCommandCanExecute()
        {
            return true;
        }

        public void RegisterMeCommandExecute()
        {
            var resp = SdjMainViewModel.Client.Sender.Register(Login, Password, Email);

            if (resp.Equals(Commands.Instance.CommandsDictionary["Error"]))
            {
                ErrorNotify = "Register error";
            }
            else
            {
                SdjMainViewModel.MainViewVisibility = MainView.Login;

                Debug.Log("Register", "Success");
            }
        }

        #endregion

        #endregion Commands
    }
}