using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SharpDj.Models
{
    public class PlaylistModel : PropertyChangedBase
    {
        public PlaylistModel(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        public PlaylistModel(string name)
        {
            Name = name;
        }

        public PlaylistModel()
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


        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                NotifyOfPropertyChange(() => IsActive);
            }
        }
    }
}
