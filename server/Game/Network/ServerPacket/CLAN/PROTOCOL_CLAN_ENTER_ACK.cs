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
using Core.Managers;
using Core.Network;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_CLAN_ENTER_ACK : SendPacket
    {
        Player player;
        public PROTOCOL_CLAN_ENTER_ACK(Player player)
        {
            this.player = player;
        }
        public override void WriteImpl()
        {
            Clan clan = player.Clan;
            WriteH(0x5A2);
            if (clan == null)
            {
                WriteD(0);//есть ли клан
                WriteD(0);//привилегии в клане
            }
            else
            {
                WriteD(clan != null ? 1 : 0);
                WriteD(clan != null ? clan.OwnerId == player.PlayerID ? 1 : 2 : 0); // Привилегии в клане
            }
            WriteD(ClansManager.Load().getClans().Count); // количество кланов
            WriteB(HexToByte.Convert("AA 01 00 80 6C 44 37"));//unk
        }
    }
}
