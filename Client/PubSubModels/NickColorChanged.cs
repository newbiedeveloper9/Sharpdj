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
        public NickColorChanged(ColorModel color)
        {
            Color = color;
        }
        public ColorModel Color { get; set; }
    }

    public interface INickColorChanged
    {
        ColorModel Color { get; set; }
    }
}
