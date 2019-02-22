using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class PlaylistViewModel : PropertyChangedBase,
        INavMainView
    {
        public BindableCollection<Track> TracksCollection { get; private set; }

        public PlaylistViewModel()
        {
            var dicPic = "../../../Images/1.jpg";
            TracksCollection = new BindableCollection<Track>()
            {
                new Track(){Author = "Anime Openings", Duration = "4:03", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
            };
        }
    }
}
