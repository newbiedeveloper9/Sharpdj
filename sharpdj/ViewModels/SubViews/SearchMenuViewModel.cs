using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews
{
    public class SearchMenuViewModel : PropertyChangedBase, IHandle<RollingMenuVisibilityEnum>
    {
        private readonly IEventAggregator _eventAggregator;

        public SearchMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public SearchMenuViewModel()
        {
            
        }

        public void Home()
        {
            _eventAggregator.PublishOnUIThread(new NavigateToHome());
        }

        public void ShowOptions()
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Options);
        }

        public void ShowConversations()
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Conversations);
        }

        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
        }


        private RollingMenuVisibilityEnum _rollingMenuVisibility;
        public RollingMenuVisibilityEnum RollingMenuVisibility
        {
            get => _rollingMenuVisibility;
            set
            {
                if (_rollingMenuVisibility == value) return;
                _rollingMenuVisibility = value;
                NotifyOfPropertyChange(() => RollingMenuVisibility);
            }
        }
    }
}
