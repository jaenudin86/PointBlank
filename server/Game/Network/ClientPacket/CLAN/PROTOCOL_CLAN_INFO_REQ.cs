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
    class PROTOCOL_CLAN_INFO_REQ : ReceivePacket
    {
        private int ClanID;

        public PROTOCOL_CLAN_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            int num = (int)ReadH();
            ClanID = (int)ReadC();
        }

        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_CLAN_INFO_ACK(ClanID));
        }
    }
}
