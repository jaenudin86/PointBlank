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
    public class PROTOCOL_LOBBY_LEAVE_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH((short)3084);
            WriteD(0);
        }
    }
}
