using System.Collections.Generic;
using System.Windows.Documents;
using SharpDj.Enums.Playlist;
using SharpDj.Models;

namespace SharpDj
{
    public class PlaylistChangedData : IPlaylistChanged {
        public List<PlaylistModel> PlaylistCollection { get; set; }

        public PlaylistChangedData(List<PlaylistModel> playlistCollection)
        {
            this.PlaylistCollection = playlistCollection;
        }
    }

    public interface IPlaylistChanged
    {
        List<PlaylistModel> PlaylistCollection { get; set; }
    }
}