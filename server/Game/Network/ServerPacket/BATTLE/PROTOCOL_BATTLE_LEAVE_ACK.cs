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
    public class PROTOCOL_BATTLE_LEAVE_ACK : SendPacket
    {
        private int Slot;

        public PROTOCOL_BATTLE_LEAVE_ACK(int Slot)
        {
            this.Slot = Slot;
        }

        public override void WriteImpl()
        {
            WriteH(3850);
            WriteD(Slot);
            WriteB(new byte[20]);
        }
    }
}
