using System;
using Caliburn.Micro;

namespace SharpDj.Models
{
    public class TrackModel : PropertyChangedBase
    {
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

        private int _duration;

        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration == value) return;
                _duration = value;
                NotifyOfPropertyChange(() => Duration);
            }
        }

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

        public SCPackets.Models.TrackModel ToSCPacketTrackModel()
        {
            return new SCPackets.Models.TrackModel()
            {
                Author = this.Author,
                Duration =  this.Duration,
                ImageUrl = this.ImgSource,
                Name = this.Name,
                TrackUrl =  this.TrackLink,
            };
        }

        public static TrackModel ToClientModel(SCPackets.Models.TrackModel model)
        {
            return new TrackModel()
            {
                Author = model.Author,
                Name = model.Name,
                Duration = model.Duration,
            ImgSource = model.ImageUrl,
                TrackLink = model.TrackUrl,
            };
        }
    }
}
