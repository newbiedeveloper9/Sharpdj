using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : PropertyChangedBase
    {
        private TopMenuViewModel _topMenuViewModel = new TopMenuViewModel();
        public TopMenuViewModel TopMenuViewModel
        {
            get => _topMenuViewModel;
            set
            {
                if (Equals(value, _topMenuViewModel)) return;
                _topMenuViewModel = value;
                NotifyOfPropertyChange(() => TopMenuViewModel);
            }
        }
    }
}
