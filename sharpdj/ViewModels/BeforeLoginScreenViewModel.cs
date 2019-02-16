using Caliburn.Micro;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.BeforeLoginComponents;
using System;

namespace SharpDj.ViewModels
{
    public class BeforeLoginScreenViewModel : Conductor<object>.Collection.OneActive, IHandle<ILoginRegisterAgentHandler>
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

            ActivateItem(new LoginViewModel(_eventAggregator));
            /*Creates a new Bindings to View each time*/
        }

        public void Handle(ILoginRegisterAgentHandler message)
        {
            switch (message.MoveTo)
            {
                case MoveTo.Login:
                    ActivateItem(new LoginViewModel(_eventAggregator));
                    break;
                case MoveTo.Register:
                    ActivateItem(new RegisterViewModel(_eventAggregator));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
