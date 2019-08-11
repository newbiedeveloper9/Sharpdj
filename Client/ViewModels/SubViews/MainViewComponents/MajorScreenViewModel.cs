using Caliburn.Micro;
using SharpDj.Interfaces;
using SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class MajorScreenViewModel : PropertyChangedBase,
        INavMainView
    {
        private readonly IEventAggregator _eventAggregator;

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
            NewsCarouselViewModel = new NewsCarouselViewModel(_eventAggregator);
        }


        #region Properties
        private RoomSquareViewModel _roomSquareViewModel;
        public RoomSquareViewModel RoomSquareViewModel
        {
            get => _roomSquareViewModel;
            set
            {
                if (_roomSquareViewModel == value) return;
                _roomSquareViewModel = value;
                NotifyOfPropertyChange(() => RoomSquareViewModel);
            }
        }

        private NewsCarouselViewModel _newsCarouselViewModel;
        public NewsCarouselViewModel NewsCarouselViewModel
        {
            get => _newsCarouselViewModel;
            set
            {
                if (_newsCarouselViewModel == value) return;
                _newsCarouselViewModel = value;
                NotifyOfPropertyChange(() => NewsCarouselViewModel);
            }
        }
        #endregion
    }
}
