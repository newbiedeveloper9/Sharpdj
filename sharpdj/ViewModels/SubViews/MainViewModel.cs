using Caliburn.Micro;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IHandle<IRoomInfoForOpen>, IHandle<INavigateToHome>
    {
        private readonly IEventAggregator _eventAggregator;

        public MajorScreenViewModel MajorScreenViewModel { get; private set; }
        public RoomViewModel RoomViewModel { get; private set; }

        public MainViewModel()
        {
            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();

            ActivateItem(MajorScreenViewModel);
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();

            ActivateItem(MajorScreenViewModel);
        }

        public void Handle(IRoomInfoForOpen message)
        {
            ActivateItem(RoomViewModel);
        }

        public void Handle(INavigateToHome message)
        {
            ActivateItem(MajorScreenViewModel);
        }
    }
}
