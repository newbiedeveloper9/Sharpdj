using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;
using Network.Interfaces;
using Network.Packets;

namespace SharpDj.Logic
{
    internal interface IAction
    {
        void RegisterPacket(Connection conn, object obj);
    }

    public abstract class ActionAbstract<TReq> : IAction where TReq : Packet
    {
        protected readonly PacketReceivedHandler<TReq> PacketHandler;

        protected ActionAbstract()
        {
            PacketHandler = async (packet, connection) =>
                await Action(packet, connection).ConfigureAwait(false);
        }

        public abstract Task Action(TReq req, Connection conn);

        public void RegisterPacket(Connection conn, object obj)
        {
            conn.RegisterPacketHandler(PacketHandler, obj);
        }
    }
}
