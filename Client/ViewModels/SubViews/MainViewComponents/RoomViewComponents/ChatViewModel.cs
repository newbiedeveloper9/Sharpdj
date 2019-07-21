using System;
using Caliburn.Micro;
using SCPackets.Models;
using SCPackets.SendRoomChatMessage;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System.Windows.Controls;
using SCPackets.PullPostsInRoom;
using SharpDj.Logic.Helpers;
using Result = SCPackets.SendRoomChatMessage.Result;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ChatViewModel : PropertyChangedBase,
        IHandle<INickColorChanged>, IHandle<IChatNewMessagePublish>, IHandle<IRoomChatMessageStatePublish>
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

            CommentsCollection = new BindableCollection<CommentModel>();
        }

        public ChatViewModel()
        {
            CommentsCollection = new BindableCollection<CommentModel>()
            {
                new CommentModel()
                {
                     Author = new UserClientModel(0,"Crisey", Rank.Admin),
                     Comment = "Testowa wiadomość",
                },
                new CommentModel()
                {
                    Author = new UserClientModel(1,"Jeff Diggins", Rank.Moderator),
                    Comment = "Druga wiadomość",
                },
                new CommentModel()
                {
                    Author = new UserClientModel(2,"Zonk256", Rank.User),
                    Comment = "Ostatnia wiadomość w celu przetestowania test test",
                },
            };

        }
        #endregion .ctor

        #region Methods

        public bool CanShowMorePosts()
        {
            return true;
        }

        public void ShowMorePosts()
        {
            _eventAggregator.PublishOnUIThread(
                new SendPacket(
                    new PullPostsInRoomRequest(UserInfoSingleton.Instance.ActiveRoom.Id, CommentsCollection.Count),
                    false));
        }

        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(ChatMessage)) return;

            _eventAggregator.PublishOnUIThread(
                new SendPacket(
                    new SendRoomChatMessageRequest(TextColor, ChatMessage, UserInfoSingleton.Instance.ActiveRoom.Id),
                    false));
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

        #region Handler's
        public void Handle(INickColorChanged message)
        {
            TextColor = message.Color;
        }

        public void Handle(IChatNewMessagePublish message)
        {
            CommentsCollection.Add(
                new CommentModel(message.Message));
        }

        public void Handle(IRoomChatMessageStatePublish message)
        {
            if (message.Result == Result.Success)
                ChatMessage = string.Empty;
        }
        #endregion Handler's
    }
}
