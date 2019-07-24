using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SCPackets.Models;
using SCPackets.RoomChatNewMessageClient;

namespace SharpDj.Models
{
    public class PostModel : PropertyChangedBase
    {
        #region Properties
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private UserClientModel _author;
        public UserClientModel Author
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

        private ColorModel _color;
        public ColorModel Color
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

        #region .ctor
        public PostModel(UserClientModel author, string comment, ColorModel color, int id)
        {
            Author = author;
            Comment = comment;
            Color = color;
            Id = id;
        }

        public PostModel()
        {
            
        }

        public PostModel(RoomPostModel post)
        {
            Author = post.Author;
            Comment = post.Message;
            Color = post.Color;
            Id = post.Id;
        }
        #endregion .ctor
    }
}
