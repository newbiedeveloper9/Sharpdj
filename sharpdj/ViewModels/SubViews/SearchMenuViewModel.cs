using System;
using System.Windows.Media;
using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews
{
    public class SearchMenuViewModel : PropertyChangedBase, IHandle<RollingMenuVisibilityEnum>
    {
        private readonly IEventAggregator _eventAggregator;
        public BindableCollection<ConversationModel> ConversationsCollection { get; private set; }

        public SearchMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            var dicPic = "../../../Images/1.jpg";

            ConversationsCollection = new BindableCollection<ConversationModel>()
            {
                new ConversationModel(){IsReaded = false, Color = Brushes.DeepPink, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(){IsReaded = true, Color = Brushes.BlueViolet, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(){IsReaded = true, Color = Brushes.Black, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(){IsReaded = false, Color = Brushes.LimeGreen, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(){IsReaded = false, Color = Brushes.DeepSkyBlue, Name = "Jeff Diggins", ImagePath = dicPic},
                new ConversationModel(){IsReaded = false, Color = Brushes.Gray, Name = "Jeff Diggins", ImagePath = dicPic},
            };
        }

        public SearchMenuViewModel()
        {
            
        }

        public void ConversationClick()
        {
            Console.WriteLine("xd");
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
    }
}
