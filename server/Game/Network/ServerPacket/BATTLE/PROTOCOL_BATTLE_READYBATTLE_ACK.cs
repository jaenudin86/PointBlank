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
    class PROTOCOL_BATTLE_READYBATTLE_ACK : SendPacket
    {
        private Room room;
        private Player player;
        private PlayerEquip equip;
        public PROTOCOL_BATTLE_READYBATTLE_ACK(Room room, Player player)
        {
            this.room = room;
            this.player = player;
        }
        public override void WriteImpl()
        {
            equip = PlayerEquipTable.players_equip[player.PlayerID];
            WriteH(0xD04);
            WriteD(9); // Так Надо. И неебёт!
            WriteH((short)room.getMapId());
            WriteC((byte)room.getStage4v4());
            WriteC((byte)room.getType());

            WriteD(equip.getCharRed());
            WriteD(equip.getCharBlue());
            WriteD(equip.getCharHelmet());
            WriteD(equip.getCharBeret());
            WriteD(equip.getCharDino());

            Logger.Info("READY  {0}",room.getRoomSlotByPlayer(player).getState());

        }
    }
}
