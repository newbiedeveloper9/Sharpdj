using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets.Models;

namespace SharpDj.PubSubModels
{
    public class NickColorChanged : INickColorChanged
    {
        public NickColorChanged(Color color)
        {
            Color = color;
        }
        public Color Color { get; set; }
    }

    public interface INickColorChanged
    {
        Color Color { get; set; }
    }
}
