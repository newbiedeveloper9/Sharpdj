using System.Net;
using Caliburn.Micro;
using System.Security;
using SCPackets.Packets.Login;
using SharpDj.Enums;
using SharpDj.Logic;
using SharpDj.Logic.ActionToServer;

namespace SharpDj.ViewModels.BeforeLoginComponents
{
    public class LoginViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ClientSender _sender;

        public LoginViewModel()
        {

        }

        public LoginViewModel(IEventAggregator eventAggregator, ClientSender sender)
        {
            _eventAggregator = eventAggregator;
            _sender = sender;
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
                                   !string.IsNullOrWhiteSpace(new NetworkCredential(string.Empty, PasswordText)
                                       .Password);

        public async void TryLogin()
        {

                var response = await _sender.Handle<LoginResponse>(new LoginRequest(LoginText,
                    new NetworkCredential(string.Empty, PasswordText).Password, Remember));

            await IoC.Get<ClientLoginAction>().Action(response);
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
