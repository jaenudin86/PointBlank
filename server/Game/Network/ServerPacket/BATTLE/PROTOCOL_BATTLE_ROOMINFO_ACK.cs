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
    public class PROTOCOL_BATTLE_ROOMINFO_ACK : SendPacket
    {
        private Room room;
        public PROTOCOL_BATTLE_ROOMINFO_ACK(Room room)
        {
            this.room = room;
        }
        public override void WriteImpl()
        {
            WriteH(3848);
            WriteD(room.getId());
            WriteS(room.getName(), Room.ROOM_NAME_SIZE);
            WriteC((byte)room.getMapId());
            WriteH((short)0);
            WriteC((byte)room.getType());
            WriteC((byte)5);
            WriteC((byte)room.getPlayers().Count);
            WriteC((byte)room.getSlots());
            WriteC((byte)5);
            WriteC((byte)room.getAllWeapons());
            WriteC((byte)room.getRandomMap());
            WriteC((byte)room.getSpecial());
            WriteS(room.getLeader().PlayerName, Player.MAX_NAME_SIZE);
            WriteD(room.getKillMask());
            WriteC((byte)room.getLimit());
            WriteC((byte)room.getSeeConf());
            WriteH((byte)room.getAutobalans());
            WriteC((byte)room.getAiCount());
            WriteC((byte)room.getAiLevel());
        }
    }
}
