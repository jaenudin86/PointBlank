using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.LOBBY
{
    public class PROTOCOL_LOBBY_ROOM_PASSWORD_ERROR_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(3082);
            WriteB(new byte[8]
            {
                (byte) 4,
                (byte) 0,
                (byte) 10,
                (byte) 12,
                (byte) 5,
                (byte) 16,
                (byte) 0,
                (byte) 128
            });
        }
    }
}
