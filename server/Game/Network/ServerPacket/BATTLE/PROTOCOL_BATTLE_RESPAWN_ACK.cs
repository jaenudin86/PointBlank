/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_BATTLE_RESPAWN_ACK : SendPacket
    {
        private int slotId, first, second, third, fourth, fifth, id, red, blue, head, beret, dino;

        public PROTOCOL_BATTLE_RESPAWN_ACK(int slotId, int first, int second, int third, int fourth, int fifth, int id,
                                  int red, int blue, int head, int beret, int dino)
        {
            this.slotId = slotId;
            this.first = first;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
            this.fifth = fifth;
            this.id = id;
            this.red = red;
            this.blue = blue;
            this.head = head;
            this.beret = beret;
            this.dino = dino;
        }

        public override void WriteImpl()
        {
            WriteH(0xD0A);
            WriteD(slotId);
            WriteD(1);
            WriteD(1);
            WriteD(first);
            WriteD(second);
            WriteD(third);
            WriteD(fourth);
            WriteD(fifth);
            WriteD(id);
            WriteB(new byte[] { 0x64, 0x64, 0x64, 0x64, 0x64, 0x01 });
            WriteD(red);
            WriteD(blue);
            WriteD(head);
            WriteD(beret);
            WriteD(dino);
        }
    }
}
