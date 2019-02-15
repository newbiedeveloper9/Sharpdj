using Caliburn.Micro;

namespace SharpDj.ViewModels.BeforeLoginComponents
{
    public class RegisterViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public RegisterViewModel()
        {
            
        }

        public RegisterViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
