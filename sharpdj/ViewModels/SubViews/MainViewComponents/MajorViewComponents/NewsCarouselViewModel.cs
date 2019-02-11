using Caliburn.Micro;
using SharpDj.Enums;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents
{
    public class NewsCarouselViewModel : Screen
    {
        public NewsModel PrimaryNews { get; set; } = new NewsModel();
        public BindableCollection<NewsModel> NewsCollection { get; private set; }

        public NewsCarouselViewModel()
        {
            PrimaryNews.Description = "xd";
            PrimaryNews.Title = "123";

            var dicPic = @"..\..\..\..\Images\1.jpg";

            NewsCollection = new BindableCollection<NewsModel>()
            {
                new NewsModel(){ImageSource = dicPic},
                new NewsModel(){ImageSource = dicPic},
            };
        }

        private void WindowResize(object sender, System.Windows.SizeChangedEventArgs e)
        {
            //724 Primary +24left margin
            //248 Side + 24left margin + 24 right
            //220 left
            //20 shadow
            SideNewsVisibility = App.Current.MainWindow.ActualWidth > 1284 ?  SideNewsVisibilityEnum.Right : SideNewsVisibilityEnum.Bottom;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            App.Current.MainWindow.SizeChanged += WindowResize;
        }

        private SideNewsVisibilityEnum _sideNewsVisibility;
        public SideNewsVisibilityEnum SideNewsVisibility
        {
            get => _sideNewsVisibility;
            set
            {
                if (_sideNewsVisibility == value) return;
                _sideNewsVisibility = value;
                NotifyOfPropertyChange(() => SideNewsVisibility);
            }
        }
    }
}
