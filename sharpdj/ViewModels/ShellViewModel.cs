using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews;

namespace SharpDj.ViewModels
{
    class ShellViewModel : PropertyChangedBase, IShell
    {
        private MainViewModel _mainViewModel = new MainViewModel();
        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set
            {
                if (Equals(value, _mainViewModel)) return;
                _mainViewModel = value;
                NotifyOfPropertyChange(() => MainViewModel);
            }
        }
    }
}
