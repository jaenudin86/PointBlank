/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointBlank.Network.ServerPacket
{
    class opcode_2678_ack : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xA77);
            WriteD(0);
        }
    }
}
