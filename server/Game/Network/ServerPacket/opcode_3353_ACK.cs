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
    public class opcode_3353_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xD19);
            WriteB(new byte[] { 0x00, 0x02, 0x05, 0x00, 0x01, 0x00 });
        }
    }
}
