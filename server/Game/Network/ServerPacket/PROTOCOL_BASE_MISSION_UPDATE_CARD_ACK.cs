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
    public class PROTOCOL_BASE_MISSION_UPDATE_CARD_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(2602);
            WriteD(0);
            WriteC((byte)6);
            WriteD(100);
        }
    }
}
