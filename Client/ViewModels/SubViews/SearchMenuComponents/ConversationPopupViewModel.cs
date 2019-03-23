using Caliburn.Micro;
using SharpDj.Logic.Helpers;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharpDj.ViewModels.SubViews.SearchMenuComponents
{
    public class ConversationPopupViewModel : PropertyChangedBase
    {
        #region _fields
        private readonly IEventAggregator _eventAggregator;
        private ScrollViewerLogic _scrollViewerLogic;

        #endregion _fields

        #region Properties
        public ConversationModel Parent { get; set; }


        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if (_message == value) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private MessageBindable<MessageModel> _messagesCollection = new MessageBindable<MessageModel>();
        public MessageBindable<MessageModel> MessagesCollection
        {
            get => _messagesCollection;
            set
            {
                if (_messagesCollection == value) return;
                _messagesCollection = value;
                NotifyOfPropertyChange(() => MessagesCollection);
            }
        }

        private bool _scrollToBottomIsVisible = false;
        public bool ScrollToBottomIsVisible
        {
            get => _scrollToBottomIsVisible;
            set
            {
                if (_scrollToBottomIsVisible == value) return;
                _scrollToBottomIsVisible = value;
                NotifyOfPropertyChange(() => ScrollToBottomIsVisible);
            }
        }

        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                NotifyOfPropertyChange(() => Color);
                foreach (var messageModel in MessagesCollection)
                {
                    messageModel.Color = value;
                }
            }
        }

        #endregion Properties

        #region .ctor
        public ConversationPopupViewModel()
        {
        }

        public ConversationPopupViewModel(IEventAggregator eventAggregator, ConversationModel parent)
        {
            _eventAggregator = eventAggregator;
            Parent = parent;
        }
        #endregion .ctor

        #region Methods
        public void Minimize()
        {
            _eventAggregator.PublishOnUIThread(new MinimizeChatPublish(Parent));
        }

        public void ScrollLoaded(ScrollViewer scrollViewer)
        {
            _scrollViewerLogic = new ScrollViewerLogic(scrollViewer);
            _scrollViewerLogic.ScrollNotOnBottom +=
                (sender, args) => ScrollToBottomIsVisible = !_scrollViewerLogic.CanScrollDown;
        }

        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(Message)) return;

            MessagesCollection.Add(new MessageModel()
            {
                Author = UserInfoSingleton.Instance.UserClient,
                Text = Message,
                Time = DateTime.Now,
                Color = Color
            });
            Message = string.Empty;
        }
        public void ScrollToBottom()
        {
            _scrollViewerLogic.ScrollToDown();
        }
        #endregion Methods
    }

    public class MessageBindable<T> : BindableCollection<Models.MessageModel>
    {
        protected override void InsertItemBase(int index, Models.MessageModel item)
        {
            if (!(item is Models.MessageModel model)) return;

            if (Count > 0)
            {
                MessageModel lastMess = Items[Count - 1];
                if (lastMess.Author.Equals(model.Author))
                {
                    lastMess.Separator = false;
                }
            }
            item.Separator = true;

            base.InsertItemBase(index, item);
        }
    }
}
