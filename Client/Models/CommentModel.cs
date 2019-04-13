using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SCPackets.Models;

namespace SharpDj.Models
{
    public class CommentModel : PropertyChangedBase
    {
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
    }
}
