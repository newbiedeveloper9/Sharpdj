using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class MajorScreenViewModel : PropertyChangedBase
    {
        public NewsCarouselViewModel NewsCarouselViewModel { get; private set; }
        public RoomSquareViewModel RoomSquareViewModel { get; private set; }

        public MajorScreenViewModel()
        {
            RoomSquareViewModel = new RoomSquareViewModel();
            NewsCarouselViewModel = new NewsCarouselViewModel();
        }
    }
}
