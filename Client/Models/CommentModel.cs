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
    public class CommentModel : PropertyChangedBase
    {
        #region Properties
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
        public CommentModel(UserClientModel author, string comment, ColorModel color)
        {
            Author = author;
            Comment = comment;
            Color = color;
        }

        public CommentModel()
        {
            
        }

        public CommentModel(RoomChatNewMessageRequest request)
        {
            Author = request.Author;
            Comment = request.Message;
            Color = request.Color;
        }
        #endregion .ctor
    }
}
