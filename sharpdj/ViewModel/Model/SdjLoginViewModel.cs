using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using SharpDj.Enums;
using SharpDj.Models.Client;

namespace SharpDj.ViewModel.Model
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

        private string _password;
        public string Password
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
            SdjMainViewModel.Client.Sender.Login(Login, Password);
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
