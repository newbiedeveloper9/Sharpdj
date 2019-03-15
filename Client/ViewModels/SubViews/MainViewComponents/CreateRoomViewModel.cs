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
    public class CreateRoomViewModel : Screen, INavMainView, IHandle<ICreatedRoomPublish>
    {
        private readonly IEventAggregator _eventAggregator;

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


        public CreateRoomViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }


        public CreateRoomViewModel()
        {

        }

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

        public void Handle(ICreatedRoomPublish message)
        {
            Model = new RoomCreationModel();
            _eventAggregator.PublishOnUIThread(NavigateMainView.Room);
        }
    }
}
