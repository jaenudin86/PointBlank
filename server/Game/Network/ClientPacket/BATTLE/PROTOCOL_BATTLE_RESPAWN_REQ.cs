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
using Game.Network.ServerPacket;
using Core.Managers;
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_RESPAWN_REQ : ReceivePacket
    {
        private int first, second, third, fourth, fifth, id, red, blue, head, beret, dino;

        public PROTOCOL_BATTLE_RESPAWN_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            first = ReadD();
            second = ReadD();
            third = ReadD();
            fourth = ReadD();
            fifth = ReadD();
            id = ReadD();
            red = ReadD();
            blue = ReadD();
            head = ReadD();
            beret = ReadD();
            dino = ReadD();
            ReadC();

        }
        public override void RunImpl()
        {
           foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
           {
                SLOT slot = getClient().getPlayer().getRoom().getRoomSlotByPlayer(member);
                if ((int)slot.getState() > 8)
                {
                    member.getClient().SendPacket(new PROTOCOL_BATTLE_RESPAWN_ACK(getClient().getPlayer().getRoom().getRoomSlotByPlayer(getClient().getPlayer()).getId(), first, second, third, fourth, fifth, id, red, blue, head, beret, dino));
               }
           }
        }

    }
}
