using Caliburn.Micro;
using SharpDj.ViewModels.SubViews;

namespace SharpDj.ViewModels
{
    public class AfterLoginScreenViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel MainViewModel { get; private set; }
        public LeftMenuViewModel LeftMenuViewModel { get; private set; }
        public SearchMenuViewModel SearchMenuViewModel { get; private set; }

        public AfterLoginScreenViewModel()
        {
            MainViewModel = new MainViewModel();
            LeftMenuViewModel = new LeftMenuViewModel();
            SearchMenuViewModel = new SearchMenuViewModel();
        }

        public AfterLoginScreenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            MainViewModel = new MainViewModel(_eventAggregator);
            LeftMenuViewModel = new LeftMenuViewModel();
            SearchMenuViewModel = new SearchMenuViewModel(_eventAggregator);
        }
    }
}
