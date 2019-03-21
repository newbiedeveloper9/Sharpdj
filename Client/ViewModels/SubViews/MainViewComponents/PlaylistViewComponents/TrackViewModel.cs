
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents
{
    public class TrackViewModel : PropertyChangedBase
    {
        private TrackModel _track;
        public TrackModel Track
        {
            get => _track;
            set
            {
                if (_track == value) return;
                _track = value;
                NotifyOfPropertyChange(() => Track);
            }
        }



        public TrackViewModel()
        {
            
        }


    }
}
