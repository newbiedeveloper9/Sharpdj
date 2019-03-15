using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using SharpDj.Interfaces;
using SharpDj.Models;
using SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class PlaylistViewModel : PropertyChangedBase,
        INavMainView,
        IHandle<IPlaylistChanged>
    {
        private readonly IEventAggregator _eventAggregator;

        #region Properties
        public SearchNewMediaDialogViewModel SearchNewMediaDialogViewModel { get; private set; }

        private BindableCollection<PlaylistModel> _playlistCollection = new BindableCollection<PlaylistModel>();
        public BindableCollection<PlaylistModel> PlaylistCollection
        {
            get => _playlistCollection;
            set
            {
                if (_playlistCollection == value) return;
                _playlistCollection = value;
                NotifyOfPropertyChange(() => PlaylistCollection);
            }
        }
      
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
        #endregion Properties

        public PlaylistViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            SearchNewMediaDialogViewModel = new SearchNewMediaDialogViewModel();
        }

        public PlaylistViewModel()
        {
            SearchNewMediaDialogViewModel = new SearchNewMediaDialogViewModel();
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
