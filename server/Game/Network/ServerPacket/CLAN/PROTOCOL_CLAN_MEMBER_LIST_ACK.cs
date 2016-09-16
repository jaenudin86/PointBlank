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
    class PROTOCOL_CLAN_MEMBER_LIST_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x51B);
            WriteC((byte)0);
            WriteC((byte)12);
            WriteC((byte)2);
            WriteS("Tester!", 33);
        }
    }
}
