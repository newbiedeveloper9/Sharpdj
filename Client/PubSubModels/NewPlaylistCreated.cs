using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Models;

namespace SharpDj.PubSubModels
{
    public class NewPlaylistCreated : INewPlaylistCreated
    {
        public PlaylistModel Model { get; set; }

        public NewPlaylistCreated(PlaylistModel model)
        {
            Model = model;
        }
    }

    public interface INewPlaylistCreated
    {
        PlaylistModel Model { get; set; }
    }
}
