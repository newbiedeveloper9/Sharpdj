using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class MajorScreenViewModel : PropertyChangedBase
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

            RoomSquareViewModel = new RoomSquareViewModel(_eventAggregator);
            NewsCarouselViewModel = new NewsCarouselViewModel();
        }
    }
}
