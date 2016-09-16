/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BASE_PLAYER_EXIT_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_PLAYER_EXIT_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
        }
        public override void RunImpl()
        {
            getClient().close();
        }
    }
}
