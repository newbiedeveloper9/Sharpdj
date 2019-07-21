﻿using System.Net;
using Caliburn.Micro;
using System.Security;
using SCPackets;
using SCPackets.LoginPacket;
using SharpDj.Enums;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.BeforeLoginComponents
{
    public class LoginViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public LoginViewModel()
        {
            
        }

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        private string _loginText;
        public string LoginText
        {
            get => _loginText;
            set
            {
                if (_loginText == value) return;
                _loginText = value;
                NotifyOfPropertyChange(() => LoginText);
                NotifyOfPropertyChange(() => CanTryLogin);
            }
        }


        private SecureString _passwordText;
        public SecureString PasswordText
        {
            get => _passwordText;
            set
            {
                if (_passwordText == value) return;
                _passwordText = value;
                NotifyOfPropertyChange(() => PasswordText);
                NotifyOfPropertyChange(() => CanTryLogin);

            }
        }

        private bool _remember;
        public bool Remember
        {
            get => _remember;
            set
            {
                if (_remember == value) return;
                _remember = value;
                NotifyOfPropertyChange(() => Remember);
            }
        }

        public bool CanTryLogin => !string.IsNullOrWhiteSpace(LoginText) &&
                                   !string.IsNullOrWhiteSpace(new System.Net.NetworkCredential(string.Empty, PasswordText)
                                       .Password);

        public void TryLogin()
        {
            _eventAggregator.PublishOnUIThread(new SendPacket(new LoginRequest(
                LoginText, new System.Net.NetworkCredential(string.Empty, PasswordText).Password, Remember)));
        }

        public void GuestLogin()
        {

        }

        public void Register()
        {
            _eventAggregator.PublishOnUIThread(BeforeLoginEnum.Register);
        }

    }
}
