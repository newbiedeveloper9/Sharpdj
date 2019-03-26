using Caliburn.Micro;
using Newtonsoft.Json;

namespace SharpDj.Models
{
    public class PlaylistModel : PropertyChangedBase
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
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                NotifyOfPropertyChange(() => IsActive);
            }
        }

        [JsonProperty]
        private BindableCollection<TrackModel> _trackCollection =
            new BindableCollection<TrackModel>();
        public BindableCollection<TrackModel> TrackCollection
        {
            get => _trackCollection;
            set
            {
                if (_trackCollection == value) return;
                _trackCollection = value;
                NotifyOfPropertyChange(() => TrackCollection);
            }
        }

        [JsonIgnore]
        private bool _contains;
        public bool Contains
        {
            get => _contains;
            set
            {
                if (_contains == value) return;
                _contains = value;
                NotifyOfPropertyChange(() => Contains);
            }
        }

        public bool Equals(PlaylistModel other)
        {
            return string.Equals(_name, other._name)
                   && _isActive == other._isActive
                   && Equals(_trackCollection, other._trackCollection)
                   && _contains == other._contains;
        }
    }
}
