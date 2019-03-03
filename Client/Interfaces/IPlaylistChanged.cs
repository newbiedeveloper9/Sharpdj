using System.Collections.Generic;
using SharpDj.Models;

namespace SharpDj.Interfaces
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