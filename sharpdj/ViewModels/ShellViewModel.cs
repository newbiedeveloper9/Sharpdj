using System;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels
{
    class ShellViewModel : PropertyChangedBase, IShell
    {
        public MainViewModel MainViewModel { get; set; }
        public TopMenuViewModel TopMenuViewModel { get; private set; }
        public LeftMenuViewModel LeftMenuViewModel { get; private set; }

        public ShellViewModel()
        {
            MainViewModel = new MainViewModel();
            LeftMenuViewModel = new LeftMenuViewModel();
            TopMenuViewModel = new TopMenuViewModel();
        }
    }
}
