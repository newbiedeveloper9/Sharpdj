using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.Interfaces;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ManageRoomsViewModel : PropertyChangedBase,
    INavMainView
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

        #endregion Properties

        #region .ctor
        public ManageRoomsViewModel()
        {
            CreateRoomViewModel = new CreateRoomViewModel();
        }

        public ManageRoomsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CreateRoomViewModel = new CreateRoomViewModel(_eventAggregator, "Modify your room");
        }
        #endregion .ctor

        #region Methods

        public void OnManageRoomChanged(RoomCreationModel model)
        {
            RoomModifyIsVisible = true;

            //CreateRoomViewModel.Model = model;
        }

        #endregion Methods
    }
}
