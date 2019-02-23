using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class PlaylistViewModel : PropertyChangedBase,
        INavMainView,
        IHandle<IPlaylistChanged>
    {
        public BindableCollection<PlaylistModel> PlaylistCollection { get; private set; }
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

        public PlaylistViewModel()
        {
            var dicPic = @"C:\Users\Michal\Desktop\Icons\maxresdefault.jpg";

            PlaylistCollection = new BindableCollection<PlaylistModel>()
            {
                new PlaylistModel(){Name = "Testowa nazwa playlisty", IsActive = false},
                new PlaylistModel(){Name = "Testowa nazwa playlisty", IsActive = true},
                new PlaylistModel(){Name = "Testowa nazwa playlisty", IsActive = false},
                new PlaylistModel(){Name = "Testowa nazwa playlisty", IsActive = false},
            };
            PlaylistCollection[1].TrackCollection = new BindableCollection<TrackModel>()
            {
                new TrackModel(){Author = "Anime Openings", Duration = "4:03", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:03", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
                new TrackModel(){Author = "Anime Openings", Duration = "4:03", ImgSource = dicPic, Name = "Kimi no Na wa", TrackLink = "none"},
            };

            OnActivePlaylistChanged(PlaylistCollection[1]);
        }

        public void OnActivePlaylistChanged(PlaylistModel model)
        {
            TrackCollection = model.TrackCollection;
        }

        public void Handle(IPlaylistChanged message)
        {
            this.PlaylistCollection = new BindableCollection<PlaylistModel>(message.PlaylistCollection);
        }
    }
}
