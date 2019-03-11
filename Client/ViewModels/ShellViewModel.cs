using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using SharpDj.Interfaces;
using SharpDj.Logic;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews;
using System;
using System.Threading;
using System.Threading.Tasks;
using SharpDj.Enums;

namespace SharpDj.ViewModels
{
    public sealed class ShellViewModel : Conductor<object>.Collection.OneActive,
        IShell,
        IHandle<ILoginPublishInfo>, IHandle<IMessageQueue>, IHandle<IReconnect>, IHandle<INotLoggedIn>
    {
        private readonly IEventAggregator _eventAggregator;
        private ClientConnection client;
        private bool reconnecting;

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
            client = new ClientConnection(_eventAggregator);
            Task.Factory.StartNew(() =>
            {
                client.Init();
            });
        }

        public void Handle(ILoginPublishInfo message)
        {
            ActivateItem(AfterLoginScreenViewModel);
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

        public void Handle(IMessageQueue message)
        {
            Task.Factory.StartNew(() =>
                MessagesQueue.Enqueue($"{message.ViewName}: {message.Message}"));
        }

        public void Handle(IReconnect message)
        {
            if (!reconnecting)
            {
                reconnecting = true;
                client = new ClientConnection(_eventAggregator);
                Task.Factory.StartNew(() =>
                {
                    client.Init();
                    if(ActiveItem == AfterLoginScreenViewModel)
                        _eventAggregator.PublishOnUIThread(new NotLoggedIn());
                    reconnecting = false;
                });
            }
        }

        public void Handle(INotLoggedIn message)
        {
            _eventAggregator.PublishOnUIThread(new MessageQueue("Connection","You are not anymore logged in"));
            _eventAggregator.PublishOnUIThread(NavigateMainView.Home);
            ActivateItem(BeforeLoginScreenViewModel);
        }
    }
}
