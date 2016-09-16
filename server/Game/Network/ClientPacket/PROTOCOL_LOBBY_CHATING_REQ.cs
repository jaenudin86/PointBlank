using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_CHATING_REQ : ReceivePacket
    {
        string message;
        public override void ReadImpl()
        {
            ReadH();
            message = ReadS(ReadH());
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_LOBBY_CHATING_ACK(getClient().getPlayer(), message));
        }
    }
}
