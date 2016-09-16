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
    public class PROTOCOL_BATTLE_STARTBATTLE_ACK : SendPacket
    {
        private Room room;
        private Player player;
        PlayerEquip equip;
        ulong id;

        public PROTOCOL_BATTLE_STARTBATTLE_ACK(Room room, Player player)
        {
            this.room = room;
            this.player = player;
        }
        public override void WriteImpl()
        {
            equip = PlayerEquipTable.players_equip[player.PlayerID];
            WriteH(0xD06);
            WriteD(room.isFigth());
            WriteD(room.getRoomSlotByPlayer(player).getId());
            WriteD(equip.getCharRed());
            WriteD(equip.getCharBlue());
            WriteD(equip.getCharHelmet());
            WriteD(equip.getCharBeret());
            WriteD(equip.getCharDino());
            WriteC(2); // wtf?
            WriteC(0);
            WriteH(0);
        }
    }
}
