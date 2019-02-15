using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.BeforeLoginComponents;

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

            LoginViewModel = new LoginViewModel(_eventAggregator);
            RegisterViewModel = new RegisterViewModel(_eventAggregator);

            ActivateItem(LoginViewModel);
        }

        public void Handle(ILoginRegisterAgentHandler message)
        {
            switch (message.MoveTo)
            {
                case MoveTo.Login:
                    ActivateItem(LoginViewModel);
                    break;
                case MoveTo.Register:
                    ActivateItem(RegisterViewModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
