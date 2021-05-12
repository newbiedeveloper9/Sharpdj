using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System.Windows.Controls;
using SCPackets.Packets.CreateRoomMessage;
using SCPackets.Packets.PullRoomChat;
using SharpDj.Logic.Helpers;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ChatViewModel : PropertyChangedBase,
        IHandle<INickColorChanged>, IHandle<IChatNewMessagePublish>, IHandle<IRoomChatMessageStatePublish>,
        IHandle<IEofPostsRoomPublish>, IHandle<IPullPostsRoomPublish>
    {
        #region _fields
        private readonly IEventAggregator _eventAggregator;
        private ScrollViewerLogic _scrollViewerLogic;
        #endregion _fields

        #region Properties

        private bool _canShowMorePosts = true;
        public bool CanShowMorePosts
        {
            get => _canShowMorePosts;
            set
            {
                if (_canShowMorePosts == value) return;
                _canShowMorePosts = value;
                NotifyOfPropertyChange(() => CanShowMorePosts);
            }
        }

        private BindableCollection<PostModel> _commentsCollection;
        public BindableCollection<PostModel> CommentsCollection
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

        private Color _textColor;
        public Color TextColor
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

            CommentsCollection = new BindableCollection<PostModel>();
        }

        public ChatViewModel()
        {
            CommentsCollection = new BindableCollection<PostModel>()
            {
                new PostModel()
                {
                     Author = new UserClient(0,"Crisey"/*, Rank.Admin*/),
                     Comment = "Testowa wiadomość",
                },
                new PostModel()
                {
                    Author = new UserClient(1,"Jeff Diggins"/*, Rank.Moderator*/),
                    Comment = "Druga wiadomość",
                },
                new PostModel()
                {
                    Author = new UserClient(2,"Zonk256"/*, Rank.User*/),
                    Comment = "Ostatnia wiadomość w celu przetestowania test test",
                },
            };

        }
        #endregion .ctor

        #region Methods


        public void ShowMorePosts()
        {
            _eventAggregator.PublishOnUIThread(
                new SendPacket(
                    new PullRoomChatRequest(UserInfoSingleton.Instance.ActiveRoom.Id, CommentsCollection.Count),
                    false));
        }

        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(ChatMessage)) return;

            _eventAggregator.PublishOnUIThread(
                new SendPacket(
                    new CreateRoomMessageRequest(TextColor, ChatMessage),
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
                new PostModel(message.Message.Post));
        }

        public void Handle(IRoomChatMessageStatePublish message)
        {
            if (message.Result == CreateRoomMessageResult.Success)
                ChatMessage = string.Empty;
        }
        public void Handle(IEofPostsRoomPublish message)
        {
            CanShowMorePosts = false;
        }

        public void Handle(IPullPostsRoomPublish message)
        {
            for (var i = 0; i < message.Posts.Count; i++)
            {
                var post = message.Posts[i];
                CommentsCollection.Insert(0+i, new PostModel(post));
            }
        }
        #endregion Handler's
    }
}