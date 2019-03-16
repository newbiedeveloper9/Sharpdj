using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SharpDj.Models
{
    public class RoomCreationModel : PropertyChangedBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _imageLink;
        public string ImageLink
        {
            get => _imageLink;
            set
            {
                if (_imageLink == value) return;
                _imageLink = value;
                NotifyOfPropertyChange(() => ImageLink);
            }
        }

        private string _localEnterMessage;
        public string LocalEnterMessage
        {
            get => _localEnterMessage;
            set
            {
                if (_localEnterMessage == value) return;
                _localEnterMessage = value;
                NotifyOfPropertyChange(() => LocalEnterMessage);
            }
        }

        private string _localLeaveMessage;
        public string LocalLeaveMessage
        {
            get => _localLeaveMessage;
            set
            {
                if (_localLeaveMessage == value) return;
                _localLeaveMessage = value;
                NotifyOfPropertyChange(() => LocalLeaveMessage);
            }
        }

        private string _publicEnterMessage;
        public string PublicEnterMessage
        {
            get => _publicEnterMessage;
            set
            {
                if (_publicEnterMessage == value) return;
                _publicEnterMessage = value;
                NotifyOfPropertyChange(() => PublicEnterMessage);
            }
        }

        private string _publicLeaveMessage;
        public string PublicLeaveMessage
        {
            get => _publicLeaveMessage;
            set
            {
                if (_publicLeaveMessage == value) return;
                _publicLeaveMessage = value;
                NotifyOfPropertyChange(() => PublicLeaveMessage);
            }
        }

        public static RoomCreationModel ToClientModel(SCPackets.CreateRoom.Container.RoomModel model)
        {
            return new RoomCreationModel()
            {
                Id = model.Id,
                ImageLink = model.ImageUrl,
                LocalEnterMessage = model.LocalEnterMessage,
                LocalLeaveMessage = model.LocalLeaveMessage,
                Name = model.Name,
                PublicEnterMessage = model.PublicEnterMessage,
                PublicLeaveMessage = model.PublicLeaveMessage
            };
        }

        public SCPackets.CreateRoom.Container.RoomModel ToSCPacketRoomCreationModel()
        {
            return new SCPackets.CreateRoom.Container.RoomModel()
            {
                Id = this.Id,
                ImageUrl = this.ImageLink,
                LocalEnterMessage = this.LocalEnterMessage,
                LocalLeaveMessage = this.LocalLeaveMessage,
                Name = this.Name,
                PublicEnterMessage = this.PublicEnterMessage,
                PublicLeaveMessage = this.PublicLeaveMessage
            };
        }
    }
}
