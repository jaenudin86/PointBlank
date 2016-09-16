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
using Core.Database.Tables;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_CLAN_LIST_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x5A6);
            WriteD(0);
            foreach (var clan in ClansTable.clans)
            {
                WriteC((byte) ClansTable.clans.Count);
                WriteD((int) clan.Value.Id);
                WriteS(clan.Value.Name, Clan.CLAN_NAME_SIZE);//clan name
                WriteC((byte) clan.Value.Rank);
                WriteC((byte) clan.Value.PlayersCount);
                WriteC((byte) clan.Value.MaxPlayersCount);
                WriteD(clan.Value.DateCreated);
                WriteC((byte)clan.Value.Logo1);
                WriteC((byte)clan.Value.Logo2);
                WriteC((byte)clan.Value.Logo3);
                WriteC((byte)clan.Value.Logo4);
            }
        }
    }
}
