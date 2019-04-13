using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;
using SCPackets.LoginPacket;

namespace SharpDj.PubSubModels
{
    public class SendPacket : ISendPacket
    {
        public SendPacket(Packet packet, bool useInstance = true)
        {
            Packet = packet;
            UseInstance = useInstance;
        }

        public Packet Packet { get; set; }
        public bool UseInstance { get; set; }
    }

    public interface ISendPacket
    {
        Packet Packet { get; set; }
        bool UseInstance { get; set; }
    }
}
