using Caliburn.Micro;
using SharpDj.Interfaces;
using SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class MajorScreenViewModel : PropertyChangedBase,
        INavMainView
    {
        private readonly IEventAggregator _eventAggregator;

        public NewsCarouselViewModel NewsCarouselViewModel { get; private set; }
        public RoomSquareViewModel RoomSquareViewModel { get; private set; }

        public MajorScreenViewModel()
        {
            RoomSquareViewModel = new RoomSquareViewModel();
            NewsCarouselViewModel = new NewsCarouselViewModel();
        }

        public MajorScreenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            RoomSquareViewModel = new RoomSquareViewModel(_eventAggregator);
            NewsCarouselViewModel = new NewsCarouselViewModel();
        }

    }
}
