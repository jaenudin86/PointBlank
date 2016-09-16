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
    public class PROTOCOL_BASE_USER_LEAVE_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(2578);
            WriteD(0);
        }
    }
}
