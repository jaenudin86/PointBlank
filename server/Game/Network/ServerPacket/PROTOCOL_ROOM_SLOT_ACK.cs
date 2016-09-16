using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_ROOM_SLOT_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(3050);//opcode
        }
    }
}
