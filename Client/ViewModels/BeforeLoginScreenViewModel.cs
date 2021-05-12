using Caliburn.Micro;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.BeforeLoginComponents;
using System;
using SharpDj.Enums;

namespace SharpDj.ViewModels
{
    public class BeforeLoginScreenViewModel : Conductor<object>.Collection.OneActive, IHandle<BeforeLoginEnum>
    {
        private readonly IEventAggregator _eventAggregator;

        public BeforeLoginScreenViewModel()
        {

        }

        public BeforeLoginScreenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _loginViewModel = IoC.Get<LoginViewModel>();
            _registerViewModel = IoC.Get<RegisterViewModel>();

            _eventAggregator.Subscribe(this);
            Activated += OnActivated;
        }

        private LoginViewModel _loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get => _loginViewModel;
            set
            {
                if (_loginViewModel == value) return;
                _loginViewModel = value;
                NotifyOfPropertyChange(() => LoginViewModel);
            }
        }

        private RegisterViewModel _registerViewModel;
        public RegisterViewModel RegisterViewModel
        {
            get => _registerViewModel;
            set
            {
                if (_registerViewModel == value) return;
                _registerViewModel = value;
                NotifyOfPropertyChange(() => RegisterViewModel);
            }
        }

        private void OnActivated(object sender, ActivationEventArgs e)
        {
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(BeforeLoginEnum message)
        {
            switch (message)
            {
                case BeforeLoginEnum.Login:
                    ActivateItem(IoC.Get<LoginViewModel>());
                    break;
                case BeforeLoginEnum.Register:
                    ActivateItem(IoC.Get<RegisterViewModel>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
