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
    class PROTOCOL_CLAN_CHECK_NAME_ACK : SendPacket
    {
        private int error;

        public PROTOCOL_CLAN_CHECK_NAME_ACK(int error)
        {
            this.error = error;
        }

        public override void WriteImpl()
        {
            WriteH(0x5A8);
            WriteD(error);
        }
    }
}
