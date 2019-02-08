using System;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews;

namespace SharpDj.ViewModels
{
    class ShellViewModel : PropertyChangedBase, IShell
    {
        public MainViewModel MainViewModel { get; set; } = new MainViewModel();
    }
}
