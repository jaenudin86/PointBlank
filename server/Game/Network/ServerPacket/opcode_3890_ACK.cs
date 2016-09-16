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
    public class opcode_3890_ACK : SendPacket
    {
        private Room room;
        public opcode_3890_ACK(Room room)
        {
            this.room = room;
        }
        public override void WriteImpl()
        {
            WriteH(0xF32);
            WriteC((byte)room.getAiLevel()); // Уровень сложности
            for (int i = 0; i < 8; i++)
            {
                WriteD(1);// ранг бота?
                WriteD(1);// ранг бота?
            }
        }
    }
}
