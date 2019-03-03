using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.MainViewComponents;
using System.Collections.Generic;
using SharpDj.Interfaces;


namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : Conductor<PropertyChangedBase>.Collection.OneActive,
        IHandle<IRoomInfoForOpen>,
        IHandle<NavigateMainView>,
        IHandle<RollingMenuVisibilityEnum>
    {
        private readonly IEventAggregator _eventAggregator;

        private Dictionary<NavigateMainView, INavMainView> NavigationDictionary;

        public INavMainView MajorScreenViewModel { get; private set; }
        public INavMainView RoomViewModel { get; private set; }
        public INavMainView PlaylistViewModel { get; private set; }

        public ProfileOptionsViewModel ProfileOptionsViewModel { get; private set; }
        public ConversationsViewModel ConversationsViewModel { get; private set; }


        public MainViewModel()
        {
            MajorScreenViewModel = new MajorScreenViewModel();
            RoomViewModel = new RoomViewModel();
            PlaylistViewModel = new PlaylistViewModel();

            ProfileOptionsViewModel = new ProfileOptionsViewModel();
            ConversationsViewModel = new ConversationsViewModel();


            ActivateItem((PropertyChangedBase)MajorScreenViewModel);
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();
            PlaylistViewModel = new PlaylistViewModel();

            ProfileOptionsViewModel = new ProfileOptionsViewModel();
            ConversationsViewModel = new ConversationsViewModel();

            NavigationDictionary = new Dictionary<NavigateMainView, INavMainView>()
            {
                {NavigateMainView.Home, MajorScreenViewModel },
                {NavigateMainView.Playlist, PlaylistViewModel },
                {NavigateMainView.Room, RoomViewModel },
            };

            ActivateItem((PropertyChangedBase)NavigationDictionary[NavigateMainView.Home]);
        }

        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
        }

        public void Handle(IRoomInfoForOpen message)
        {
            Handle(NavigateMainView.Room);
        }

        public void Handle(NavigateMainView message)
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Void);
            ActivateItem((PropertyChangedBase)NavigationDictionary[message]);
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
