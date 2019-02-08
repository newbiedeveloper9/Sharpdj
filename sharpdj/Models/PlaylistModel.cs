using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.Models
{
    public class PlaylistModel
    {
        public PlaylistModel(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        public PlaylistModel(string name)
        {
            Name = name;
        }

        public PlaylistModel()
        {
            
        }

        public string Name { get; set; } 
        public bool IsActive { get; set; }
    }
}
