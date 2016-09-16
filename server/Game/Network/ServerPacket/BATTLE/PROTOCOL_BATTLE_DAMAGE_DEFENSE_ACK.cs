using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.BATTLE
{
    public class PROTOCOL_BATTLE_DAMAGE_DEFENSE_ACK : SendPacket
    {
        private int _u1;
        private int _u2;
        private int _u3;
        private int _u4;

        public PROTOCOL_BATTLE_DAMAGE_DEFENSE_ACK(int u1, int u2, int u3, int u4)
        {
            _u1 = u1;
            _u2 = u2;
            _u3 = u3;
            _u4 = u4;
        }

        public override void WriteImpl()
        {
            WriteH(3387);
            WriteC((byte) _u1);
            WriteC((byte) _u2);
            WriteC((byte) _u3);
            WriteC((byte) _u4);
        }
    }
}
