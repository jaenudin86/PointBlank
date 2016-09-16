/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Parsers;
using Core.Model;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_BATTLE_CHANGE_NETWORK_ACK : SendPacket
    {
        Room room;

        public PROTOCOL_BATTLE_CHANGE_NETWORK_ACK(Room room)
        {
            this.room = room;
        }

        public override void WriteImpl()
        {
            WriteH(3330);
            if (room != null)
            {
                WriteD(room.getRoomSlotByPlayer(room.getLeader()).getId());
                for (int i = 0; i < 16; i++)
                {
                    Player p = room.getRoomSlot(i).getPlayer();
                    if (p != null)
                    {
                        WriteB(p.getClient().getPlayer().getAddress());
                        WriteH(29890);
                        WriteB(p.getClient().getPlayer().getAddress());
                        WriteH(29890);
                        WriteC(0); //NAT?
                    }
                    else
                    {
                        WriteB(new byte[13]);
                    }
                }
            }
        }
    }
}
