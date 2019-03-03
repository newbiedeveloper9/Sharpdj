using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Communication.Client;
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

        private MessageListModel _parent;

        public MessageListModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

    }
}
