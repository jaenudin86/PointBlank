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
    class PROTOCOL_BATTLE_BOT_RESPAWN_ACK : SendPacket
    {
        private int slot;
        public PROTOCOL_BATTLE_BOT_RESPAWN_ACK(int slot)
        {
            this.slot = slot;
        }
        public override void WriteImpl()
        {
            WriteH(0xD33);
            WriteD(slot);
        }
    }
}
