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
    public class PROTOCOL_BATTLE_ROUND_START : SendPacket
    {
        private Room room;
        public PROTOCOL_BATTLE_ROUND_START(Room room)
        {
            this.room = room;
        }

        public override void WriteImpl()
        {
            WriteH(0xD2B);
           // WriteB(new byte[] { 0x01, 0xB4, 0x00, 0x00, 0x00, 0x03, 0x00 });
            WriteC(1);
            WriteD(room.getKillTime() * 60);
            //WriteD(60);
            //WriteD(20000);
            WriteH(3);
          //  WriteC(2); // что это? - тип боя
        //    WriteD(room.getTimeLost());
         //   WriteH(3);
        }
    }
}
