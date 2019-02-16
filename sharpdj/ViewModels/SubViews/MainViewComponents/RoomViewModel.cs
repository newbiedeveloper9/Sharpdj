using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.Views.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class RoomViewModel : Screen
    {
        private RoomView _view;
        public BindableCollection<CommentModel> CommentsCollection { get; private set; }

        public RoomViewModel()
        {
            CommentsCollection = new BindableCollection<CommentModel>()
            {
                new CommentModel(){Author = "Crisey", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Zonk256", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Jeff Diggins", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
            };
        }

        protected override void OnViewLoaded(object view)
        {
            _view = view as RoomView;
            _view.CanScrollDown += (sender, args) =>
                ChatToBottomIsVisible = _view.Test;

            base.OnViewLoaded(view);
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                if (_messageText == value) return;
                _messageText = value;
                NotifyOfPropertyChange(() => MessageText);
            }
        }

        private bool _chatToToBottomIsVisible = false;
        public bool ChatToBottomIsVisible
        {
            get => _chatToToBottomIsVisible;
            set
            {
                if (_chatToToBottomIsVisible == value) return;
                _chatToToBottomIsVisible = value;
                NotifyOfPropertyChange(() => ChatToBottomIsVisible);
            }
        }

        public void ChatToBottom()
        {
            _view.ChatScrollDown();
        }

        public void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText)) return;

            CommentsCollection.Add(new CommentModel() { Author = "Crisey", Comment = MessageText });
            MessageText = string.Empty;
        }
    }
}
