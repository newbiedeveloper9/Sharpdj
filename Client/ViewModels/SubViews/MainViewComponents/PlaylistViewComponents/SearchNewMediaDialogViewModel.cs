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
        private BindableCollection<TrackModel> _trackCollection;
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
            var dicPic = @"C:\Users\Michal\Desktop\Icons\maxresdefault.jpg";

            TrackCollection = new BindableCollection<TrackModel>()
            {
                new TrackModel(){Author = "Anime Openings", Duration = "4:01", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:02", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:02", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:02", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:03", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
            };
        }
    }
}
    