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
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BASE_MISSION_UPDATE_CARD_REQ : ReceivePacket
    {
        private int CardID;

        public PROTOCOL_BASE_MISSION_UPDATE_CARD_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            int unk = ReadC();
            CardID = ReadC();
            int unk2 = ReadC();
        }
        public override void RunImpl()
        {
            QuestsTable.UpdateCard(getClient().getPlayer().PlayerID, CardID);

            getClient().SendPacket(new PROTOCOL_BASE_MISSION_UPDATE_CARD_ACK());
        }
    }
}
