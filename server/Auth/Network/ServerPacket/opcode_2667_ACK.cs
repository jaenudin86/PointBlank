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
    class opcode_2667_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xA6B);
            WriteC(0);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);
            WriteC(0xFF);

            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
        }
    }
}
