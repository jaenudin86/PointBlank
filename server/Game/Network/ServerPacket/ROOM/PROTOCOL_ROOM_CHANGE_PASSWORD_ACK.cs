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
    public class PROTOCOL_ROOM_CHANGE_PASSWORD_ACK : SendPacket
    {
        private Room room;

        public PROTOCOL_ROOM_CHANGE_PASSWORD_ACK(Room room)
        {
            this.room = room;
        }

        public override void WriteImpl()
        {
            WriteH(0xF43);
            WriteS(room.getPassword(), Room.ROOM_NAME_SIZE);
        }
    }
}
