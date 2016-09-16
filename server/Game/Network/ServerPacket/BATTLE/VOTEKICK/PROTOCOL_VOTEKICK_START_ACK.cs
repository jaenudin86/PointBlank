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
    public class PROTOCOL_VOTEKICK_START_ACK : SendPacket
    {
        public PROTOCOL_VOTEKICK_START_ACK()
        {
        }

        public override void WriteImpl()
        {
            WriteH(0xD45);
            WriteD(0);
        }
    }
}
