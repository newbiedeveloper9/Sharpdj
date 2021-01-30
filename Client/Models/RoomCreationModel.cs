using Caliburn.Micro;
using SharpDj.Common.DTO;

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

        public static RoomCreationModel ToClientModel(RoomDetailsDTO model)
        {
            return new RoomCreationModel()
            {
                Id = model.Id,
                ImageLink = model.ImageUrl,
                LocalEnterMessage = model.RoomConfigDTO.LocalEnterMessage,
                LocalLeaveMessage = model.RoomConfigDTO.LocalLeaveMessage,
                Name = model.Name,
                PublicEnterMessage = model.RoomConfigDTO.PublicEnterMessage,
                PublicLeaveMessage = model.RoomConfigDTO.PublicLeaveMessage
            };
        }

        public RoomDetailsDTO ToSCPacketRoomCreationModel()
        {
            return new RoomDetailsDTO()
            {
                Id = Id,
                ImageUrl = ImageLink,
                Name = Name,
                RoomConfigDTO = new RoomConfigDTO()
                {
                    LocalEnterMessage = LocalEnterMessage,
                    LocalLeaveMessage = LocalLeaveMessage,
                    PublicEnterMessage = PublicEnterMessage,
                    PublicLeaveMessage = PublicLeaveMessage
                }
            };
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RoomCreationModel);
        }

        protected bool Equals(RoomCreationModel other)
        {
            return _id == other._id && string.Equals(_name, other._name) &&
                   string.Equals(_imageLink, other._imageLink) &&
                   string.Equals(_localEnterMessage, other._localEnterMessage) &&
                   string.Equals(_localLeaveMessage, other._localLeaveMessage) && 
                   string.Equals(_publicEnterMessage, other._publicEnterMessage) &&
                   string.Equals(_publicLeaveMessage, other._publicLeaveMessage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _id;
                hashCode = (hashCode * 397) ^ (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_imageLink != null ? _imageLink.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_localEnterMessage != null ? _localEnterMessage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_localLeaveMessage != null ? _localLeaveMessage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_publicEnterMessage != null ? _publicEnterMessage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_publicLeaveMessage != null ? _publicLeaveMessage.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
