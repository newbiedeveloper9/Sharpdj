using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using SharpDj.Interfaces;
using SharpDj.Logic;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews;
using System;
using System.Threading.Tasks;

namespace SharpDj.ViewModels
{
    public sealed class ShellViewModel : Conductor<object>.Collection.OneActive, IShell, IHandle<ILoginPublishInfo>, IHandle<IMessageQueue>
    {
        private readonly IEventAggregator _eventAggregator;
        private ClientConnection client;

        public AfterLoginScreenViewModel AfterLoginScreenViewModel { get; private set; }
        public BeforeLoginScreenViewModel BeforeLoginScreenViewModel { get; private set; }
        public TopMenuViewModel TopMenuViewModel { get; private set; }


        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);
            MessagesQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3750));
            

            TopMenuViewModel = new TopMenuViewModel();
            AfterLoginScreenViewModel = new AfterLoginScreenViewModel(_eventAggregator);
            BeforeLoginScreenViewModel = new BeforeLoginScreenViewModel(_eventAggregator);

#if DEBUG
            ActivateItem(AfterLoginScreenViewModel);
#else  
            ActivateItem(BeforeLoginScreenViewModel);
#endif

            Task.Factory.StartNew(() =>
            {
                client = new ClientConnection(_eventAggregator);

            });
        }

        public void Handle(ILoginPublishInfo message)
        {
            ActivateItem(AfterLoginScreenViewModel);
        }

        public void Handle(IMessageQueue message)
        {
            Task.Factory.StartNew(() => 
                MessagesQueue.Enqueue($"{message.ViewName}: {message.Message}"));
        }

        private SnackbarMessageQueue _messagesQueue;
        public SnackbarMessageQueue MessagesQueue
        {
            get => _messagesQueue;
            set
            {
                if (_messagesQueue == value) return;
                _messagesQueue = value;
                NotifyOfPropertyChange(() => MessagesQueue);
            }
        }
    }
}
