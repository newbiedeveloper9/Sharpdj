using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : PropertyChangedBase
    {
        public TopMenuViewModel TopMenuViewModel { get; private set; } = new TopMenuViewModel();
        public LeftMenuViewModel LeftMenuViewModel { get; private set; } = new LeftMenuViewModel();
        public NewsCarouselViewModel NewsCarouselViewModel { get; private set; } = new NewsCarouselViewModel();
        public RoomSquareViewModel RoomSquareViewModel { get; private set; } = new RoomSquareViewModel();
    }
}
