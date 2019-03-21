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

        public LoginViewModel LoginViewModel { get; private set; }
        public RegisterViewModel RegisterViewModel { get; private set; }

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
