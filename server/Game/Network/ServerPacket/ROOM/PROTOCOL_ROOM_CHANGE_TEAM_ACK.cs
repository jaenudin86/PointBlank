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
    class PROTOCOL_ROOM_CHANGE_TEAM_ACK : SendPacket
    {
        private Player sender;
        private Room room;
        private SLOT leaderSlot;

        private int cmd, slotcount, newTeam;

        public PROTOCOL_ROOM_CHANGE_TEAM_ACK(Player sender, Room room, int cmd, int slotcount, int newTeam)
        {
            this.sender = sender;
            this.room = room;
            this.cmd = cmd;
            this.slotcount = slotcount;
            this.newTeam = newTeam;

            this.leaderSlot = room.getRoomSlotByPlayer(room.getLeader());
        }

        public override void WriteImpl()
        {
            WriteH(0xF25);
            WriteC(0);
            WriteC((byte)leaderSlot.getId());
            WriteC(1);//(byte)slotcount
            if (slotcount > 1)
            {
                foreach (int slot in Room.RED_TEAM)
                {
                    SLOT oldSlot = room.getRoomSlots()[slot];
                    SLOT newSlot = room.getRoomSlots()[slot + 1];
                    SLOT_STATE oldState = oldSlot.getState();
                    oldSlot.setState(newSlot.getState());
                    newSlot.setState(oldState);
                    Player oldPlayer = oldSlot.getPlayer();
                    oldSlot.setPlayer(newSlot.getPlayer());
                        newSlot.setPlayer(oldPlayer);

                        WriteC((byte)oldSlot.getId());
                        WriteC((byte)newSlot.getId());
                        WriteC((byte)(int)oldSlot.getState());
                        WriteC((byte)(int)newSlot.getState());
                    }
                }
                else
                {
                    SLOT oldSlot = room.getRoomSlotByPlayer(sender);
                    SLOT newSlot = room.changeTeam(sender, newTeam);

                    WriteC((byte)oldSlot.getId());
                    WriteC((byte)newSlot.getId());
                    WriteC((byte)(int)oldSlot.getState());
                    WriteC((byte)(int)newSlot.getState());
                }
        }
    }
}
