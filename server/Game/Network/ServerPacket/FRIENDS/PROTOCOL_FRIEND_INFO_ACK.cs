using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.FRIENDS
{
    public class PROTOCOL_FRIEND_INFO_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH((short)2592);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(0);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(0);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(0);
            WriteD(100);
            WriteD(100);
            WriteD(100);
            WriteD(0);
            WriteD(100);
            WriteD(100);
        }
    }
}
