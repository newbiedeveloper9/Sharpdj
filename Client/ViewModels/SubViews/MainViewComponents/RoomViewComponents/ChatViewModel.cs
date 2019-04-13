using Caliburn.Micro;
using SCPackets.Models;
using SCPackets.SendRoomChatMessage;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System.Windows.Controls;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ChatViewModel : PropertyChangedBase,
        IHandle<INickColorChanged>
    {
        #region _fields
        private readonly IEventAggregator _eventAggregator;
        private ScrollViewerLogic _scrollViewerLogic;

        #endregion _fields

        #region Properties

        private BindableCollection<CommentModel> _commentsCollection;
        public BindableCollection<CommentModel> CommentsCollection
        {
            get => _commentsCollection;
            set
            {
                if (_commentsCollection == value) return;
                _commentsCollection = value;
                NotifyOfPropertyChange(() => CommentsCollection);
            }
        }

        private bool _scrollToBottomIsVisible;
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

        private string _chatMessage;
        public string ChatMessage
        {
            get => _chatMessage;
            set
            {
                if (_chatMessage == value) return;
                _chatMessage = value;
                NotifyOfPropertyChange(() => ChatMessage);
            }
        }

        private ColorModel _textColor;
        public ColorModel TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor == value) return;
                _textColor = value;
                NotifyOfPropertyChange(() => TextColor);
            }
        }

        #endregion Properties

        #region .ctor
        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public ChatViewModel()
        {
            CommentsCollection = new BindableCollection<CommentModel>()
            {
                new CommentModel()
                {
                     Author = new UserClient(0,"Crisey", Rank.Admin),
                     Comment = "Testowa wiadomość",
                },
                new CommentModel()
                {
                    Author = new UserClient(1,"Jeff Diggins", Rank.Moderator),
                    Comment = "Druga wiadomość",
                },
                new CommentModel()
                {
                    Author = new UserClient(2,"Zonk256", Rank.User),
                    Comment = "Ostatnia wiadomość w celu przetestowania",
                },
            };

        }
        #endregion .ctor

        #region Methods
        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(ChatMessage)) return;

            _eventAggregator.PublishOnUIThread(
                new SendPacket(new SendRoomChatMessageRequest(ChatMessage, TextColor), false));
        }

        public void ScrollToBottom()
        {
            _scrollViewerLogic.ScrollToDown();
        }

        public void ScrollLoaded(ScrollViewer scrollViewer)
        {
            _scrollViewerLogic = new ScrollViewerLogic(scrollViewer);
            _scrollViewerLogic.ScrollNotOnBottom +=
                (sender, args) => ScrollToBottomIsVisible = !_scrollViewerLogic.CanScrollDown;
        }
        #endregion Methods

        public void Handle(INickColorChanged message)
        {
            TextColor = message.Color;
        }
    }
}
