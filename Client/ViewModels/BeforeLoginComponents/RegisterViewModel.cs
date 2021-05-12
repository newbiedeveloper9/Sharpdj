﻿using Caliburn.Micro;
using SCPackets.Packets.Register;
using SharpDj.Common;
using SharpDj.Enums;
using SharpDj.PubSubModels;
using System.ComponentModel;
using System.Net;
using System.Security;
using SCPackets.Packets.Login;
using SharpDj.Logic;

namespace SharpDj.ViewModels.BeforeLoginComponents
{
    public class RegisterViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ClientSender _sender;

        public RegisterViewModel()
        {

        }

        public RegisterViewModel(IEventAggregator eventAggregator, ClientSender sender)
        {
            _eventAggregator = eventAggregator;
            _sender = sender;
        }

        #region Properties
        private string _loginText;
        public string LoginText
        {
            get => _loginText;
            set
            {
                if (_loginText == value) return;
                _loginText = value;
                NotifyOfPropertyChange(() => LoginText);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        private string _emailText;
        public string EmailText
        {
            get => _emailText;
            set
            {
                if (_emailText == value) return;

                _emailText = value;
                NotifyOfPropertyChange(() => EmailText);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        private string _usernameText;
        public string UsernameText
        {
            get => _usernameText;
            set
            {
                if (_usernameText == value) return;
                _usernameText = value;
                NotifyOfPropertyChange(() => UsernameText);
                NotifyOfPropertyChange(() => CanRegister);
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
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        private bool _toS;
        public bool ToS
        {
            get => _toS;
            set
            {
                if (_toS == value) return;
                _toS = value;
                NotifyOfPropertyChange(() => ToS);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        private bool _privacyPolicy;
        public bool PrivacyPolicy
        {
            get => _privacyPolicy;
            set
            {
                if (_privacyPolicy == value) return;
                _privacyPolicy = value;
                NotifyOfPropertyChange(() => PrivacyPolicy);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        #endregion Properties

        public bool CanRegister => //PrivacyPolicy && ToS &&
                                   !string.IsNullOrWhiteSpace(LoginText) &&
                                   !string.IsNullOrWhiteSpace(EmailText) &&
                                   DataValidation.EmailIsValid(EmailText) &&
                                   DataValidation.PasswordIsValid(PasswordText, 6, 48) &&
                                   DataValidation.LengthIsValid(LoginText, 2, 32) &&
                                   DataValidation.LengthIsValid(UsernameText, 2, 32) &&
                                   !string.IsNullOrWhiteSpace(new System.Net.NetworkCredential(string.Empty, PasswordText)
                                       .Password);

        public async void Register()
        {
            var response = await _sender.Handle<RegisterResponse>(new RegisterRequest(LoginText,
                new NetworkCredential(string.Empty, PasswordText).Password, EmailText, UsernameText));
        }

        public void BackToLogin()
        {
            _eventAggregator.PublishOnUIThread(BeforeLoginEnum.Login);
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "EmailText":
                        if (string.IsNullOrEmpty(EmailText))
                            break;
                        if (!DataValidation.EmailIsValid(EmailText))
                            return "Please enter a valid email.";
                        break;
                    case "UsernameText":
                        if (!DataValidation.LengthIsValid(UsernameText, 2, 32))
                            return "Your Username must be between 2 and 32 characters";
                        break;
                    case "LoginText":
                        if (!DataValidation.LengthIsValid(LoginText, 2, 32))
                            return "Your Login must be between 2 and 32 characters";
                        break;
                }
                return string.Empty;
            }
        }

        public string Error { get; }
    }
}
