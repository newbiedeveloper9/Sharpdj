using System;
using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.MainViewComponents;
using System.Collections.Generic;
using SharpDj.Interfaces;


namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : Conductor<object>.Collection.OneActive,
        IHandle<NavigateMainView>,
        IHandle<RollingMenuVisibilityEnum>,
        IHandle<IRoomInfoForOpen>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly Dictionary<NavigateMainView, INavMainView> _navigationDictionary;
        private readonly Config _config;
        #endregion Fields

        #region VM's
        public INavMainView MajorScreenViewModel { get; private set; }
        public INavMainView RoomViewModel { get; private set; }
        public INavMainView PlaylistViewModel { get; private set; }
        public INavMainView CreateRoomViewModel { get; private set; }
        public INavMainView ManageRoomsViewModel { get; private set; }

        public ProfileOptionsViewModel ProfileOptionsViewModel { get; private set; }
        public ConversationsViewModel ConversationsViewModel { get; private set; }
        #endregion VM's

        #region .ctor
        public MainViewModel()
        {
            MajorScreenViewModel = new MajorScreenViewModel();
            RoomViewModel = new RoomViewModel();
            PlaylistViewModel = new PlaylistViewModel();
            CreateRoomViewModel = new CreateRoomViewModel();
            ManageRoomsViewModel = new ManageRoomsViewModel();

            ProfileOptionsViewModel = new ProfileOptionsViewModel();
            ConversationsViewModel = new ConversationsViewModel();


            ActivateItem((PropertyChangedBase)MajorScreenViewModel);
        }

        public MainViewModel(IEventAggregator eventAggregator, Config config)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _config = config;

            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();
            PlaylistViewModel = new PlaylistViewModel(_eventAggregator);
            CreateRoomViewModel = new CreateRoomViewModel(_eventAggregator);
            ManageRoomsViewModel = new ManageRoomsViewModel(_eventAggregator);

            ProfileOptionsViewModel = new ProfileOptionsViewModel(_eventAggregator);
            ConversationsViewModel = new ConversationsViewModel();

            _navigationDictionary = new Dictionary<NavigateMainView, INavMainView>()
            {
                {NavigateMainView.Home, MajorScreenViewModel },
                {NavigateMainView.Playlist, PlaylistViewModel },
                {NavigateMainView.Room, RoomViewModel },
                {NavigateMainView.CreateRoom, CreateRoomViewModel },
                {NavigateMainView.ManageRooms, ManageRoomsViewModel },
            };

            ActivateItem((PropertyChangedBase)_navigationDictionary[NavigateMainView.Home]);
        }
        #endregion ctor

        #region Properties

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
        #endregion Properties

        #region Handle's
        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
        }

        public void Handle(NavigateMainView message)
        {
            if (ActiveItem is PlaylistViewModel playlistViewModel)
            {
                _config.SavePlaylistWithDelay(0);
            }

            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Void);
            var newItem = _navigationDictionary[message];
            ActivateItem((PropertyChangedBase)newItem);
        }

        public void Handle(IRoomInfoForOpen message)
        {
            Handle(NavigateMainView.Room);
        }
        #endregion Handle's
    }
}
