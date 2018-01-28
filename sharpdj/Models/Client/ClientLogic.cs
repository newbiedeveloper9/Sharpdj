using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.Models.Helpers;
using SharpDj.ViewModel;

namespace SharpDj.Models.Client
{
    public class ClientLogic
    {
        private UserState _userState = UserState.NotLoggedIn;
        public UserState UserState
        {
            get { return _userState; }
            set
            {
                _userState = value;
                SdjMainViewModel.MainViewVisibility = value == UserState.Logged ? MainView.Main : MainView.Register;
            }
        }

        public ClientLogic(SdjMainViewModel main)
        {
            SdjMainViewModel = main;

            main.Client.Receiver.RegisterAccExistErr += Receiver_RegisterAccExistErr;
            main.Client.Receiver.RegisterErr += Receiver_RegisterErr;
            main.Client.Receiver.SuccesfulRegister += Receiver_SuccesfulRegister;
            main.Client.Receiver.SuccessfulLogin += Receiver_SuccessfulLogin;
        }

        private void Receiver_RegisterAccExistErr(object sender, EventArgs e)
        {
            SdjMainViewModel.SdjRegisterViewModel.ErrorNotify = ErrorMessages.RegisterAccountExists;
        }

        private void Receiver_RegisterErr(object sender, EventArgs e)
        {
            SdjMainViewModel.SdjRegisterViewModel.ErrorNotify = ErrorMessages.RegisterErrorMessage;
        }

        private void Receiver_SuccesfulRegister(object sender, EventArgs e)
        {
            SdjMainViewModel.MainViewVisibility = MainView.Login;
            Debug.Log("Register", "Succesful register");
        }

        private void Receiver_SuccessfulLogin(object sender, EventArgs e)
        {
            UserState = UserState.Logged;
            Debug.Log("Login", "Succesful login");
        }



        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
            }
        }

    }
}
