using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDj.Core;

namespace SharpDj.ViewModel
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
                OnPropertyChanged("Username");
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
                       ?? (_registerMeCommand = new RelayCommand(RegisterMeCommandExecute, RegisterMeCommandCanExecute));
            }
        }

        public bool RegisterMeCommandCanExecute()
        {
            return true;
        }

        public void RegisterMeCommandExecute()
        {
            string password = new System.Net.NetworkCredential(string.Empty, Password).Password;

            SdjMainViewModel.Client.Sender.Register(Login, password, Email);
        }


        #endregion

        #endregion Commands


    }
}
