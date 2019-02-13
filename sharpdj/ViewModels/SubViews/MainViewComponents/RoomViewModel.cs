using System;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class RoomViewModel : PropertyChangedBase
    {
        public BindableCollection<CommentModel> CommentsCollection { get; private set; }

        public RoomViewModel()
        {
            CommentsCollection = new BindableCollection<CommentModel>()
            {
                new CommentModel(){Author = "Crisey", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Zonk256", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Jeff Diggins", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
            };
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

        public void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText)) return;

            CommentsCollection.Add(new CommentModel(){Author = "Crisey", Comment = MessageText});
            MessageText = string.Empty;
        }
    }
}
