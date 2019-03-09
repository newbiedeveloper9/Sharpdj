using Caliburn.Micro;
using SCPackets.RegisterPacket;
using SharpDj.Input;
using SharpDj.PubSubModels;
using System.ComponentModel;
using System.Security;
using SCPackets;
using SharpDj.Enums;

namespace SharpDj.ViewModels.BeforeLoginComponents
{
    public class RegisterViewModel : PropertyChangedBase, IDataErrorInfo
    {
        private readonly IEventAggregator _eventAggregator;

        public RegisterViewModel()
        {

        }

        public RegisterViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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

        public bool CanRegister => PrivacyPolicy && ToS &&
                                   !string.IsNullOrWhiteSpace(LoginText) &&
                                   !string.IsNullOrWhiteSpace(EmailText) &&
                                   UserValidation.EmailIsValid(EmailText) &&
                                   UserValidation.PasswordIsValid(PasswordText, 6, 48) &&
                                   UserValidation.LoginIsValid(LoginText, 2, 32) &&
                                   UserValidation.LoginIsValid(UsernameText, 2, 32) &&
                                   !string.IsNullOrWhiteSpace(new System.Net.NetworkCredential(string.Empty, PasswordText)
                                       .Password);

        public void Register()
        {
            _eventAggregator.PublishOnUIThread(
                new SendPacket(
                    new RegisterRequest(LoginText, new System.Net.NetworkCredential(string.Empty, PasswordText).Password, EmailText, UsernameText)
                )
            );
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
                        if (!UserValidation.EmailIsValid(EmailText))
                            return "Please enter a valid email.";
                        break;
                    case "UsernameText":
                        if (!UserValidation.LoginIsValid(UsernameText, 2, 32))
                            return "Your Username must be between 2 and 32 characters";
                        break;
                    case "LoginText":
                        if (!UserValidation.LoginIsValid(LoginText, 2, 32))
                            return "Your Login must be between 2 and 32 characters";
                        break;
                }
                return string.Empty;
            }
        }

        public string Error { get; }
    }
}
