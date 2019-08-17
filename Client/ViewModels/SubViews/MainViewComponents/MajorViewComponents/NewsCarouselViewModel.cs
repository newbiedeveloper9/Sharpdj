using System.Linq;
using Caliburn.Micro;
using SCPackets.ConnectToRoom;
using SCPackets.Models;
using SharpDj.Enums;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents
{
    public class NewsCarouselViewModel : Screen,
        IHandle<IRoomInfoForOpen>
    {
        private readonly IEventAggregator _eventAggregator;

        public NewsCarouselViewModel()
        {
            ActiveRoom.Description = "xd";
            ActiveRoom.Title = "123";

            var dicPic = @"..\..\..\..\Images\1.jpg";

            RoomsCollection = new BindableCollection<NewsModel>()
            {
                new NewsModel(){ImageSource = dicPic},
                new NewsModel(){ImageSource = dicPic},
            };
        }

        public NewsCarouselViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        #region Properties

        private NewsModel _activeRoom = new NewsModel();
        public NewsModel ActiveRoom
        {
            get => _activeRoom;
            set
            {
                if (_activeRoom == value) return;
                _activeRoom = value;
                NotifyOfPropertyChange(() => ActiveRoom);
            }
        }

        private BindableCollection<NewsModel> _roomsCollection = new BindableCollection<NewsModel>();
        public BindableCollection<NewsModel> RoomsCollection
        {
            get => _roomsCollection;
            set
            {
                if (_roomsCollection == value) return;
                _roomsCollection = value;
                NotifyOfPropertyChange(() => RoomsCollection);
            }
        }

        #endregion

        private void WindowResize(object sender, System.Windows.SizeChangedEventArgs e)
        {
            //724 Primary +24left margin
            //248 Side + 24left margin + 24 right
            //220 left
            //20 shadow
            SideNewsVisibility = App.Current.MainWindow.ActualWidth > 1284 ?
                SideNewsVisibilityEnum.Right : SideNewsVisibilityEnum.Bottom;
        }

        protected override void OnViewLoaded(object view)
        {
            if (view == null) return;

            base.OnViewLoaded(view);
            App.Current.MainWindow.SizeChanged += WindowResize;
            WindowResize(null, null);
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

        public void OnActiveRoomClick()
        {
            ConnectToRoomRequest(ActiveRoom);
        }

        public void ConnectToRoomRequest(NewsModel model)
        {
            if (model.Id != -1)
                _eventAggregator.PublishOnUIThread(
                    new SendPacket(new ConnectToRoomRequest(model.Id)));
        }

        public void Handle(IRoomInfoForOpen message)
        {
            if (message.OutsideModel == null) return;

            var exist = RoomsCollection.FirstOrDefault(x => x.Id == message.OutsideModel.Id);
            if (exist != null)
                RoomsCollection.Remove(exist);

            if (RoomsCollection.Count >= 2)
                RoomsCollection.RemoveAt(1);
            if (ActiveRoom.Id != -1 && ActiveRoom.IsCreated)
                RoomsCollection.Insert(0, ActiveRoom);

            ActiveRoom = NewsModel.CreateModel(message.OutsideModel);
        }
    }
}
