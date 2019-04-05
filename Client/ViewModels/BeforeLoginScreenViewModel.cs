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

        #region VM's
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
        #endregion VM's

        #region .ctor
        public BeforeLoginScreenViewModel()
        {

        }

        public BeforeLoginScreenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Activated += OnActivated;
            /*Creates a new Bindings to View each time*/
        }
        #endregion .ctor

        private void OnActivated(object sender, ActivationEventArgs e)
        {
            ActivateItem(new LoginViewModel(_eventAggregator));
        }

        public void Handle(BeforeLoginEnum message)
        {
            switch (message)
            {
                case BeforeLoginEnum.Login:
                    ActivateItem(new LoginViewModel(_eventAggregator));
                    break;
                case BeforeLoginEnum.Register:
                    ActivateItem(new RegisterViewModel(_eventAggregator));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
