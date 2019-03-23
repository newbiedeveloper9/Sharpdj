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

        public BindableCollection<ConversationModel> ConversationsCollection { get; private set; }


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
                new ConversationModel(_eventAggregator){IsReaded = true, Color = Brushes.Black, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(_eventAggregator){IsReaded = false, Color = Brushes.LimeGreen, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(_eventAggregator){IsReaded = false, Color = Brushes.DeepSkyBlue, Name = "Test Diggins", ImagePath = dicPic},
                new ConversationModel(_eventAggregator){IsReaded = false, Color = Brushes.Gray, Name = "Test Diggins", ImagePath = dicPic},
            };

          //  ConversationsCollection[0].ConversationPopupViewModel.MessageCollection.Add(new MessageModel(){Text = "test"});
        }

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

        public void Handle(RollingMenuVisibilityEnum message)
        {
            RollingMenuVisibility = RollingMenuVisibility != message ? message : RollingMenuVisibilityEnum.Void;
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

        public void Handle(IMinimizeChatPublish message)
        {
            var item = ConversationsCollection.FirstOrDefault(x => x.Equals(message.Model));
            if (item == null) return;

            item.IsOpen = false;
        }
    }
}
