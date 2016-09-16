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
    class opcode_3879_ACK : SendPacket
    {
        private Room room;

        public opcode_3879_ACK(Room room)
        {
            this.room = room;
        }
        public override void WriteImpl()
        {
            WriteH(0xF27);
            for (int slot = 0; slot < 15; ++slot)
            {
                Player playerBySlot = room.getRoomSlot(slot).getPlayer();
                WriteD(playerBySlot.getSlot());
                WriteD(playerBySlot.getEquipFromSlot(6).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(7).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(8).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(9).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(10).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(1).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(2).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(3).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(4).ItemId);
                WriteD(playerBySlot.getEquipFromSlot(5).ItemId);
                WriteD(0);
                WriteB(new byte[5]);
                WriteC(1);
                WriteD(0);
            }
        }
    }
}
