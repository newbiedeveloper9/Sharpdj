using Caliburn.Micro;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews;

namespace SharpDj.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IShell, IHandle<ILoginPublishInfo>
    {
        private readonly IEventAggregator _eventAggregator;

        public AfterLoginScreenViewModel AfterLoginScreenViewModel { get; private set; }
        public BeforeLoginScreenViewModel BeforeLoginScreenViewModel { get; private set; }
        public TopMenuViewModel TopMenuViewModel { get; private set; }


        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);

            TopMenuViewModel = new TopMenuViewModel();
            AfterLoginScreenViewModel = new AfterLoginScreenViewModel(_eventAggregator);
            BeforeLoginScreenViewModel = new BeforeLoginScreenViewModel(_eventAggregator);

#if DEBUG
            ActivateItem(AfterLoginScreenViewModel);
#else  
            ActivateItem(BeforeLoginScreenViewModel);
#endif

        }

        public void Handle(ILoginPublishInfo message)
        {
            ActivateItem(AfterLoginScreenViewModel);
        }
    }
}
