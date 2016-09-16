/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;
using Core.Model;
using Game.Managers;

namespace Game.Network.ClientPacket
{
    class opcode_3860_REQ : ReceivePacket
    {
        private byte[] _bytes;
        public opcode_3860_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            _bytes = ReadB(16);
        }
        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            if(getClient().getPlayer().getRoom().getPlayers() != null)
            {
                foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                {
                    SLOT slot = room.getRoomSlotByPlayer(member);
                    if (slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE)
                    {
                        member.getClient().SendPacket(new opcode_3860_ACK(_bytes));
                    }
                }
            }
        }
    }
}
