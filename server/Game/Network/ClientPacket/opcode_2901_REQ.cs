using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class opcode_2901_REQ : ReceivePacket
    {
        public override void ReadImpl()
        {

        }

        public override void RunImpl()
        {
            getClient().SendPacket(new opcode_2901_ACK());
        }
    }
}
