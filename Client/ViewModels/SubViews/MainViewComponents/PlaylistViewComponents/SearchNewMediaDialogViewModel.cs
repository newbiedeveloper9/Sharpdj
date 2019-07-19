using System;
using Caliburn.Micro;
using SharpDj.Interfaces;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using SharpDj.PubSubModels;
using SharpDj.Views.SubViews.MainViewComponents.PlaylistViewComponents;
using YoutubeExplode;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents
{
    public class SearchNewMediaDialogViewModel : PropertyChangedBase,
        IHandle<IPlaylistCollectionChanged>, IHandle<INewPlaylistCreated>
    {
        #region _fields
        private readonly IEventAggregator _eventAggregator;

        private const int Delay = 550;

        private string _delayedSearchText = "";
        private bool _searched = false;

        #endregion _fields

        #region Properties
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
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

        private string _titlebar = "Dodaj utwór";
        public string Titlebar
        {
            get => _titlebar;
            set
            {
                if (_titlebar == value) return;
                _titlebar = value;
                NotifyOfPropertyChange(() => Titlebar);
            }
        }


        private bool _mediaSearchIsVisible = true;
        public bool MediaSearchIsVisible
        {
            get => _mediaSearchIsVisible;
            set
            {
                Titlebar = value ? "Dodaj utwór" : "Dodaj utwór do playlisty";
                NotifyOfPropertyChange(() => Titlebar);

                if (_mediaSearchIsVisible == value) return;
                _mediaSearchIsVisible = value;
                NotifyOfPropertyChange(() => MediaSearchIsVisible);
            }
        }

        private TrackModel _temporaryTrack = new TrackModel();
        public TrackModel TemporaryTrack
        {
            get => _temporaryTrack;
            set
            {
                if (_temporaryTrack == value) return;
                _temporaryTrack = value;
                NotifyOfPropertyChange(() => TemporaryTrack);
            }
        }

        #endregion Properties

        #region .ctor
        public SearchNewMediaDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Task.Factory.StartNew(ContinuousSearchNewMedia);
        }

        public SearchNewMediaDialogViewModel()
        {

            PlaylistCollection.Add(new PlaylistModel()
            {
                IsActive = false,
                Name = "testowy test"
            });
            PlaylistCollection.Add(new PlaylistModel()
            {
                IsActive = false,
                Name = "drugi heheheheheh teścik"
            });
        }
        #endregion .ctor

        #region Methods

        public void SaveTrack(TrackModel model)
        {
            MediaSearchIsVisible = false;
            TemporaryTrack = model;

            foreach (var playlistModel in PlaylistCollection)
            {
                playlistModel.Contains = playlistModel.TrackCollection.Any(
                    x => x.TrackLink.Equals(model.TrackLink));
            }
        }

        #region Search
        public void FasterSearch()
        {
            MediaSearchIsVisible = true;
            _searched = false;
            _delayedSearchText = SearchText;
        }

        public async void ContinuousSearchNewMedia()
        {
            var client = new YoutubeClient();

            while (true)
            {
                try
                {
                    Thread.Sleep(Delay);

                    if (_delayedSearchText != SearchText)
                    {
                        _delayedSearchText = SearchText;
                        _searched = false;
                    }
                    else if (_delayedSearchText == SearchText && !_searched)
                    {
                        TrackCollection.Clear();

                        var id = YtVideoHelper.NormalizeVideoId(SearchText);
                        if (YoutubeClient.ValidateVideoId(id))
                        {
                            var video = await client.GetVideoAsync(id);
                            TrackCollection.Add(YtVideoHelper.ToTrackModel(video));
                        }
                        else
                        {
                            foreach (var video in await client.SearchVideosAsync(_delayedSearchText, 1))
                            {
                                TrackCollection.Add(YtVideoHelper.ToTrackModel(video));
                            }
                        }

                        Console.WriteLine("xd");
                        _searched = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        #endregion Search

        public void BackToSearch()
        {
            MediaSearchIsVisible = true;
            TemporaryTrack = null;
        }

        public void SaveToPlaylist(PlaylistModel model)
        {
            if (model.Contains)
                model.TrackCollection.Add(TemporaryTrack);
            else
                model.TrackCollection.Remove(TemporaryTrack);
        }
        #endregion Methods

        public void Handle(IPlaylistCollectionChanged message)
        {
            PlaylistCollection = new BindableCollection<PlaylistModel>(message.PlaylistCollection);
        }

        public void Handle(INewPlaylistCreated message)
        {
            PlaylistCollection.Add(message.Model);
        }
    }
}
