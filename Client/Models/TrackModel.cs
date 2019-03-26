using Caliburn.Micro;
using System;
using Newtonsoft.Json;

namespace SharpDj.Models
{
    public class TrackModel : PropertyChangedBase
    {
        [JsonProperty]
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

        [JsonProperty]
        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                if (_author == value) return;
                _author = value;
                NotifyOfPropertyChange(() => Author);
            }
        }

        [JsonProperty]
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                if (_duration == value) return;
                _duration = value;
                NotifyOfPropertyChange(() => Duration);
            }
        }

        [JsonProperty]
        private string _trackLink;
        public string TrackLink
        {
            get => _trackLink;
            set
            {
                if (_trackLink == value) return;
                _trackLink = value;
                NotifyOfPropertyChange(() => TrackLink);
            }
        }

        [JsonProperty]
        private string _imgSource;
        public string ImgSource
        {
            get => _imgSource;
            set
            {
                if (_imgSource == value) return;
                _imgSource = value;
                NotifyOfPropertyChange(() => ImgSource);
            }
        }

        public bool Equals(TrackModel other)
        {
            return string.Equals(_name, other._name) &&
                   string.Equals(_author, other._author) &&
                   _duration.Equals(other._duration) &&
                   string.Equals(_trackLink, other._trackLink) &&
                   string.Equals(_imgSource, other._imgSource);
        }

        public SCPackets.Models.TrackModel ToSCPacketTrackModel()
        {
            return new SCPackets.Models.TrackModel()
            {
                Author = Author,
                Duration = (int)Duration.TotalSeconds,
                ImageUrl = ImgSource,
                Name = Name,
                TrackUrl = TrackLink,
            };
        }

        public static TrackModel ToClientModel(SCPackets.Models.TrackModel model)
        {
            return new TrackModel()
            {
                Author = model.Author,
                Name = model.Name,
                Duration = TimeSpan.FromSeconds(model.Duration),
                ImgSource = model.ImageUrl,
                TrackLink = model.TrackUrl,
            };
        }
    }
}
