using Caliburn.Micro;
using SCPackets.Models;

namespace SharpDj.Models
{
    public class PostModel : PropertyChangedBase
    {
        #region .ctor
        public PostModel(UserClient author, string comment, Color color, ulong id)
        {
            Author = author;
            Comment = comment;
            Color = color;
            Id = id;
        }

        public PostModel()
        {

        }

        public PostModel(ChatMessage post)
        {
            Author = post.Author;
            Comment = post.Message;
            Color = post.Color;
            Id = post.Id;
        }
        #endregion .ctor

        #region Properties
        private ulong _id;
        public ulong Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private UserClient _author;
        public UserClient Author
        {
            get => _author;
            set
            {
                if (_author == value) return;
                _author = value;
                NotifyOfPropertyChange(() => Author);
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;
                _comment = value;
                NotifyOfPropertyChange(() => Comment);
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }
        #endregion Properties
    }
}
