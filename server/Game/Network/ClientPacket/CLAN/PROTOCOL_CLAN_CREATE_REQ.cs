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
using Core.Managers;
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_CLAN_CREATE_REQ : ReceivePacket
    {
        private string Info;
        private string Name;
        private long ClanID;

        public PROTOCOL_CLAN_CREATE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            int clanLen = ReadC();
            int descLen = ReadC();
            ReadC();
            Name = ReadS(clanLen - 1);
            Info = ReadS(descLen);

        }

        public override void RunImpl()
        {
            ClansTable.AddClan(getClient().getPlayer().AccountID, Name, Info);

            getClient().SendPacket(new PROTOCOL_CLAN_CREATE_ACK(Name));
        }
    }
}
