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
    class PROTOCOL_LOBBY_JOIN_ROOM_ACK : SendPacket
    {
        private Room room;
        private int slodId;
        private uint error;
        public PROTOCOL_LOBBY_JOIN_ROOM_ACK(Room room, int slodId, uint error)
        {
            this.room = room;
            this.slodId = slodId;
            this.error = error;
        }
        public override void WriteImpl()
        {
            WriteH(0xc0a);
            if(room.getRoomSlot(this.slodId).getPlayer() != null)
            {
                WriteD(12);
                WriteD(slodId);
                WriteD(room.getId());
                WriteS(room.getName(), 23);
                WriteC((byte)room.getMapId());
                WriteC(0);
                WriteC(0);
                WriteC((byte)room.getType());
                WriteC(5);
                WriteC((byte)room.getPlayers().Count);
                WriteC((byte)room.getSlots());
                WriteC(5);
                WriteC((byte)room.getAllWeapons());
                WriteC((byte)room.getRandomMap());
                WriteC((byte)room.getSpecial());
                WriteS(room.getLeader().getName(), Player.MAX_NAME_SIZE);
                WriteC((byte)room.getKillMask());
                WriteC(0);
                WriteC(0);
                WriteC(0);
                WriteC((byte)room.getLimit());
                WriteC((byte)room.getSeeConf());
                WriteH((byte)room.getAutobalans());
                WriteS(room.getPassword(), 4);// password
                WriteC(0);
                WriteD(room.getRoomSlotByPlayer(room.getLeader()).getId());

                foreach (SLOT slot in room.getRoomSlots())
                {
                    if (slot.getPlayer() != null)
                    {
                        WriteC((byte)(int)slot.getState());
                        WriteC((byte)slot.getPlayer().getRank());
                        WriteB(new byte[8]);
                        WriteC(slot.getPlayer() == null || slot.getPlayer().ClanID == 0 ? (byte)0 : (byte)slot.getPlayer().Clan.Logo1);
                        WriteC(slot.getPlayer() == null || slot.getPlayer().ClanID == 0 ? (byte)0 : (byte)slot.getPlayer().Clan.Logo2);
                        WriteC(slot.getPlayer() == null || slot.getPlayer().ClanID == 0 ? (byte)0 : (byte)slot.getPlayer().Clan.Logo3);
                        WriteC(slot.getPlayer() == null || slot.getPlayer().ClanID == 0 ? (byte)0 : (byte)slot.getPlayer().Clan.Logo4);
                        WriteC(slot.getPlayer() == null || slot.getPlayer().ClanID == 0 ? (byte)0 : (byte)slot.getPlayer().Clan.Color);
                        WriteB(new byte[6]);
                        WriteS(slot.getPlayer() == null || slot.getPlayer().getClan() == null ? "" : slot.getPlayer().getClan().getName(), Clan.CLAN_NAME_SIZE);
                        WriteD(0);
                    }
                    else
                    {
                        WriteC((byte)(int)slot.getState());
                        WriteH(0);
                        WriteB(new byte[8]);
                        WriteC(0xff);
                        WriteC(0xff);
                        WriteC(0xff);
                        WriteC(0xff);
                        WriteC(0);
                        WriteB(new byte[6]);
                        WriteS("", 0x11);
                        WriteD(0);
                    }
                }

                WriteC((byte)room.getPlayers().Count);
                foreach (Player player in room.getPlayers().Values)
                {
                    WriteC((byte)room.getRoomSlotByPlayer(player).getId());
                    WriteC((byte)Player.MAX_NAME_SIZE);
                    WriteS(player.getName(), Player.MAX_NAME_SIZE);
                    WriteC(0); //player.getColor()
                }
                WriteC((byte)room.getAiCount()); // aiCount
                WriteC((byte)room.getAiLevel()); // aiLevel
            }
            else
            {
                WriteQ(0x80001004L);
            }
        }
    }
}