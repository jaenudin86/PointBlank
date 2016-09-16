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
    class PROTOCOL_TUTORIAL_END_REQ : ReceivePacket
    {
        public PROTOCOL_TUTORIAL_END_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_TUTORIAL_END_ACK());
            getClient().SendPacket(new PROTOCOL_BATTLE_END_ACK(getClient().getPlayer(), getClient().getPlayer().getRoom()));
        }
    }
}
