using System;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels
{
    class ShellViewModel : PropertyChangedBase, IShell
    {
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel MainViewModel { get; private set; }
        public TopMenuViewModel TopMenuViewModel { get; private set; }
        public LeftMenuViewModel LeftMenuViewModel { get; private set; }
        public SearchMenuViewModel SearchMenuViewModel { get; private set; }

        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();

            MainViewModel = new MainViewModel(_eventAggregator);
            LeftMenuViewModel = new LeftMenuViewModel();
            TopMenuViewModel = new TopMenuViewModel();
            SearchMenuViewModel = new SearchMenuViewModel(_eventAggregator);
        }
    }
}
