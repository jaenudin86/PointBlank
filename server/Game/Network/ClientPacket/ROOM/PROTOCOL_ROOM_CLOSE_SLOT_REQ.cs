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

namespace Game.Network.ClientPacket
{
    class PROTOCOL_ROOM_CLOSE_SLOT_REQ : ReceivePacket
    {
        private int slotId;

        public PROTOCOL_ROOM_CLOSE_SLOT_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            slotId = ReadC();
        }

        public override void RunImpl()
        {
            SLOT slot = getClient().getPlayer().getRoom().getRoomSlots()[slotId];

            if(slot.getState() == SLOT_STATE.SLOT_STATE_CLOSE)
            {
                slot.setState(SLOT_STATE.SLOT_STATE_EMPTY);
            }
            else
            {
                slot.setState(SLOT_STATE.SLOT_STATE_CLOSE);
            }

            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(getClient().getPlayer().getRoom()));
            }
        }
    }
}
