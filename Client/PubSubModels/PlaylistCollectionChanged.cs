using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.PubSubModels
{
    public class PlaylistCollectionChanged : IPlaylistCollectionChanged {
        public BindableCollection<PlaylistModel> PlaylistCollection { get; set; }

        public PlaylistCollectionChanged(BindableCollection<PlaylistModel> playlistCollection)
        {
            this.PlaylistCollection = playlistCollection;
        }
    }

    public interface IPlaylistCollectionChanged
    {
        BindableCollection<PlaylistModel> PlaylistCollection { get; set; }
    }
}