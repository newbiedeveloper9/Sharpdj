using Caliburn.Micro;

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

        private Track _previousTrack;
        public Track PreviousTrack
        {
            get => _previousTrack;
            set
            {
                if (_previousTrack == value) return;
                _previousTrack = value;
                NotifyOfPropertyChange(() => PreviousTrack);
            }
        }

        private Track _currentTrack;
        public Track CurrentTrack
        {
            get => _currentTrack;
            set
            {
                if (_currentTrack == value) return;
                _currentTrack = value;
                NotifyOfPropertyChange(() => CurrentTrack);
            }
        }

        private Track _nextTrack;
        public Track NextTrack
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

        public class Track : PropertyChangedBase
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
        }
    }
}
