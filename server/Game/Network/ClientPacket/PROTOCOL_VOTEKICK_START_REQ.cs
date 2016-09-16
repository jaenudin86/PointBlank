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
using Core.Model;
using Game.Managers;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_VOTEKICK_START_REQ : ReceivePacket
    {
        private int Slot;
        private int unk;

        public PROTOCOL_VOTEKICK_START_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            Slot = (int)ReadC();
        }

        public override void RunImpl()
        {
            getClient().getPlayer();
            Logger.Info("[VOTEKICK] Started vote.");
            if (getClient() == null)
                return;
            getClient().SendPacket(new PROTOCOL_VOTEKICK_START_ACK());
        }
    }
}
