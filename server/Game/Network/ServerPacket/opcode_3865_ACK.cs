/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace Game.Network.ServerPacket
{
    public class opcode_3865_ACK : SendPacket
    {
        public override void  WriteImpl()
        {
            WriteH(0xD17);
           // WriteH(1); // в некоторых дампах летит 3
            WriteB(new byte[] { 0x03, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
        }
    }
}
