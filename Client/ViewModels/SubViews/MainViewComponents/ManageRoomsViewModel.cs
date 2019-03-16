using System;
using Caliburn.Micro;
using SCPackets.UpdateRoomData;
using SharpDj.Interfaces;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ManageRoomsViewModel : PropertyChangedBase,
    INavMainView,
    IHandle<IManageRoomsPublish>
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        #endregion Fields

        #region Properties

        public CreateRoomViewModel CreateRoomViewModel { get; private set; }

        private bool _roomModifyIsVisible;
        public bool RoomModifyIsVisible
        {
            get => _roomModifyIsVisible;
            set
            {
                if (_roomModifyIsVisible == value) return;
                _roomModifyIsVisible = value;
                NotifyOfPropertyChange(() => RoomModifyIsVisible);
            }
        }

        private BindableCollection<RoomCreationModel> _serverCollection = new BindableCollection<RoomCreationModel>();
        public BindableCollection<RoomCreationModel> ServerCollection
        {
            get => _serverCollection;
            set
            {
                if (_serverCollection == value) return;
                _serverCollection = value;
                NotifyOfPropertyChange(() => ServerCollection);
            }
        }

        private bool _modifyBarIsVisible;
        public bool ModifyBarIsVisible
        {
            get => _modifyBarIsVisible;
            set
            {
                if (_modifyBarIsVisible == value) return;
                _modifyBarIsVisible = value;
                NotifyOfPropertyChange(() => ModifyBarIsVisible);
            }
        }

        #endregion Properties

        #region .ctor
        public ManageRoomsViewModel()
        {
            CreateRoomViewModel = new CreateRoomViewModel();
            ServerCollection.Add(new RoomCreationModel() { Name = "Test" });
            ServerCollection.Add(new RoomCreationModel() { Name = "Heheszki" });
        }

        public ManageRoomsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            CreateRoomViewModel = new CreateRoomViewModel(_eventAggregator, "Modify your room");
        }
        #endregion .ctor

        #region Methods

        public void OnManageRoomChanged(RoomCreationModel model)
        {
            RoomModifyIsVisible = true;
            ModifyBarIsVisible = true;

            CreateRoomViewModel.Model = model;
        }

        public void SaveRoom()
        {
            var model = CreateRoomViewModel.Model.ToSCPacketRoomCreationModel();
            _eventAggregator.PublishOnUIThread( new SendPacket(
                new UpdateRoomDataRequest(model)));
        }
        #endregion Methods

        #region  Handle's
        public void Handle(IManageRoomsPublish message)
        {
            ServerCollection.Clear();

            if (message.RoomModelsList == null) return;
            foreach (var roomModel in message.RoomModelsList)
            {
                ServerCollection.Add(
                    RoomCreationModel.ToClientModel(roomModel));
            }
        }

        #endregion Handle's
    }
}
