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
    class PROTOCOL_CLAN_LEAVE_ACK : SendPacket
    {
        public PROTOCOL_CLAN_LEAVE_ACK()
        {

        }

        public override void WriteImpl()
        {
            WriteH(0x5A4);
        }
    }
}
