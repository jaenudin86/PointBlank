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
    class PROTOCOL_CLAN_MEMBER_LIST2_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x51D);
            WriteB(new byte[18]
            {
               (byte) 118,
               (byte) 0,
               (byte) 29,
               (byte) 5,
               (byte) 0,
               (byte) 0,
               (byte) 0,
               (byte) 0,
               (byte) 0,
               (byte) 2,
               (byte) 110,
               (byte) 25,
               (byte) 112,
               (byte) 0,
               (byte) 0,
               (byte) 0,
               (byte) 0,
               (byte) 0
            });
            WriteS("Tester!", 33);
        }
    }
}
