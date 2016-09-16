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

namespace Game.Network.ServerPacket
{
    class PROTOCOL_ROOM_PLAYER_ENTER_ACK : SendPacket
    {
        private Player player;
        private Clan Clan;
        private SLOT playerSlot;

        public PROTOCOL_ROOM_PLAYER_ENTER_ACK(Player player, Clan Clan)
        {
            this.player = player;
            this.Clan = Clan;
        }

        public PROTOCOL_ROOM_PLAYER_ENTER_ACK(SLOT playerSlot)
        {
            this.playerSlot = playerSlot;
        }

        public override void WriteImpl()
        {
            WriteH(0xf45);
            WriteD(playerSlot.getId());
            WriteC((byte)playerSlot.getState());
            WriteH((byte)playerSlot.getPlayer().getRank());
            WriteB(new byte[8]);
            if (playerSlot.getPlayer().getClanID() == 0)
            {
                WriteC(0xff);
                WriteC(0xff);
                WriteC(0xff);
                WriteC(0xff);
                WriteC(0);
                WriteS("", 0x16);
            }
            else
            {
                WriteC((byte)playerSlot.getPlayer().getClan().getLogo1());
                WriteC((byte)playerSlot.getPlayer().getClan().getLogo2());
                WriteC((byte)playerSlot.getPlayer().getClan().getLogo3());
                WriteC((byte)playerSlot.getPlayer().getClan().getLogo4());
                WriteC((byte)playerSlot.getPlayer().getClan().getColor());
                WriteS(playerSlot.getPlayer().getClan().getName(), Clan.CLAN_NAME_SIZE);
            }
            WriteC((byte)playerSlot.getId());
            WriteC((byte)playerSlot.getPlayer().PlayerName.Length);
            WriteC(0);
            WriteH((byte)(playerSlot.getPlayer().PlayerName.Length + 1));
            WriteS(playerSlot.getPlayer().PlayerName, Player.MAX_NAME_SIZE);
            WriteC(0);
        }
    }
}
