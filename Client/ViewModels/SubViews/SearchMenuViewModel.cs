using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using SharpDj.Enums;
using SharpDj.Models;
using SharpDj.PubSubModels;
using SharpDj.ViewModels.SubViews.SearchMenuComponents;
using SharpDj.Views.SubViews;

namespace SharpDj.ViewModels.SubViews
{
    public class SearchMenuViewModel : PropertyChangedBase,
        IHandle<RollingMenuVisibilityEnum>, IHandle<IMinimizeChatPublish>
    {
        private readonly IEventAggregator _eventAggregator;

        #region Properties
        private BindableCollection<ConversationModel> _conversationsCollection;
        public BindableCollection<ConversationModel> ConversationsCollection
        {
            get => _conversationsCollection;
            set
            {
                if (_conversationsCollection == value) return;
                _conversationsCollection = value;
                NotifyOfPropertyChange(() => ConversationsCollection);
            }
        }

        private RollingMenuVisibilityEnum _rollingMenuVisibility;
        public RollingMenuVisibilityEnum RollingMenuVisibility
        {
            get => _rollingMenuVisibility;
            set
            {
                if (_rollingMenuVisibility == value) return;
                _rollingMenuVisibility = value;
                NotifyOfPropertyChange(() => RollingMenuVisibility);
            }
        }
        #endregion Properties

        #region .ctor
        public SearchMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Design();
        }

        public SearchMenuViewModel()
        {
            Design();
        }

        private void Design()
        {
            var dicPic = "../../../Images/1.jpg";

            ConversationsCollection = new BindableCollection<ConversationModel>()
            {
                new ConversationModel(_eventAggregator){IsReaded = false, Color = Brushes.DeepPink, Name = "Test Diggins", ImagePath = dicPic},
                new ConversationModel(_eventAggregator){IsReaded = true, Color = Brushes.BlueViolet, Name = "Test Diggins", ImagePath = dicPic},
            };
        }
        #endregion .ctor


        #region Methods
        public void ConversationClick(ConversationModel model)
        {
            model.IsOpen = !model.IsOpen;
        }

        public void ConversationDeleteClick(ConversationModel model)
        {
            ConversationsCollection.Remove(model);
        }

        public void Home()
        {
            _eventAggregator.PublishOnUIThread(NavigateMainView.Home);
        }

        public void ShowOptionsPanel()
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Options);
        }

        public void ShowConversationsPanel()
        {
            _eventAggregator.PublishOnUIThread(RollingMenuVisibilityEnum.Conversations);
        }
        #endregion Methods

        #region Handle's
        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
        }

        public void Handle(IMinimizeChatPublish message)
        {
            var item = ConversationsCollection.FirstOrDefault(x => x.Equals(message.Model));
            if (item == null) return;

            item.IsOpen = false;
        }
        #endregion Handle's
    }
}
