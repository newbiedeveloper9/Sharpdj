using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents;

namespace SharpDj.ViewModels.SubViews
{
    public class MainViewModel : PropertyChangedBase
    {
        public TopMenuViewModel TopMenuViewModel { get; private set; }
        public LeftMenuViewModel LeftMenuViewModel { get; private set; }
        public NewsCarouselViewModel NewsCarouselViewModel { get; private set; }
        public RoomSquareViewModel RoomSquareViewModel { get; private set; }

        public MainViewModel()
        {
            RoomSquareViewModel = new RoomSquareViewModel();
            NewsCarouselViewModel = new NewsCarouselViewModel();
            LeftMenuViewModel = new LeftMenuViewModel();
            TopMenuViewModel = new TopMenuViewModel();
        }
    }
}
