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
using Game.Managers;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_PRESTARTBATTLE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
        }
        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            SLOT slot = room.getRoomSlotByPlayer(getClient().getPlayer());
            if (room == null)
                return;

            Player leader = player.getRoom().getLeader();
            room.getRoomSlotByPlayer(player).setState(SLOT_STATE.SLOT_STATE_PRESTART);
            getClient().SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(room, player));
            if (player.PlayerID != leader.PlayerID)
                leader.getClient().SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(room, player)); // лидер
            Logger.Info("PRESTART  {0}",slot.getState());
            if (Array.BinarySearch(Room.RED_TEAM, slot.getId()) >= 0)
            {
                room.redTeamCount = room.redTeamCount + 1;
            }
            else
            {
                room.blueTeamCount = room.blueTeamCount + 1;
            }
            if (!room.getLeader().Equals(player) && (int)room.getRoomSlotByPlayer(room.getLeader()).getState() > 8)
            {
                BattleHandler.AddPlayer(player);
            }
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
            }
        }
    }
}
