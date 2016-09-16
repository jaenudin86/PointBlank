using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.LOBBY
{
    public class PROTOCOL_LOBBY_QUICKJOIN_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH((short)3078);
            WriteB(new byte[8]
            {
                (byte) 4,
                (byte) 0,
                (byte) 6,
                (byte) 12,
                (byte) 0,
                (byte) 0,
                (byte) 0,
                (byte) 128
            });
        }
    }
}
