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
        public ConversationModel()
        {

        }

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

        private bool _badgeIsVisible;
        public bool BadgeIsVisible
        {
            get => _badgeIsVisible;
            set
            {
                if (_badgeIsVisible == value) return;
                _badgeIsVisible = value;
                NotifyOfPropertyChange(() => BadgeIsVisible);
            }
        }

        private int _badgeCount;
        public int BadgeCount
        {
            get => _badgeCount;
            set
            {
                if (_badgeCount == value) return;
                _badgeCount = value;
                NotifyOfPropertyChange(() => BadgeCount);
                BadgeIsVisible = value > 0;
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
    }
}
