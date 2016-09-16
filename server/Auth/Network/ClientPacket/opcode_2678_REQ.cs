/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointBlank.Network.ServerPacket;

namespace PointBlank.Network.ClientPacket
{
    class opcode_2678_REQ : ReceivePacket
    {
        public opcode_2678_REQ(Auth Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new opcode_2678_ack());
        }
    }
}
