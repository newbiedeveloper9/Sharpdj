using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using SharpDj.Enums;
using SharpDj.Logic;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews;
using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Serilog;

namespace SharpDj.ViewModels
{
    public sealed class ShellViewModel : Conductor<object>.Collection.OneActive,
        IHandle<ILoginPublish>, IHandle<IMessageQueue>, IHandle<IReconnect>, IHandle<INotLoggedIn>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly Config _config;

        private ClientConnection _client;
        private bool _reconnecting;

        #endregion Fields

        #region .ctor
        public ShellViewModel(IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _config = IoC.Get<Config>();
            if (!_config.LoadCore())
            {
                Console.ReadLine();
                App.Current.Shutdown();
            }

            MessagesQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3750));

            TopMenuViewModel = new TopMenuViewModel();
            AfterLoginScreenViewModel = new AfterLoginScreenViewModel(_eventAggregator, _config);
            BeforeLoginScreenViewModel = new BeforeLoginScreenViewModel(_eventAggregator);

            ActivateItem(BeforeLoginScreenViewModel);

            _client = IoC.Get<ClientConnection>();
            Task.Factory.StartNew(() =>
            {
                _client.Init();
            });
        }
        #endregion .ctor

        #region ViewModels
        private AfterLoginScreenViewModel _afterLoginScreenViewModel;
        public AfterLoginScreenViewModel AfterLoginScreenViewModel
        {
            get => _afterLoginScreenViewModel;
            set
            {
                if (_afterLoginScreenViewModel == value) return;
                _afterLoginScreenViewModel = value;
                NotifyOfPropertyChange(() => AfterLoginScreenViewModel);
            }
        }

        private BeforeLoginScreenViewModel _beforeLoginScreenViewModel;
        public BeforeLoginScreenViewModel BeforeLoginScreenViewModel
        {
            get => _beforeLoginScreenViewModel;
            set
            {
                if (_beforeLoginScreenViewModel == value) return;
                _beforeLoginScreenViewModel = value;
                NotifyOfPropertyChange(() => BeforeLoginScreenViewModel);
            }
        }

        private TopMenuViewModel _topMenuViewModel;
        public TopMenuViewModel TopMenuViewModel
        {
            get => _topMenuViewModel;
            set
            {
                if (_topMenuViewModel == value) return;
                _topMenuViewModel = value;
                NotifyOfPropertyChange(() => TopMenuViewModel);
            }
        }


        #endregion ViewModels

        #region Props

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

        #endregion Props

        #region Handle's
        public void Handle(ILoginPublish message)
        {
            UserInfoSingleton.Instance.UserClient = message.Client;
            ActivateItem(AfterLoginScreenViewModel);
        }

        public void Handle(IMessageQueue message)
        {
            Task.Factory.StartNew(() =>
                MessagesQueue.Enqueue($"{message.ViewName}: {message.Message}"));
        }

        public void Handle(IReconnect message)
        {
            if (_reconnecting) return;

            _reconnecting = true;
            _client = IoC.Get<ClientConnection>();
            Task.Factory.StartNew(() =>
            {
                Task.Run(_client.Init);
                if (ActiveItem == AfterLoginScreenViewModel)
                    _eventAggregator.PublishOnUIThread(new NotLoggedIn());
                _reconnecting = false;
            });
        }

        public void Handle(INotLoggedIn message)
        {
            _eventAggregator.PublishOnUIThread(
                new MessageQueue("Connection", "You are not anymore logged in"));
            _eventAggregator.PublishOnUIThread(NavigateMainView.Home);
            ActivateItem(BeforeLoginScreenViewModel);
        }
        #endregion Handle's
    }
}
