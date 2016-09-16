using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.BATTLE
{
    public class PROTOCOL_BATTLE_DAMAGE_SABOTAGE_ACK : SendPacket
    {
        private int _u;
        private int _u2;
        private int _u3;
        private int _u4;
        private int _u5;
        private int _u6;
        private int _u7;
        private int _u8;

        public PROTOCOL_BATTLE_DAMAGE_SABOTAGE_ACK(int unk, int unk2, int unk3, int unk4, int unk5, int unk6, int unk7, int unk8)
        {
            _u = unk;
            _u2 = unk2;
            _u3 = unk3;
            _u4 = unk4;
            _u5 = unk5;
            _u6 = unk6;
            _u7 = unk7;
            _u8 = unk8;
        }

        public override void WriteImpl()
        {
            WriteH(3369);
            WriteC((byte) _u);
            WriteC((byte) _u2);
            WriteC((byte) _u3);
            WriteC((byte) _u4);
            WriteC((byte) _u5);
            WriteC((byte) _u6);
            WriteC((byte) _u7);
            WriteC((byte) _u8);
        }
    }
}
