using Caliburn.Micro;
using Microsoft.Win32;
using SCPackets.CreateRoom;
using SharpDj.Enums;
using SharpDj.Interfaces;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System;
using System.Threading.Tasks;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class CreateRoomViewModel : PropertyChangedBase, INavMainView,
        IHandle<ICreatedRoomPublish>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        #endregion Fields

        #region Properties
        private RoomCreationModel _model = new RoomCreationModel();
        public RoomCreationModel Model
        {
            get => _model;
            set
            {
                if (_model == value) return;
                _model = value;
                NotifyOfPropertyChange(() => Model);
            }
        }

        private string _title = "Create your own room";
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private bool _createRoomIsVisible = true;
        public bool CreateRoomIsVisible
        {
            get => _createRoomIsVisible;
            set
            {
                if (_createRoomIsVisible == value) return;
                _createRoomIsVisible = value;
                NotifyOfPropertyChange(() => CreateRoomIsVisible);
            }
        }

        #endregion Properties

        #region .ctor
        public CreateRoomViewModel(IEventAggregator eventAggregator,
            string title="Create your own room", bool roomCreator = false)
        {
            Title = title;
            CreateRoomIsVisible = roomCreator;

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }


        public CreateRoomViewModel()
        {
        }
        #endregion .ctor

        #region Methods
        public void UploadLocalFile()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "All Image Files|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
                           "|PNG|*.PNG;*.png" +
                           "|JPEG|*.JPG;*.JPEG*.jpg;*.jpeg" +
                           "|Bitmap|*.BMP;*.bmp" +
                           "|GIF|*.GIF;*.gif" +
                           "|TIF|*.tif;*.tiff" +
                           "|ICO|*.ico;*.ICO"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Imgur imgur = new Imgur();
                imgur.XmlParsingError += (sender, args) => _eventAggregator.PublishOnUIThread(
                    new MessageQueue("Upload image", "Error occured while uploading local file."));
                imgur.UploadSuccess += (sender, args) => _eventAggregator.PublishOnUIThread(
                    new MessageQueue("Upload image", "Success"));

                Task.Factory.StartNew(() =>
                {
                    string imgUrl = imgur.AnonymousImageUpload(dlg.FileName);
                    Model.ImageLink = imgUrl;
                });
            }
        }

        public void CreateRoom()
        {
            _eventAggregator.PublishOnUIThread(new SendPacket(
                new CreateRoomRequest(Model.ToSCPacketRoomCreationModel())));
        }
        #endregion Methods

        #region Handle's
        public void Handle(ICreatedRoomPublish message)
        {
            Model = new RoomCreationModel();
            _eventAggregator.PublishOnUIThread(NavigateMainView.Room);
        }
        #endregion Handle's
    }
}
