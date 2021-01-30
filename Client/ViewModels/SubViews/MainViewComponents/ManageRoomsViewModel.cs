using Caliburn.Micro;
using SCPackets.Packets.UpdateRoom;
using SharpDj.Interfaces;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ManageRoomsViewModel : PropertyChangedBase,
    INavMainView,
    IHandle<IManageRoomsPublish>,
    IHandle<IManageEditedRoomPublish>,
    IHandle<ICreatedRoomPublish>

    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private RoomCreationModel _saveModel;
        private RoomCreationModel _withoutChangedModel;
        #endregion Fields

        #region Properties

        public CreateRoomViewModel EditRoomViewModel { get; private set; }

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

        private CreateRoomViewModel _createRoomViewModel;
        public CreateRoomViewModel CreateRoomViewModel
        {
            get => _createRoomViewModel;
            set
            {
                if (_createRoomViewModel == value) return;
                _createRoomViewModel = value;
                NotifyOfPropertyChange(() => CreateRoomViewModel);
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
            EditRoomViewModel = new CreateRoomViewModel();
            ServerCollection.Add(new RoomCreationModel() { Name = "Test" });
            ServerCollection.Add(new RoomCreationModel() { Name = "Heheszki" });
        }

        public ManageRoomsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            EditRoomViewModel = new CreateRoomViewModel(_eventAggregator, "Modify your room");
            CreateRoomViewModel = new CreateRoomViewModel(_eventAggregator, roomCreator: true, titleIsVisible: true);
        }
        #endregion .ctor

        #region Methods

        public void OnManageRoomChanged(RoomCreationModel model)
        {
            if (model == null)
            {
                RoomModifyIsVisible = false;
                ModifyBarIsVisible = false;
                return;
            };

            RoomModifyIsVisible = true;
            ModifyBarIsVisible = true;

            _saveModel = RoomCreationModel.ToClientModel(model.ToSCPacketRoomCreationModel());
            _withoutChangedModel = model;
            EditRoomViewModel.Model = _saveModel;
        }

        public void SaveRoom()
        {
            if (_saveModel.Equals(_withoutChangedModel)) return;

            var model = _saveModel.ToSCPacketRoomCreationModel();

            _eventAggregator.PublishOnUIThread(new SendPacket(
                new UpdateRoomRequest(model)));
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

        public void Handle(IManageEditedRoomPublish message)
        {
            for (var i = 0; i < ServerCollection.Count; i++)
                if (ServerCollection[i].Id == message.Room.Id)
                    ServerCollection[i] = RoomCreationModel.ToClientModel(message.Room);

            _withoutChangedModel = RoomCreationModel.ToClientModel(message.Room);
        }

        public void Handle(ICreatedRoomPublish message)
        {
            ServerCollection.Add(
                RoomCreationModel.ToClientModel(message.Room));
        }
        #endregion Handle's
    }
}
