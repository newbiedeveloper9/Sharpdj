using Caliburn.Micro;
using SCPackets.Models;

namespace SharpDj.Models
{
    public class RoomModel : PropertyChangedBase
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

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource == value) return;
                _imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }

        private Activity _status;
        public Activity Status
        {
            get => _status;
            set
            {
                if (_status == value) return;
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }


        private int _amountOfPeople;
        public int AmountOfPeople
        {
            get => _amountOfPeople;
            set
            {
                if (_amountOfPeople == value) return;
                _amountOfPeople = value;
                NotifyOfPropertyChange(() => AmountOfPeople);
            }
        }

        private int _amountOfAdministration;
        public int AmountOfAdministration
        {
            get => _amountOfAdministration;
            set
            {
                if (_amountOfAdministration == value) return;
                _amountOfAdministration = value;
                NotifyOfPropertyChange(() => AmountOfAdministration);
            }
        }

        private TrackModel _previousTrack;
        public TrackModel PreviousTrack
        {
            get => _previousTrack;
            set
            {
                if (_previousTrack == value) return;
                _previousTrack = value;
                NotifyOfPropertyChange(() => PreviousTrack);
            }
        }

        private TrackModel _currentTrack;
        public TrackModel CurrentTrack
        {
            get => _currentTrack;
            set
            {
                if (_currentTrack == value) return;
                _currentTrack = value;
                NotifyOfPropertyChange(() => CurrentTrack);
            }
        }

        private TrackModel _nextTrack;
        public TrackModel NextTrack
        {
            get => _nextTrack;
            set
            {
                if (_nextTrack == value) return;
                _nextTrack = value;
                NotifyOfPropertyChange(() => NextTrack);
            }
        }

        public enum Activity
        {
            Active,
            Sleep,
            InActive
        }

        public RoomOutsideModel ToSCPacketOutsideModel()
        {
            return new RoomOutsideModel()
            {
                Id = this.Id,
                Name = this.Name,
                AmountOfAdministration = this.AmountOfAdministration,
                AmountOfPeople = this.AmountOfPeople,
                ImagePath = this.ImageSource,
                CurrentTrack = this.CurrentTrack.ToSCPacketTrackModel(),
                NextTrack = this.NextTrack.ToSCPacketTrackModel(),
                PreviousTrack = this.PreviousTrack.ToSCPacketTrackModel()
            };
        }

        public static RoomModel ToClientModel(RoomOutsideModel model)
        {
            return new RoomModel()
            {
                Id = model.Id,
                Name = model.Name,
                AmountOfAdministration = model.AmountOfAdministration,
                AmountOfPeople = model.AmountOfPeople,
                ImageSource = model.ImagePath,
                CurrentTrack = TrackModel.ToClientModel(model.CurrentTrack),
                NextTrack = TrackModel.ToClientModel(model.NextTrack),
                PreviousTrack = TrackModel.ToClientModel(model.PreviousTrack),
            };
        }
    }
}
