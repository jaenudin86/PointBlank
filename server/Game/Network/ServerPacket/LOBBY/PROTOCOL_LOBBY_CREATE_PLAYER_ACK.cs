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
    class PROTOCOL_LOBBY_CREATE_PLAYER_ACK : SendPacket
    {
        public uint result;

        public PROTOCOL_LOBBY_CREATE_PLAYER_ACK(uint result)
        {
            this.result = result;
        }

        public override void WriteImpl()
        {
            WriteH(3102); //
            WriteD((int)result);
        }
    }
}
