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
using Core.Managers;
using Core.Model;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_CLAN_CHECK_NAME_REQ : ReceivePacket
    {
        private string clanName;

        public PROTOCOL_CLAN_CHECK_NAME_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            clanName = ReadS((int)this.ReadC());
        }

        public override void RunImpl()
        {
            if (ClansManager._instance.getClanForName(clanName) == null)
            {
                getClient().SendPacket(new PROTOCOL_CLAN_CHECK_NAME_ACK(0));
            }
            else
            {
                getClient().SendPacket(new PROTOCOL_CLAN_CHECK_NAME_ACK(int.MaxValue));
            }
        }
    }
}
