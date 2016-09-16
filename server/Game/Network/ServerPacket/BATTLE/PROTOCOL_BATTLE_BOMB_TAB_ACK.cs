/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_BATTLE_BOMB_TAB_ACK : SendPacket
    {
        public int zone;
        public int slot;
        public int x, y, z;
        public PROTOCOL_BATTLE_BOMB_TAB_ACK(int zone, int slot, int x, int y, int z)
        {
            this.z = z;
            this.y = y;
            this.x = x;
            this.slot = slot;
            this.zone = zone;
        }

        public override void WriteImpl()
        {
            WriteH(0xD1D);
            WriteD(slot);
            WriteC((byte)zone);
            WriteH(42);
            WriteD(x);
            WriteD(y);
            WriteD(z);
        }
    }
}
