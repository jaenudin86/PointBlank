/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_LOBBY_ENTER_ACK : SendPacket
    {
        Player player;

        public override void WriteImpl()
        {
            WriteH(0xC08);
        }
    }
}
