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
    class PROTOCOL_BATTLE_BOT_RESPAWN_REQ : ReceivePacket
    {
        private int slot;
        public PROTOCOL_BATTLE_BOT_RESPAWN_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            slot = ReadD();
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_BATTLE_BOT_RESPAWN_ACK(slot));
        }
    }
}
