using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : PropertyChangedBase
    {
        public MajorScreenViewModel MajorScreenViewModel { get; private set; }
        public RoomViewModel RoomViewModel { get; private set; }

        public MainViewModel()
        {
            MajorScreenViewModel = new MajorScreenViewModel();
            RoomViewModel = new RoomViewModel();
        }
    }
}
