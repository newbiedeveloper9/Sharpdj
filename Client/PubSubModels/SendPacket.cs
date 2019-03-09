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
        public SendPacket(Packet packet)
        {
            Packet = packet;
        }

        public Packet Packet { get; set; }
    }

    public interface ISendPacket
    {
        Packet Packet { get; set; }
    }
}
