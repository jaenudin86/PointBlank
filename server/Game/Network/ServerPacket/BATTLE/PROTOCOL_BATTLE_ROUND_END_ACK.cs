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
    class PROTOCOL_BATTLE_ROUND_END_ACK : SendPacket
    {
        private int team;
        private Room room;
        private int type;

        public PROTOCOL_BATTLE_ROUND_END_ACK(int team, int type, Room room)
        {
            this.team = team;
            this.room = room;
            this.type = type;
        }
        public override void WriteImpl()
        {
            WriteH(0xD19);
           // WriteD(team);
            WriteC((byte)team);
            WriteC((byte)type);
            WriteH((short)room.getRedWinRounds());
            WriteH((short)room.getBlueWinRounds());
           // WriteB(new byte[] { 0x00, 0x02, 0x03, 0x00, 0x00, 0x00 });
        }
    }
}
