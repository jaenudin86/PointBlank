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
    class PROTOCOL_BATTLE_BOMB_UNTAB_ACK : SendPacket
    {
        public int slot;

        public PROTOCOL_BATTLE_BOMB_UNTAB_ACK(int slot)
        {
            this.slot = slot;
        }

        public override void WriteImpl()
        {
            WriteH(0xD1F);
            WriteD(slot);
        }
    }
}
