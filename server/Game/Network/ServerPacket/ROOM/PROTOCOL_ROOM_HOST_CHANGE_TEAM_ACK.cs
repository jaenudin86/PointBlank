using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_ROOM_HOST_CHANGE_TEAM_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xf23);
            WriteD(0);
        }
    }
}
