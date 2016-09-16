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
    class PROTOCOL_CLAN_INFO_ACK : SendPacket
    {
        public int ClanID;

        public PROTOCOL_CLAN_INFO_ACK(int ClanID)
        {
            this.ClanID = ClanID;
            
        }
        public override void WriteImpl()
        {
            Logger.Warn("" + ClanID);

            Clan clan = ClansTable.clans[(ulong)ClanID + 1];
            Player player = PlayersTable.players[clan.OwnerId];

            WriteH(1305);
            WriteD(0);
            WriteD((int) clan.Id);
            WriteS(clan.Name, 16);
            WriteH(clan.Rank);
            WriteC((byte) clan.PlayersCount);
            WriteC(50);
            WriteB(new byte[4]);
            WriteC((byte) clan.Logo1);
            WriteC((byte) clan.Logo2);
            WriteC((byte) clan.Logo3);
            WriteC((byte) clan.Logo4);
            WriteB(new byte[10]);
            WriteC(2);
            WriteC(23);
            WriteC(6);
            WriteB(new byte[5]);
            WriteS(player.PlayerName, 33);
            WriteC((byte) player.Rank);
            WriteS(clan.Notice, byte.MaxValue);
            WriteB(new byte[21]);
            WriteC(0);//limite_rank
            WriteC(0);//limite_idade
            WriteC(0);//limite_idade2
            WriteC(0);//autoridade
            WriteS(clan.Info, (int)byte.MaxValue);
            WriteD(100);//матчи сезон
            WriteD(90);//победы сезон
            WriteD(10);//поражения сезон
            WriteD(50);//матчи
            WriteD(49);//победы
            WriteD(1);//поражения
            WriteC((byte)1);
            WriteC((byte)2);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
            WriteC((byte)1);
        }
    }
}
