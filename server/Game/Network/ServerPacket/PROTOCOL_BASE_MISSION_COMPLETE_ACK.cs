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
    public class PROTOCOL_BASE_MISSION_COMPLETE_ACK : SendPacket
    {
        private int Mission, Count;

        public PROTOCOL_BASE_MISSION_COMPLETE_ACK(int Mission, int Count)
        {
            this.Mission = Mission;
            this.Count = Count;
        }
        public override void WriteImpl()
        {
            WriteH(2600);

            WriteC((byte)Mission);//айди миссии
            WriteC((byte)Count);//кол-во выполнено
        }
    }
}
