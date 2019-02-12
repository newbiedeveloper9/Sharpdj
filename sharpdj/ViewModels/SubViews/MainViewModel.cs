using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IHandle<MainViewModel.RoomInfoForOpen>
    {
        private readonly IEventAggregator _eventAggregator;

        public MajorScreenViewModel MajorScreenViewModel { get; private set; }
        public RoomViewModel RoomViewModel { get; private set; }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            MajorScreenViewModel = new MajorScreenViewModel(_eventAggregator);
            RoomViewModel = new RoomViewModel();

            ActivateItem(MajorScreenViewModel);
        }

        public void Handle(RoomInfoForOpen message)
        {
            System.Console.WriteLine("test");
            ActivateItem(RoomViewModel);
        }

        public class RoomInfoForOpen
        {
            
        }
    }
}
