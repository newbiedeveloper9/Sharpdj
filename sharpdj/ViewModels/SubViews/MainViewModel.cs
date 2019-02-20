using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.MainViewComponents;


namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IHandle<IRoomInfoForOpen>, IHandle<INavigateToHome>, IHandle<RollingMenuVisibilityEnum>
    {
        private readonly IEventAggregator _eventAggregator;

        public MajorScreenViewModel MajorScreenViewModel { get; private set; }
        public RoomViewModel RoomViewModel { get; private set; }
        public ProfileOptionsViewModel ProfileOptionsViewModel { get; private set; }
        public ConversationsViewModel ConversationsViewModel { get; private set; }

        public MainViewModel()
        {
            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();

            ProfileOptionsViewModel = new ProfileOptionsViewModel();
            ConversationsViewModel = new ConversationsViewModel();

            ActivateItem(MajorScreenViewModel);
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();

            ProfileOptionsViewModel = new ProfileOptionsViewModel();
            ConversationsViewModel = new ConversationsViewModel();

            ActivateItem(MajorScreenViewModel);
        }

        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
        }

        public void Handle(IRoomInfoForOpen message)
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Void);
            ActivateItem(RoomViewModel);
        }

        public void Handle(INavigateToHome message)
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Void);
            ActivateItem(MajorScreenViewModel);
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
