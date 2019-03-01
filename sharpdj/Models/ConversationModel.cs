using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;

namespace SharpDj.Models
{
    public class ConversationModel : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private bool _isReaded;
        public bool IsReaded
        {
            get => _isReaded;
            set
            {
                if (_isReaded == value) return;
                _isReaded = value;
                NotifyOfPropertyChange(() => IsReaded);
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

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath == value) return;
                _imagePath = value;
                NotifyOfPropertyChange(() => ImagePath);
            }
        }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                NotifyOfPropertyChange(() => IsOpen);
            }
        }
    }
}
