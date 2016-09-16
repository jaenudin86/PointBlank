using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    public class PACKET_LEVEL_UP_ACK : SendPacket
    {
        private int Rank;

        public PACKET_LEVEL_UP_ACK(int Rank)
        {
            this.Rank = Rank;
        }

        public override void WriteImpl()
        {
            WriteH(2614);
            WriteD(Rank);
            WriteD(1);
            WriteD(0);// item id
        }
    }
}
