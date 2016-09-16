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
    class opcode_3860_ACK : SendPacket
    {
        private byte[] _bytes;

        public opcode_3860_ACK(byte[] bytes)
        {
            _bytes = bytes;
        }

        public override void WriteImpl()
        {
            WriteH(0xD11); //
            WriteB(_bytes);
        }
    }
}
