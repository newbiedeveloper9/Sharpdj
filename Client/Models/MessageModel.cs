using System;
using System.Windows.Media;
using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.Logic.Helpers;

namespace SharpDj.Models
{
    public class MessageModel : PropertyChangedBase
    {
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

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value;
                NotifyOfPropertyChange(() => Text);
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

                if (UserInfoSingleton.Instance.UserClient.Equals(Author))
                    IsOwnMessage = true;
            }
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                NotifyOfPropertyChange(() => Time);
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
            }
        }

        private bool _isOwnMessage;
        public bool IsOwnMessage
        {
            get => _isOwnMessage;
            set
            {
                if (_isOwnMessage == value) return;
                _isOwnMessage = value;
                NotifyOfPropertyChange(() => IsOwnMessage);
            }
        }

        private bool _separator;
        public bool Separator
        {
            get => _separator;
            set
            {
                if (_separator == value) return;
                _separator = value;
                NotifyOfPropertyChange(() => Separator);
            }
        }
    }
}