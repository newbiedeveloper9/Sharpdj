using Caliburn.Micro;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.PlaylistViewComponents
{
    public class SearchNewMediaDialogViewModel : PropertyChangedBase
    {
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

        private const int Delay = 700;

        public SearchNewMediaDialogViewModel()
        {
            Task.Factory.StartNew(ContinuousSearchNewMedia);
        }


        string delayedSearchText = "";
        bool searched = false;

        public void FasterSearch()
        {
            searched = false;
            delayedSearchText = SearchText;
        }

        public async void ContinuousSearchNewMedia()
        {
            var client = new YoutubeClient();

            while (true)
            {
                Thread.Sleep(Delay);

                if (delayedSearchText != SearchText)
                {
                    delayedSearchText = SearchText;
                    searched = false;
                }
                else if (delayedSearchText == SearchText && !searched)
                {
                    TrackCollection.Clear();

                    var id = YtVideoHelper.NormalizeVideoId(SearchText);
                    if (YoutubeClient.ValidateVideoId(id))
                    {
                        var video = await client.GetVideoAsync(id);
                        TrackCollection.Add(
                            YtVideoHelper.ToTrackModel(video));
                    }
                    else
                    {
                        foreach (var video in await client.SearchVideosAsync(delayedSearchText, 1))
                        {
                            TrackCollection.Add(
                                YtVideoHelper.ToTrackModel(video));
                        }
                    }

                    searched = true;
                }
            }
        }
    }
}
