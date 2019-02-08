using System;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : PropertyChangedBase
    {
        public TopMenuViewModel TopMenuViewModel { get; set; } = new TopMenuViewModel();
        public LeftMenuViewModel LeftMenuViewModel { get; set; } = new LeftMenuViewModel();
    }
}
