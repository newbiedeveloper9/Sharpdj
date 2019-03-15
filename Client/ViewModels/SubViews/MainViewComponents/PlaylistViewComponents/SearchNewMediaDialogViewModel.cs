using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents
{
    public class SearchNewMediaDialogViewModel : PropertyChangedBase
    {
        private BindableCollection<TrackModel> _trackCollection = new BindableCollection<TrackModel>();
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

        public SearchNewMediaDialogViewModel()
        {

        }
    }
}
    